
namespace MinecraftTextureEditorUI
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.okButton = new System.Windows.Forms.Button();
            this.labelProductName = new System.Windows.Forms.PictureBox();
            this.labelVersion = new System.Windows.Forms.PictureBox();
            this.labelCopyright = new System.Windows.Forms.PictureBox();
            this.labelCompanyName = new System.Windows.Forms.PictureBox();
            this.labelDescription = new System.Windows.Forms.PictureBox();
            this.labelAbout = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.labelProductName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelCopyright)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelCompanyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(580, 400);
            this.okButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(125, 30);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            // 
            // labelProductName
            // 
            this.labelProductName.BackColor = System.Drawing.Color.Transparent;
            this.labelProductName.ForeColor = System.Drawing.Color.Yellow;
            this.labelProductName.Location = new System.Drawing.Point(318, 31);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(387, 27);
            this.labelProductName.TabIndex = 25;
            this.labelProductName.TabStop = false;
            this.labelProductName.Text = "label1";
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.ForeColor = System.Drawing.Color.Yellow;
            this.labelVersion.Location = new System.Drawing.Point(318, 79);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(387, 27);
            this.labelVersion.TabIndex = 25;
            this.labelVersion.TabStop = false;
            this.labelVersion.Text = "label1";
            // 
            // labelCopyright
            // 
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyright.ForeColor = System.Drawing.Color.Yellow;
            this.labelCopyright.Location = new System.Drawing.Point(318, 127);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(387, 27);
            this.labelCopyright.TabIndex = 25;
            this.labelCopyright.TabStop = false;
            this.labelCopyright.Text = "label1";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.labelCompanyName.ForeColor = System.Drawing.Color.Yellow;
            this.labelCompanyName.Location = new System.Drawing.Point(318, 175);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(387, 27);
            this.labelCompanyName.TabIndex = 25;
            this.labelCompanyName.TabStop = false;
            this.labelCompanyName.Text = "label1";
            // 
            // labelDescription
            // 
            this.labelDescription.BackColor = System.Drawing.Color.Transparent;
            this.labelDescription.ForeColor = System.Drawing.Color.Yellow;
            this.labelDescription.Location = new System.Drawing.Point(315, 219);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(390, 154);
            this.labelDescription.TabIndex = 25;
            this.labelDescription.TabStop = false;
            this.labelDescription.Text = "label1";
            // 
            // labelAbout
            // 
            this.labelAbout.BackColor = System.Drawing.Color.Transparent;
            this.labelAbout.Font = new System.Drawing.Font("Minecraft", 12F);
            this.labelAbout.ForeColor = System.Drawing.Color.Yellow;
            this.labelAbout.Location = new System.Drawing.Point(7, 12);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(184, 27);
            this.labelAbout.TabIndex = 25;
            this.labelAbout.TabStop = false;
            this.labelAbout.Text = "About";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.steve;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(725, 446);
            this.ControlBox = false;
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelCompanyName);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.labelProductName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelCopyright)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelCompanyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labelAbout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox labelProductName;
        private System.Windows.Forms.PictureBox labelVersion;
        private System.Windows.Forms.PictureBox labelCopyright;
        private System.Windows.Forms.PictureBox labelCompanyName;
        private System.Windows.Forms.PictureBox labelDescription;
        private System.Windows.Forms.PictureBox labelAbout;
    }
}
