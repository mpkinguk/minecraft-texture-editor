using log4net;
using System;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class ResolutionForm : Form
    {
        #region Public properties

        /// <summary>
        /// The image height
        /// </summary>
        public int ImageHeight { get; set; }

        /// <summary>
        /// The image width
        /// </summary>
        public int ImageWidth { get; set; }

        #endregion Public properties

        #region Private properties

        private readonly ILog _log;
        private bool _squareImage;

        #endregion Private properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log"></param>
        public ResolutionForm(ILog log)
        {
            _log = log;

            try
            {
                InitializeComponent();

                comboBoxWidth.SelectedIndexChanged += ComboBoxWidthSelectedIndexChanged;

                comboBoxHeight.SelectedIndexChanged += ComboBoxHeightSelectedIndexChanged;

                // Default to 16 x 16
                comboBoxHeight.SelectedIndex = 0;
                comboBoxWidth.SelectedIndex = 0;

                checkBoxSquareImage.Checked = true;
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
        private void ButtonOKClick(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;

                switch (button.Name)
                {
                    case (nameof(buttonOK)):
                        ImageHeight = Convert.ToInt32(comboBoxHeight.Text);
                        ImageWidth = Convert.ToInt32(comboBoxWidth.Text);

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
        /// Capture the square image checkbox change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxSquareImageCheckedChanged(object sender, EventArgs e)
        {
            _squareImage = checkBoxSquareImage.Checked;
        }

        /// <summary>
        /// Capture the height combo box selection change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxHeightSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ImageHeight = Convert.ToInt32(comboBoxHeight.Text);

                if (_squareImage && comboBoxHeight.SelectedIndex != comboBoxWidth.SelectedIndex)
                {
                    comboBoxWidth.SelectedIndex = comboBoxHeight.SelectedIndex;
                }
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
        private void ComboBoxWidthSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ImageWidth = Convert.ToInt32(comboBoxWidth.Text);

                if (_squareImage && comboBoxHeight.SelectedIndex != comboBoxWidth.SelectedIndex)
                {
                    comboBoxHeight.SelectedIndex = comboBoxWidth.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Private form events
    }
}