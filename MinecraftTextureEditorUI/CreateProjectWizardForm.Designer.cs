
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
            this.tabPageDeploy1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageDeploy2 = new System.Windows.Forms.TabPage();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonProjectPathBrowser = new System.Windows.Forms.Button();
            this.textBoxProjectPath = new System.Windows.Forms.TextBox();
            this.labelMetaFile = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageDeploy3 = new System.Windows.Forms.TabPage();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
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
            this.panel1.Location = new System.Drawing.Point(0, 410);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 40);
            this.panel1.TabIndex = 1;
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFinish.Enabled = false;
            this.buttonFinish.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFinish.Location = new System.Drawing.Point(556, 6);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(91, 27);
            this.buttonFinish.TabIndex = 2;
            this.buttonFinish.Text = "&Finish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.ButtonFinishClick);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.Location = new System.Drawing.Point(459, 6);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(91, 27);
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
            this.buttonPrevious.Location = new System.Drawing.Point(362, 6);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(91, 27);
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
            this.tabControlDeploy.Name = "tabControlDeploy";
            this.tabControlDeploy.SelectedIndex = 0;
            this.tabControlDeploy.Size = new System.Drawing.Size(659, 410);
            this.tabControlDeploy.TabIndex = 2;
            // 
            // tabPageDeploy1
            // 
            this.tabPageDeploy1.Controls.Add(this.label8);
            this.tabPageDeploy1.Controls.Add(this.label6);
            this.tabPageDeploy1.Controls.Add(this.label5);
            this.tabPageDeploy1.Controls.Add(this.label4);
            this.tabPageDeploy1.Location = new System.Drawing.Point(4, 26);
            this.tabPageDeploy1.Name = "tabPageDeploy1";
            this.tabPageDeploy1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDeploy1.Size = new System.Drawing.Size(651, 380);
            this.tabPageDeploy1.TabIndex = 0;
            this.tabPageDeploy1.Text = "Step 1";
            this.tabPageDeploy1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label8.Location = new System.Drawing.Point(119, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(373, 102);
            this.label8.TabIndex = 5;
            this.label8.Text = "You can set the default path to the new one in the options panel, so the applicat" +
    "ion knows to select this next time you open it!";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(119, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(373, 53);
            this.label6.TabIndex = 5;
            this.label6.Text = "To begin, click &Next to select the assets you wish to copy and change. ";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(119, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(373, 52);
            this.label5.TabIndex = 4;
            this.label5.Text = "This wizard allows you to create a new project for use in this editor.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(18, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(295, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Create Texture Pack Project";
            // 
            // tabPageDeploy2
            // 
            this.tabPageDeploy2.Controls.Add(this.comboBoxVersion);
            this.tabPageDeploy2.Controls.Add(this.label9);
            this.tabPageDeploy2.Controls.Add(this.label2);
            this.tabPageDeploy2.Controls.Add(this.buttonProjectPathBrowser);
            this.tabPageDeploy2.Controls.Add(this.textBoxProjectPath);
            this.tabPageDeploy2.Controls.Add(this.labelMetaFile);
            this.tabPageDeploy2.Controls.Add(this.label3);
            this.tabPageDeploy2.Controls.Add(this.label1);
            this.tabPageDeploy2.Location = new System.Drawing.Point(4, 26);
            this.tabPageDeploy2.Name = "tabPageDeploy2";
            this.tabPageDeploy2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDeploy2.Size = new System.Drawing.Size(651, 380);
            this.tabPageDeploy2.TabIndex = 1;
            this.tabPageDeploy2.Text = "Step 2";
            this.tabPageDeploy2.UseVisualStyleBackColor = true;
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(144, 60);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(490, 25);
            this.comboBoxVersion.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(140, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(461, 87);
            this.label9.TabIndex = 7;
            this.label9.Text = "The project path is where the your new project will be edited. This usually would" +
    " be in the %appdata%.minecraft folder under a separate sub-folder!";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Minecraft", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(140, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(461, 65);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select which version you wish to base your new project on!";
            // 
            // buttonProjectPathBrowser
            // 
            this.buttonProjectPathBrowser.Location = new System.Drawing.Point(607, 176);
            this.buttonProjectPathBrowser.Name = "buttonProjectPathBrowser";
            this.buttonProjectPathBrowser.Size = new System.Drawing.Size(27, 27);
            this.buttonProjectPathBrowser.TabIndex = 5;
            this.buttonProjectPathBrowser.Text = "...";
            this.buttonProjectPathBrowser.UseVisualStyleBackColor = true;
            this.buttonProjectPathBrowser.Click += new System.EventHandler(this.ButtonProjectPathBrowserClick);
            // 
            // textBoxProjectPath
            // 
            this.textBoxProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProjectPath.Location = new System.Drawing.Point(144, 178);
            this.textBoxProjectPath.Name = "textBoxProjectPath";
            this.textBoxProjectPath.Size = new System.Drawing.Size(457, 24);
            this.textBoxProjectPath.TabIndex = 0;
            // 
            // labelMetaFile
            // 
            this.labelMetaFile.AutoSize = true;
            this.labelMetaFile.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMetaFile.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelMetaFile.Location = new System.Drawing.Point(18, 18);
            this.labelMetaFile.Name = "labelMetaFile";
            this.labelMetaFile.Size = new System.Drawing.Size(209, 20);
            this.labelMetaFile.TabIndex = 2;
            this.labelMetaFile.Text = "Current Pack details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(20, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(20, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Project Path:";
            // 
            // tabPageDeploy3
            // 
            this.tabPageDeploy3.Controls.Add(this.labelProgress);
            this.tabPageDeploy3.Controls.Add(this.label7);
            this.tabPageDeploy3.Location = new System.Drawing.Point(4, 26);
            this.tabPageDeploy3.Name = "tabPageDeploy3";
            this.tabPageDeploy3.Size = new System.Drawing.Size(651, 380);
            this.tabPageDeploy3.TabIndex = 2;
            this.tabPageDeploy3.Text = "Step 3";
            this.tabPageDeploy3.UseVisualStyleBackColor = true;
            // 
            // labelProgress
            // 
            this.labelProgress.Font = new System.Drawing.Font("Minecraft", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelProgress.Location = new System.Drawing.Point(41, 97);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(573, 72);
            this.labelProgress.TabIndex = 5;
            this.labelProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(18, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(277, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Project Creation Progress";
            // 
            // CreateProjectWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 450);
            this.Controls.Add(this.tabControlDeploy);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateProjectWizardForm";
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
        private System.Windows.Forms.Label labelMetaFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonProjectPathBrowser;
        private System.Windows.Forms.TextBox textBoxProjectPath;
        private System.Windows.Forms.ComboBox comboBoxVersion;
    }
}