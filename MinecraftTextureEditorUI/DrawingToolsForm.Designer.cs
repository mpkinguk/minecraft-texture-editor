
namespace MinecraftTextureEditorUI
{
    partial class DrawingToolsForm
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
            this.buttonDraw = new System.Windows.Forms.Button();
            this.buttonEraser = new System.Windows.Forms.Button();
            this.panelColour1 = new System.Windows.Forms.Panel();
            this.panelColour2 = new System.Windows.Forms.Panel();
            this.buttonColourPicker = new System.Windows.Forms.Button();
            this.buttonMirrorX = new System.Windows.Forms.Button();
            this.buttonMirrorY = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxGamma = new System.Windows.Forms.PictureBox();
            this.pictureBoxColourPicker = new System.Windows.Forms.PictureBox();
            this.buttonTexturiser = new System.Windows.Forms.Button();
            this.buttonFloodFill = new System.Windows.Forms.Button();
            this.pictureBoxAlpha = new System.Windows.Forms.PictureBox();
            this.buttonRainbow = new System.Windows.Forms.Button();
            this.buttonSize1 = new System.Windows.Forms.Button();
            this.buttonSize2 = new System.Windows.Forms.Button();
            this.buttonSize4 = new System.Windows.Forms.Button();
            this.buttonTransparencyLock = new System.Windows.Forms.Button();
            this.buttonShape = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStripShape = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColourPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDraw
            // 
            this.buttonDraw.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.Pen;
            this.buttonDraw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDraw.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonDraw.Location = new System.Drawing.Point(20, 384);
            this.buttonDraw.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(32, 32);
            this.buttonDraw.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonDraw, "Draw");
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.ButtonPenClick);
            // 
            // buttonEraser
            // 
            this.buttonEraser.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.Eraser;
            this.buttonEraser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonEraser.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonEraser.Location = new System.Drawing.Point(65, 384);
            this.buttonEraser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonEraser.Name = "buttonEraser";
            this.buttonEraser.Size = new System.Drawing.Size(32, 32);
            this.buttonEraser.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonEraser, "Erase");
            this.buttonEraser.UseVisualStyleBackColor = true;
            this.buttonEraser.Click += new System.EventHandler(this.ButtonEraserClick);
            // 
            // panelColour1
            // 
            this.panelColour1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColour1.Font = new System.Drawing.Font("Minecraft", 10F);
            this.panelColour1.Location = new System.Drawing.Point(20, 486);
            this.panelColour1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelColour1.Name = "panelColour1";
            this.panelColour1.Size = new System.Drawing.Size(50, 50);
            this.panelColour1.TabIndex = 8;
            this.toolTip1.SetToolTip(this.panelColour1, "Colour 1");
            // 
            // panelColour2
            // 
            this.panelColour2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColour2.Location = new System.Drawing.Point(182, 486);
            this.panelColour2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelColour2.Name = "panelColour2";
            this.panelColour2.Size = new System.Drawing.Size(50, 50);
            this.panelColour2.TabIndex = 9;
            this.toolTip1.SetToolTip(this.panelColour2, "Colour 2");
            // 
            // buttonColourPicker
            // 
            this.buttonColourPicker.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.dropper;
            this.buttonColourPicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonColourPicker.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonColourPicker.Location = new System.Drawing.Point(110, 384);
            this.buttonColourPicker.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonColourPicker.Name = "buttonColourPicker";
            this.buttonColourPicker.Size = new System.Drawing.Size(32, 32);
            this.buttonColourPicker.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonColourPicker, "Colour Picker");
            this.buttonColourPicker.UseVisualStyleBackColor = true;
            this.buttonColourPicker.Click += new System.EventHandler(this.ButtonDropperClick);
            // 
            // buttonMirrorX
            // 
            this.buttonMirrorX.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.mirrorx;
            this.buttonMirrorX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMirrorX.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonMirrorX.Location = new System.Drawing.Point(155, 423);
            this.buttonMirrorX.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonMirrorX.Name = "buttonMirrorX";
            this.buttonMirrorX.Size = new System.Drawing.Size(32, 32);
            this.buttonMirrorX.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonMirrorX, "Mirror X");
            this.buttonMirrorX.UseVisualStyleBackColor = true;
            this.buttonMirrorX.Click += new System.EventHandler(this.ButtonMirrorXClick);
            // 
            // buttonMirrorY
            // 
            this.buttonMirrorY.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.mirrory;
            this.buttonMirrorY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMirrorY.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonMirrorY.Location = new System.Drawing.Point(200, 423);
            this.buttonMirrorY.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonMirrorY.Name = "buttonMirrorY";
            this.buttonMirrorY.Size = new System.Drawing.Size(32, 32);
            this.buttonMirrorY.TabIndex = 7;
            this.toolTip1.SetToolTip(this.buttonMirrorY, "Mirror Y");
            this.buttonMirrorY.UseVisualStyleBackColor = true;
            this.buttonMirrorY.Click += new System.EventHandler(this.ButtonMirrorYClick);
            // 
            // toolTip1
            // 
            this.toolTip1.OwnerDraw = true;
            // 
            // pictureBoxGamma
            // 
            this.pictureBoxGamma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGamma.Location = new System.Drawing.Point(17, 305);
            this.pictureBoxGamma.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBoxGamma.Name = "pictureBoxGamma";
            this.pictureBoxGamma.Size = new System.Drawing.Size(216, 27);
            this.pictureBoxGamma.TabIndex = 3;
            this.pictureBoxGamma.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxGamma, "Gamma Selector");
            // 
            // pictureBoxColourPicker
            // 
            this.pictureBoxColourPicker.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxColourPicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxColourPicker.Image = global::MinecraftTextureEditorUI.Properties.Resources.ColourWheel;
            this.pictureBoxColourPicker.Location = new System.Drawing.Point(17, 64);
            this.pictureBoxColourPicker.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBoxColourPicker.Name = "pictureBoxColourPicker";
            this.pictureBoxColourPicker.Size = new System.Drawing.Size(216, 216);
            this.pictureBoxColourPicker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxColourPicker.TabIndex = 0;
            this.pictureBoxColourPicker.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxColourPicker, "Colour Selector");
            // 
            // buttonTexturiser
            // 
            this.buttonTexturiser.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturiser;
            this.buttonTexturiser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTexturiser.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonTexturiser.Location = new System.Drawing.Point(155, 384);
            this.buttonTexturiser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonTexturiser.Name = "buttonTexturiser";
            this.buttonTexturiser.Size = new System.Drawing.Size(32, 32);
            this.buttonTexturiser.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonTexturiser, "Texturiser");
            this.buttonTexturiser.UseVisualStyleBackColor = true;
            this.buttonTexturiser.Click += new System.EventHandler(this.ButtonTexturiserClick);
            // 
            // buttonFloodFill
            // 
            this.buttonFloodFill.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.floodfill;
            this.buttonFloodFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFloodFill.Location = new System.Drawing.Point(200, 384);
            this.buttonFloodFill.Name = "buttonFloodFill";
            this.buttonFloodFill.Size = new System.Drawing.Size(32, 32);
            this.buttonFloodFill.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonFloodFill, "Flood fill");
            this.buttonFloodFill.UseVisualStyleBackColor = true;
            this.buttonFloodFill.Click += new System.EventHandler(this.ButtonFloodFillClick);
            // 
            // pictureBoxAlpha
            // 
            this.pictureBoxAlpha.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.transparentGrid;
            this.pictureBoxAlpha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxAlpha.Location = new System.Drawing.Point(16, 340);
            this.pictureBoxAlpha.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pictureBoxAlpha.Name = "pictureBoxAlpha";
            this.pictureBoxAlpha.Size = new System.Drawing.Size(216, 27);
            this.pictureBoxAlpha.TabIndex = 3;
            this.pictureBoxAlpha.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxAlpha, "Alpha Selector (Yellow = Colour1, Red = Colour2)");
            // 
            // buttonRainbow
            // 
            this.buttonRainbow.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.rainbow;
            this.buttonRainbow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRainbow.Location = new System.Drawing.Point(20, 423);
            this.buttonRainbow.Name = "buttonRainbow";
            this.buttonRainbow.Size = new System.Drawing.Size(32, 32);
            this.buttonRainbow.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonRainbow, "Rainbow!");
            this.buttonRainbow.UseVisualStyleBackColor = true;
            this.buttonRainbow.Click += new System.EventHandler(this.ButtonRainbowClick);
            // 
            // buttonSize1
            // 
            this.buttonSize1.BackColor = System.Drawing.Color.Black;
            this.buttonSize1.Location = new System.Drawing.Point(99, 517);
            this.buttonSize1.Name = "buttonSize1";
            this.buttonSize1.Size = new System.Drawing.Size(8, 8);
            this.buttonSize1.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonSize1, "1x1 Pixel");
            this.buttonSize1.UseVisualStyleBackColor = false;
            this.buttonSize1.Click += new System.EventHandler(this.ButtonBrushSizeClick);
            // 
            // buttonSize2
            // 
            this.buttonSize2.BackColor = System.Drawing.Color.Black;
            this.buttonSize2.Location = new System.Drawing.Point(113, 513);
            this.buttonSize2.Name = "buttonSize2";
            this.buttonSize2.Size = new System.Drawing.Size(12, 12);
            this.buttonSize2.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonSize2, "2x2 Pixel");
            this.buttonSize2.UseVisualStyleBackColor = false;
            this.buttonSize2.Click += new System.EventHandler(this.ButtonBrushSizeClick);
            // 
            // buttonSize4
            // 
            this.buttonSize4.BackColor = System.Drawing.Color.Black;
            this.buttonSize4.Location = new System.Drawing.Point(131, 505);
            this.buttonSize4.Name = "buttonSize4";
            this.buttonSize4.Size = new System.Drawing.Size(20, 20);
            this.buttonSize4.TabIndex = 12;
            this.toolTip1.SetToolTip(this.buttonSize4, "4x4 Pixel");
            this.buttonSize4.UseVisualStyleBackColor = false;
            this.buttonSize4.Click += new System.EventHandler(this.ButtonBrushSizeClick);
            // 
            // buttonTransparencyLock
            // 
            this.buttonTransparencyLock.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.lockoff;
            this.buttonTransparencyLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTransparencyLock.Location = new System.Drawing.Point(110, 423);
            this.buttonTransparencyLock.Name = "buttonTransparencyLock";
            this.buttonTransparencyLock.Size = new System.Drawing.Size(32, 32);
            this.buttonTransparencyLock.TabIndex = 13;
            this.toolTip1.SetToolTip(this.buttonTransparencyLock, "Lock the transparency");
            this.buttonTransparencyLock.UseVisualStyleBackColor = true;
            this.buttonTransparencyLock.Click += new System.EventHandler(this.ButtonLockTransparencyClick);
            // 
            // buttonShape
            // 
            this.buttonShape.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.shape;
            this.buttonShape.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonShape.Location = new System.Drawing.Point(65, 423);
            this.buttonShape.Name = "buttonShape";
            this.buttonShape.Size = new System.Drawing.Size(32, 32);
            this.buttonShape.TabIndex = 14;
            this.toolTip1.SetToolTip(this.buttonShape, "Shape");
            this.buttonShape.UseVisualStyleBackColor = true;
            this.buttonShape.Click += new System.EventHandler(this.ButtonShapeClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "Drawing Tools";
            // 
            // contextMenuStripShape
            // 
            this.contextMenuStripShape.Name = "contextMenuStripShape";
            this.contextMenuStripShape.Size = new System.Drawing.Size(61, 4);
            // 
            // DrawingToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.ClientSize = new System.Drawing.Size(264, 589);
            this.ControlBox = false;
            this.Controls.Add(this.buttonShape);
            this.Controls.Add(this.buttonTransparencyLock);
            this.Controls.Add(this.buttonSize4);
            this.Controls.Add(this.buttonSize2);
            this.Controls.Add(this.buttonSize1);
            this.Controls.Add(this.buttonRainbow);
            this.Controls.Add(this.buttonFloodFill);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBoxAlpha);
            this.Controls.Add(this.pictureBoxGamma);
            this.Controls.Add(this.panelColour2);
            this.Controls.Add(this.panelColour1);
            this.Controls.Add(this.buttonTexturiser);
            this.Controls.Add(this.buttonMirrorY);
            this.Controls.Add(this.buttonMirrorX);
            this.Controls.Add(this.buttonColourPicker);
            this.Controls.Add(this.buttonEraser);
            this.Controls.Add(this.buttonDraw);
            this.Controls.Add(this.pictureBoxColourPicker);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(294, 639);
            this.MinimizeBox = false;
            this.Name = "DrawingToolsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColourPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxColourPicker;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonEraser;
        private System.Windows.Forms.Panel panelColour1;
        private System.Windows.Forms.Panel panelColour2;
        private System.Windows.Forms.PictureBox pictureBoxGamma;
        private System.Windows.Forms.Button buttonColourPicker;
        private System.Windows.Forms.Button buttonMirrorX;
        private System.Windows.Forms.Button buttonMirrorY;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonTexturiser;
        private System.Windows.Forms.Button buttonFloodFill;
        private System.Windows.Forms.PictureBox pictureBoxAlpha;
        private System.Windows.Forms.Button buttonRainbow;
        private System.Windows.Forms.Button buttonSize1;
        private System.Windows.Forms.Button buttonSize2;
        private System.Windows.Forms.Button buttonSize4;
        private System.Windows.Forms.Button buttonTransparencyLock;
        private System.Windows.Forms.Button buttonShape;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripShape;
    }
}