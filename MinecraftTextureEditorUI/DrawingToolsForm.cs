using log4net;
using MinecraftTextureEditorAPI.Helpers;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;

namespace MinecraftTextureEditorUI
{
    public partial class DrawingToolsForm : Form
    {
        #region Public delegates

        public delegate void BackColourChangedEventHandler();

        public delegate void BrushSizeChangedEventHandler();

        public delegate void ForeColourChangedEventHandler();

        public delegate void ToolTypeChangedEventHandler();

        public delegate void ShapeTypeChangedEventHandler();

        public delegate void ModifierChangedEventHandler();

        public delegate void TransparencyLockChangedEventHandler();

        #endregion Public delegates

        #region Public properties

        /// <summary>
        /// The pen foreground colour
        /// </summary>
        public Color Colour1
        {
            get { return State.Colour1; }
            set
            {
                State.Colour1 = value;
                DrawSaturation(State.Colour1);
                panelColour1.BackColor = State.Colour1;
            }
        }

        /// <summary>
        /// The pen background colour
        /// </summary>
        public Color Colour2
        {
            get { return State.Colour2; }
            set
            {
                State.Colour2 = value;
                DrawSaturation(State.Colour2);
                panelColour2.BackColor = State.Colour2;
            }
        }

        /// <summary>
        /// The current tool type in use
        /// </summary>
        public ToolType CurrentToolType
        {
            get { return State.ToolType; }
            set
            {
                State.ToolType = value;

                var button = (Button)Controls[$"button{value}"];

                if (button is null)
                {
                    return;
                }

                button.Focus();

                Invalidate(true);
            }
        }

        /// <summary>
        /// The current tool type in use
        /// </summary>
        public ShapeType CurrentShapeType
        {
            get { return State.ShapeType; }
            set
            {
                State.ShapeType = value;

                Invalidate(true);
            }
        }

        /// <summary>
        /// The modifiers flag
        /// </summary>
        public Modifier Modifiers
        {
            get { return State.Modifiers; }
            set
            {
                State.Modifiers = value;
                UpdateModifierButtons();
            }
        }

        #endregion Public properties

        #region Public events

        /// <summary>
        /// Back colour changed event
        /// </summary>
        public event BrushSizeChangedEventHandler BrushSizeChanged;

        /// <summary>
        /// Fore colour changed event
        /// </summary>
        public event ForeColourChangedEventHandler Colour1Changed;

        /// <summary>
        /// Back colour changed event
        /// </summary>
        public event BackColourChangedEventHandler Colour2Changed;

        /// <summary>
        /// Modifier changed event
        /// </summary>
        public event ModifierChangedEventHandler ModifierChanged;

        /// <summary>
        /// Tool type changed event
        /// </summary>
        public event ToolTypeChangedEventHandler ToolTypeChanged;

        /// <summary>
        /// Shape type changed event
        /// </summary>
        public event ShapeTypeChangedEventHandler ShapeTypeChanged;

        #endregion Public events

        #region Private properties

        private readonly ILog _log;

        private bool _mirrorX;

        private bool _mirrorY;

        private bool _transparencyLock;

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

                Init();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #region Public methods

