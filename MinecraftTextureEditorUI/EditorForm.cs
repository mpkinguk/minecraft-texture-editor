using GenericUndoRedoManagerAPI;
using log4net;
using MinecraftTextureEditorAPI.Helpers;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;
using static MinecraftTextureEditorAPI.Helpers.FileHelper;
using System;
using System.Drawing;
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
        public Bitmap Texture { get; set; }

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
        private Point _lastRainbowPosition = new Point();
        private bool _showGrid;
        private bool _showTransparentGrid;
        private UndoManagerAction<Bitmap> _undoManager;
        private int _width;
        private bool _ctrlIsDown;
        private bool _altIsDown;
        private bool _shiftIsDown;

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
                Texture = new Bitmap(width, height);

                _width = width;
                _height = height;          

                _undoManager = new UndoManagerAction<Bitmap>(_log);

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

                KeyDown += EditorFormKeyDown;
                KeyUp += EditorFormKeyDown;

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
        /// Update flags for modifier keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorFormKeyDown(object sender, KeyEventArgs e)
        {
            _ctrlIsDown = e.Control;
            _altIsDown = e.Alt;
            _shiftIsDown = e.Shift;
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

                var cursorPosition = new Point((_cursor.X / Zoom), (_cursor.Y / Zoom));

                var tmpTexture = (Bitmap)Texture.Clone();

                var cursorColour = GetColour(tmpTexture, cursorPosition.X, cursorPosition.Y);

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        switch (State.ToolType)
                        {
                            case ToolType.Dropper:
                                // Only require first pixel
                                OnColourSelected(cursorColour, true);
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
                                OnColourSelected(cursorColour, false);
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

                for (var y = 0; y < State.BrushSize; y++)
                {
                    for (var x = 0; x < State.BrushSize; x++)
                    {
                        var pixelPosition = new Point(cursorPosition.X + x, cursorPosition.Y + y);

                        // If out of bounds, continue onto next pixel
                        if (pixelPosition.X > Texture.Width - 1 || (pixelPosition.Y > Texture.Height - 1))
                        {
                            continue;
                        }

                        // Do not draw pixel if transparency lock is on and the underlying pixel is transparent
                        if (State.TransparencyLock)
                        {
                            var pixelColour = GetColour(tmpTexture, pixelPosition.X, pixelPosition.Y);

                            if (pixelColour.A.Equals(0))
                            {
                                continue;
                            }
                        }

                        if (State.ToolType.Equals(ToolType.Texturiser))
                        {
                            colour = Randomiser(colour);
                        }

                        if (State.ToolType.Equals(ToolType.Rainbow))
                        {
                            colour = Rainbow(pixelPosition, e.Button.Equals(MouseButtons.Left), ref State.CurrentRainbowColourIndex, ref _lastRainbowPosition);
                        }

                        // Use flag so we can do both at the same time :)
                        if (State.Modifiers.Equals(Modifier.MirrorX) || State.Modifiers.Equals(Modifier.MirrorX | Modifier.MirrorY))
                        {
                            Point inversePixel = new Point(_width - 1 - pixelPosition.X, pixelPosition.Y);

                            tmpTexture = tmpTexture.SetColour(colour, inversePixel.X, inversePixel.Y);

                            if(State.Modifiers.Equals(Modifier.MirrorY) || State.Modifiers.Equals(Modifier.MirrorX | Modifier.MirrorY))
                            {
                                inversePixel = new Point(pixelPosition.X, _height - 1 - pixelPosition.Y);

                                tmpTexture = tmpTexture.SetColour(colour, inversePixel.X, inversePixel.Y);

                                inversePixel = new Point(_width - 1 - pixelPosition.X, _height - 1 - pixelPosition.Y);

                                tmpTexture = tmpTexture.SetColour(colour, inversePixel.X, inversePixel.Y);
                            }

                            if (State.Modifiers.Equals(Modifier.MirrorY))
                            {
                                inversePixel = new Point(pixelPosition.X, _height - 1 - pixelPosition.Y);

                                tmpTexture = tmpTexture.SetColour(colour, inversePixel.X, inversePixel.Y);
                            }
                        }

                        // Break out of this as we do not want to it repeat up to 8 times for the brushsize!
                        if (State.ToolType.Equals(ToolType.FloodFill))
                        {
                            var currentColour = cursorColour;

                            var floodX = pixelPosition.X;

                            var floodY = pixelPosition.Y;

                            tmpTexture = tmpTexture.FloodFill(currentColour, colour, floodX, floodY);
                        } 
                        else 
                        { 
                            tmpTexture = tmpTexture.SetColour(colour, pixelPosition.X, pixelPosition.Y);
                        }
                    }
                }

                Texture = (Bitmap)tmpTexture.Clone();

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

                if (!_ctrlIsDown)
                {
                    return;
                }

                var delta = Math.Sign(e.Delta);

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
            if(Texture is null)
            {
                return;
            }

            try
            {
                var g = e.Graphics;

                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                var srcRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
                var destRectangle = new Rectangle(0, 0, srcRectangle.Width * Zoom, srcRectangle.Height * Zoom);
                var borderRectangle = new Rectangle(0, 0, destRectangle.Width - 1, destRectangle.Height - 1);

                for (int y = 0; y < Texture.Height; y++)
                {
                    for (int x = 0; x < Texture.Width; x++)
                    {
                        var pixelRectangle = new Rectangle(x * Zoom, y * Zoom, Zoom, Zoom);

                        if (pixelRectangle.IntersectsWith(e.ClipRectangle))
                        {
                            var pixelColour = Texture.GetPixel(x, y);

                            g.FillRectangle(new SolidBrush(pixelColour), pixelRectangle);

                            if (ShowGrid && Zoom > 6)
                            {
                                g.DrawLine(Pens.Black, x * Zoom, 0, x * Zoom, pictureBoxImage.Height);

                                g.DrawLine(Pens.Black, 0, y * Zoom, pictureBoxImage.Width, y * Zoom);
                            }
                        }
                    }
                }

                g.DrawRectangle(Pens.Black, borderRectangle);

                // Show cursor

                int cursorX = _cursor.X / Zoom * Zoom;
                int cursorY = _cursor.Y / Zoom * Zoom;

                if (!CursorOutOfBounds())
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Yellow)), cursorX, cursorY, Zoom * State.BrushSize, Zoom * State.BrushSize);
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
            try
            {
                if (Texture is null)
                {
                    return;
                }

                _undoManager.AddItem((Bitmap)Texture.Clone());

                UndoRedoHappened();
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Clear the current texture
        /// </summary>
        public void Clear()
        {
            try
            {
                Texture = new Bitmap(Texture.Width, Texture.Height);

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
                using (var image = (Bitmap)FileHelper.LoadFile(fileName))
                {
                    if (image is null)
                    {
                        return;
                    }

                    FileName = fileName;

                    Texture = (Bitmap)image.Clone();

                    //pictureBoxImage.SizeMode = PictureBoxSizeMode.Normal;

                    //Texture = GetTextureFromImage(image);

                    _undoManager = new UndoManagerAction<Bitmap>(_log);

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
            try
            {
                if (RedoEnabled)
                {
                    Texture = (Bitmap)_undoManager.Redo().Clone();

                    RefreshDisplay();

                    UndoRedoHappened();
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Refresh the display
        /// </summary>
        public void RefreshDisplay()
        {
            try
            {
                if (Texture is null)
                {
                    return;
                }

                //pictureBoxImage.SizeMode = PictureBoxSizeMode.Normal;

                pictureBoxImage.Width = Texture.Width * Zoom;
                pictureBoxImage.Height = Texture.Height * Zoom;

                //pictureBoxImage.SizeMode = PictureBoxSizeMode.AutoSize;

                pictureBoxImage.Invalidate(true);
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }

        /// <summary>
        /// Save a file with optional filename
        /// </summary>
        /// <param name="fileName">The filename</param>
        public bool SaveFile(string fileName = "")
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = SaveFileName();
                }

                Texture.Save(fileName);

                // Set flag back to false
                HasChanged = false;

                // Clear undos
                _undoManager.Clear();

                // And tell MDI about it
                UndoRedoHappened();

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
            try
            {
                if (UndoEnabled)
                {
                    Texture = (Bitmap)_undoManager.Undo().Clone();

                    RefreshDisplay();

                    UndoRedoHappened();
                }
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
            }
        }
        #endregion Public methods
    }
}