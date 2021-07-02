
namespace MinecraftTextureEditorUI
{
    partial class DeploymentWizardForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.tabControlDeploy = new System.Windows.Forms.TabControl();
            this.tabPageDeploy1 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageDeploy2 = new System.Windows.Forms.TabPage();
            this.checkBoxUnpackZipFile = new System.Windows.Forms.CheckBox();
            this.textBoxPackName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMetaFile = new System.Windows.Forms.Label();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageDeploy3 = new System.Windows.Forms.TabPage();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progressBarDeploymentProgress = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControlDeploy.SuspendLayout();
            this.tabPageDeploy1.SuspendLayout();
            this.tabPageDeploy2.SuspendLayout();
            this.tabPageDeploy3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonFinish);
            this.panel1.Controls.Add(this.buttonNext);
            this.panel1.Controls.Add(this.buttonPrevious);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 398);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 52);
            this.panel1.TabIndex = 1;
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFinish.Enabled = false;
            this.buttonFinish.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFinish.Location = new System.Drawing.Point(505, 9);
            this.buttonFinish.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(120, 30);
            this.buttonFinish.TabIndex = 2;
            this.buttonFinish.Text = "&Finish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.ButtonFinishClick);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.Location = new System.Drawing.Point(375, 9);
            this.buttonNext.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(120, 30);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "&Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.ButtonNextClick);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevious.Enabled = false;
            this.buttonPrevious.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrevious.Location = new System.Drawing.Point(245, 9);
            this.buttonPrevious.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(120, 30);
            this.buttonPrevious.TabIndex = 0;
            this.buttonPrevious.Text = "&Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.ButtonPreviousClick);
            // 
            // tabControlDeploy
            // 
            this.tabControlDeploy.Controls.Add(this.tabPageDeploy1);
            this.tabControlDeploy.Controls.Add(this.tabPageDeploy2);
            this.tabControlDeploy.Controls.Add(this.tabPageDeploy3);
            this.tabControlDeploy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDeploy.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlDeploy.Location = new System.Drawing.Point(0, 0);
            this.tabControlDeploy.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabControlDeploy.Name = "tabControlDeploy";
            this.tabControlDeploy.SelectedIndex = 0;
            this.tabControlDeploy.Size = new System.Drawing.Size(639, 398);
            this.tabControlDeploy.TabIndex = 2;
            // 
            // tabPageDeploy1
            // 
            this.tabPageDeploy1.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageDeploy1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageDeploy1.Controls.Add(this.label6);
            this.tabPageDeploy1.Controls.Add(this.label5);
            this.tabPageDeploy1.Controls.Add(this.label4);
            this.tabPageDeploy1.Location = new System.Drawing.Point(4, 26);
            this.tabPageDeploy1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageDeploy1.Name = "tabPageDeploy1";
            this.tabPageDeploy1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageDeploy1.Size = new System.Drawing.Size(631, 368);
            this.tabPageDeploy1.TabIndex = 0;
            this.tabPageDeploy1.Text = "Step 1";
            this.tabPageDeploy1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(65, 196);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(500, 76);
            this.label6.TabIndex = 5;
            this.label6.Text = "To begin, click &Next to setup the pack details and create the zip file. Once com" +
    "plete you will then be asked to open Minecraft and test your new pack.";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(65, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(500, 79);
            this.label5.TabIndex = 4;
            this.label5.Text = "This deployment wizard allows you to take your changes and package them into a zi" +
    "p file that can then be deployed to your ResourcePack folder in Minecraft!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Texture Pack Deployment";
            // 
            // tabPageDeploy2
            // 
            this.tabPageDeploy2.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageDeploy2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageDeploy2.Controls.Add(this.checkBoxUnpackZipFile);
            this.tabPageDeploy2.Controls.Add(this.textBoxPackName);
            this.tabPageDeploy2.Controls.Add(this.textBoxDescription);
            this.tabPageDeploy2.Controls.Add(this.label2);
            this.tabPageDeploy2.Controls.Add(this.labelMetaFile);
            this.tabPageDeploy2.Controls.Add(this.comboBoxFormat);
            this.tabPageDeploy2.Controls.Add(this.label10);
            this.tabPageDeploy2.Controls.Add(this.label9);
            this.tabPageDeploy2.Controls.Add(this.label8);
            this.tabPageDeploy2.Controls.Add(this.label3);
            this.tabPageDeploy2.Controls.Add(this.label1);
            this.tabPageDeploy2.Location = new System.Drawing.Point(4, 26);
            this.tabPageDeploy2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageDeploy2.Name = "tabPageDeploy2";
            this.tabPageDeploy2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageDeploy2.Size = new System.Drawing.Size(631, 368);
            this.tabPageDeploy2.TabIndex = 1;
            this.tabPageDeploy2.Text = "Step 2";
            this.tabPageDeploy2.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnpackZipFile
            // 
            this.checkBoxUnpackZipFile.AutoSize = true;
            this.checkBoxUnpackZipFile.ForeColor = System.Drawing.Color.Yellow;
            this.checkBoxUnpackZipFile.Location = new System.Drawing.Point(158, 311);
            this.checkBoxUnpackZipFile.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.checkBoxUnpackZipFile.Name = "checkBoxUnpackZipFile";
            this.checkBoxUnpackZipFile.Size = new System.Drawing.Size(299, 21);
            this.checkBoxUnpackZipFile.TabIndex = 4;
            this.checkBoxUnpackZipFile.Text = "Unpack zip file to resource folder";
            this.checkBoxUnpackZipFile.UseVisualStyleBackColor = true;
            this.checkBoxUnpackZipFile.CheckedChanged += new System.EventHandler(this.CheckBoxCheckedChanged);
            // 
            // textBoxPackName
            // 
            this.textBoxPackName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPackName.Location = new System.Drawing.Point(158, 100);
            this.textBoxPackName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxPackName.Name = "textBoxPackName";
            this.textBoxPackName.Size = new System.Drawing.Size(437, 24);
            this.textBoxPackName.TabIndex = 0;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.Location = new System.Drawing.Point(158, 220);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(437, 51);
            this.textBoxDescription.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(33, 223);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // labelMetaFile
            // 
            this.labelMetaFile.AutoSize = true;
            this.labelMetaFile.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMetaFile.ForeColor = System.Drawing.Color.Yellow;
            this.labelMetaFile.Location = new System.Drawing.Point(8, 8);
            this.labelMetaFile.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelMetaFile.Name = "labelMetaFile";
            this.labelMetaFile.Size = new System.Drawing.Size(147, 20);
            this.labelMetaFile.TabIndex = 2;
            this.labelMetaFile.Text = "MetaFile Setup";
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(158, 153);
            this.comboBoxFormat.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(437, 25);
            this.comboBoxFormat.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(33, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Pack Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(33, 156);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Format:";
            // 
            // tabPageDeploy3
            // 
            this.tabPageDeploy3.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageDeploy3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageDeploy3.Controls.Add(this.labelProgress);
            this.tabPageDeploy3.Controls.Add(this.label7);
            this.tabPageDeploy3.Controls.Add(this.progressBarDeploymentProgress);
            this.tabPageDeploy3.Location = new System.Drawing.Point(4, 26);
            this.tabPageDeploy3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageDeploy3.Name = "tabPageDeploy3";
            this.tabPageDeploy3.Size = new System.Drawing.Size(631, 368);
            this.tabPageDeploy3.TabIndex = 2;
            this.tabPageDeploy3.Text = "Step 3";
            this.tabPageDeploy3.UseVisualStyleBackColor = true;
            // 
            // labelProgress
            // 
            this.labelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProgress.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress.ForeColor = System.Drawing.Color.Yellow;
            this.labelProgress.Location = new System.Drawing.Point(65, 134);
            this.labelProgress.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(500, 100);
            this.labelProgress.TabIndex = 5;
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(219, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Deployment Progress";
            // 
            // progressBarDeploymentProgress
            // 
            this.progressBarDeploymentProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarDeploymentProgress.Location = new System.Drawing.Point(68, 262);
            this.progressBarDeploymentProgress.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.progressBarDeploymentProgress.Name = "progressBarDeploymentProgress";
            this.progressBarDeploymentProgress.Size = new System.Drawing.Size(500, 35);
            this.progressBarDeploymentProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarDeploymentProgress.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Yellow;
            this.label8.Location = new System.Drawing.Point(155, 125);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(321, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "What do you want to call your new pack?";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Yellow;
            this.label9.Location = new System.Drawing.Point(155, 182);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(279, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Which format does your pack use?";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Yellow;
            this.label10.Location = new System.Drawing.Point(155, 275);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(312, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "The description to display in Minecraft";
            // 
            // DeploymentWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 450);
            this.Controls.Add(this.tabControlDeploy);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "DeploymentWizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.tabControlDeploy.ResumeLayout(false);
            this.tabPageDeploy1.ResumeLayout(false);
            this.tabPageDeploy1.PerformLayout();
            this.tabPageDeploy2.ResumeLayout(false);
            this.tabPageDeploy2.PerformLayout();
            this.tabPageDeploy3.ResumeLayout(false);
            this.tabPageDeploy3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.TabControl tabControlDeploy;
        private System.Windows.Forms.TabPage tabPageDeploy1;
        private System.Windows.Forms.TabPage tabPageDeploy2;
        private System.Windows.Forms.TabPage tabPageDeploy3;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelMetaFile;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPackName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progressBarDeploymentProgress;
        private System.Windows.Forms.CheckBox checkBoxUnpackZipFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}