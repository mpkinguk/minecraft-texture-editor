using log4net;
using MinecraftTextureEditorAPI.Helpers;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;
using System;
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

        public delegate void BrushSizeChangedEventHandler(int brushSize);

        public delegate void TransparencyLockChangedEventHandler(bool locked);

        #endregion Public delegates

        #region Public properties

        /// <summary>
        /// The brush size
        /// </summary>
        public int BrushSize { 
            get 
            { 
                return _brushSize; 
            } 
            set 
            { 
                _brushSize = value; 
            } 
        }

        /// <summary>
        /// The current tool type in use
        /// </summary>
        public ToolType CurrentToolType
        {
            get { return _currentToolType; }
            set
            {
                _currentToolType = value;

                if (value.Equals(ToolType.TransparencyLock))
                {
                    return;
                }

                var button = (Button)Controls[$"button{value}"];

                if(button is null)
                {
                    return;
                }

                button.Focus();

                Invalidate(true);
            }
        }

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

        /// <summary>
        /// The transparency lock
        /// </summary>
        public bool TransparencyLock
        {
            get { return _transparencyLock; }
            set
            {
                _transparencyLock = value;
                UpdateTransparencyButton();
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

        /// <summary>
        /// Back colour changed event
        /// </summary>
        public event BrushSizeChangedEventHandler BrushSizeChanged;

        /// <summary>
        /// Back colour changed event
        /// </summary>
        public event TransparencyLockChangedEventHandler TransparencyLockChanged;

        #endregion Public events

        #region Private properties

        private int _alpha;

        private int _brushSize;

        private ToolType _currentToolType;

        private bool _transparencyLock;

        private readonly ILog _log;

        #endregion Private properties

        /// <summary>
        /// The constructor
        /// </summary>
        public DrawingToolsForm(ILog log)
        {
            _log = log;

            try
            {
                InitializeComponent();

                DrawAlpha();

                pictureBoxColourPicker.MouseDown += PictureBoxColourPicker_MouseDown;

                pictureBoxColourPicker.MouseMove += PictureBoxColourPicker_MouseMove;

                pictureBoxGamma.MouseDown += PictureBoxSaturationMouseDown;

                pictureBoxGamma.MouseMove += PictureBoxSaturationMouseMove;

                pictureBoxAlpha.MouseDown += PictureBoxAlphaMouseDown;

                pictureBoxAlpha.MouseMove += PictureBoxAlphaMouseMove;

                panelColour1.Click += PanelColourClick;

                panelColour2.Click += PanelColourClick;

                toolTip1.Draw += ToolTipDraw;

                Colour2 = Color.Black;

                Colour1 = Color.White;

                _brushSize = 1;

                _alpha = 255;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Override the tool tip drawing function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTipDraw(object sender, DrawToolTipEventArgs e)
        {
            try
            {
                var g = e.Graphics;

                using (StringFormat sf = new StringFormat())
                {
                    e.DrawBackground();

                    // Top.
                    sf.LineAlignment = StringAlignment.Center;

                    // Top/Left.
                    sf.Alignment = StringAlignment.Center;

                    g.DrawString(e.ToolTipText, new Font("Minecraft", 6F), Brushes.Black, e.Bounds, sf);

                    e.DrawBorder();
                }

                g.Flush();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Private methods

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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Pick a colour, any colour :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="colourSelectionType">The colour selection type</param>
        private void PickColour(object sender, MouseEventArgs e, ColourSelectionType colourSelectionType = ColourSelectionType.ColourWheel)
        {
            try
            {
                var cursorPosition = e.Location;

                var pictureBox = (PictureBox)sender;

                if (pictureBox.Image is null)
                {
                    return;
                }

                var colour = GetColour(pictureBox, cursorPosition.X, cursorPosition.Y);

                if (colourSelectionType.Equals(ColourSelectionType.Alpha))
                {
                    if (e.Button.Equals(MouseButtons.Left) || e.Button.Equals(MouseButtons.Right))
                    {
                        _alpha = Convert.ToInt32(GetColour(pictureBox, cursorPosition.X, cursorPosition.Y).A).Clamp(0, 255);
                    }
                }

                colour = colourSelectionType.Equals(ColourSelectionType.Alpha) ? Color.FromArgb(_alpha, e.Button.Equals(MouseButtons.Left) ? Colour1 : Colour2) : Color.FromArgb(_alpha, colour);

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
                };

                switch (colourSelectionType)
                {
                    case ColourSelectionType.ColourWheel:
                        DrawSaturation(colour);
                        DrawAlpha();
                        break;

                    case ColourSelectionType.Alpha:
                        DrawAlpha();
                        break;
                };
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Draw the saturation bar
        /// </summary>
        /// <param name="colour">Base colour</param>
        private void DrawSaturation(Color colour)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Draw alpha picker
        /// </summary>
        private void DrawAlpha()
        {
            try
            {
                var tmp = new Bitmap(pictureBoxAlpha.ClientSize.Width, pictureBoxAlpha.ClientSize.Height);

                var gradient1 = new LinearGradientBrush(new Rectangle(0, 0, pictureBoxAlpha.ClientSize.Width, pictureBoxAlpha.ClientSize.Height), Color.FromArgb(0, Color.Black), Color.Black, LinearGradientMode.Horizontal);

                using (var g = Graphics.FromImage(tmp))
                {
                    g.FillRectangle(gradient1, new Rectangle(0, 0, pictureBoxAlpha.ClientSize.Width, pictureBoxAlpha.ClientSize.Height));

                    var x1 = (int)(pictureBoxAlpha.ClientSize.Width / 255F * Colour1.A);
                    var x2 = (int)(pictureBoxAlpha.ClientSize.Width / 255F * Colour2.A);

                    g.DrawLine(new Pen(Color.Yellow, 2F), x1, 0, x1, pictureBoxAlpha.ClientSize.Width);
                    g.DrawLine(new Pen(Color.Red, 2F), x2, 0, x2, pictureBoxAlpha.ClientSize.Width);
                }

                pictureBoxAlpha.Image = tmp;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update the transparency lock button
        /// </summary>
        private void UpdateTransparencyButton()
        {
            buttonLockTransparency.Image = TransparencyLock ? Properties.Resources.lockon : Properties.Resources.lockoff;
        }

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxAlphaMouseDown(object sender, MouseEventArgs e)
        {
            PickColour(sender, e, ColourSelectionType.Alpha);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxAlphaMouseMove(object sender, MouseEventArgs e)
        {
            PickColour(sender, e, ColourSelectionType.Alpha);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxSaturationMouseDown(object sender, MouseEventArgs e)
        {
            PickColour(sender, e, ColourSelectionType.Saturation);
        }

        /// <summary>
        /// Pick a colour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxSaturationMouseMove(object sender, MouseEventArgs e)
        {
            PickColour(sender, e, ColourSelectionType.Saturation);
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

        /// <summary>
        /// Texturiser click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTexturiserClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.Texturiser);
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

        /// <summary>
        /// Tool type changed event
        /// </summary>
        /// <param name="toolType"></param>
        private void OnToolTypeChanged(ToolType toolType)
        {
            _currentToolType = toolType;
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
        /// Brush size changed event
        /// </summary>
        /// <param name="brushSize">The brush size</param>
        private void OnBrushSizeChanged(int brushSize)
        {
            BrushSize = brushSize;

            BrushSizeChanged?.Invoke(brushSize);
        }

        /// <summary>
        /// Transparency lock changed event
        /// </summary>
        /// <param name="locked">Locked</param>
        private void OnTransparencyLockChanged(bool locked)
        {
            TransparencyLockChanged?.Invoke(locked);
        }

        /// <summary>
        /// Flood fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFloodFillClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.FloodFill);
        }

        /// <summary>
        /// Rainbow :D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRainbowClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.Rainbow);
        }

        /// <summary>
        /// Brush size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBrushSizeClick(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;

                if (int.TryParse(button.Name[button.Name.Length - 1].ToString(), out int size))
                {
                    OnBrushSizeChanged(size);
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Transaparency lock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLockTransparencyClick(object sender, EventArgs e)
        {
            try
            {
                _transparencyLock = !_transparencyLock;

                UpdateTransparencyButton();

                OnTransparencyLockChanged(TransparencyLock);
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Form events
    }
}