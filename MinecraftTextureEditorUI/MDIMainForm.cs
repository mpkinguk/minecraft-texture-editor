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
                if (State.CurrentEditor is null)
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
                    State.CurrentEditor.ToolType = toolType;
                }
                else
                {
                    State.CurrentEditor.TransparencyLock = State.DrawingTools.TransparencyLock;
                }

                if (State.DrawingTools is null)
                {
                    return;
                }

                State.DrawingTools.CurrentToolType = toolType;
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
        private void ShowToolWindows()
        {
            try
            {
                if (MdiChildren.Contains(State.TexturePicker))
                {
                    State.TexturePicker.Close();
                }

                if (MdiChildren.Contains(State.DrawingTools))
                {
                    State.DrawingTools.Close();
                }

                State.TexturePicker = new TexturePickerForm(_log) { MdiParent = this };

                State.TexturePicker.TextureClicked += TexturePickerTextureClicked;

                State.TexturePicker.LoadTextures();

                State.TexturePicker.Show();

                State.DrawingTools = new DrawingToolsForm(_log) { MdiParent = this };

                State.DrawingTools.ToolTypeChanged += DrawingToolsToolTypeChanged;
                State.DrawingTools.Colour2Changed += DrawingToolsBackColourChanged;
                State.DrawingTools.Colour1Changed += DrawingToolsForeColourChanged;
                State.DrawingTools.BrushSizeChanged += DrawingToolsBrushSizeChanged;
                State.DrawingTools.TransparencyLockChanged += DrawingToolsTransparencyLockChanged;

                State.DrawingTools.Show();

                State.DrawingTools.Location = new Point(ClientSize.Width / 8 * 5, 50);

                State.TexturePicker.Location = new Point(ClientSize.Width / 8 * 6 + 50, 50);
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
            if (State.CurrentEditor is null)
            {
                return;
            }

            State.CurrentEditor.TransparencyLock = locked;
        }

        /// <summary>
        /// Capture the brush size changed event from the Drawing tools window
        /// </summary>
        /// <param name="brushSize"></param>
        private void DrawingToolsBrushSizeChanged(int brushSize)
        {
            if (State.CurrentEditor is null)
            {
                return;
            }

            State.CurrentEditor.BrushSize = brushSize;
        }

        /// <summary>
        /// Capture the fore colour changed event from the Drawing tools window
        /// </summary>
        /// <param name="colour">The colour</param>
        private void DrawingToolsForeColourChanged(Color colour)
        {
            if (State.CurrentEditor is null)
            {
                return;
            }

            State.CurrentEditor.Colour1 = colour;
        }

        /// <summary>
        /// Capture the Back colour changed event from the Drawing tools window
        /// </summary>
        /// <param name="colour">The colour</param>
        private void DrawingToolsBackColourChanged(Color colour)
        {
            if (State.CurrentEditor is null)
            {
                return;
            }

            State.CurrentEditor.Colour2 = colour;
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
                if (State.CurrentEditor is null)
                {
                    return;
                }

                toolStripStatusLabel.Text = $"Current Tool type is {toolType}";

                State.CurrentEditor.ToolType = toolType;
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
                        if (resolutionForm.ShowDialog(this).Equals(DialogResult.OK))
                        {

                            width = resolutionForm.ImageWidth;
                            height = resolutionForm.ImageHeight;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                EditorForm childForm = new EditorForm(width, height, _log)
                {
                    MdiParent = this,
                    Text = "Editor " + MdiChildren.Count(x => x.Name == "EditorForm"),
                    Colour1 = State.DrawingTools.Colour1,
                    Colour2 = State.DrawingTools.Colour2,
                    ToolType = State.DrawingTools.CurrentToolType,
                    BrushSize = State.DrawingTools.BrushSize,
                    TransparencyLock = State.DrawingTools.TransparencyLock,
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
                if (State.CurrentEditor is null)
                {
                    undoToolStripMenuItem.Enabled = false;
                    redoToolStripMenuItem.Enabled = false;
                    toolStripButtonUndo.Enabled = false;
                    toolStripButtonRedo.Enabled = false;
                    return;
                }

                undoToolStripMenuItem.Enabled = State.CurrentEditor.UndoEnabled;
                redoToolStripMenuItem.Enabled = State.CurrentEditor.RedoEnabled;
                toolStripButtonUndo.Enabled = State.CurrentEditor.UndoEnabled;
                toolStripButtonRedo.Enabled = State.CurrentEditor.RedoEnabled;
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
                State.CurrentEditor.LoadFile(fileName);
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
            if (State.CurrentEditor is null)
            {
                return;
            }

            if (State.CurrentEditor.UndoEnabled)
            {
                State.CurrentEditor.Undo();
            }
        }

        /// <summary>
        /// Redo changes
        /// </summary>
        private void Redo()
        {
            if (State.CurrentEditor is null)
            {
                return;
            }

            if (State.CurrentEditor.RedoEnabled)
            {
                State.CurrentEditor.Redo();
            }
        }

        /// <summary>
        /// Open the Create Project Wizard form
        /// </summary>
        private void OpenCreateProjectWizardForm()
        {
            try
            {
                var createProjectWizard = new CreateProjectWizardForm(_log);

                if (createProjectWizard.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Project Created!", "Deployment complete");

                    ShowToolWindows();
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
                var deploymentWizard = new DeploymentWizardForm(_log);

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
                        toolStripStatusLabel.Text = "Restarting App...";

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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                
                var result = State.CurrentEditor.SaveFile(State.CurrentEditor.FileName);

                if (result)
                {
                    State.TexturePicker.RefreshImage(State.CurrentEditor.FileName);
                } 
                else
                {
                    throw new Exception($"Could not save file {State.CurrentEditor.FileName}");
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                
                var result = State.CurrentEditor.SaveFile();

                if (!result)
                {
                    throw new Exception($"Could not save file {State.CurrentEditor.FileName}");
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                var data = State.PixelClipboard.Clone();
                if (data is null)
                {
                    return;
                }
                State.CurrentEditor.Texture = data;
                State.CurrentEditor.HasChanged = true;
                State.CurrentEditor.AddItem();
                State.CurrentEditor.RefreshDisplay();
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
            State.CurrentEditor = null;

            CheckUndos();
        }

        /// <summary>
        /// Capture the undo manager action event from the current editor window
        /// </summary>
        private void ChildFormUndoManagerAction()
        {
            try
            {
                if (State.CurrentEditor is null)
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
                State.DrawingTools.Colour1 = colour;
            }
            else
            {
                State.DrawingTools.Colour2 = colour;
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
                State.CurrentEditor = (EditorForm)sender;
                State.CurrentEditor.Colour1 = State.DrawingTools.Colour1;
                State.CurrentEditor.Colour2 = State.DrawingTools.Colour2;
                State.CurrentEditor.ToolType = State.DrawingTools.CurrentToolType;
                State.CurrentEditor.TransparencyLock = State.DrawingTools.TransparencyLock;
                State.CurrentEditor.BrushSize = State.DrawingTools.BrushSize;

                toolStripStatusLabel.Text = $"Current editor is {State.CurrentEditor.Text}";
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                State.PixelClipboard = State.CurrentEditor.Texture.Clone();
                State.CurrentEditor.Texture = new Texture(State.PixelClipboard.Width, State.PixelClipboard.Height);
                State.CurrentEditor.HasChanged = true;
                State.CurrentEditor.AddItem();
                State.CurrentEditor.RefreshDisplay();
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                State.PixelClipboard = State.CurrentEditor.Texture.Clone();
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                State.CurrentEditor.ShowGrid = !State.CurrentEditor.ShowGrid;
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                State.CurrentEditor.ShowTransparent = !State.CurrentEditor.ShowTransparent;
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
                if (State.CurrentEditor is null)
                {
                    return;
                }
                State.CurrentEditor.Clear();
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