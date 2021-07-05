using GenericUndoRedoManagerAPI;
using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Helpers;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static MinecraftTextureEditorAPI.DrawingHelper;

namespace MinecraftTextureEditorUI
{
    public partial class EditorForm : Form
    {
        #region Public delegates

        /// <summary>
        /// Colour selected delegate for dropper
        /// </summary>
        /// <param name="colour">The colour</param>
        /// <param name="isForeColour">Is fore colour?</param>
        public delegate void ColourSelectedEventHandler(Color colour, bool isForeColour);

        /// <summary>
        /// Undo manager has done something
        /// </summary>
        /// <param name="undo"></param>
        public delegate void UndoManagerActionEventHandler();

        #endregion Public delegates

        #region Public variables

        /// <summary>
        /// Display drawing grid
        /// </summary>
        public bool ShowGrid
        {
            get => _showGrid;
            set
            {
                _showGrid = value;
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Display drawing grid
        /// </summary>
        public bool ShowTransparent
        {
            get => _showTransparent;
            set
            {
                _showTransparent = !_showTransparent;
                BackgroundImage = _showTransparent ? Properties.Resources.transparentGrid : null;
                RefreshDisplay();
            }
        }

        /// <summary>
        /// Has the image changed since loading?
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// The current fore colour
        /// </summary>
        public Color Colour1 { get; set; }

        /// <summary>
        /// the current back colour
        /// </summary>
        public Color Colour2 { get; set; }

        /// <summary>
        /// The eraser colour
        /// </summary>
        public Color EraserColor { get; set; }

        /// <summary>
        /// The current filename for this editor window
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The current tool type in use
        /// </summary>
        public ToolType ToolType { get; set; }

        /// <summary>
        /// The pixel objects in use
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// The current zoom value
        /// </summary>
        public int Zoom { get; set; }

        /// <summary>
        /// Undo enabled
        /// </summary>
        public bool UndoEnabled => _undoManager.CanUndo;

        /// <summary>
        /// Redo enabled
        /// </summary>
        public bool RedoEnabled => _undoManager.CanRedo;

        #endregion Public variables

        #region private variables

        private bool _showGrid;

        private bool _showTransparent;

        private const int StartZoom = 16;

        private int _width;

        private int _height;

        private int _currentRainbowColour;

        private Point _cursor;

        private Pixel _lastRainbowPixel = new Pixel();

        private UndoManagerAction<Texture> _undoManager;

        #endregion private variables

        #region Event constructors

        /// <summary>
        /// Colour selected event for Dropper
        /// </summary>
        public event ColourSelectedEventHandler ColourSelected;

        /// <summary>
        /// An undo manager action occured
        /// </summary>
        public event UndoManagerActionEventHandler UndoManagerAction;

        #endregion Event constructors

        /// <summary>
        /// The constructor
        /// </summary>
        public EditorForm(int width, int height)
        {
            InitializeComponent();

            Init(width, height);
        }

        #region Private methods

        /// <summary>
        /// Init variables
        /// </summary>
        private void Init(int width, int height)
        {
            Texture = DrawingHelper.GetBlankTexture(width, height);

            _width = width;
            _height = height;

            _currentRainbowColour = 0;

            _undoManager = new UndoManagerAction<Texture>();

            AddItem();

            FormClosing += EditorFormFormClosing;

            pictureBoxImage.Paint += PictureBoxImagePaint;
            pictureBoxImage.MouseMove += EditorMousePaintPixel;
            pictureBoxImage.MouseDown += EditorMousePaintPixel;
            pictureBoxImage.MouseUp += PictureBoxImageMouseUp;
            pictureBoxImage.MouseWheel += PictureBoxImageMouseWheel;

            pictureBoxImage.BackColor = Color.FromKnownColor(KnownColor.Transparent);

            MouseWheel += PictureBoxImageMouseWheel;
            MouseMove += EditorFormMouseMove;
            LostFocus += EditorFormLostFocus;

            Zoom = StartZoom;

            EraserColor = Color.Transparent;

            HasChanged = false;

            ShowGrid = true;

            ShowTransparent = true;

            KeyUp += EditorFormKeyUp;

            RefreshDisplay();
        }

        /// <summary>
        /// Allows commands using single key presses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    if (e.KeyCode.Equals(Keys.Delete))
                    {
                        var newTexture = DrawingHelper.GetBlankTexture(Texture.Width, Texture.Height);

                        Texture = newTexture.Clone();

                        AddItem();

                        RefreshDisplay();
                    }
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    ToolType = ToolType.Pen;
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    ToolType = ToolType.Eraser;
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    ToolType = ToolType.Dropper;
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    ToolType = ToolType.Texturiser;
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    ToolType = ToolType.FloodFill;
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    ToolType = ToolType.Rainbow;
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    ToolType = ToolType.MirrorX;
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    ToolType = ToolType.MirrorY;
                    break;
            }
        }

        /// <summary>
        /// Is the cursor out of bounds?
        /// </summary>
        /// <returns>Bool</returns>
        private bool CursorOutOfBounds()
        {
            return (_cursor.X < 0 || _cursor.Y < 0 || _cursor.X >= pictureBoxImage.Width || _cursor.Y >= pictureBoxImage.Height);
        }

