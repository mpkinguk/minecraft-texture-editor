using MinecraftTextureEditorAPI;
using MinecraftTextureEditorAPI.Model;
using GenericUndoRedoManagerAPI;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static MinecraftTextureEditorUI.DrawingToolsForm;

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
        public bool ShowGrid { get; set; }

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

        private const int StartZoom = 16;

        private int _width;

        private int _height;

        private Point _cursor;

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
        public EditorForm()
        {
            InitializeComponent();

            Init();
        }

        #region Private methods

        /// <summary>
        /// Init variables
        /// </summary>
        private void Init()
        {
            Texture = DrawingHelper.GetBlankTexture(16, 16);

            _width = 15;
            _height = 15;

            _undoManager = new UndoManagerAction<Texture>();

            AddItem();

            FormClosing += EditorFormFormClosing;

            pictureBoxImage.Paint += PictureBoxImagePaint;
            pictureBoxImage.MouseMove += EditorMousePaintPixel;
            pictureBoxImage.MouseDown += EditorMousePaintPixel;
            pictureBoxImage.MouseUp += PictureBoxImageMouseUp;
            pictureBoxImage.MouseWheel += PictureBoxImageMouseWheel;
            pictureBoxImage.BackgroundImage = Properties.Resources.transparentGrid;

            MouseWheel += PictureBoxImageMouseWheel;
            MouseMove += EditorFormMouseMove;
            LostFocus += EditorFormLostFocus;

            Zoom = StartZoom;

            EraserColor = Color.Transparent;

            HasChanged = false;

            RefreshDisplay();
        }

        /// <summary>
        /// Is the cursor out of bounds?
        /// </summary>
        /// <returns>Bool</returns>
        private bool CursorOutOfBounds()
        {
            return (_cursor.X < 0 || _cursor.Y < 0 || _cursor.X >= pictureBoxImage.Width || _cursor.Y >= pictureBoxImage.Height);
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
        /// <param name="isForeColour">Is fore colour</param>
        private void OnColourSelected(Color colour, bool isForeColour)
        {
            ColourSelected?.Invoke(colour, isForeColour);

            if (isForeColour)
            {
                Colour1 = colour;
            }
            else
            {
                Colour2 = colour;
            }
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
                g.FillRectangle(new SolidBrush(pixel.PixelColour), pixel.X * Zoom, pixel.Y * Zoom, Zoom, Zoom);

                // Otherwise you can't see what you're painting!
                if (Zoom > StartZoom / 2 && ShowGrid)
                {
                    g.DrawRectangle(Pens.Black, pixel.X * Zoom, pixel.Y * Zoom, Zoom, Zoom);
                }
            }

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

            pixel.PixelColour = colour;

            if (ToolType.Equals(ToolType.MirrorX))
            {
                Pixel inversePixel = Texture.PixelList.Where(o => o.X.Equals(_width - pixel.X) && o.Y.Equals(pixel.Y)).FirstOrDefault();

                inversePixel.PixelColour = colour;
            }
            else if (ToolType.Equals(ToolType.MirrorY))
            {
                Pixel inversePixel = Texture.PixelList.Where(o => o.Y.Equals(_height - pixel.Y) && o.X.Equals(pixel.X)).FirstOrDefault();

                inversePixel.PixelColour = colour;
            }

            HasChanged = true;

            RefreshDisplay();
        }

        /// <summary>
        /// Refresh the display
        /// </summary>
        public void RefreshDisplay()
        {
            pictureBoxImage.Width = (_width + 1) * Zoom;
            pictureBoxImage.Height = (_height + 1) * Zoom;

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

                if(image.Width > 256 || image.Height > 256) 
                { 
                    
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
            using (var tmp = new Bitmap(_width + 1, _height + 1))
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