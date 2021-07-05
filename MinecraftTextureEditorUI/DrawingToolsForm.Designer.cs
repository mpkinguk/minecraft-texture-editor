
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
            this.buttonPen = new System.Windows.Forms.Button();
            this.buttonEraser = new System.Windows.Forms.Button();
            this.panelColour1 = new System.Windows.Forms.Panel();
            this.panelColour2 = new System.Windows.Forms.Panel();
            this.buttonDropper = new System.Windows.Forms.Button();
            this.buttonMirrorX = new System.Windows.Forms.Button();
            this.buttonMirrorY = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxGamma = new System.Windows.Forms.PictureBox();
            this.pictureBoxColourPicker = new System.Windows.Forms.PictureBox();
            this.buttonTexturiser = new System.Windows.Forms.Button();
            this.buttonFloodFill = new System.Windows.Forms.Button();
            this.pictureBoxAlpha = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonRainbow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColourPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonPen
            // 
            this.buttonPen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPen.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonPen.Location = new System.Drawing.Point(20, 384);
            this.buttonPen.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonPen.Name = "buttonPen";
            this.buttonPen.Size = new System.Drawing.Size(32, 32);
            this.buttonPen.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonPen, "Draw");
            this.buttonPen.UseVisualStyleBackColor = true;
            this.buttonPen.Click += new System.EventHandler(this.ButtonPenClick);
            // 
            // buttonEraser
            // 
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
            this.panelColour1.Location = new System.Drawing.Point(20, 472);
            this.panelColour1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelColour1.Name = "panelColour1";
            this.panelColour1.Size = new System.Drawing.Size(64, 64);
            this.panelColour1.TabIndex = 5;
            this.toolTip1.SetToolTip(this.panelColour1, "Colour 1");
            // 
            // panelColour2
            // 
            this.panelColour2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColour2.Location = new System.Drawing.Point(168, 472);
            this.panelColour2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelColour2.Name = "panelColour2";
            this.panelColour2.Size = new System.Drawing.Size(64, 64);
            this.panelColour2.TabIndex = 6;
            this.toolTip1.SetToolTip(this.panelColour2, "Colour 2");
            // 
            // buttonDropper
            // 
            this.buttonDropper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDropper.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonDropper.Location = new System.Drawing.Point(110, 384);
            this.buttonDropper.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonDropper.Name = "buttonDropper";
            this.buttonDropper.Size = new System.Drawing.Size(32, 32);
            this.buttonDropper.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonDropper, "Colour Picker");
            this.buttonDropper.UseVisualStyleBackColor = true;
            this.buttonDropper.Click += new System.EventHandler(this.ButtonDropperClick);
            // 
            // buttonMirrorX
            // 
            this.buttonMirrorX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMirrorX.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonMirrorX.Location = new System.Drawing.Point(155, 423);
            this.buttonMirrorX.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonMirrorX.Name = "buttonMirrorX";
            this.buttonMirrorX.Size = new System.Drawing.Size(32, 32);
            this.buttonMirrorX.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonMirrorX, "Mirror X");
            this.buttonMirrorX.UseVisualStyleBackColor = true;
            this.buttonMirrorX.Click += new System.EventHandler(this.ButtonMirrorXClick);
            // 
            // buttonMirrorY
            // 
            this.buttonMirrorY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMirrorY.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonMirrorY.Location = new System.Drawing.Point(200, 423);
            this.buttonMirrorY.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonMirrorY.Name = "buttonMirrorY";
            this.buttonMirrorY.Size = new System.Drawing.Size(32, 32);
            this.buttonMirrorY.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonMirrorY, "Mirror Y");
            this.buttonMirrorY.UseVisualStyleBackColor = true;
            this.buttonMirrorY.Click += new System.EventHandler(this.ButtonMirrorYClick);
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
            this.buttonTexturiser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTexturiser.Font = new System.Drawing.Font("Minecraft", 10F);
            this.buttonTexturiser.Location = new System.Drawing.Point(155, 384);
            this.buttonTexturiser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonTexturiser.Name = "buttonTexturiser";
            this.buttonTexturiser.Size = new System.Drawing.Size(32, 32);
            this.buttonTexturiser.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonTexturiser, "Texturiser");
            this.buttonTexturiser.UseVisualStyleBackColor = true;
            this.buttonTexturiser.Click += new System.EventHandler(this.ButtonTexturiserClick);
            // 
            // buttonFloodFill
            // 
            this.buttonFloodFill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFloodFill.Location = new System.Drawing.Point(200, 384);
            this.buttonFloodFill.Name = "buttonFloodFill";
            this.buttonFloodFill.Size = new System.Drawing.Size(32, 32);
            this.buttonFloodFill.TabIndex = 7;
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
            this.toolTip1.SetToolTip(this.pictureBoxAlpha, "Alpha");
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
            // buttonRainbow
            // 
            this.buttonRainbow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRainbow.Location = new System.Drawing.Point(20, 423);
            this.buttonRainbow.Name = "buttonRainbow";
            this.buttonRainbow.Size = new System.Drawing.Size(32, 32);
            this.buttonRainbow.TabIndex = 8;
            this.buttonRainbow.UseVisualStyleBackColor = true;
            this.buttonRainbow.Click += new System.EventHandler(this.buttonRainbow_Click);
            // 
            // DrawingToolsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.ClientSize = new System.Drawing.Size(251, 549);
            this.ControlBox = false;
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
            this.Controls.Add(this.buttonDropper);
            this.Controls.Add(this.buttonEraser);
            this.Controls.Add(this.buttonPen);
            this.Controls.Add(this.pictureBoxColourPicker);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "DrawingToolsForm";
            this.Text = " ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColourPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxColourPicker;
        private System.Windows.Forms.Button buttonPen;
        private System.Windows.Forms.Button buttonEraser;
        private System.Windows.Forms.Panel panelColour1;
        private System.Windows.Forms.Panel panelColour2;
        private System.Windows.Forms.PictureBox pictureBoxGamma;
        private System.Windows.Forms.Button buttonDropper;
        private System.Windows.Forms.Button buttonMirrorX;
        private System.Windows.Forms.Button buttonMirrorY;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonTexturiser;
        private System.Windows.Forms.Button buttonFloodFill;
        private System.Windows.Forms.PictureBox pictureBoxAlpha;
        private System.Windows.Forms.Button buttonRainbow;
    }
}