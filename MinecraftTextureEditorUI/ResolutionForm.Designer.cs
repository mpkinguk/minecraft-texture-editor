
namespace MinecraftTextureEditorUI
{
    partial class ResolutionForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelImageResolution = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.comboBoxWidth = new System.Windows.Forms.ComboBox();
            this.comboBoxHeight = new System.Windows.Forms.ComboBox();
            this.checkBoxSquareImage = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(315, 198);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(152, 35);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // labelImageResolution
            // 
            this.labelImageResolution.AutoSize = true;
            this.labelImageResolution.BackColor = System.Drawing.Color.Transparent;
            this.labelImageResolution.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImageResolution.ForeColor = System.Drawing.Color.Yellow;
            this.labelImageResolution.Location = new System.Drawing.Point(8, 8);
            this.labelImageResolution.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelImageResolution.Name = "labelImageResolution";
            this.labelImageResolution.Size = new System.Drawing.Size(171, 20);
            this.labelImageResolution.TabIndex = 5;
            this.labelImageResolution.Text = "Image Resolution";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.BackColor = System.Drawing.Color.Transparent;
            this.labelWidth.Font = new System.Drawing.Font("Minecraft", 9.75F);
            this.labelWidth.ForeColor = System.Drawing.Color.Yellow;
            this.labelWidth.Location = new System.Drawing.Point(58, 100);
            this.labelWidth.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(51, 17);
            this.labelWidth.TabIndex = 6;
            this.labelWidth.Text = "Width:";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.BackColor = System.Drawing.Color.Transparent;
            this.labelHeight.Font = new System.Drawing.Font("Minecraft", 9.75F);
            this.labelHeight.ForeColor = System.Drawing.Color.Yellow;
            this.labelHeight.Location = new System.Drawing.Point(226, 100);
            this.labelHeight.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(61, 17);
            this.labelHeight.TabIndex = 6;
            this.labelHeight.Text = "Height:";
            // 
            // comboBoxWidth
            // 
            this.comboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWidth.FormattingEnabled = true;
            this.comboBoxWidth.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.comboBoxWidth.Location = new System.Drawing.Point(127, 100);
            this.comboBoxWidth.Name = "comboBoxWidth";
            this.comboBoxWidth.Size = new System.Drawing.Size(90, 25);
            this.comboBoxWidth.TabIndex = 7;
            // 
            // comboBoxHeight
            // 
            this.comboBoxHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHeight.FormattingEnabled = true;
            this.comboBoxHeight.Items.AddRange(new object[] {
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024",
            "2048"});
            this.comboBoxHeight.Location = new System.Drawing.Point(307, 100);
            this.comboBoxHeight.Name = "comboBoxHeight";
            this.comboBoxHeight.Size = new System.Drawing.Size(90, 25);
            this.comboBoxHeight.TabIndex = 7;
            // 
            // checkBoxSquareImage
            // 
            this.checkBoxSquareImage.AutoSize = true;
            this.checkBoxSquareImage.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxSquareImage.ForeColor = System.Drawing.Color.Yellow;
            this.checkBoxSquareImage.Location = new System.Drawing.Point(127, 154);
            this.checkBoxSquareImage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.checkBoxSquareImage.Name = "checkBoxSquareImage";
            this.checkBoxSquareImage.Size = new System.Drawing.Size(148, 22);
            this.checkBoxSquareImage.TabIndex = 8;
            this.checkBoxSquareImage.Text = "Square Image";
            this.checkBoxSquareImage.UseVisualStyleBackColor = false;
            this.checkBoxSquareImage.CheckedChanged += new System.EventHandler(this.CheckBoxSquareImageCheckedChanged);
            // 
            // ResolutionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.ClientSize = new System.Drawing.Size(481, 246);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxSquareImage);
            this.Controls.Add(this.comboBoxHeight);
            this.Controls.Add(this.comboBoxWidth);
            this.Controls.Add(this.labelHeight);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelImageResolution);
            this.Controls.Add(this.buttonOK);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ResolutionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelImageResolution;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.ComboBox comboBoxWidth;
        private System.Windows.Forms.ComboBox comboBoxHeight;
        private System.Windows.Forms.CheckBox checkBoxSquareImage;
    }
}