        /// <summary>
        /// Stack-based floodfill routine
        /// </summary>
        /// <param name="currentColour">The current colour</param>
        /// <param name="newColour">The new colour</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        private void FloodFill(Color currentColour, Color newColour, int x, int y)
        {
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(new Point(x, y));

            if (Texture.PixelList.Any(o => o.PixelColour != currentColour))
            {

                // The *1.75F denotes the various offshoots from the main coordinates
                while (pixels.Count > 0 && pixels.Count <= Convert.ToInt32(Texture.PixelList.Count * 1.75F))
                {
                    Point a = pixels.Pop();
                    if (a.X >= 0 && a.X < Texture.Width &&
                         a.Y >= 0 && a.Y < Texture.Height)//make sure we stay within bounds
                    {

                        var currentPixel = Texture.PixelList.FirstOrDefault(o => o.X.Equals(a.X) && o.Y.Equals(a.Y));

                        if (currentPixel.PixelColour.Equals(currentColour) && currentColour != newColour)
                        {
                            currentPixel.PixelColour = newColour;
                            pixels.Push(new Point(a.X - 1, a.Y));
                            pixels.Push(new Point(a.X + 1, a.Y));
                            pixels.Push(new Point(a.X, a.Y - 1));
                            pixels.Push(new Point(a.X, a.Y + 1));
                        }
                    }
                }
            } else
            {
                foreach(var pixel in Texture.PixelList)
                {
                    pixel.PixelColour = newColour;
                }
            }
        }

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Check for mouse up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxImageMouseUp(object sender, MouseEventArgs e)
        {
            AddItem();
            RefreshDisplay();
        }

        /// <summary>
        /// Track cursor movement around editor form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormMouseMove(object sender, MouseEventArgs e)
        {
            _cursor = e.Location;
            RefreshDisplay();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormLostFocus(object sender, EventArgs e)
        {
            _cursor = new Point(_width + 1, _height + 1);
            RefreshDisplay();
        }

        /// <summary>
        /// Use mouse wheel to Zoom in/out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxImageMouseWheel(object sender, MouseEventArgs e)
        {
            _cursor = e.Location;

            var delta = Math.Sign(e.Delta) * 1;

            if (Zoom + delta < 1)
            {
                Zoom = 1;
            }
            else if (Zoom + delta > 32)
            {
                Zoom = 32;
            }
            else
            {
                Zoom += delta;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Fire colour selected event
        /// </summary>
        /// <param name="colour">The colour selected</param>
        /// <param name="isColour1">Is fore colour</param>
        private void OnColourSelected(Color colour, bool isColour1)
        {
            ColourSelected?.Invoke(colour, isColour1);

            Colour1 = isColour1 ? colour : Colour1;
            Colour2 = isColour1 ? Colour2 : colour;
        }

        /// <summary>
        /// Paint the picture box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxImagePaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            foreach (var pixel in Texture.PixelList)
            {
                var pixelRectangle = new Rectangle(pixel.X * Zoom, pixel.Y * Zoom, Zoom, Zoom);

                if (pixelRectangle.IntersectsWith(e.ClipRectangle))
                {
                    g.FillRectangle(new SolidBrush(pixel.PixelColour), pixelRectangle);

                    // Otherwise you can't see what you're painting!
                    if (Zoom > StartZoom / 2 && ShowGrid)
                    {
                        g.DrawRectangle(Pens.Black, pixelRectangle);
                    }
                }
            }

            var gridRectangle = new Rectangle(pictureBoxImage.Location, new Size(pictureBoxImage.ClientRectangle.Width - 1, pictureBoxImage.ClientRectangle.Height - 1));

            g.DrawRectangle(Pens.Black, gridRectangle);

            // Show cursor

            int cursorX = (_cursor.X / Zoom) * Zoom;
            int cursorY = (_cursor.Y / Zoom) * Zoom;

            if (!CursorOutOfBounds())
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Yellow)), cursorX, cursorY, Zoom, Zoom);
            }

