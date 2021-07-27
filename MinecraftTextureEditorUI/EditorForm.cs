using GenericUndoRedoManagerAPI;
using log4net;
using MinecraftTextureEditorAPI.Helpers;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;
using static MinecraftTextureEditorAPI.Helpers.FileHelper;
using MinecraftTextureEditorAPI.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        /// The current filename for this editor window
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Has the image changed since loading?
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// Redo enabled
        /// </summary>
        public bool RedoEnabled => _undoManager.CanRedo;

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
        /// Display transparent grid
        /// </summary>
        public bool ShowTransparentGrid
        {
            get => _showTransparentGrid;
            set
            {            
                _showTransparentGrid = value;
                BackgroundImage = _showTransparentGrid ? Properties.Resources.transparentGrid : null;
                RefreshDisplay();
            }
        }
        /// <summary>
        /// The pixel objects in use
        /// </summary>
        public Texture Texture { get; set; }

        /// <summary>
        /// Undo enabled
        /// </summary>
        public bool UndoEnabled => _undoManager.CanUndo;

        /// <summary>
        /// The current zoom value
        /// </summary>
        public int Zoom { get; set; }
        #endregion Public variables

        #region private variables

        private const int StartZoom = 16;

        private readonly ILog _log;
        private Point _cursor;
        private int _height;
        private Pixel _lastRainbowPixel = new Pixel();
        private bool _showGrid;
        private bool _showTransparentGrid;
        private UndoManagerAction<Texture> _undoManager;
        private int _width;
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
        public EditorForm(int width, int height, ILog log)
        {
            _log = log;

            InitializeComponent();

            Init(width, height);
        }

        #region Private methods

        /// <summary>
        /// Is the cursor out of bounds?
        /// </summary>
        /// <returns>Bool</returns>
        private bool CursorOutOfBounds()
        {
            return (_cursor.X < 0 || _cursor.Y < 0 || _cursor.X >= pictureBoxImage.Width || _cursor.Y >= pictureBoxImage.Height);
        }

        /// <summary>
        /// Init variables
        /// </summary>
        private void Init(int width, int height)
        {
            try
            {
                Texture = GetBlankTexture(width, height);

                _width = width;
                _height = height;          

                _undoManager = new UndoManagerAction<Texture>(_log);

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

                HasChanged = false;

                ShowGrid = true;

                ShowTransparentGrid = true;

                RefreshDisplay();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Let calling thread know an action has occurred (for updating UI elements)
        /// </summary>
        private void UndoRedoHappened()
        {
            UndoManagerAction?.Invoke();
        }

        #endregion Private methods

        #region Form events

        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
        }

        /// <summary>
        /// Form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Track focus of form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormLostFocus(object sender, EventArgs e)
        {
            _cursor = new Point(_width + 1, _height + 1);
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
        /// Paint pixels using fore or back color where appropriate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorMousePaintPixel(object sender, MouseEventArgs e)
        {
            try
            {
                Color colour;

                _cursor = e.Location;

                if (CursorOutOfBounds())
                {
                    return;
                }

                if (State.BrushSize == 0)
                {
                    return;
                }

                for (var y = 0; y < State.BrushSize; y++)
                {
                    for (var x = 0; x < State.BrushSize; x++)
                    {
                        if (((_cursor.X / Zoom) + x > Texture.Width - 1 || ((_cursor.Y / Zoom) + y > Texture.Height - 1)))
                        {
                            continue;
                        }

                        Pixel pixel = Texture.PixelList.FirstOrDefault(o => (o.X == (_cursor.X / Zoom) + x) && (o.Y == (_cursor.Y / Zoom) + y));

                        if (pixel is null)
                        {
                            return;
                        }

                        // Do not draw pixel if transparency lock is on and the underlying pixel is transparent
                        if (State.TransparencyLock)
                        {
                            if (pixel.PixelColour.A.Equals(0))
                            {
                                return;
                            }
                        }

                        switch (e.Button)
                        {
                            case MouseButtons.Left:
                                switch (State.ToolType)
                                {
                                    case ToolType.Dropper:
                                        // Only require first pixel
                                        OnColourSelected(pixel.PixelColour, true);
                                        return;

                                    case ToolType.Eraser:
                                        colour = State.EraserColor;
                                        break;

                                    default:
                                        colour = State.Colour1;
                                        break;
                                }
                                break;

                            case MouseButtons.Right:
                                switch (State.ToolType)
                                {
                                    case ToolType.Dropper:
                                        // Only require first pixel
                                        OnColourSelected(pixel.PixelColour, false);
                                        return;

                                    case ToolType.Eraser:
                                        colour = State.EraserColor;
                                        break;

                                    default:
                                        colour = State.Colour2;
                                        break;
                                }
                                break;

                            default:
                                RefreshDisplay();
                                return;
                        }

                        if (State.ToolType.Equals(ToolType.Rainbow))
                        {
                            colour = Rainbow(pixel, e.Button.Equals(MouseButtons.Left), ref State.CurrentRainbowColourIndex, ref _lastRainbowPixel);
                        }

                        if (State.ToolType.Equals(ToolType.MirrorX))
                        {
                            Pixel inversePixel = Texture.PixelList.FirstOrDefault(o => o.X.Equals(_width - 1 - pixel.X) && o.Y.Equals(pixel.Y));

                            inversePixel.PixelColour = colour;
                        }
                        else if (State.ToolType.Equals(ToolType.MirrorY))
                        {
                            Pixel inversePixel = Texture.PixelList.FirstOrDefault(o => o.Y.Equals(_height - 1 - pixel.Y) && o.X.Equals(pixel.X));

                            inversePixel.PixelColour = colour;
                        }

                        if (State.ToolType.Equals(ToolType.Texturiser))
                        {
                            colour = Randomiser(colour);
                        }

                        if (State.ToolType.Equals(ToolType.FloodFill))
                        {
                            var currentColour = pixel.PixelColour;

                            var floodX = pixel.X;

                            var floodY = pixel.Y;

                            var result = FloodFill(currentColour, colour, floodX, floodY, Texture);
                        }
                        else
                        {
                            pixel.PixelColour = colour;
                        }
                    }
                }

                HasChanged = true;

                RefreshDisplay();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Fire colour selected event
        /// </summary>
        /// <param name="colour">The colour selected</param>
        /// <param name="isColour1">Is fore colour</param>
        private void OnColourSelected(Color colour, bool isColour1)
        {
            ColourSelected?.Invoke(colour, isColour1);
        }

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
        /// Use mouse wheel to Zoom in/out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxImageMouseWheel(object sender, MouseEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }
        /// <summary>
        /// Paint the picture box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxImagePaint(object sender, PaintEventArgs e)
        {
            try
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
                    for (var y = 0; y < State.BrushSize; y++)
                    {
                        for (var x = 0; x < State.BrushSize; x++)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Yellow)), cursorX + (x * Zoom), cursorY + (y * Zoom), Zoom, Zoom);
                        }
                    }
                }

                g.Flush();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        #endregion Form events

        #region Public methods

        /// <summary>
        /// Add item to undo stack
        /// </summary>
        public void AddItem()
        {
            _undoManager.AddItem(Texture.Clone());

            UndoRedoHappened();
        }

        /// <summary>
        /// Clear the current texture
        /// </summary>
        public void Clear()
        {
            try
            {
                var newTexture = GetBlankTexture(Texture.Width, Texture.Height);

                Texture = newTexture.Clone();

                AddItem();

                RefreshDisplay();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Load a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        public void LoadFile(string fileName = "")
        {
            try
            {
                using (var image = (Image)FileHelper.LoadFile(fileName))
                {
                    if (image is null)
                    {
                        return;
                    }

                    FileName = fileName;

                    Texture = GetTextureFromImage(image);

                    _undoManager = new UndoManagerAction<Texture>(_log);

                    Text = FileName.FileDetails()?.Name.ToUpper();

                    AddItem();

                    _width = Texture.Width;
                    _height = Texture.Height;
                }

                RefreshDisplay();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
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

        /// <summary>
        /// Refresh the display
        /// </summary>
        public void RefreshDisplay()
        {
            if (Texture.PixelList.Count > 0)
            {
                pictureBoxImage.Width = Texture.Width * Zoom;
                pictureBoxImage.Height = Texture.Height * Zoom;
            }

            pictureBoxImage.Invalidate(true);
        }

        /// <summary>
        /// Save a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        public bool SaveFile(string fileName = "")
        {
            try
            {
                using (var tmp = BitmapFromTexture(Texture.Clone()))
                {
                    FileHelper.SaveFile((Image)tmp, fileName);

                    // Set flag back to false
                    HasChanged = false;
                }

                RefreshDisplay();

                return true;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                return false;
            }
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
        #endregion Public methods
    }
}