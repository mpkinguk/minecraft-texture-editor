using log4net;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;

namespace MinecraftTextureEditorUI
{
    public partial class MDIMainForm : Form
    {
        /// <summary>
        /// Drawing Tools form
        /// </summary>
        public DrawingToolsForm DrawingTools { get; set; }

        /// <summary>
        /// Texture Picker form
        /// </summary>
        public TexturePickerForm TexturePicker { get; set; }

        /// <summary>
        /// Editor form
        /// </summary>
        public EditorForm CurrentEditor { get; set; }

        /// <summary>
        /// Pixel clipboard
        /// </summary>
        public Texture PixelClipboard { get; set; }

        /// <summary>
        /// The current path
        /// </summary>
        public string CurrentPath { get; set; }

        #region Private properties

        private bool _skipResolutionCheck;

        private readonly ILog _log;

        #endregion Private properties

        /// <summary>
        /// Constructor
        /// </summary>
        public MDIMainForm(ILog log)
        {
            _log = log;

            if (!CheckFontLoaded())
            {
                var message = "Could not load font. Please check this Fonts directory for MINECRAFT_FONT.TTF";

                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            try
            {
                ResizeRedraw = true;

                Resize += (sender, e) => { Refresh(); };

                BackgroundImageLayout = ImageLayout.Center;

                InitializeComponent();

                WindowState = FormWindowState.Maximized;

                Shown += MDIMainFormShown;

                // Reduce display flicker
                SetStyle(ControlStyles.AllPaintingInWmPaint & ControlStyles.UserPaint & ControlStyles.OptimizedDoubleBuffer & ControlStyles.ResizeRedraw, true);

                RandomiseWallpaper();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Private methods

        /// <summary>
        /// Tool has been selected. Inform other controls
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="toolType">The tooltype</param>
        private void SelectTool(object sender, ToolType toolType)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }

                if (sender.GetType().Equals(typeof(ToolStripMenuItem)))
                {
                    var selectedMenuItem = (ToolStripMenuItem)sender;

                    // Reset other items
                    foreach (object item in selectedMenuItem.GetCurrentParent().Items)
                    {
                        if (item.GetType().Equals(typeof(ToolStripMenuItem)))
                        {
                            ToolStripMenuItem menuItem = (ToolStripMenuItem)item;
                            menuItem.Checked = false;
                        }
                    }

                    selectedMenuItem.Checked = true;
                }

                if (toolType != ToolType.TransparencyLock)
                {
                    CurrentEditor.ToolType = toolType;
                }
                else
                {
                    CurrentEditor.TransparencyLock = DrawingTools.TransparencyLock;
                }

                if (DrawingTools is null)
                {
                    return;
                }

                DrawingTools.CurrentToolType = toolType;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Randomises the wallpaper
        /// </summary>
        private void RandomiseWallpaper()
        {
            try
            {
                var rnd = new System.Random();

                switch (rnd.Next(1, 6))
                {
                    case 2:
                        BackgroundImage = Properties.Resources.wallpaper2;
                        break;

                    case 3:
                        BackgroundImage = Properties.Resources.wallpaper3;
                        break;

                    case 4:
                        BackgroundImage = Properties.Resources.wallpaper4;
                        break;

                    case 5:
                        BackgroundImage = Properties.Resources.wallpaper5;
                        break;

                    case 6:
                        BackgroundImage = Properties.Resources.steve;
                        break;

                    default:
                        BackgroundImage = Properties.Resources.wallpaper1;
                        break;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Is called once form is fully loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDIMainFormShown(object sender, EventArgs e)
        {
            ShowToolWindows();
        }

        /// <summary>
        /// Check that the minecraft font is installed
        /// </summary>
        /// <returns>Bool</returns>
        private bool CheckFontLoaded()
        {
            try
            {
                var systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                if (string.IsNullOrEmpty(systemFolder))
                {
                    MessageBox.Show(this, "Could not find system directory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var fontPath = Path.Combine(systemFolder, "Microsoft\\Windows\\Fonts\\minecraft_font.ttf");

                if (File.Exists(fontPath))
                {
                    return true;
                }

                if (MessageBox.Show(this, "Minecraft font needs to be installed.\nPlease click OK to install font", "Font not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    return false;
                }

                var applicationFontPath = Path.Combine(Application.StartupPath, "Fonts\\MINECRAFT_FONT.TTF");

                var process = Process.Start(applicationFontPath);

                while (!process.HasExited)
                {
                    Thread.Sleep(1000);
                }

                return process.ExitCode >= 0;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Show tool windows
        /// </summary>
        private void ShowToolWindows(string filename = "")
        {
            try
            {
                if (MdiChildren.Contains(TexturePicker))
                {
                    TexturePicker.Close();
                }

                if (MdiChildren.Contains(DrawingTools))
                {
                    DrawingTools.Close();
                }

                TexturePicker = new TexturePickerForm(filename, _log) { MdiParent = this };

                TexturePicker.TextureClicked += TexturePickerTextureClicked;

                TexturePicker.LoadTextures();

                CurrentPath = TexturePicker.CurrentPath;

                TexturePicker.Show();

                DrawingTools = new DrawingToolsForm(_log) { MdiParent = this };

                DrawingTools.ToolTypeChanged += DrawingToolsToolTypeChanged;
                DrawingTools.Colour2Changed += DrawingToolsBackColourChanged;
                DrawingTools.Colour1Changed += DrawingToolsForeColourChanged;
                DrawingTools.BrushSizeChanged += DrawingToolsBrushSizeChanged;
                DrawingTools.TransparencyLockChanged += DrawingToolsTransparencyLockChanged;

                DrawingTools.Show();

                DrawingTools.Location = new Point(ClientSize.Width / 8 * 5, 50);

                TexturePicker.Location = new Point(ClientSize.Width / 8 * 6 + 50, 50);
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Capture the transparency lock changed event from the Drawing tools window
        /// </summary>
        /// <param name="locked">Locked</param>
        private void DrawingToolsTransparencyLockChanged(bool locked)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.TransparencyLock = locked;
        }

        /// <summary>
        /// Capture the brush size changed event from the Drawing tools window
        /// </summary>
        /// <param name="brushSize"></param>
        private void DrawingToolsBrushSizeChanged(int brushSize)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.BrushSize = brushSize;
        }

        /// <summary>
        /// Capture the fore colour changed event from the Drawing tools window
        /// </summary>
        /// <param name="colour">The colour</param>
        private void DrawingToolsForeColourChanged(Color colour)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.Colour1 = colour;
        }

        /// <summary>
        /// Capture the Back colour changed event from the Drawing tools window
        /// </summary>
        /// <param name="colour">The colour</param>
        private void DrawingToolsBackColourChanged(Color colour)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.Colour2 = colour;
        }

        /// <summary>
        /// Captures the texture clicked event from the texture picker window
        /// </summary>
        /// <param name="filename"></param>
        private void TexturePickerTextureClicked(string filename)
        {
            LoadFile(filename);
        }

        /// <summary>
        /// Captures the tool type changed event from the drawing tools window
        /// </summary>
        /// <param name="toolType"></param>
        private void DrawingToolsToolTypeChanged(ToolType toolType)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }

                toolStripStatusLabel.Text = $"Current Tool type is {toolType}";

                CurrentEditor.ToolType = toolType;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new editor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNewEditorForm(object sender, EventArgs e)
        {
            try
            {
                int width;
                int height;

                if (_skipResolutionCheck)
                {
                    // Default to 16x16 if loading
                    width = 16;
                    height = 16;
                }
                else
                {
                    using (ResolutionForm resolutionForm = new ResolutionForm(_log))
                    {
                        resolutionForm.ShowDialog(this);

                        width = resolutionForm.ImageWidth;
                        height = resolutionForm.ImageHeight;
                    }
                }

                EditorForm childForm = new EditorForm(width, height, _log)
                {
                    MdiParent = this,
                    Text = "Editor " + MdiChildren.Count(x => x.Name == "EditorForm"),
                    Colour1 = DrawingTools.Colour1,
                    Colour2 = DrawingTools.Colour2,
                    ToolType = DrawingTools.CurrentToolType,
                    BrushSize = DrawingTools.BrushSize,
                    TransparencyLock = DrawingTools.TransparencyLock,
                    Zoom = 16
                };

                childForm.GotFocus += ChildFormGotFocus;
                childForm.ColourSelected += ChildFormColourSelected;
                childForm.UndoManagerAction += ChildFormUndoManagerAction;
                childForm.Disposed += ChildFormDisposed;
                childForm.Show();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Enable/disable undo controls based on current editor status
        /// </summary>
        private void CheckUndos()
        {
            try
            {
                if (CurrentEditor is null)
                {
                    undoToolStripMenuItem.Enabled = false;
                    redoToolStripMenuItem.Enabled = false;
                    toolStripButtonUndo.Enabled = false;
                    toolStripButtonRedo.Enabled = false;
                    return;
                }

                undoToolStripMenuItem.Enabled = CurrentEditor.UndoEnabled;
                redoToolStripMenuItem.Enabled = CurrentEditor.RedoEnabled;
                toolStripButtonUndo.Enabled = CurrentEditor.UndoEnabled;
                toolStripButtonRedo.Enabled = CurrentEditor.RedoEnabled;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Load a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        private void LoadFile(string fileName = "")
        {
            try
            {
                _skipResolutionCheck = true;
                ShowNewEditorForm(this, new EventArgs());
                CurrentEditor.LoadFile(fileName);
                _skipResolutionCheck = false;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Undo changes
        /// </summary>
        private void Undo()
        {
            if (CurrentEditor is null)
            {
                return;
            }

            if (CurrentEditor.UndoEnabled)
            {
                CurrentEditor.Undo();
            }
        }

        /// <summary>
        /// Redo changes
        /// </summary>
        private void Redo()
        {
            if (CurrentEditor is null)
            {
                return;
            }

            if (CurrentEditor.RedoEnabled)
            {
                CurrentEditor.Redo();
            }
        }

        /// <summary>
        /// Open the Create Project Wizard form
        /// </summary>
        private void OpenCreateProjectWizardForm()
        {
            try
            {
                var createProjectWizard = new CreateProjectWizardForm(_log)
                {
                    CurrentPath = CurrentPath
                };

                if (createProjectWizard.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Project Created!", "Deployment complete");

                    CurrentPath = createProjectWizard.CurrentPath;

                    ShowToolWindows(CurrentPath);
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Open the deployment wizard form
        /// </summary>
        private void OpenDeploymentWizardForm()
        {
            try
            {
                var deploymentWizard = new DeploymentWizardForm(_log)
                {
                    DeploymentPath = CurrentPath
                };

                if (deploymentWizard.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Package deployed\nPlease open Minecraft and select your texture pack to test it out!", "Deployment complete");
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Display the options dialog
        /// </summary>
        private void ShowOptions()
        {
            try
            {
                var optionsForm = new OptionsForm(_log);
                if (optionsForm.ShowDialog(this) == DialogResult.OK)
                {
                    if (optionsForm.HasSaved)
                    {
                        RestartApplication();
                    }
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Save file
        /// </summary>
        private void Save()
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                
                var result = CurrentEditor.SaveFile(CurrentEditor.FileName);

                if (result)
                {
                    TexturePicker.RefreshImage(CurrentEditor.FileName);
                } 
                else
                {
                    throw new Exception($"Could not save file {CurrentEditor.FileName}");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Save file As
        /// </summary>
        private void SaveAs()
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                
                var result = CurrentEditor.SaveFile();

                if (!result)
                {
                    throw new Exception($"Could not save file {CurrentEditor.FileName}");
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Paste
        /// </summary>
        private void Paste()
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                var data = PixelClipboard.Clone();
                if (data is null)
                {
                    return;
                }
                CurrentEditor.Texture = data;
                CurrentEditor.HasChanged = true;
                CurrentEditor.AddItem();
                CurrentEditor.RefreshDisplay();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Restart the application
        /// </summary>
        public void RestartApplication()
        {
            toolStripStatusLabel.Text = "Restarting App...";
            Close();
            ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath);
            Process.Start(info);
        }


        #endregion Private methods

        #region Form events

        /// <summary>
        /// Clear down the current editor object if form closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildFormDisposed(object sender, EventArgs e)
        {
            CurrentEditor = null;

            CheckUndos();
        }

        /// <summary>
        /// Capture the undo manager action event from the current editor window
        /// </summary>
        private void ChildFormUndoManagerAction()
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                toolStripStatusLabel.Text = $"Undo manager action occurred";
                CheckUndos();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Capture the colour from the dropper and return it to the drawing tools window
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="isColour1"></param>
        private void ChildFormColourSelected(Color colour, bool isColour1)
        {
            if (isColour1)
            {
                DrawingTools.Colour1 = colour;
            }
            else
            {
                DrawingTools.Colour2 = colour;
            }
        }

        /// <summary>
        /// Captures the child form got focus event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildFormGotFocus(object sender, EventArgs e)
        {
            try
            {
                if (sender is null)
                {
                    return;
                }
                CurrentEditor = (EditorForm)sender;
                CurrentEditor.Colour1 = DrawingTools.Colour1;
                CurrentEditor.Colour2 = DrawingTools.Colour2;
                CurrentEditor.ToolType = DrawingTools.CurrentToolType;
                CurrentEditor.TransparencyLock = DrawingTools.TransparencyLock;
                CurrentEditor.BrushSize = DrawingTools.BrushSize;

                toolStripStatusLabel.Text = $"Current editor is {CurrentEditor.Text}";
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFile(object sender, EventArgs e)
        {
            LoadFile();
        }

        /// <summary>
        /// Save as (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        /// <summary>
        /// Exit (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Cut (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                PixelClipboard = CurrentEditor.Texture.Clone();
                CurrentEditor.Texture = new Texture(PixelClipboard.Width, PixelClipboard.Height);
                CurrentEditor.HasChanged = true;
                CurrentEditor.AddItem();
                CurrentEditor.RefreshDisplay();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Copy (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                PixelClipboard = CurrentEditor.Texture.Clone();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Paste (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        /// <summary>
        /// Toolbar (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Statusbar (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Close all windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren.Where(o => o.GetType().Equals(typeof(EditorForm))))
            {
                childForm.Close();
            }
        }

        /// <summary>
        /// Save (button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripButtonClick(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Save (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Undo (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoToolStripMenuItemClick(object sender, EventArgs e)
        {
            Redo();
        }

        /// <summary>
        /// Redo (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoToolStripMenuItemClick(object sender, EventArgs e)
        {
            Undo();
        }

        /// <summary>
        /// Save as (button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonSaveAsClick(object sender, EventArgs e)
        {
            SaveAs();
        }

        /// <summary>
        /// Undo (button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonUndoClick(object sender, EventArgs e)
        {
            Undo();
        }

        /// <summary>
        /// Redo (button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonRedoClick(object sender, EventArgs e)
        {
            Redo();
        }

        /// <summary>
        /// Toggle the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonToggleGridClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                CurrentEditor.ShowGrid = !CurrentEditor.ShowGrid;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Deployment Wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonDeploymentWizardClick(object sender, EventArgs e)
        {
            OpenDeploymentWizardForm();
        }

        /// <summary>
        /// Create Project Wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonCreateWizardClick(object sender, EventArgs e)
        {
            OpenCreateProjectWizardForm();
        }

        /// <summary>
        /// Options clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowOptions();
        }

        /// <summary>
        /// Show about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm(_log))
            {
                aboutForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Toggle the transparent grid background for the current editor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonTransparentClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                CurrentEditor.ShowTransparent = !CurrentEditor.ShowTransparent;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Open the create project wizard form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCreateProjectWizardClick(object sender, EventArgs e)
        {
            OpenCreateProjectWizardForm();
        }

        /// <summary>
        /// Open the deployment wizard form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemDeploymentWizardClick(object sender, EventArgs e)
        {
            OpenDeploymentWizardForm();
        }

        /// <summary>
        /// Clear the current texture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (CurrentEditor is null)
                {
                    return;
                }
                CurrentEditor.Clear();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Texturiser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemTexturiserClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Texturiser);
        }

        /// <summary>
        /// FLood fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemFloodFillClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.FloodFill);
        }

        /// <summary>
        /// Rainbow!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRainbowClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Rainbow);
        }

        /// <summary>
        /// Eraser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemEraserClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Eraser);
        }

        /// <summary>
        /// Pen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemDrawClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Pen);
        }

        /// <summary>
        /// Colour picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemColourPickerClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Dropper);
        }

        /// <summary>
        /// Mirror X
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemMirrorXClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.MirrorX);
        }

        /// <summary>
        /// Mirror Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemMirrorYClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.MirrorY);
        }

        /// <summary>
        /// Transparency Lock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemTransparencyLockClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.TransparencyLock);
        }


        /// <summary>
        /// New Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewImageToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowNewEditorForm(sender, e);
        }

        /// <summary>
        /// New Project
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProjectToolStripMenuItemClick(object sender, EventArgs e)
        {
            RestartApplication();
        }

        #endregion Form events
    }
}