            g.Flush();
        }

        /// <summary>
        /// Paint pixels using fore or back color where appropriate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorMousePaintPixel(object sender, MouseEventArgs e)
        {
            Color colour;

            _cursor = e.Location;

            if (CursorOutOfBounds())
            {
                return;
            }

            Pixel pixel = Texture.PixelList.FirstOrDefault(o => (o.X == (_cursor.X / Zoom)) && (o.Y == (_cursor.Y / Zoom)));

            if (pixel is null)
            {
                return;
            }

            switch (e.Button)
            {
                case MouseButtons.Left:
                    switch (ToolType)
                    {
                        case ToolType.Dropper:
                            OnColourSelected(pixel.PixelColour, true);
                            return;

                        case ToolType.Eraser:
                            colour = EraserColor;
                            break;

                        default:
                            colour = Colour1;
                            break;
                    }
                    break;

                case MouseButtons.Right:
                    switch (ToolType)
                    {
                        case ToolType.Dropper:
                            OnColourSelected(pixel.PixelColour, false);
                            return;

                        case ToolType.Eraser:
                            colour = EraserColor;
                            break;

                        default:
                            colour = Colour2;
                            break;
                    }
                    break;

                default:
                    RefreshDisplay();
                    return;
            }

            if (ToolType.Equals(ToolType.Rainbow))
            {
                colour = RainbowColours[_currentRainbowColour];

                var moveNextColour = false;

                if (e.Button.Equals(MouseButtons.Left))
                {
                    if (pixel.X != _lastRainbowPixel.X && pixel.Y != _lastRainbowPixel.Y)
                    {
                        moveNextColour = true;
                    }
                } else if (e.Button.Equals(MouseButtons.Right))
                {
                    if (pixel.X != _lastRainbowPixel.X || pixel.Y != _lastRainbowPixel.Y)
                    {
                        moveNextColour = true;
                    }
                }

                if (moveNextColour)
                {
                    _currentRainbowColour = _currentRainbowColour >= RainbowColours.Count - 1 ? 0 : _currentRainbowColour + 1;

                    _lastRainbowPixel.X = pixel.X;
                    _lastRainbowPixel.Y = pixel.Y;
                }

            }

            if (ToolType.Equals(ToolType.MirrorX))
            {
                Pixel inversePixel = Texture.PixelList.FirstOrDefault(o => o.X.Equals(_width - 1 - pixel.X) && o.Y.Equals(pixel.Y));

                inversePixel.PixelColour = colour;
            }
            else if (ToolType.Equals(ToolType.MirrorY))
            {
                Pixel inversePixel = Texture.PixelList.FirstOrDefault(o => o.Y.Equals(_height - 1 - pixel.Y) && o.X.Equals(pixel.X));

                inversePixel.PixelColour = colour;
            }

            if (ToolType.Equals(ToolType.Texturiser))
            {
                var rnd = new Random();

                var a = colour.A;
                var r = (rnd.Next(-20, 20) + colour.R).Clamp(0, 255);
                var g = (rnd.Next(-20, 20) + colour.G).Clamp(0, 255);
                var b = (rnd.Next(-20, 20) + colour.B).Clamp(0, 255);

                colour = Color.FromArgb(a, r, g, b);
            }

            if (ToolType.Equals(ToolType.FloodFill))
            {
                var currentColour = pixel.PixelColour;

                var x = pixel.X;

                var y = pixel.Y;

                FloodFill(currentColour, colour, x, y);
            }
            else
            {
                pixel.PixelColour = colour;
            }

            HasChanged = true;

            RefreshDisplay();
        }

        /// <summary>
        /// Refresh the display
        /// </summary>
        public void RefreshDisplay()
        {
            pictureBoxImage.Width = Texture.Width * Zoom;
            pictureBoxImage.Height = Texture.Height * Zoom;

            pictureBoxImage.Invalidate(true);
        }

        /// <summary>
        /// Form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (HasChanged)
            {
                if (MessageBox.Show("Changes have been made. Do you wish to save them?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    SaveFile(FileName);
                }

                _undoManager.Dispose();

                Dispose();
            }
        }

        #endregion Form events

        #region Public methods

        /// <summary>
        /// Load a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        public void LoadFile(string fileName = "")
        {
            using (var image = (Image)FileHelper.LoadFile(fileName))
            {
                if (image is null)
                {
                    return;
                }

                FileName = fileName;

                Texture = DrawingHelper.GetTextureFromImage(image);

                _undoManager = new UndoManagerAction<Texture>();

                Text = FileName.FileDetails()?.Name.ToUpper();

                AddItem();

                _width = Texture.Width;
                _height = Texture.Height;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Add item to undo stack
        /// </summary>
        public void AddItem()
        {
            _undoManager.AddItem(Texture.Clone());

            UndoRedoHappened();
        }

        /// <summary>
        /// Let calling thread know an action has occurred (for updating UI elements)
        /// </summary>
        private void UndoRedoHappened()
        {
            UndoManagerAction?.Invoke();
        }

        /// <summary>
        /// Save a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        public void SaveFile(string fileName = "")
        {
            using (var tmp = new Bitmap(_width, _height))
            {
                foreach (Pixel pixel in Texture.PixelList)
                {
                    tmp.SetPixel(pixel.X, pixel.Y, pixel.PixelColour);
                }

                FileHelper.SaveFile(tmp, fileName);

                // Set flag back to false
                HasChanged = false;
            }

            RefreshDisplay();
        }

        /// <summary>
        /// Undo
        /// </summary>
        public void Undo()
        {
            if (UndoEnabled)
            {
                Texture = _undoManager.Undo().Clone();

                RefreshDisplay();

                UndoRedoHappened();
            }
        }

        /// <summary>
        /// Redo
        /// </summary>
        public void Redo()
        {
            if (RedoEnabled)
            {
                Texture = _undoManager.Redo().Clone();

                RefreshDisplay();

                UndoRedoHappened();
            }
        }

        #endregion Public methods
    }
}