using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftTextureEditorAPI.Helpers
{
    /// <summary>
    /// DrawingHelper class
    /// </summary>
    public static class DrawingHelper
    {
        #region Enums

        /// <summary>
        /// Colour selection types
        /// </summary>
        public enum ColourSelectionType
        {
            ColourWheel,
            Saturation,
            Alpha
        }

        [Flags]
        public enum Modifier
        {
            MirrorX = 1,
            MirrorY = 2,
            TransparencyLock = 4
        }

        /// <summary>
        /// Shape types
        /// </summary>
        public enum ShapeType
        {
            Line,
            Square,
            Rectangle,
            Circle,
            Ellipse,
            SemiCircle,
            Triangle,
            Star
        }

        /// <summary>
        /// Tool types
        /// </summary>
        public enum ToolType
        {
            Draw,
            Eraser,
            ColourPicker,
            Texturiser,
            FloodFill,
            Rainbow,
            Shape
        }

        #endregion Enums

        #region Public constants

        /// <summary>
        /// The eraser colour
        /// </summary>
        public static Color EraserColour = Color.FromArgb(0, 0, 0, 0);

        public static List<Color> RainbowColours = new List<Color> { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };

        #endregion Public constants

        #region Public methods

        /// <summary>
        /// Does the color match exactly
        /// </summary>
        /// <param name="a">Colour a</param>
        /// <param name="b">Colour b</param>
        /// <returns>Bool</returns>
        public static bool ColourMatch(Color a, Color b)
        {
            return (a.ToArgb().Equals(b.ToArgb()));
        }

        /// <summary>
        /// Does the color match exactly
        /// </summary>
        /// <param name="a">Colour a</param>
        /// <param name="b">Colour b</param>
        /// <returns>Bool</returns>
        public static bool ColourMatch(PixelData a, Color b)
        {
            return a.alpha == b.A && a.red == b.R && a.green == b.G && a.blue == b.B;
        }

        /// <summary>
        /// Stack-based flood-fill routine
        /// Override for PointF
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="currentColour">The current colour</param>
        /// <param name="newColour">The new colour</param>
        /// <param name="position">The texture</param>
        /// <returns>Bitmap</returns>
        public static Bitmap FloodFill(this Bitmap bitmap, Color currentColour, Color newColour, PointF position)
        {
            return FloodFill(bitmap, currentColour, newColour, (int)position.X, (int)position.Y);
        }


        /// <summary>
        /// Stack-based flood-fill routine
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="currentColour">The current colour</param>
        /// <param name="newColour">The new colour</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>Image</returns>
        public static Bitmap FloodFill(this Bitmap bitmap, Color currentColour, Color newColour, int x, int y)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;

            var pixelColour = new PixelData { alpha = newColour.A, red = newColour.R, green = newColour.G, blue = newColour.B };

            var tmp = new UnsafeBitmapHelper(bitmap);

            var pixels = new Queue<Point>();

            pixels.Enqueue(new Point(x, y));

            tmp.LockBitmap();

            while (pixels.Count > 0)
            {
                var a = pixels.Dequeue();

                //make sure we stay within bounds
                if (InBounds(a.X, a.Y, width, height))
                {
                    var currentPixelColour = tmp.GetPixel(a.X, a.Y);

                    if (ColourMatch(currentPixelColour, currentColour) && !ColourMatch(currentColour, newColour))
                    {
                        tmp.SetPixel(a.X, a.Y, pixelColour);

                        // Check bounds and colour before queuing next point
                        pixels.Enqueue(new Point(a.X - 1, a.Y));
                        pixels.Enqueue(new Point(a.X + 1, a.Y));
                        pixels.Enqueue(new Point(a.X, a.Y - 1));
                        pixels.Enqueue(new Point(a.X, a.Y + 1));
                    }
                }
            }

            tmp.UnlockBitmap();

            return (Bitmap)tmp.Bitmap.Clone();
        }

        /// <summary>
        /// Get colour using PictureBox object
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>Color</returns>
        public static Color GetColour(PictureBox pictureBox, int x, int y)
        {
            try
            {
                using (var tmp = new Bitmap(pictureBox.Width, pictureBox.Height))
                {
                    var rectangle = new Rectangle(0, 0, tmp.Width, tmp.Height);

                    var p = GraphicsUnit.Pixel;

                    using (var g = Graphics.FromImage(tmp))
                    {
                        g.DrawImage(pictureBox.Image, rectangle, pictureBox.Image.GetBounds(ref p), GraphicsUnit.Pixel);
                    }

                    return tmp.GetPixel(x, y);
                }
            }
            catch
            {
                return EraserColour;
            }
        }

        /// <summary>
        /// Get colour using Image object
        /// Override for float
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>Color</returns>
        public static Color GetColour(Bitmap bitmap, float x, float y)
        {
            return GetColour(bitmap, (int)x, (int)y);
        }

        /// <summary>
        /// Get colour using Image object
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>Color</returns>
        public static Color GetColour(Bitmap bitmap, int x, int y)
        {
            try
            {
                var tmp = new UnsafeBitmapHelper(bitmap);

                tmp.LockBitmap();

                var colour = tmp.GetPixel(x, y);

                tmp.UnlockBitmap();

                return colour.ToColour();
            }
            catch
            {
                return EraserColour;
            }
        }

        /// <summary>
        /// Draw a shape
        /// Override for RectangleF
        /// </summary>
        /// <param name="image">The image to draw on</param>
        /// <param name="colour">The colour</param>
        /// <param name="rectangle">The rectangle</param>
        /// <param name="shapeType">The shape type</param>
        /// <param name="brushSize">The brush size</param>
        /// <param name="fill">Fill or not</param>
        /// <param name="transparencyLock">Use transparency lock</param>
        /// <returns>Bitmap</returns>
        public static Image GetShape(Image image, Color colour, RectangleF rectangle, ShapeType shapeType, int brushSize,
            bool fill = false, bool transparencyLock = false)
        {
            var rectangleInt = new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width,
                (int)rectangle.Height);

            return GetShape(image, colour, rectangleInt, shapeType, brushSize, fill, transparencyLock);
        }

        /// <summary>
        /// Draw a shape
        /// </summary>
        /// <param name="image">The image to draw on</param>
        /// <param name="colour">The colour</param>
        /// <param name="rectangle">The rectangle</param>
        /// <param name="shapeType">The shape type</param>
        /// <param name="brushSize">The brush size</param>
        /// <param name="fill">Fill or not</param>
        /// <param name="transparencyLock">Use transparency lock</param>
        /// <returns>Bitmap</returns>
        public static Image GetShape(Image image, Color colour, Rectangle rectangle, ShapeType shapeType, int brushSize, bool fill = false, bool transparencyLock = false)
        {
            var tmp = new Bitmap(image.Width, image.Height);

            //Correct the rectangle if width or height is negative, but not for line
            if (!shapeType.Equals(ShapeType.Line))
            {
                rectangle.Validate();
            }

            var transparencyMap = (Bitmap)image.Clone();
            var transparentColour = new PixelData() { alpha = 0, red = 0, green = 0, blue = 0 };

            var square = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Width);
            var colourBrush = new SolidBrush(colour);
            var colourPen = new Pen(colour, brushSize);

            var g = Graphics.FromImage(tmp);

            g.DrawImageUnscaled(image, 0, 0);

            switch (shapeType)
            {
                case ShapeType.Line:
                    g.DrawLine(colourPen, rectangle.X, rectangle.Y, rectangle.Right, rectangle.Bottom);
                    break;

                case ShapeType.Rectangle:
                    if (fill)
                    {
                        g.FillRectangle(colourBrush, rectangle);
                    }

                    g.DrawRectangle(colourPen, rectangle);
                    break;

                case ShapeType.Circle:
                    if (fill)
                    {
                        g.FillEllipse(colourBrush, square);
                    }

                    g.DrawEllipse(colourPen, square);

                    break;

                case ShapeType.Ellipse:
                    if (fill)
                    {
                        g.FillEllipse(colourBrush, rectangle);
                    }

                    g.DrawEllipse(colourPen, rectangle);
                    break;

                case ShapeType.SemiCircle:
                    if (fill)
                    {
                        g.FillPie(colourBrush, rectangle, 0, 180);
                    }

                    g.DrawPie(colourPen, rectangle, 0, 180);
                    break;

                case ShapeType.Triangle:

                    var triangle = new GraphicsPath();

                    triangle.AddLines(new PointF[] { new PointF(rectangle.Left, rectangle.Bottom), new PointF(rectangle.Left + (rectangle.Width / 2), rectangle.Top), new PointF(rectangle.Right, rectangle.Bottom) });
                    triangle.CloseFigure();

                    if (fill)
                    {
                        g.FillPath(colourBrush, triangle);
                    }
                    g.DrawPath(colourPen, triangle);
                    break;

                case ShapeType.Star:
                    var star = fill ? Properties.Resources.star : Properties.Resources.star_outline;

                    var tmpStar = new Bitmap(star, rectangle.Width, rectangle.Height);

                    var h = tmpStar.Height;
                    var w = tmpStar.Width;

                    var t = new UnsafeBitmapHelper(tmpStar);
                    var i = new UnsafeBitmapHelper(tmp);

                    unsafe
                    {
                        var pixelColour = colour.ToPixelData();

                        t.LockBitmap();
                        i.LockBitmap();

                        for (int y = 0; y < h; y++)
                        {
                            for (int x = 0; x < w; x++)
                            {
                                if (t.GetPixel(x, y).red == 0)
                                {
                                    i.SetPixel(x + rectangle.X, y + rectangle.Y, pixelColour);
                                }
                            }
                        }

                        i.UnlockBitmap();
                        t.UnlockBitmap();
                    }

                    tmp = (Bitmap)i.Bitmap.Clone();

                    break;

                case ShapeType.Square:
                default:
                    if (fill)
                    {
                        g.FillRectangle(colourBrush, square);
                    }
                    g.DrawRectangle(colourPen, square);
                    break;
            }

            g.Flush();

            if (transparencyLock)
            {
                var h = tmp.Height;
                var w = tmp.Width;

                var t = new UnsafeBitmapHelper(transparencyMap);
                var i = new UnsafeBitmapHelper(tmp);

                unsafe
                {
                    i.LockBitmap();
                    t.LockBitmap();

                    Parallel.For(0, h, y =>
                    {
                        for (int x = 0; x < w; x++)
                        {
                            var pixelColour = t.GetPixel(x, y);

                            if (pixelColour.alpha.Equals(0))
                            {
                                i.SetPixel(x, y, pixelColour);
                            }
                        }
                    });

                    t.UnlockBitmap();
                    i.UnlockBitmap();
                }

                tmp = (Bitmap)i.Bitmap.Clone();
            }

            return tmp;
        }

        /// <summary>
        /// Basic method to add shape menu items to a collection
        /// </summary>
        /// <param name="parent"></param>
        /// <returns>Array of ToolStripMenuItem</returns>
        public static ToolStripMenuItem[] GetShapeMenuItems()
        {
            List<ToolStripMenuItem> toolStripItems = new List<ToolStripMenuItem>();

            foreach (var shape in typeof(ShapeType).GetEnumNames())
            {
                var toolStripItemShape = new ToolStripMenuItem() { Name = shape, Text = shape };

                toolStripItems.Add(toolStripItemShape);
            }

            return toolStripItems.ToArray();
        }

        /// <summary>
        /// Is the point within the bounds of the texture
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        /// <returns>Bool</returns>
        public static bool InBounds(int x, int y, int width, int height)
        {
            return (x >= 0 && x < width &&
                         y >= 0 && y < height);
        }

        /// <summary>
        /// Paste
        /// </summary>
        /// <param name="image">The original image</param>
        /// <param name="pasteImage">The paste image</param>
        /// <param name="x">x coordinate of cursor</param>
        /// <param name="y">y coordinate of cursor</param>
        /// <returns>Bitmap</returns>
        public static Bitmap Paste(this Bitmap image, Bitmap pasteImage, int x, int y, bool transparencyLock = false, bool shift = false)
        {
            var tmp = new Bitmap(image.Width, image.Height);
            
            var transparencyMap = (Bitmap)image.Clone();
            var transparentColour = new PixelData() { alpha = 0, red = 0, green = 0, blue = 0 };        

            if (shift)
            {
                var g = Graphics.FromImage(tmp);

                g.DrawImageUnscaled(image, 0, 0);

                g.DrawImage(pasteImage, x, y);

                g.Flush();
            }
            else
            {
                tmp = new Bitmap(pasteImage); 
            }

            if (transparencyLock)
            {
                var h = tmp.Height;
                var w = tmp.Width;

                var t = new UnsafeBitmapHelper(transparencyMap);
                var i = new UnsafeBitmapHelper(tmp);

                unsafe
                {
                    i.LockBitmap();
                    t.LockBitmap();

                    Parallel.For(0, h, py =>
                    {
                        for (int px = 0; px < w; px++)
                        {
                            var pixelColour = t.GetPixel(px, py);

                            if (pixelColour.alpha.Equals(0))
                            {
                                i.SetPixel(px, py, pixelColour);
                            }
                        }
                    });

                    t.UnlockBitmap();
                    i.UnlockBitmap();
                }

                tmp = (Bitmap)i.Bitmap.Clone();
            }

            return tmp;
        }

        /// <summary>
        /// Rainbow
        /// </summary>
        /// <param name="pixel">The position of the pixel</param>
        /// <param name="leftButton">Left button used</param>
        /// <param name="currentRainbowColour">The current rainbow colour</param>
        /// <param name="lastRainbowPixel">The last rainbox position</param>
        /// <returns></returns>
        public static Color Rainbow(PointF pixel, bool leftButton, ref int currentRainbowColour, ref PointF lastRainbowPixel)
        {
            var colour = RainbowColours[currentRainbowColour];

            var moveNextColour = false;

            if (leftButton)
            {
                if ((int)pixel.X != (int)lastRainbowPixel.X && (int)pixel.Y != (int)lastRainbowPixel.Y)
                {
                    moveNextColour = true;
                }
            }
            else
            {
                if ((int)pixel.X != (int)lastRainbowPixel.X || (int)pixel.Y != (int)lastRainbowPixel.Y)
                {
                    moveNextColour = true;
                }
            }

            if (moveNextColour)
            {
                currentRainbowColour = currentRainbowColour >= RainbowColours.Count - 1 ? 0 : currentRainbowColour + 1;

                lastRainbowPixel.X = (int)pixel.X;
                lastRainbowPixel.Y = (int)pixel.Y;
            }

            return colour;
        }

        /// <summary>
        /// Randomiser
        /// </summary>
        /// <param name="colour">The color</param>
        /// <returns>Color</returns>
        public static Color Randomiser(Color colour)
        {
            var rnd = new Random();

            var brightnessTransform = rnd.NextDouble().Clamp(0.4, 0.8);

            var newColour = ColourHelper.TransformBrightness(colour, ColourHelper.ColorTransformMode.Hsb, brightnessTransform);

            return newColour;
        }

        /// <summary>
        /// Rotate/Flip an image
        /// </summary>
        /// <param name="image">The image</param>
        /// <param name="rotateFlipType">The rotate/flip type</param>
        /// <returns>Bitmap</returns>
        public static Bitmap RotateAndFlip(this Bitmap image, RotateFlipType rotateFlipType)
        {
            var img = new Bitmap(image.Width, image.Height);

            var g = Graphics.FromImage(img);

            g.DrawImageUnscaled(image, 0, 0);

            img.RotateFlip(rotateFlipType);

            g.Flush();

            return (Bitmap)img.Clone();
        }

        /// <summary>
        /// Set colour of image
        /// Override for float
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="colour">The colour</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="position">The position</param>
        /// <returns>Bitmap</returns>
        public static Bitmap SetColour(this Bitmap bitmap, Color colour, PointF position)
        {
            return SetColour(bitmap, colour, (int)position.X, (int)position.Y);
        }

        /// <summary>
        /// Set colour of image
        /// </summary>
        /// <param name="bitmap">The bitmap</param>
        /// <param name="colour">The colour</param>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <returns>Bitmap</returns>
        public static Bitmap SetColour(this Bitmap bitmap, Color colour, int x, int y)
        {
            var tmp = new UnsafeBitmapHelper(bitmap);

            tmp.LockBitmap();

            tmp.SetPixel(x, y, colour.ToPixelData());

            tmp.UnlockBitmap();

            return tmp.Bitmap;
        }

        /// <summary>
        /// Returns a colour object from pixeldata object
        /// </summary>
        /// <param name="colour">The pixeldata</param>
        /// <returns>colour</returns>
        public static Color ToColour(this PixelData pixelData)
        {
            return Color.FromArgb(pixelData.alpha, pixelData.red, pixelData.green, pixelData.blue);
        }

        /// <summary>
        /// Returns a pixeldata object from colour object
        /// </summary>
        /// <param name="colour">The colour</param>
        /// <returns>pixeldata</returns>
        public static PixelData ToPixelData(this Color colour)
        {
            return new PixelData() { alpha = colour.A, red = colour.R, green = colour.G, blue = colour.B };
        }

        /// <summary>
        /// Validate a rectangle and return correct coords
        /// </summary>
        /// <param name="rectangle">The rectangle</param>
        public static void Validate(this ref Rectangle rectangle)
        {
            //Sort out reverse rectangles
            if (rectangle.Width < 0)
            {
                var width = Math.Abs(rectangle.Width);

                rectangle.X -= width;
                rectangle.Width = width;
            }

            if (rectangle.Height < 0)
            {
                var height = Math.Abs(rectangle.Height);

                rectangle.Y -= height;
                rectangle.Height = height;
            }
        }

        /// <summary>
        /// Round out the value to the zoom equivalent
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="zoom">The zoom value</param>
        /// <returns>float</returns>
        public static int Zoomify(this float value, int zoom)
        {
            return (int)value / zoom * zoom;
        }

        #endregion Public methods
    }
}