using log4net;
using MinecraftTextureEditorAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

                Dictionary<string, string> assetList = new Dictionary<string, string>();

                foreach (var asset in assets)
                {
                    var info = new FileInfo(asset);
                    
                    var assetName = info.Directory.Name;

                    try
                    {
                        assetList.Add(assetName, asset);
                    }
                    catch(Exception ex) 
                    {
                        // Version number is the sub directory, so get the value of the conflicted key and remove it
                        var value = assetList[assetName];

                        assetList.Remove(assetName);

                        var newAssetName = new FileInfo(value).Directory.Parent.Name;

                        // Re-add the old one, as you can't update a dictionary key once defined
                        assetList.Add(newAssetName, value);                        

                        // Then apply same logic to the new one
                        newAssetName = new FileInfo(asset).Directory.Parent.Name;

                        // Add the new key! :)
                        assetList.Add(newAssetName, asset);

                        _log?.Warn(ex.Message);
                    }
                }

                comboBoxAsset.DataSource = new BindingSource(assetList, null);
                comboBoxAsset.DisplayMember = "Key";
                comboBoxAsset.ValueMember = "Value";

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
                var button = (System.Windows.Forms.Button)sender;

                switch (button.Name)
                {
                    case (nameof(buttonOK)):
                        Asset = ((KeyValuePair<string, string>)comboBoxAsset.SelectedItem).Value;
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
                toolTip1.SetToolTip(comboBoxAsset, ((KeyValuePair<string, string>)comboBoxAsset.SelectedItem).Value);
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Private form events
    }
}