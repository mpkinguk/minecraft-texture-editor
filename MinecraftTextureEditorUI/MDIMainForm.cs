using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            Shown += MDIMainFormShown;
        }

        #region Private methods

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
            ShowNewEditorForm(this, new EventArgs());

            LoadFile(filename);
        }

        /// <summary>
        /// Captures the tool type changed event from the drawing tools window
        /// </summary>
        /// <param name="toolType"></param>
        private void DrawingToolsToolTypeChanged(DrawingToolsForm.ToolType toolType)
        {
            if (CurrentEditor is null)
            {
                return;
            }

            Text = $"Current Tool type is {toolType}";

            CurrentEditor.ToolType = toolType;
        }

        /// <summary>
        /// Creates a new editor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNewEditorForm(object sender, EventArgs e)
        {
            EditorForm childForm = new EditorForm
            {
                MdiParent = this,
                Text = "Editor " + MdiChildren.Count(x => x.Name == "EditorForm"),
                Colour1 = DrawingTools.Colour1,
                Colour2 = DrawingTools.Colour2,
                ToolType = DrawingTools.CurrentToolType,
                Texture = DrawingHelper.GetBlankTexture(16, 16),
                Zoom = 16
            };

            childForm.GotFocus += ChildFormGotFocus;
            childForm.ColourSelected += ChildFormColourSelected;
            childForm.UndoManagerAction += ChildFormUndoManagerAction;
            childForm.Show();
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

            Text = $"Undo manager action occurred";

            undoToolStripMenuItem.Enabled = CurrentEditor.UndoEnabled;
            redoToolStripMenuItem.Enabled = CurrentEditor.RedoEnabled;
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
        /// Load a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        private void LoadFile(string fileName = "")
        {
            // Open a new form if none exist
            if (MdiChildren.Count(x => x.Name == "EditorForm") == 0)
            {
                ShowNewEditorForm(this, new EventArgs());
            }

            CurrentEditor.LoadFile(fileName);
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

        #endregion Private methods

        #region Form events

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

            Text = $"Current editor is {CurrentEditor.Text}";
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

            PixelClipboard = CurrentEditor.Texture;

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

            PixelClipboard = CurrentEditor.Texture;
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

            var data = PixelClipboard;

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
            var deploymentWizard = new DeploymentWizardForm()
            {
                DeploymentPath = CurrentPath
            };

            if (deploymentWizard.ShowDialog(this) == DialogResult.OK)
            {
                MessageBox.Show("Package deployed\nPlease open Minecraft and select your texture pack to test it out!", "Deployment complete");
            }
        }

        /// <summary>
        /// Create Project Wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonCreateWizardClick(object sender, EventArgs e)
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
                    Text = "Restarting App...";

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

        }


        #endregion Form events

    }
}