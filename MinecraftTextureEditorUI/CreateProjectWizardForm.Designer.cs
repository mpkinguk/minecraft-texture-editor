
namespace MinecraftTextureEditorUI
{
    partial class CreateProjectWizardForm
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
            this.tabPageCreate1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageCreate2 = new System.Windows.Forms.TabPage();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMetaFile = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageCreate3 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxPackName = new System.Windows.Forms.TextBox();
            this.labelPackName = new System.Windows.Forms.Label();
            this.textBoxProjectPath = new System.Windows.Forms.TextBox();
            this.buttonProjectPathBrowser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPageCreate4 = new System.Windows.Forms.TabPage();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControlDeploy.SuspendLayout();
            this.tabPageCreate1.SuspendLayout();
            this.tabPageCreate2.SuspendLayout();
            this.tabPageCreate3.SuspendLayout();
            this.tabPageCreate4.SuspendLayout();
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
            this.tabControlDeploy.Controls.Add(this.tabPageCreate1);
            this.tabControlDeploy.Controls.Add(this.tabPageCreate2);
            this.tabControlDeploy.Controls.Add(this.tabPageCreate3);
            this.tabControlDeploy.Controls.Add(this.tabPageCreate4);
            this.tabControlDeploy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDeploy.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlDeploy.Location = new System.Drawing.Point(0, 0);
            this.tabControlDeploy.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabControlDeploy.Name = "tabControlDeploy";
            this.tabControlDeploy.SelectedIndex = 0;
            this.tabControlDeploy.Size = new System.Drawing.Size(639, 398);
            this.tabControlDeploy.TabIndex = 2;
            // 
            // tabPageCreate1
            // 
            this.tabPageCreate1.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageCreate1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageCreate1.Controls.Add(this.label8);
            this.tabPageCreate1.Controls.Add(this.label6);
            this.tabPageCreate1.Controls.Add(this.label5);
            this.tabPageCreate1.Controls.Add(this.label4);
            this.tabPageCreate1.Location = new System.Drawing.Point(4, 26);
            this.tabPageCreate1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageCreate1.Name = "tabPageCreate1";
            this.tabPageCreate1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageCreate1.Size = new System.Drawing.Size(631, 368);
            this.tabPageCreate1.TabIndex = 0;
            this.tabPageCreate1.Text = "Step 1";
            this.tabPageCreate1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Yellow;
            this.label8.Location = new System.Drawing.Point(65, 231);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(500, 62);
            this.label8.TabIndex = 5;
            this.label8.Text = "You can set the default path to the new one in the options panel, so the applicat" +
    "ion knows to select this next time you open it!";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(65, 152);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(500, 50);
            this.label6.TabIndex = 5;
            this.label6.Text = "To begin, click &Next to select the assets you wish to copy and change. ";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(65, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(500, 50);
            this.label5.TabIndex = 4;
            this.label5.Text = "This wizard allows you to create a new project for use in this editor.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(8, 8);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(295, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Create Texture Pack Project";
            // 
            // tabPageCreate2
            // 
            this.tabPageCreate2.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageCreate2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageCreate2.Controls.Add(this.comboBoxVersion);
            this.tabPageCreate2.Controls.Add(this.label2);
            this.tabPageCreate2.Controls.Add(this.labelMetaFile);
            this.tabPageCreate2.Controls.Add(this.label3);
            this.tabPageCreate2.ForeColor = System.Drawing.Color.Yellow;
            this.tabPageCreate2.Location = new System.Drawing.Point(4, 26);
            this.tabPageCreate2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageCreate2.Name = "tabPageCreate2";
            this.tabPageCreate2.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageCreate2.Size = new System.Drawing.Size(631, 368);
            this.tabPageCreate2.TabIndex = 1;
            this.tabPageCreate2.Text = "Step 2";
            this.tabPageCreate2.UseVisualStyleBackColor = true;
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(158, 129);
            this.comboBoxVersion.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(437, 25);
            this.comboBoxVersion.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Minecraft", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(155, 158);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(433, 52);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select which version you wish to base your new project on!";
            // 
            // labelMetaFile
            // 
            this.labelMetaFile.AutoSize = true;
            this.labelMetaFile.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMetaFile.ForeColor = System.Drawing.Color.Yellow;
            this.labelMetaFile.Location = new System.Drawing.Point(8, 8);
            this.labelMetaFile.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelMetaFile.Name = "labelMetaFile";
            this.labelMetaFile.Size = new System.Drawing.Size(209, 20);
            this.labelMetaFile.TabIndex = 2;
            this.labelMetaFile.Text = "Current Pack details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(33, 129);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Version:";
            // 
            // tabPageCreate3
            // 
            this.tabPageCreate3.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageCreate3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageCreate3.Controls.Add(this.label10);
            this.tabPageCreate3.Controls.Add(this.textBoxPackName);
            this.tabPageCreate3.Controls.Add(this.labelPackName);
            this.tabPageCreate3.Controls.Add(this.textBoxProjectPath);
            this.tabPageCreate3.Controls.Add(this.buttonProjectPathBrowser);
            this.tabPageCreate3.Controls.Add(this.label1);
            this.tabPageCreate3.Controls.Add(this.label9);
            this.tabPageCreate3.Location = new System.Drawing.Point(4, 26);
            this.tabPageCreate3.Name = "tabPageCreate3";
            this.tabPageCreate3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCreate3.Size = new System.Drawing.Size(631, 368);
            this.tabPageCreate3.TabIndex = 3;
            this.tabPageCreate3.Text = "Step 3";
            this.tabPageCreate3.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Yellow;
            this.label10.Location = new System.Drawing.Point(8, 8);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "New Pack details";
            // 
            // textBoxPackName
            // 
            this.textBoxPackName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPackName.Location = new System.Drawing.Point(157, 129);
            this.textBoxPackName.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxPackName.Name = "textBoxPackName";
            this.textBoxPackName.Size = new System.Drawing.Size(438, 24);
            this.textBoxPackName.TabIndex = 13;
            // 
            // labelPackName
            // 
            this.labelPackName.AutoSize = true;
            this.labelPackName.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPackName.ForeColor = System.Drawing.Color.Yellow;
            this.labelPackName.Location = new System.Drawing.Point(33, 129);
            this.labelPackName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelPackName.Name = "labelPackName";
            this.labelPackName.Size = new System.Drawing.Size(92, 17);
            this.labelPackName.TabIndex = 12;
            this.labelPackName.Text = "Pack Name:";
            // 
            // textBoxProjectPath
            // 
            this.textBoxProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProjectPath.Location = new System.Drawing.Point(158, 197);
            this.textBoxProjectPath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxProjectPath.Name = "textBoxProjectPath";
            this.textBoxProjectPath.Size = new System.Drawing.Size(384, 24);
            this.textBoxProjectPath.TabIndex = 8;
            // 
            // buttonProjectPathBrowser
            // 
            this.buttonProjectPathBrowser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonProjectPathBrowser.Location = new System.Drawing.Point(560, 191);
            this.buttonProjectPathBrowser.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonProjectPathBrowser.Name = "buttonProjectPathBrowser";
            this.buttonProjectPathBrowser.Size = new System.Drawing.Size(35, 35);
            this.buttonProjectPathBrowser.TabIndex = 10;
            this.buttonProjectPathBrowser.Text = "...";
            this.buttonProjectPathBrowser.UseVisualStyleBackColor = true;
            this.buttonProjectPathBrowser.Click += new System.EventHandler(this.ButtonProjectPathBrowserClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(33, 200);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Project Path:";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Minecraft", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.Yellow;
            this.label9.Location = new System.Drawing.Point(154, 230);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(441, 45);
            this.label9.TabIndex = 11;
            this.label9.Text = "The project path is where the your new project will be edited. ";
            // 
            // tabPageCreate4
            // 
            this.tabPageCreate4.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.tabPageCreate4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tabPageCreate4.Controls.Add(this.labelProgress);
            this.tabPageCreate4.Controls.Add(this.label7);
            this.tabPageCreate4.Location = new System.Drawing.Point(4, 26);
            this.tabPageCreate4.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.tabPageCreate4.Name = "tabPageCreate4";
            this.tabPageCreate4.Size = new System.Drawing.Size(631, 368);
            this.tabPageCreate4.TabIndex = 2;
            this.tabPageCreate4.Text = "Step 4";
            this.tabPageCreate4.UseVisualStyleBackColor = true;
            // 
            // labelProgress
            // 
            this.labelProgress.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress.ForeColor = System.Drawing.Color.Yellow;
            this.labelProgress.Location = new System.Drawing.Point(65, 134);
            this.labelProgress.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(500, 100);
            this.labelProgress.TabIndex = 5;
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(8, 8);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(277, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Project Creation Progress";
            // 
            // CreateProjectWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 450);
            this.Controls.Add(this.tabControlDeploy);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "CreateProjectWizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.tabControlDeploy.ResumeLayout(false);
            this.tabPageCreate1.ResumeLayout(false);
            this.tabPageCreate1.PerformLayout();
            this.tabPageCreate2.ResumeLayout(false);
            this.tabPageCreate2.PerformLayout();
            this.tabPageCreate3.ResumeLayout(false);
            this.tabPageCreate3.PerformLayout();
            this.tabPageCreate4.ResumeLayout(false);
            this.tabPageCreate4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.TabControl tabControlDeploy;
        private System.Windows.Forms.TabPage tabPageCreate1;
        private System.Windows.Forms.TabPage tabPageCreate2;
        private System.Windows.Forms.TabPage tabPageCreate4;
        private System.Windows.Forms.Label labelMetaFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxVersion;
        private System.Windows.Forms.TabPage tabPageCreate3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxPackName;
        private System.Windows.Forms.Label labelPackName;
        private System.Windows.Forms.TextBox textBoxProjectPath;
        private System.Windows.Forms.Button buttonProjectPathBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
    }
}