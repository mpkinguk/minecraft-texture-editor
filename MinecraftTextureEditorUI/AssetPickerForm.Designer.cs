
namespace MinecraftTextureEditorUI
{
    partial class AssetPickerForm
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelImageResolution = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.comboBoxAsset = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(412, 198);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 35);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonClick);
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
            this.labelImageResolution.Size = new System.Drawing.Size(131, 20);
            this.labelImageResolution.TabIndex = 5;
            this.labelImageResolution.Text = "Asset Picker";
            // 
            // labelWidth
            // 
            this.labelWidth.BackColor = System.Drawing.Color.Transparent;
            this.labelWidth.Font = new System.Drawing.Font("Minecraft", 9.75F);
            this.labelWidth.ForeColor = System.Drawing.Color.Yellow;
            this.labelWidth.Location = new System.Drawing.Point(14, 100);
            this.labelWidth.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(480, 41);
            this.labelWidth.TabIndex = 6;
            this.labelWidth.Text = "You have multiple asset folders within your chosen folder. Please chose one from " +
    "the dropdown list below:";
            // 
            // comboBoxAsset
            // 
            this.comboBoxAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAsset.FormattingEnabled = true;
            this.comboBoxAsset.Location = new System.Drawing.Point(12, 157);
            this.comboBoxAsset.Name = "comboBoxAsset";
            this.comboBoxAsset.Size = new System.Drawing.Size(480, 25);
            this.comboBoxAsset.TabIndex = 7;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(322, 198);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(80, 35);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonClick);
            // 
            // AssetPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.ClientSize = new System.Drawing.Size(506, 246);
            this.ControlBox = false;
            this.Controls.Add(this.comboBoxAsset);
            this.Controls.Add(this.labelWidth);
            this.Controls.Add(this.labelImageResolution);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AssetPickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelImageResolution;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.ComboBox comboBoxAsset;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}