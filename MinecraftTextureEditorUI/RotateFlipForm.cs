using log4net;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class RotateFlipForm : Form
    {
        #region Public properties

        /// <summary>
        /// The image height
        /// </summary>
        public RotateFlipType RotateFlip { get; set; }

        /// <summary>
        /// The image width
        /// </summary>
        public int ImageWidth { get; set; }

        #endregion Public properties

        #region Private properties

        private readonly ILog _log;

        #endregion Private properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log"></param>
        public RotateFlipForm(ILog log)
        {
            _log = log;

            try
            {
                InitializeComponent();

                var types = Enum.GetNames(typeof(RotateFlipType)).ToList();

                types.Sort();

                foreach (string item in types)
                {
                    comboBoxRotateFlip.Items.Add(item);
                }

                comboBoxRotateFlip.SelectedIndex = 0;
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
                        RotateFlip = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), (string)comboBoxRotateFlip.SelectedItem);

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

        #endregion Private form events
    }
}