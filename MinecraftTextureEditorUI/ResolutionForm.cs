using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class ResolutionForm : Form
    {
        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        private bool _squareImage;

        public ResolutionForm()
        {
            InitializeComponent();

            comboBoxWidth.SelectedIndexChanged += ComboBoxWidthSelectedIndexChanged;

            comboBoxHeight.SelectedIndexChanged += ComboBoxHeightSelectedIndexChanged;

            // Default to 16 x 16
            comboBoxHeight.SelectedIndex = 0;
            comboBoxWidth.SelectedIndex = 0;

            checkBoxSquareImage.Checked = true;
        }

        private void ComboBoxHeightSelectedIndexChanged(object sender, EventArgs e)
        {
            ImageHeight = Convert.ToInt32(comboBoxHeight.Text);

            if (_squareImage && comboBoxHeight.SelectedIndex != comboBoxWidth.SelectedIndex)
            {
                comboBoxWidth.SelectedIndex = comboBoxHeight.SelectedIndex;
            }
        }

        private void ComboBoxWidthSelectedIndexChanged(object sender, EventArgs e)
        {
            ImageHeight = Convert.ToInt32(comboBoxWidth.Text);

            if (_squareImage && comboBoxHeight.SelectedIndex != comboBoxWidth.SelectedIndex)
            {
                comboBoxHeight.SelectedIndex = comboBoxWidth.SelectedIndex;
            }
        }

        private void ButtonOKClick(object sender, EventArgs e)
        {
            ImageHeight = Convert.ToInt32(comboBoxHeight.Text);
            ImageWidth = Convert.ToInt32(comboBoxWidth.Text);

            DialogResult = DialogResult.OK;
        }

        private void CheckBoxSquareImageCheckedChanged(object sender, EventArgs e)
        {
            _squareImage = checkBoxSquareImage.Checked;
        }
    }
}