        /// <summary>
        /// Update the button states
        /// </summary>
        public void UpdateButtons()
        {
            try
            {
                Controls[$"button{State.ToolType}"].Focus();
                UpdateModifierButtons();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update the button states
        /// </summary>
        public void UpdateShapesMenu()
        {
            try
            {
                foreach (ToolStripMenuItem menuItem in contextMenuStripShape.Items)
                {
                    menuItem.Checked = State.ShapeType.ToString().Equals(menuItem.Text);
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Update the modifier buttons
        /// </summary>
        public void UpdateModifierButtons()
        {
            _mirrorX = State.MirrorX;
            _mirrorY = State.MirrorY;
            _transparencyLock = State.TransparencyLock;

            buttonTransparencyLock.Image = _transparencyLock ? Properties.Resources.lockon : Properties.Resources.lockoff;
            buttonTransparencyLock.BackColor = _transparencyLock ? Color.FromKnownColor(KnownColor.ControlDark) : Color.FromKnownColor(KnownColor.Control);

            buttonMirrorX.BackColor = _mirrorX ? Color.FromKnownColor(KnownColor.ControlDark) : Color.FromKnownColor(KnownColor.Control);
            buttonMirrorY.BackColor = _mirrorY ? Color.FromKnownColor(KnownColor.ControlDark) : Color.FromKnownColor(KnownColor.Control);
        }

        /// <summary>
        /// Update the colours
        /// </summary>
        public void UpdateColours()
        {
            Colour1 = State.Colour1;
            Colour2 = State.Colour2;
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Initilaise the form elements
        /// </summary>
        private void Init()
        {
            pictureBoxColourPicker.MouseDown += PictureBoxColourPicker_MouseDown;

            pictureBoxColourPicker.MouseMove += PictureBoxColourPicker_MouseMove;

            pictureBoxGamma.MouseDown += PictureBoxSaturationMouseDown;

            pictureBoxGamma.MouseMove += PictureBoxSaturationMouseMove;

            pictureBoxAlpha.MouseDown += PictureBoxAlphaMouseDown;

            pictureBoxAlpha.MouseMove += PictureBoxAlphaMouseMove;

            panelColour1.Click += PanelColourClick;

            panelColour2.Click += PanelColourClick;

            toolTip1.Draw += ToolTipDraw;

            State.CurrentRainbowColourIndex = 0;

            State.EraserColor = Color.Transparent;

            Colour1 = Color.White;

            Colour2 = Color.Black;

            State.BrushSize = 1;

            State.Alpha = 255;

            State.ShapeType = 0;

            contextMenuStripShape.Items.AddRange(GetShapeMenuItems());

            UpdateShapesMenu();
        }

        /// <summary>
        /// Get shape menu items and add click events
        /// </summary>
        /// <returns>Array of ToolStripItem</returns>
        private ToolStripItem[] GetShapeMenuItems()
        {
            var items = DrawingHelper.GetShapeMenuItems();

            foreach (ToolStripItem item in items)
            {
                item.Click += ToolStripItemShapeClick;
            }

            return items;
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

                    var x1 = (int)(pictureBoxAlpha.ClientSize.Width / 255F * State.Colour1.A);
                    var x2 = (int)(pictureBoxAlpha.ClientSize.Width / 255F * State.Colour2.A);

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
                        State.Alpha = Convert.ToInt32(GetColour(pictureBox, cursorPosition.X, cursorPosition.Y).A).Clamp(0, 255);
                    }
                }

                colour = colourSelectionType.Equals(ColourSelectionType.Alpha) ?
                    Color.FromArgb(State.Alpha, e.Button.Equals(MouseButtons.Left) ?
                    State.Colour1 : State.Colour2) : Color.FromArgb(State.Alpha, colour);

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

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Capture the shape type changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripItemShapeClick(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            State.ShapeType = (ShapeType)Enum.Parse(typeof(ShapeType), item.Text);

            UpdateShapesMenu();

            ShapeTypeChanged?.Invoke();
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
        /// Dropper click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDropperClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.ColourPicker);
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
        /// Flood fill
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFloodFillClick(object sender, EventArgs e)
        {
            OnToolTypeChanged(ToolType.FloodFill);
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

                OnModifierChanged();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// MirrorX click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMirrorXClick(object sender, EventArgs e)
        {
            _mirrorX = !_mirrorX;

            OnModifierChanged();
        }

        /// <summary>
        /// MirrorY click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMirrorYClick(object sender, EventArgs e)
        {
            _mirrorY = !_mirrorY;

            OnModifierChanged();
        }

        /// <summary>
        /// Pen click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPenClick(object sender, System.EventArgs e)
        {
            OnToolTypeChanged(ToolType.Draw);
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
        /// Shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShapeClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Point screenPoint = button.PointToScreen(new Point(button.Left, button.Bottom));
            if (screenPoint.Y + contextMenuStripShape.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                contextMenuStripShape.Show(button, new Point(0, -contextMenuStripShape.Size.Height));
            }
            else
            {
                contextMenuStripShape.Show(button, new Point(0, button.Height));
            }

            OnToolTypeChanged(ToolType.Shape);
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
        /// Brush size changed event
        /// </summary>
        /// <param name="brushSize">The brush size</param>
        private void OnBrushSizeChanged(int brushSize)
        {
            State.BrushSize = brushSize;

            BrushSizeChanged?.Invoke();
        }

        /// <summary>
        /// Fore colour changed event
        /// </summary>
        private void OnColour1Changed(Color colour)
        {
            State.Colour1 = colour;

            Colour1Changed?.Invoke();
        }

        /// <summary>
        /// Back colour changed event
        /// </summary>
        private void OnColour2Changed(Color colour)
        {
            State.Colour2 = colour;

            Colour2Changed?.Invoke();
        }

        /// <summary>
        /// Modifier changed event
        /// </summary>
        private void OnModifierChanged()
        {
            Modifiers = (_mirrorX ? Modifier.MirrorX : 0) | (_mirrorY ? Modifier.MirrorY : 0) | (_transparencyLock ? Modifier.TransparencyLock : 0);

            UpdateButtons();

            ModifierChanged?.Invoke();
        }

        /// <summary>
        /// Tool type changed event
        /// </summary>
        /// <param name="toolType"></param>
        private void OnToolTypeChanged(ToolType toolType)
        {
            State.ToolType = toolType;

            ToolTypeChanged?.Invoke();
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

        #endregion Form events
    }
}