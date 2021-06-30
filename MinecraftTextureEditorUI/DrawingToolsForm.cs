using MinecraftTextureEditorAPI;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MinecraftTextureEditorUI
{
    public partial class DrawingToolsForm : Form
    {
        #region Public delegates

        public delegate void ToolTypeChangedEventHandler(ToolType toolType);

        public delegate void BackColourChangedEventHandler(Color colour);

        public delegate void ForeColourChangedEventHandler(Color colour);

        #endregion Public delegates

        #region Enums

        /// <summary>
        /// Tool types
        /// </summary>
        public enum ToolType
        {
            Pen,
            Eraser,
            Dropper,
            MirrorX,
            MirrorY
        }

        #endregion Enums

        #region Public properties

        /// <summary>
        /// The current tool type in use
        /// </summary>
        public ToolType CurrentToolType { get; set; }

        /// <summary>
        /// The pen foreground colour
        /// </summary>
        public Color Colour1
        {
            get { return panelColour1.BackColor; }
            set
            {
                DrawSaturation(value);
                panelColour1.BackColor = value;
            }
        }

        /// <summary>
        /// The pen background colour
        /// </summary>
        public Color Colour2
        {
            get { return panelColour2.BackColor; }
            set
            {
                DrawSaturation(value);
                panelColour2.BackColor = value;
            }
        }

        #endregion Public properties

        #region Public events

        /// <summary>
        /// Tool type changed event
        /// </summary>
        public event ToolTypeChangedEventHandler ToolTypeChanged;

        /// <summary>
        /// Fore colour changed event
        /// </summary>
        public event ForeColourChangedEventHandler Colour1Changed;

        /// <summary>
        /// Back colour changed event
        /// </summary>
        public event BackColourChangedEventHandler Colour2Changed;

        #endregion Public events

        /// <summary>
        /// The constructor
        /// </summary>
        public DrawingToolsForm()
        {
            InitializeComponent();

            LoadImages();

            pictureBoxColourPicker.MouseDown += PictureBoxColourPicker_MouseDown;

            pictureBoxColourPicker.MouseMove += PictureBoxColourPicker_MouseMove;

            pictureBoxGamma.MouseDown += PictureBoxSaturationMouseDown;

            pictureBoxGamma.MouseMove += PictureBoxSaturationMouseMove;

            panelColour1.Click += PanelColourClick;

            panelColour2.Click += PanelColourClick;

            Colour2 = Color.Black;

            Colour1 = Color.White;
        }

        /// <summary>
        /// Change the saturation panel to reflect panel back colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelColourClick(object sender, EventArgs e)
        {
            var panel = (Panel)sender;

            PickColour(panel);
        }

        #region Private methods

        /// <summary>
        /// Tool type changed event
        /// </summary>
        /// <param name="toolType"></param>
        private void OnToolTypeChanged(ToolType toolType)
        {
            CurrentToolType = toolType;
            ToolTypeChanged?.Invoke(toolType);
        }

        /// <summary>
        /// Fore colour changed event
        /// </summary>
        private void OnColour1Changed(Color colour)
        {
            Colour1Changed?.Invoke(colour);
        }

        /// <summary>
        /// Back colour changed event
        /// </summary>
        private void OnColour2Changed(Color colour)
        {
            Colour2Changed?.Invoke(colour);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="e"></param>
        private void PickColour(Panel panel)
        {
            try
            {
                var colour = panel.BackColor;

                DrawSaturation(colour);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="e"></param>
        private void PickColour(object pictureBox, MouseEventArgs e, bool isPicker = true)
        {
            try
            {
                var cursorPosition = e.Location;

                var colour = DrawingHelper.GetColour((PictureBox)pictureBox, cursorPosition.X, cursorPosition.Y);

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        panelColour1.BackColor = colour;
                        OnColour1Changed(colour);
                        break;

                    case MouseButtons.Right:
                        panelColour2.BackColor = colour;
                        OnColour2Changed(colour);
                        break;

                    default:
                        return;
                }

                if (isPicker)
                {
                    DrawSaturation(colour);
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Draw the saturation bar
        /// </summary>
        /// <param name="colour">Base colour</param>
        private void DrawSaturation(Color colour)
        {
            var tmp = new Bitmap(pictureBoxGamma.ClientSize.Width, pictureBoxGamma.ClientSize.Height);

            var gradient1 = new LinearGradientBrush(new Rectangle(0, 0, pictureBoxGamma.ClientSize.Width / 2, pictureBoxGamma.ClientSize.Height), Color.Black, colour, LinearGradientMode.Horizontal);
            var gradient2 = new LinearGradientBrush(new Rectangle(0, 0, pictureBoxGamma.ClientSize.Width / 2, pictureBoxGamma.ClientSize.Height), colour, Color.White, LinearGradientMode.Horizontal);

            using (var g = Graphics.FromImage(tmp))
            {
                g.FillRectangle(gradient1, new Rectangle(0, 0, pictureBoxGamma.ClientSize.Width / 2, pictureBoxGamma.ClientSize.Height));
                g.FillRectangle(gradient2, new Rectangle(pictureBoxGamma.ClientSize.Width / 2, 0, pictureBoxGamma.ClientSize.Width / 2, pictureBoxGamma.ClientSize.Height));
            }

            pictureBoxGamma.Image = tmp;
        }

        /// <summary>
        /// Load images for buttons
        /// </summary>
        private void LoadImages()
        {
            pictureBoxColourPicker.Image = Properties.Resources.ColourWheel;

            buttonEraser.BackgroundImage = Properties.Resources.Eraser;

            buttonPen.BackgroundImage = Properties.Resources.Pen;

            buttonDropper.BackgroundImage = Properties.Resources.dropper;

            buttonMirrorX.BackgroundImage = Properties.Resources.mirrorx;

            buttonMirrorY.BackgroundImage = Properties.Resources.mirrory;

            Invalidate(true);
        }

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxSaturationMouseDown(object sender, MouseEventArgs e)
        {
            PickColour(sender, e, false);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxSaturationMouseMove(object sender, MouseEventArgs e)
        {
            PickColour(sender, e, false);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxColourPicker_MouseDown(object sender, MouseEventArgs e)
        {
            PickColour(sender, e);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxColourPicker_MouseMove(object sender, MouseEventArgs e)
        {
            PickColour(sender, e);
        }

        /// <summary>
        /// Pen click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPenClick(object sender, System.EventArgs e)
        {
            OnToolTypeChanged(ToolType.Pen);
        }

        /// <summary>
        /// Eraser click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEraserClick(object sender, System.EventArgs e)
        {
            OnToolTypeChanged(ToolType.Eraser);
        }

        /// <summary>
        /// Dropper click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDropperClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.Dropper);
        }

        /// <summary>
        /// MirrorX click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMirrorXClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.MirrorX);
        }

        /// <summary>
        /// MirrorY click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMirrorYClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.MirrorY);
        }

        #endregion Form events
    }
}