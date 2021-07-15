using log4net;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class OptionsForm : Form
    {
        #region Public properties

        /// <summary>
        /// Has Changed
        /// </summary>
        public bool HasChanged
        {
            get { return _hasChanged; }
            set { _hasChanged = value; }
        }

        /// <summary>
        /// Has Saved
        /// </summary>
        public bool HasSaved
        {
            get { return _hasSaved; }
            set { _hasSaved = value; }
        }

        #endregion Public properties

        #region Private properties

        private bool _hasChanged;

        private bool _hasSaved;

        private readonly ILog _log;

        #endregion Private properties

        /// <summary>
        /// Constructor
        /// </summary>
        public OptionsForm(ILog log)
        {
            _log = log;

            InitializeComponent();

            Load += OptionsForm_Load;

            FormClosing += OptionsFormFormClosing;
        }

        #region Private methods

        /// <summary>
        /// Save the settings
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                foreach (Control control in Controls.OfType<TextBox>())
                {
                    var key = (string)control.Tag;
                    ConfigurationHelper.SaveSetting(key, control.Text);
                }

                _hasSaved = true;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Load settings
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                var x = 20; int y = 60;

                var inc = 30;

                TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

                foreach (var setting in ConfigurationHelper.GetAllSettings())
                {
                    var label = new Label()
                    {
                        Text = textInfo.ToTitleCase(setting.Key),
                        Name = $"label{setting.Key}",
                        Font = new Font("Minecraft", 10F),
                        ForeColor = Color.Yellow,
                        BackColor = Color.FromKnownColor(KnownColor.Transparent),
                        Location = new Point(x, y),
                        AutoSize = true
                    };

                    y += inc;

                    var textBox = new TextBox
                    {
                        Name = $"textBox{setting.Key}",
                        Text = setting.Value,
                        Size = new Size(ClientRectangle.Width - x * 2, 21),
                        Font = new Font("Minecraft", 10F),
                        Location = new Point(x, y),
                        Tag = setting.Key,
                        Anchor = AnchorStyles.Left & AnchorStyles.Top & AnchorStyles.Right
                    };

                    textBox.TextChanged += TextBoxTextChanged;

                    Controls.Add(label);
                    Controls.Add(textBox);

                    y += inc;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Capture the form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsForm_Load(object sender, System.EventArgs e)
        {
            LoadSettings();
        }

        /// <summary>
        /// Capture the form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsFormFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_hasChanged && !_hasSaved)
                {
                    switch (MessageBox.Show(this, "Some options have been changed.\nDo you wish to save your settings?", "Save settings?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                    {
                        case DialogResult.Yes:
                            SaveSettings();
                            DialogResult = DialogResult.OK;
                            break;

                        case DialogResult.No:
                            DialogResult = DialogResult.OK;
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            DialogResult = DialogResult.Cancel;
                            break;
                    }
                }
                else if (_hasSaved)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Capture text changed event to set _hasChanged flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxTextChanged(object sender, System.EventArgs e)
        {
            _hasChanged = true;
        }

        /// <summary>
        /// Save the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveClick(object sender, System.EventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// Reload the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReloadClick(object sender, System.EventArgs e)
        {
            LoadSettings();
        }

        #endregion Form events
    }
}