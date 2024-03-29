﻿using log4net;
using MinecraftTextureEditorAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class AssetPickerForm : Form
    {
        #region Public properties

        /// <summary>
        /// The asset list
        /// </summary>
        public string Asset { get; set; }

        #endregion Public properties

        #region Private properties

        private readonly ILog _log;

        #endregion Private properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log"></param>
        public AssetPickerForm(ILog log, List<string> assets)
        {
            _log = log;

            try
            {
                InitializeComponent();

                comboBoxAsset.SelectedIndexChanged += ComboBoxAssetSelectedIndexChanged;

                comboBoxAsset.Items.AddRange(assets.ToArray());

                comboBoxAsset.SelectedIndex = 0;

                if (Constants.LessLag)
                {
                    BackgroundImage = null;
                    BackColor = Color.DimGray;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Private form events

        /// <summary>
        /// Capture the OK button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;

                switch (button.Name)
                {
                    case (nameof(buttonOK)):
                        Asset = comboBoxAsset.Text;
                        DialogResult = DialogResult.OK;
                        break;

                    case (nameof(buttonCancel)):
                        DialogResult = DialogResult.Cancel;
                        break;
                }

                Close();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Capture the width combo box selection change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxAssetSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Asset = comboBoxAsset.Text;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Private form events
    }
}