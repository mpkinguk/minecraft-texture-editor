
namespace MinecraftTextureEditorUI
{
    partial class MDIMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            _timer?.Stop();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIMainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEraser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemColourPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTexturiser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFloodFill = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRainbow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMirrorX = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMirrorY = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShape = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTransparencyLock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRotateFlip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemDeploymentWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCreateProjectWizard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonTransparent = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonToggleGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCreateProjectWizard = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeploymentWizard = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripModifierLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripToolTypeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripBrushSizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarCpu = new CustomControls.MyToolStripProgressBar();
            this.toolStripProgressBarRam = new CustomControls.MyToolStripProgressBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.viewMenu,
            this.toolsMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(916, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator4,
            this.exitToolStripMenuItem});
            this.fileMenu.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(46, 20);
            this.fileMenu.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newImageToolStripMenuItem,
            this.newProjectToolStripMenuItem});
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // newImageToolStripMenuItem
            // 
            this.newImageToolStripMenuItem.Name = "newImageToolStripMenuItem";
            this.newImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newImageToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.newImageToolStripMenuItem.Text = "Image";
            this.newImageToolStripMenuItem.Click += new System.EventHandler(this.NewImageToolStripMenuItemClick);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.newProjectToolStripMenuItem.Text = "Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.NewProjectToolStripMenuItemClick);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.LoadFile);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(229, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(229, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // editMenu
            // 
            this.editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.pasteCursorToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.editMenu.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editMenu.Name = "editMenu";
            this.editMenu.Size = new System.Drawing.Size(47, 20);
            this.editMenu.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItemClick);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItemClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(170, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // pasteCursorToolStripMenuItem
            // 
            this.pasteCursorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteCursorToolStripMenuItem.Image")));
            this.pasteCursorToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.pasteCursorToolStripMenuItem.Name = "pasteCursorToolStripMenuItem";
            this.pasteCursorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteCursorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.pasteCursorToolStripMenuItem.Text = "&Paste at cursor";
            this.pasteCursorToolStripMenuItem.Click += new System.EventHandler(this.PasteCursorToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItemClick);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(50, 20);
            this.viewMenu.Text = "&View";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.toolBarToolStripMenuItem.Text = "&Toolbar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.statusBarToolStripMenuItem.Text = "&Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDraw,
            this.toolStripMenuItemEraser,
            this.toolStripMenuItemColourPicker,
            this.toolStripMenuItemTexturiser,
            this.toolStripMenuItemFloodFill,
            this.toolStripMenuItemRainbow,
            this.toolStripMenuItemShape,
            this.toolStripMenuItemMirrorX,
            this.toolStripMenuItemMirrorY,
            this.toolStripMenuItemTransparencyLock,
            this.toolStripSeparator10,
            this.toolStripMenuItemRotateFlip,
            this.toolStripSeparator7,
            this.toolStripMenuItemDeploymentWizard,
            this.toolStripMenuItemCreateProjectWizard,
            this.toolStripSeparator5,
            this.optionsToolStripMenuItem});
            this.toolsMenu.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(61, 20);
            this.toolsMenu.Text = "&Tools";
            // 
            // toolStripMenuItemDraw
            // 
            this.toolStripMenuItemDraw.Checked = true;
            this.toolStripMenuItemDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemDraw.Name = "toolStripMenuItemDraw";
            this.toolStripMenuItemDraw.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.toolStripMenuItemDraw.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemDraw.Text = "Draw";
            this.toolStripMenuItemDraw.Click += new System.EventHandler(this.ToolStripMenuItemDrawClick);
            // 
            // toolStripMenuItemEraser
            // 
            this.toolStripMenuItemEraser.Name = "toolStripMenuItemEraser";
            this.toolStripMenuItemEraser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.toolStripMenuItemEraser.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemEraser.Text = "Eraser";
            this.toolStripMenuItemEraser.Click += new System.EventHandler(this.ToolStripMenuItemEraserClick);
            // 
            // toolStripMenuItemColourPicker
            // 
            this.toolStripMenuItemColourPicker.Name = "toolStripMenuItemColourPicker";
            this.toolStripMenuItemColourPicker.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.toolStripMenuItemColourPicker.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemColourPicker.Text = "Colour Picker";
            this.toolStripMenuItemColourPicker.Click += new System.EventHandler(this.ToolStripMenuItemColourPickerClick);
            // 
            // toolStripMenuItemTexturiser
            // 
            this.toolStripMenuItemTexturiser.Name = "toolStripMenuItemTexturiser";
            this.toolStripMenuItemTexturiser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.toolStripMenuItemTexturiser.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemTexturiser.Text = "Texturiser";
            this.toolStripMenuItemTexturiser.Click += new System.EventHandler(this.ToolStripMenuItemTexturiserClick);
            // 
            // toolStripMenuItemFloodFill
            // 
            this.toolStripMenuItemFloodFill.Name = "toolStripMenuItemFloodFill";
            this.toolStripMenuItemFloodFill.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.toolStripMenuItemFloodFill.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemFloodFill.Text = "Flood Fill";
            this.toolStripMenuItemFloodFill.Click += new System.EventHandler(this.ToolStripMenuItemFloodFillClick);
            // 
            // toolStripMenuItemRainbow
            // 
            this.toolStripMenuItemRainbow.Name = "toolStripMenuItemRainbow";
            this.toolStripMenuItemRainbow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.toolStripMenuItemRainbow.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemRainbow.Text = "Rainbow!";
            this.toolStripMenuItemRainbow.Click += new System.EventHandler(this.ToolStripMenuItemRainbowClick);
            // 
            // toolStripMenuItemShape
            // 
            this.toolStripMenuItemShape.Name = "toolStripMenuItemShape";
            this.toolStripMenuItemShape.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
            this.toolStripMenuItemShape.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemShape.Text = "Shape";
            this.toolStripMenuItemShape.Click += new System.EventHandler(this.ToolStripMenuItemShapeClick);
            // 
            // toolStripMenuItemMirrorX
            // 
            this.toolStripMenuItemMirrorX.Name = "toolStripMenuItemMirrorX";
            this.toolStripMenuItemMirrorX.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D8)));
            this.toolStripMenuItemMirrorX.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemMirrorX.Text = "Mirror X";
            this.toolStripMenuItemMirrorX.Click += new System.EventHandler(this.ToolStripMenuItemMirrorXClick);
            // 
            // toolStripMenuItemMirrorY
            // 
            this.toolStripMenuItemMirrorY.Name = "toolStripMenuItemMirrorY";
            this.toolStripMenuItemMirrorY.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D9)));
            this.toolStripMenuItemMirrorY.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemMirrorY.Text = "Mirror Y";
            this.toolStripMenuItemMirrorY.Click += new System.EventHandler(this.ToolStripMenuItemMirrorYClick);
            // 
            // toolStripMenuItemTransparencyLock
            // 
            this.toolStripMenuItemTransparencyLock.Name = "toolStripMenuItemTransparencyLock";
            this.toolStripMenuItemTransparencyLock.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.toolStripMenuItemTransparencyLock.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemTransparencyLock.Text = "Transparency Lock";
            this.toolStripMenuItemTransparencyLock.Click += new System.EventHandler(this.ToolStripMenuItemTransparencyLockClick);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(387, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItemRotateFlip.Name = "toolStripMenuItemRotateFlip";
            this.toolStripMenuItemRotateFlip.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.toolStripMenuItemRotateFlip.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemRotateFlip.Text = "Rotate and Flip...";
            this.toolStripMenuItemRotateFlip.Click += new System.EventHandler(this.ToolStripRotateFlipMenuItemClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(387, 6);
            // 
            // toolStripMenuItemDeploymentWizard
            // 
            this.toolStripMenuItemDeploymentWizard.Name = "toolStripMenuItemDeploymentWizard";
            this.toolStripMenuItemDeploymentWizard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.toolStripMenuItemDeploymentWizard.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemDeploymentWizard.Text = "\"Texture Pack Deployment\" &Wizard ...";
            this.toolStripMenuItemDeploymentWizard.Click += new System.EventHandler(this.ToolStripMenuItemDeploymentWizardClick);
            // 
            // toolStripMenuItemCreateProjectWizard
            // 
            this.toolStripMenuItemCreateProjectWizard.Name = "toolStripMenuItemCreateProjectWizard";
            this.toolStripMenuItemCreateProjectWizard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toolStripMenuItemCreateProjectWizard.Size = new System.Drawing.Size(390, 22);
            this.toolStripMenuItemCreateProjectWizard.Text = "\"Create Project\" Wizard ...";
            this.toolStripMenuItemCreateProjectWizard.Click += new System.EventHandler(this.ToolStripMenuItemCreateProjectWizardClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(387, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(390, 22);
            this.optionsToolStripMenuItem.Text = "&Options ...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(77, 20);
            this.windowsMenu.Text = "&Windows";
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newWindowToolStripMenuItem.Text = "&New Window";
            this.newWindowToolStripMenuItem.Click += new System.EventHandler(this.ShowNewEditorForm);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.closeAllToolStripMenuItem.Text = "C&lose All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(52, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.aboutToolStripMenuItem.Text = "&About ...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripButtonSaveAs,
            this.toolStripSeparator1,
            this.toolStripButtonUndo,
            this.toolStripButtonRedo,
            this.toolStripSeparator2,
            this.toolStripButtonTransparent,
            this.toolStripButtonToggleGrid,
            this.toolStripSeparator9,
            this.toolStripButtonCreateProjectWizard,
            this.toolStripButtonDeploymentWizard});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(916, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Font = new System.Drawing.Font("Minecraft", 10F);
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "New";
            this.newToolStripButton.Click += new System.EventHandler(this.ShowNewEditorForm);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "Open";
            this.openToolStripButton.Click += new System.EventHandler(this.LoadFile);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "Save";
            this.saveToolStripButton.Click += new System.EventHandler(this.SaveToolStripButtonClick);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.Image = global::MinecraftTextureEditorUI.Properties.Resources.SaveAs;
            this.toolStripButtonSaveAs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveAs.Text = "Save As...";
            this.toolStripButtonSaveAs.Click += new System.EventHandler(this.ToolStripButtonSaveAsClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Image = global::MinecraftTextureEditorUI.Properties.Resources.undo;
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUndo.Text = "Undo";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.ToolStripButtonUndoClick);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image = global::MinecraftTextureEditorUI.Properties.Resources.redo;
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRedo.Text = "Redo";
            this.toolStripButtonRedo.Click += new System.EventHandler(this.ToolStripButtonRedoClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonTransparent
            // 
            this.toolStripButtonTransparent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTransparent.Image = global::MinecraftTextureEditorUI.Properties.Resources.transparentGrid;
            this.toolStripButtonTransparent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTransparent.Name = "toolStripButtonTransparent";
            this.toolStripButtonTransparent.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTransparent.Text = "Toggle Transparent background";
            this.toolStripButtonTransparent.Click += new System.EventHandler(this.ToolStripButtonTransparentClick);
            // 
            // toolStripButtonToggleGrid
            // 
            this.toolStripButtonToggleGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonToggleGrid.Image = global::MinecraftTextureEditorUI.Properties.Resources.grid;
            this.toolStripButtonToggleGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonToggleGrid.Name = "toolStripButtonToggleGrid";
            this.toolStripButtonToggleGrid.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonToggleGrid.Text = "Toggle grid";
            this.toolStripButtonToggleGrid.Click += new System.EventHandler(this.ToolStripButtonToggleGridClick);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCreateProjectWizard
            // 
            this.toolStripButtonCreateProjectWizard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCreateProjectWizard.Image = global::MinecraftTextureEditorUI.Properties.Resources.wizardcreate;
            this.toolStripButtonCreateProjectWizard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateProjectWizard.Name = "toolStripButtonCreateProjectWizard";
            this.toolStripButtonCreateProjectWizard.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCreateProjectWizard.Text = "Create Project Wizard";
            this.toolStripButtonCreateProjectWizard.Click += new System.EventHandler(this.ToolStripButtonCreateWizardClick);
            // 
            // toolStripButtonDeploymentWizard
            // 
            this.toolStripButtonDeploymentWizard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDeploymentWizard.Image = global::MinecraftTextureEditorUI.Properties.Resources.wizarddeploy;
            this.toolStripButtonDeploymentWizard.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDeploymentWizard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeploymentWizard.Name = "toolStripButtonDeploymentWizard";
            this.toolStripButtonDeploymentWizard.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDeploymentWizard.Text = "Deployment Wizard";
            this.toolStripButtonDeploymentWizard.Click += new System.EventHandler(this.ToolStripButtonDeploymentWizardClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripToolTypeLabel,
            this.toolStripModifierLabel,
            this.toolStripBrushSizeLabel,
            (CustomControls.MyToolStripProgressBar)this.toolStripProgressBarCpu,
            (CustomControls.MyToolStripProgressBar)this.toolStripProgressBarRam});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.Size = new System.Drawing.Size(916, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Minecraft", 9F);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripModifierLabel
            // 
            this.toolStripModifierLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripModifierLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripModifierLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripModifierLabel.Font = new System.Drawing.Font("Minecraft", 9F);
            this.toolStripModifierLabel.Name = "toolStripModifierLabel";
            this.toolStripModifierLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripToolTypeLabel
            // 
            this.toolStripToolTypeLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripToolTypeLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripToolTypeLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripToolTypeLabel.Font = new System.Drawing.Font("Minecraft", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripToolTypeLabel.Name = "toolStripToolTypeLabel";
            this.toolStripToolTypeLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripBrushSizeLabel
            // 
            this.toolStripBrushSizeLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripBrushSizeLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripBrushSizeLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBrushSizeLabel.Font = new System.Drawing.Font("Minecraft", 9F);
            this.toolStripBrushSizeLabel.Name = "toolStripBrushSizeLabel";
            this.toolStripBrushSizeLabel.Size = new System.Drawing.Size(4, 17);
            // 
            // toolStripProgressBarCpu
            // 
            this.toolStripProgressBarCpu.Font = new System.Drawing.Font("Minecraft", 6F);
            this.toolStripProgressBarCpu.Name = "toolStripProgressBarCpu";
            this.toolStripProgressBarCpu.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBarCpu.ToolTipText = "CPU";
            // 
            // toolStripProgressBarRam
            // 
            this.toolStripProgressBarRam.Font = new System.Drawing.Font("Minecraft", 6F);
            this.toolStripProgressBarRam.Name = "toolStripProgressBarRam";
            this.toolStripProgressBarRam.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBarRam.ToolTipText = "RAM";
            // 
            // MDIMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(916, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDIMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minecraft Texture Editor";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripToolTypeLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenu;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeploymentWizard;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeploymentWizard;
        private System.Windows.Forms.ToolStripButton toolStripButtonToggleGrid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDraw;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEraser;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemColourPicker;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMirrorX;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMirrorY;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateProjectWizard;
        private System.Windows.Forms.ToolStripButton toolStripButtonTransparent;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCreateProjectWizard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTexturiser;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFloodFill;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRainbow;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTransparencyLock;
        private System.Windows.Forms.ToolStripMenuItem newImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripBrushSizeLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripModifierLabel;
        private CustomControls.MyToolStripProgressBar toolStripProgressBarCpu;
        private CustomControls.MyToolStripProgressBar toolStripProgressBarRam;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRotateFlip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShape;
    }
}



