using log4net;
using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;
using static MinecraftTextureEditorAPI.Helpers.FileHelper;

namespace MinecraftTextureEditorUI
{
    public partial class MDIMainForm : Form
    {
        #region External methods

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        #endregion External methods

        #region Private properties

        private readonly PerformanceCounter _cpuCounter;
        private readonly ILog _log;
        private readonly PerformanceCounter _ramCounter;

        private readonly System.Timers.Timer _timer;

        private readonly long _totalRam;

        private bool _skipResolutionCheck;

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

                toolStripMenuItemShape.DropDownItems.AddRange(GetShapeMenuItems());

                WindowState = FormWindowState.Maximized;

                Shown += MDIMainFormShown;

                // Reduce display flicker
                SetStyle(ControlStyles.AllPaintingInWmPaint & ControlStyles.UserPaint & ControlStyles.OptimizedDoubleBuffer & ControlStyles.ResizeRedraw, true);

                UpdateLabels();

                RandomiseWallpaper();

                GetPhysicallyInstalledSystemMemory(out _totalRam);

                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _ramCounter = new PerformanceCounter("Memory", "Available MBytes");

                toolStripProgressBarCpu.Maximum = 100;
                toolStripProgressBarCpu.Value = 0;

                // Make this MB
                toolStripProgressBarRam.Maximum = (int)_totalRam / 1024;
                toolStripProgressBarRam.Value = 0;

                _timer = new System.Timers.Timer
                {
                    Interval = 1200
                };

                _timer.Elapsed += TimerElapsed;

                _timer.Start();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Private methods

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
        /// Enable/disable undo controls based on current editor status
        /// </summary>
        private void CheckUndos()
        {
            try
            {
                if (State.Editor is null)
                {
                    undoToolStripMenuItem.Enabled = false;
                    redoToolStripMenuItem.Enabled = false;
                    toolStripButtonUndo.Enabled = false;
                    toolStripButtonRedo.Enabled = false;
                    return;
                }

                undoToolStripMenuItem.Enabled = State.Editor.UndoEnabled;
                redoToolStripMenuItem.Enabled = State.Editor.RedoEnabled;
                toolStripButtonUndo.Enabled = State.Editor.UndoEnabled;
                toolStripButtonRedo.Enabled = State.Editor.RedoEnabled;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Clear the current texture
        /// </summary>
        private void Clear()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }
                State.Editor.Clear();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Close all the editor forms
        /// </summary>
        private void CloseAll()
        {
            foreach (Form childForm in MdiChildren.Where(o => o.GetType().Equals(typeof(EditorForm))))
            {
                childForm.Close();
            }

            State.Editor = null;
        }

        /// <summary>
        /// Copy
        /// </summary>
        private void Copy()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }
                State.PixelClipboard = (Bitmap)State.Editor.Texture.Clone();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Cut
        /// </summary>
        private void Cut()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }

                State.PixelClipboard = (Bitmap)State.Editor.Texture.Clone();

                Clear();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Brush size changed event
        /// </summary>
        private void DrawingToolsBrushSizeChanged()
        {
            UpdateLabels();
        }

