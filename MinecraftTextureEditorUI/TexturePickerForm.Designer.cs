﻿
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    partial class TexturePickerForm
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
            this.listViewTextureList = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.flowLayoutPanelTextures = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewTextureList
            // 
            this.listViewTextureList.Font = new System.Drawing.Font("Minecraft", 8F);
            this.listViewTextureList.HideSelection = false;
            this.listViewTextureList.Location = new System.Drawing.Point(0, 585);
            this.listViewTextureList.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.listViewTextureList.Name = "listViewTextureList";
            this.listViewTextureList.OwnerDraw = true;
            this.listViewTextureList.Size = new System.Drawing.Size(337, 48);
            this.listViewTextureList.TabIndex = 1;
            this.listViewTextureList.UseCompatibleStateImageBehavior = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::MinecraftTextureEditorUI.Properties.Resources.texturewallpaper;
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Controls.Add(this.textBoxFilter);
            this.panel1.Controls.Add(this.buttonRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(337, 56);
            this.panel1.TabIndex = 3;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Minecraft", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Yellow;
            this.labelTitle.Location = new System.Drawing.Point(8, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(155, 20);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Texture Picker";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilter.Location = new System.Drawing.Point(0, 32);
            this.textBoxFilter.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(218, 24);
            this.textBoxFilter.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxFilter, "Type in your text and press enter to filter the textures!");
            this.textBoxFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxFilterKeyDown);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Location = new System.Drawing.Point(217, 27);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 30);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "&Refresh";
            this.toolTip1.SetToolTip(this.buttonRefresh, "Click here to refresh your textures");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.ButtonRefreshClick);
            // 
            // flowLayoutPanelTextures
            // 
            this.flowLayoutPanelTextures.AutoScroll = true;
            this.flowLayoutPanelTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelTextures.Location = new System.Drawing.Point(0, 56);
            this.flowLayoutPanelTextures.Name = "flowLayoutPanelTextures";
            this.flowLayoutPanelTextures.Size = new System.Drawing.Size(337, 577);
            this.flowLayoutPanelTextures.TabIndex = 4;
            this.toolTip1.SetToolTip(this.flowLayoutPanelTextures, "Your textures :)");
            // 
            // toolTip1
            // 
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.Draw += new DrawToolTipEventHandler(this.ToolTipDraw);
            // 
            // TexturePickerForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 633);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanelTextures);
            this.Controls.Add(this.listViewTextureList);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Minecraft", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "TexturePickerForm";
            this.Text = " ";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewTextureList;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTextures;
        private ToolTip toolTip1;
    }
}