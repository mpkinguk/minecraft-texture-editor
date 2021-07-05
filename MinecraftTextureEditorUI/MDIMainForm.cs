using MinecraftTextureEditorAPI.Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static MinecraftTextureEditorAPI.DrawingHelper;

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

        private bool _skipResolutionCheck;

        /// <summary>
        /// Constructor
        /// </summary>
        public MDIMainForm()
        {
            if (!CheckFontLoaded())
            {
                MessageBox.Show(this, "Could not load font. Please check Fonts directory for MINECRAFT_FONT.TTF", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

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

        #region Private methods

        /// <summary>
        /// Randomises the wallpaper
        /// </summary>
        private void RandomiseWallpaper()
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

        /// <summary>
        /// Is called once form is fully loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDIMainFormShown(object sender, EventArgs e)
        {
            ShowToolWindows();
        }

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

                if (MessageBox.Show(this, "Minecraft font needs to be loaded.\nPlease click OK to install font", "Font not found", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
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
            catch (Exception exc)
            {
                MessageBox.Show(this, exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Show tool windows
        /// </summary>
        private void ShowToolWindows(string filename = "")
        {
            if (MdiChildren.Contains(TexturePicker))
            {
                TexturePicker.Close();
            }

            if (MdiChildren.Contains(DrawingTools))
            {
                DrawingTools.Close();
            }

            DrawingTools = new DrawingToolsForm { MdiParent = this };

            TexturePicker = new TexturePickerForm(filename) { MdiParent = this };

            DrawingTools.ToolTypeChanged += DrawingToolsToolTypeChanged;

            DrawingTools.Colour2Changed += DrawingToolsBackColourChanged;
            DrawingTools.Colour1Changed += DrawingToolsForeColourChanged;

            TexturePicker.TextureClicked += TexturePickerTextureClicked;

            DrawingTools.Show();

            TexturePicker.Show();

            CurrentPath = TexturePicker.CurrentPath;

            DrawingTools.Location = new Point(ClientSize.Width / 8 * 5, 50);

            TexturePicker.Location = new Point(ClientSize.Width / 8 * 6 + 50, 50);
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
            //ShowNewEditorForm(this, new EventArgs());
        }

        /// <summary>
        /// Captures the tool type changed event from the drawing tools window
        /// </summary>
        /// <param name="toolType"></param>
        private void DrawingToolsToolTypeChanged(ToolType toolType)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            toolStripStatusLabel.Text = $"Current Tool type is {toolType}";

            CurrentEditor.ToolType = toolType;
        }

        /// <summary>
        /// Creates a new editor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNewEditorForm(object sender, EventArgs e)
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
                using (ResolutionForm resolutionForm = new ResolutionForm())
                {
                    resolutionForm.ShowDialog(this);

                    width = resolutionForm.ImageWidth;
                    height = resolutionForm.ImageHeight;
                }
            }

            EditorForm childForm = new EditorForm(width, height)
            {
                MdiParent = this,
                Text = "Editor " + MdiChildren.Count(x => x.Name == "EditorForm"),
                Colour1 = DrawingTools.Colour1,
                Colour2 = DrawingTools.Colour2,
                ToolType = DrawingTools.CurrentToolType,
                Zoom = 16
            };

            childForm.GotFocus += ChildFormGotFocus;
            childForm.ColourSelected += ChildFormColourSelected;
            childForm.UndoManagerAction += ChildFormUndoManagerAction;
            childForm.Disposed += ChildFormDisposed;
            childForm.Show();
        }

        /// <summary>
        /// Enable/disable undo controls based on current editor status
        /// </summary>
        private void CheckUndos()
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

        /// <summary>
        /// Load a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        private void LoadFile(string fileName = "")
        {
            _skipResolutionCheck = true;
            ShowNewEditorForm(this, new EventArgs());
            CurrentEditor.LoadFile(fileName);
            _skipResolutionCheck = false;
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
            var createProjectWizard = new CreateProjectWizardForm()
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

        /// <summary>
        /// Open the deployment wizard form
        /// </summary>
        private void OpenDeploymentWizardForm()
        {
            var deploymentWizard = new DeploymentWizardForm()
            {
                DeploymentPath = CurrentPath
            };

            if (deploymentWizard.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show("Package deployed\nPlease open Minecraft and select your texture pack to test it out!", "Deployment complete");
            }
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
            if (CurrentEditor is null)
            {
                return;
            }

            toolStripStatusLabel.Text = $"Undo manager action occurred";

            CheckUndos();
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
            if (sender is null)
            {
                return;
            }

            CurrentEditor = (EditorForm)sender;

            CurrentEditor.Colour1 = DrawingTools.Colour1;
            CurrentEditor.Colour2 = DrawingTools.Colour2;
            CurrentEditor.ToolType = DrawingTools.CurrentToolType;

            toolStripStatusLabel.Text = $"Current editor is {CurrentEditor.Text}";
        }

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile(object sender, EventArgs e)
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
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.SaveFile();
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

        /// <summary>
        /// Copy (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            PixelClipboard = CurrentEditor.Texture.Clone();
        }

        /// <summary>
        /// Paste (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// Cascade (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// Tile vertical (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// Tile horizontal (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Arrange icons (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        /// <summary>
        /// Override layout so only Editor windows are affected
        /// </summary>
        /// <param name="layout">The layout</param>
        private new void LayoutMdi(MdiLayout layout)
        {
            foreach (Form childForm in MdiChildren.Where(o => o.GetType().Equals(typeof(EditorForm))))
            {
                childForm.LayoutMdi(layout);
            }

            Refresh();
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
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.SaveFile(CurrentEditor.FileName);
        }

        /// <summary>
        /// Save (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.SaveFile(CurrentEditor.FileName);
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
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.SaveFile();
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
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.ShowGrid = !CurrentEditor.ShowGrid;
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
            var optionsForm = new OptionsForm();

            if (optionsForm.ShowDialog(this) == DialogResult.OK)
            {
                if (optionsForm.HasSaved)
                {
                    toolStripStatusLabel.Text = "Restarting App...";

                    Close();

                    ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath);

                    Process.Start(info);
                }
            }
        }

        /// <summary>
        /// Show about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            using (var aboutForm = new AboutForm())
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
            if (CurrentEditor is null)
            {
                return;
            }

            CurrentEditor.ShowTransparent = !CurrentEditor.ShowTransparent;
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

        #endregion Form events
    }
}