        /// <summary>
        /// Captures the tool type changed event from the drawing tools window
        /// </summary>
        private void DrawingToolsToolTypeChanged()
        {
            UpdateLabels();
            SelectTool(null, State.ToolType);
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
                State.Editor.LoadFile(fileName);
                _skipResolutionCheck = false;

                UpdateLabels();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Load Textures
        /// </summary>
        /// <param name="browse">Open folder browser dialog</param>
        private async Task LoadTextures(bool browse = false)
        {
            var defaultProjectPath = Path.Combine(GetDefaultProjectFolder());

            // Only grab it from settings once
            State.Path = string.IsNullOrEmpty(State.Path) ? defaultProjectPath : State.Path;

            if (browse)
            {
                State.Path = SelectFolder(State.Path);

                // if the user cancels, exit the application, as no textures will be loaded and it will not be usable
                if (string.IsNullOrEmpty(State.Path))
                {
                    var message = "No path selected to load textures. Do you want to use the Create Deployment Wizard?\nClick \"Yes\" to use the wizard\nClick \"No\" to choose another path\nClick \"Cancel\" to choose you own file or create a new one";
                    _log.Debug("No path selected");
                    var result = MessageBox.Show(this, message, "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                    switch (result)
                    {
                        case DialogResult.Cancel:
                            return;

                        case DialogResult.Yes:
                            await OpenCreateProjectWizardForm().ConfigureAwait(false);
                            return;

                        case DialogResult.No:
                            await LoadTextures(browse).ConfigureAwait(false);
                            break;
                    }
                }

                if (string.IsNullOrEmpty(State.Path))
                {
                    State.Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    return;
                }

                var assetsDirectorySearch = SafeFileEnumerator.EnumerateDirectories(State.Path, Constants.AssetsFolder, SearchOption.AllDirectories).ToList();

                string directory = string.Empty;

                if (!assetsDirectorySearch.Any())
                {
                    if (State.Path.Contains(Constants.AssetsFolder))
                    {
                        directory = State.Path;
                    }
                    else
                    {
                        if (MessageBox.Show(this, "This path does not contain an asset folder.\nClick OK to choose a different path, or click cancel to continue without using a project.", "Information", MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
                        {
                            await LoadTextures(browse).ConfigureAwait(false);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (assetsDirectorySearch.Count.Equals(1))
                    {
                        directory = assetsDirectorySearch.FirstOrDefault();
                    }
                    else
                    {
                        using (var form = new AssetPickerForm(_log, assetsDirectorySearch))
                        {
                            if (form.ShowDialog(this).Equals(DialogResult.OK))
                            {
                                directory = form.Asset;
                            }
                            else
                            {
                                throw new OperationCanceledException("No asset chosen");
                            }
                        }
                    }
                }

                // Otherwise CurrentPath will just be set to the root folder,
                // which creates all sorts of fun when deploying... :o|
                State.Path = directory;
            }

            await State.TexturePicker.LoadTextures().ConfigureAwait(false);
        }

        /// <summary>
        /// Open the Create Project Wizard form
        /// </summary>
        private async Task OpenCreateProjectWizardForm()
        {
            try
            {
                using (var createProjectWizard = new CreateProjectWizardForm(_log))
                {
                    if (createProjectWizard.ShowDialog(this) == DialogResult.OK)
                    {
                        if (createProjectWizard.Success)
                        {
                            MessageBox.Show("Project Created!", "Deployment complete");

                            await LoadTextures().ConfigureAwait(false);
                        }
                    }
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
                    var deployed = deploymentWizard.Deployed;
                    var unPacked = deploymentWizard.UnPacked;
                    var zipFilePath = deploymentWizard.ZipFilePath;

                    if (deployed)
                    {
                        if (unPacked)
                        {
                            MessageBox.Show(this, $"Package deployed\nPlease open Minecraft and select your texture pack to test it out!", "Deployment complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        } else
                        {
                            if(MessageBox.Show(
                                this, 
                                "Package deployed as zip file\nWould you like to access this location?", 
                                "Deployment complete. Access location?", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                            {
                                Process.Start(new ProcessStartInfo("Explorer.exe", $"/select, {zipFilePath}"));
                            }
                        }
                    } 
                    else
                    {
                        MessageBox.Show("Package not deployed");
                    }
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Paste
        /// </summary>
        private void Paste()
        {
            try
            {
                if (State.Editor is null || State.PixelClipboard is null)
                {
                    return;
                }

                var data = State.PixelClipboard.Clone();

                if (data is null)
                {
                    return;
                }

                State.Editor.Texture = (Bitmap)data;
                State.Editor.HasChanged = true;
                State.Editor.AddItem();
                State.Editor.RefreshDisplay();
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
        /// Redo changes
        /// </summary>
        private void Redo()
        {
            if (State.Editor is null)
            {
                return;
            }

            if (State.Editor.RedoEnabled)
            {
                State.Editor.Redo();
            }
        }

        /// <summary>
        /// Restart the application
        /// </summary>
        private void RestartApplication()
        {
            _timer?.Stop();
            Close();
            ProcessStartInfo info = new ProcessStartInfo(Application.ExecutablePath);
            Process.Start(info);
        }

        /// <summary>
        /// Save file
        /// </summary>
        private void Save()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }

                var result = State.Editor.SaveFile(State.Editor.FileName);

                if (result)
                {
                    State.TexturePicker.RefreshImage(State.Editor.FileName);
                }
                else
                {
                    throw new Exception($"Could not save file {State.Editor.FileName}");
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
                if (State.Editor is null)
                {
                    return;
                }

                var result = State.Editor.SaveFile();

                if (!result)
                {
                    throw new Exception($"Could not save file {State.Editor.FileName}");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Modifier has been selected. Inform other controls
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="modifier">The modifier</param>
        private void SelectModifier(object sender, Modifier modifier)
        {
            try
            {
                if (sender.GetType().Equals(typeof(ToolStripMenuItem)))
                {
                    var selectedMenuItem = (ToolStripMenuItem)sender;

                    selectedMenuItem.Checked = !selectedMenuItem.Checked;

                    State.Modifiers = (toolStripMenuItemMirrorX.Checked ? Modifier.MirrorX : 0) |
                        (toolStripMenuItemMirrorY.Checked ? Modifier.MirrorY : 0) |
                        (toolStripMenuItemTransparencyLock.Checked ? Modifier.TransparencyLock : 0);
                }             

                if (State.DrawingTools is null)
                {
                    return;
                }

                State.DrawingTools.UpdateButtons();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Tool has been selected. Inform other controls
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="toolType">The tooltype</param>
        private void SelectTool(object sender, ToolType toolType)
        {
            try
            {
                var selectedMenuItem = (ToolStripMenuItem)sender is null? GetMenuItem(toolStripMenuItemDraw.GetCurrentParent(), toolType): (ToolStripMenuItem)sender;

                // Reset other items
                foreach (object item in selectedMenuItem.GetCurrentParent().Items)
                {
                    if (item.GetType().Equals(typeof(ToolStripMenuItem)))
                    {
                        ToolStripMenuItem menuItem = (ToolStripMenuItem)item;

                        foreach (var toolTypeItem in Enum.GetValues(typeof(ToolType)))
                        {
                            if (menuItem.Name.Contains($"{toolTypeItem}"))
                            {
                                menuItem.Checked = false;
                            }
                        }
                    }

                    selectedMenuItem.Checked = true;
                }

                State.ToolType = toolType;

                if (State.DrawingTools is null)
                {
                    return;
                }

                State.DrawingTools.UpdateButtons();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Get shape menu items and add click events
        /// </summary>
        /// <returns>Array of ToolStripItem</returns>
        private ToolStripMenuItem[] GetShapeMenuItems()
        {
            var items = DrawingHelper.GetShapeMenuItems();

            foreach (ToolStripMenuItem item in items)
            {
                item.Click += ToolStripMenuItemShapeClick;
            }

            return items;
        }

        /// <summary>
        /// Select a shape
        /// </summary>
        /// <param name="sender">The calling object</param>
        private void SelectShape(object sender)
        {
            var item = (ToolStripMenuItem)sender;

            if (Enum.TryParse<ShapeType>(item.Text, out var result))
            {
                State.ShapeType = result;
            }
            else
            {
                State.ShapeType = 0;
            }

            CheckShapeTypeMenuItem();

            State.DrawingTools.UpdateShapesMenu();
        }

        /// <summary>
        /// Checks the correct shape type menu item based on State.ShapeTypes
        /// </summary>
        private void CheckShapeTypeMenuItem()
        {
            foreach (ToolStripMenuItem menuItem in toolStripMenuItemShape.DropDownItems)
            {
                menuItem.Checked = menuItem.Text.Equals(State.ShapeType.ToString());
            }
        }

        /// <summary>
        /// Gets a toolstrip menu item based on the selected tooltip
        /// </summary>
        /// <param name="toolStrip">The toolstrip</param>
        /// <param name="toolType">The tool type</param>
        /// <returns>ToolStripMenuItem</returns>
        private ToolStripMenuItem GetMenuItem(ToolStrip toolStrip, ToolType toolType)
        {
            foreach (object item in toolStrip.Items)
            {
                if (item.GetType().Equals(typeof(ToolStripMenuItem)))
                {
                    ToolStripMenuItem menuItem = (ToolStripMenuItem)item;

                    if (menuItem.Name.Contains($"{toolType}"))
                    {
                        return menuItem;
                    }

                }
            }

            return null;
        }

        /// <summary>
        /// Show the about form
        /// </summary>
        private void ShowAbout()
        {
            using (var aboutForm = new AboutForm(_log))
            {
                aboutForm.ShowDialog(this);
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
                    Text = $"Editor {MdiChildren.Count(x => x.Name == "EditorForm")}",
                    Zoom = 16
                };

                childForm.GotFocus += ChildFormGotFocus;
                childForm.ColourSelected += ChildFormColourSelected;
                childForm.UndoManagerAction += ChildFormUndoManagerAction;
                childForm.Disposed += ChildFormDisposed;
                childForm.Show();

                State.Editor = childForm;
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

                State.TexturePicker = new TexturePickerForm(_log) { MdiParent = this, Visible = false };

                State.TexturePicker.TextureClicked += TexturePickerTextureClicked;

                State.TexturePicker.Location = new Point(ClientSize.Width / 8 * 6 + 50, 50);

                State.TexturePicker.Show();

                State.DrawingTools = new DrawingToolsForm(_log) { MdiParent = this, Visible = false };

                State.DrawingTools.ToolTypeChanged += DrawingToolsToolTypeChanged;
                State.DrawingTools.BrushSizeChanged += DrawingToolsBrushSizeChanged;
                State.DrawingTools.ModifierChanged += DrawingToolsModifierChanged;
                State.DrawingTools.ShapeTypeChanged += DrawingToolsShapeTypeChanged;

                State.DrawingTools.Location = new Point(ClientSize.Width / 8 * 5, 50);

                State.DrawingTools.Show();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Updates the menu items if the shape type changes
        /// </summary>
        private void DrawingToolsShapeTypeChanged()
        {
            CheckShapeTypeMenuItem();
        }

        /// <summary>
        /// Update menu items if modifier changes
        /// </summary>
        private void DrawingToolsModifierChanged()
        {
            toolStripMenuItemMirrorX.Checked = State.MirrorX;

            toolStripMenuItemMirrorY.Checked = State.MirrorY;

            toolStripMenuItemTransparencyLock.Checked = State.TransparencyLock;
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
        /// Toggle the grid
        /// </summary>
        private void ToggleGrid()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }

                State.Editor.ShowGrid = !State.Editor.ShowGrid;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Toggle transparent grid
        /// </summary>
        private void ToggleTransparentGrid()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }

                State.Editor.ShowTransparentGrid = !State.Editor.ShowTransparentGrid;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Undo changes
        /// </summary>
        private void Undo()
        {
            if (State.Editor is null)
            {
                return;
            }

            if (State.Editor.UndoEnabled)
            {
                State.Editor.Undo();
            }
        }

        /// <summary>
        /// Update the labels
        /// </summary>
        private void UpdateLabels()
        {
            if (State.Editor is null)
            {
                toolStripStatusLabel.Text = $"No editor selected";
            }
            else
            {
                toolStripStatusLabel.Text = $"Current texture is {State.Editor.Text}";
            }
            toolStripToolTypeLabel.Text = $"Tool = {State.ToolType}";
            toolStripBrushSizeLabel.Text = $"Brush = {State.BrushSize} px";
        }

        /// <summary>
        /// Rotate/Flip the image
        /// </summary>
        private void RotateAndFlip()
        {
            if (State.Editor is null)
            {
                return;
            }

            using (var rotateForm = new RotateFlipForm(_log))
            {
                if (rotateForm.ShowDialog(this).Equals(DialogResult.OK))
                {
                    State.Editor.Rotate(rotateForm.RotateFlip);
                }
            }
        }

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Show about box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            ShowAbout();
        }

        /// <summary>
        /// Capture the colour from the dropper and return it to the drawing tools window
        /// </summary>
        /// <param name="colour"></param>
        /// <param name="isColour1"></param>
        private void ChildFormColourSelected(Color colour, bool isColour1)
        {
            if (State.Editor is null)
            {
                return;
            }

            if (isColour1)
            {
                State.Colour1 = colour;
            }
            else
            {
                State.Colour2 = colour;
            }

            if (State.DrawingTools is null)
            {
                return;
            }

            State.DrawingTools.UpdateColours();
        }

        /// <summary>
        /// Clear down the current editor object if form closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildFormDisposed(object sender, EventArgs e)
        {
            State.Editor = null;

            CheckUndos();

            UpdateLabels();
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

                if (!sender.GetType().Equals(typeof(EditorForm)))
                {
                    return;
                }

                State.Editor = (EditorForm)sender;

                CheckUndos();

                UpdateLabels();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Capture the undo manager action event from the current editor window
        /// </summary>
        private void ChildFormUndoManagerAction()
        {
            try
            {
                if (State.Editor is null)
                {
                    return;
                }

                CheckUndos();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Clear the current texture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearToolStripMenuItemClick(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// Close all windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAll();
        }

        /// <summary>
        /// Copy (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        /// <summary>
        /// Cut (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
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
        /// Open file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadFile(object sender, EventArgs e)
        {
            LoadFile();
        }

        /// <summary>
        /// Is called once form is fully loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MDIMainFormShown(object sender, EventArgs e)
        {
            ShowToolWindows();

            await LoadTextures(true).ConfigureAwait(false);
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
        private async void NewProjectToolStripMenuItemClick(object sender, EventArgs e)
        {
            await LoadTextures(true).ConfigureAwait(false);
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
        /// Paste (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
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
        /// Save as (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
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
        /// Statusbar (menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Timer elapsed event for updating cpu and ram usage bars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateProgressBars();
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
        /// Create Project Wizard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToolStripButtonCreateWizardClick(object sender, EventArgs e)
        {
            await OpenCreateProjectWizardForm().ConfigureAwait(false);
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
        /// Redo (button)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonRedoClick(object sender, EventArgs e)
        {
            Redo();
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
        /// Toggle the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonToggleGridClick(object sender, EventArgs e)
        {
            ToggleGrid();
        }

        /// <summary>
        /// Toggle the transparent grid background for the current editor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripButtonTransparentClick(object sender, EventArgs e)
        {
            ToggleTransparentGrid();
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
        /// Colour picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemColourPickerClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.ColourPicker);
        }

        /// <summary>
        /// Open the create project wizard form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToolStripMenuItemCreateProjectWizardClick(object sender, EventArgs e)
        {
            await OpenCreateProjectWizardForm().ConfigureAwait(false);
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
        /// Pen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemDrawClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Draw);
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
        /// FLood fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemFloodFillClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.FloodFill);
        }

        /// <summary>
        /// Mirror X
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemMirrorXClick(object sender, EventArgs e)
        {
            SelectModifier(sender, Modifier.MirrorX);
        }

        /// <summary>
        /// Mirror Y
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemMirrorYClick(object sender, EventArgs e)
        {
            SelectModifier(sender, Modifier.MirrorY);
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
        /// Shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemShapeClick(object sender, EventArgs e)
        {
            SelectTool(sender, ToolType.Shape);

            SelectShape(sender);
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
        /// Transparency Lock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemTransparencyLockClick(object sender, EventArgs e)
        {
            SelectModifier(sender, Modifier.TransparencyLock);
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
        /// Capture the ToolStripRotateFlipMenuItemClick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripRotateFlipMenuItemClick(object sender, EventArgs e)
        {
            RotateAndFlip();
        }

        #endregion Form events

        #region Threadsafe Mehods

        /// <summary>
        /// Update the progress bars safely
        /// </summary>
        private void UpdateProgressBars()
        {
            try
            {
                if (toolStrip.InvokeRequired)
                {
                    var d = new Action(UpdateProgressBars);

                    Invoke(d);
                }
                else
                {
                    if(toolStripProgressBarCpu is null || toolStripProgressBarRam is null)
                    {
                        return;
                    }

                    toolStripProgressBarCpu.Value = (int)_cpuCounter.NextValue();
                    toolStripProgressBarCpu.ProgressBar.Text = $"CPU: {toolStripProgressBarCpu.Value}%";

                    toolStripProgressBarRam.Value = (int)_totalRam / 1024 - (int)_ramCounter.NextValue();
                    toolStripProgressBarRam.ProgressBar.Text = $"RAM: {toolStripProgressBarRam.Value} MB of {_totalRam / 1024} MB";
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Threadsafe Mehods
    }
}