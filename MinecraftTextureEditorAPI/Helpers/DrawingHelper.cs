using MinecraftTextureEditorAPI.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        /// Stack-based floodfill routine
        /// </summary>
        /// <param name="currentColour">The current colour</param>
        /// <param name="newColour">The new colour</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="texture">The texture</param>
        /// <returns>Image</returns>
        public static Bitmap FloodFill(this Bitmap image, Color currentColour, Color newColour, int x, int y)
        {
            var width = image.Width;
            var height = image.Height;

            var tmp = new Bitmap(width, height);

            Queue<Point> pixels = new Queue<Point>();

            pixels.Enqueue(new Point(x, y));

            var g = Graphics.FromImage(tmp);

            g.DrawImageUnscaled(image, 0, 0);

            while (pixels.Count > 0)
            {
                Point a = pixels.Dequeue();

                //make sure we stay within bounds
                if (InBounds(a.X, a.Y, width, height))
                {
                    var currentPixelColour = tmp.GetPixel(a.X, a.Y);

                    if (ColourMatch(currentPixelColour, currentColour) && !ColourMatch(currentColour, newColour))
                    {
                        tmp.SetPixel(a.X, a.Y, newColour);

                        // Check bounds and colour before queuing next point
                        pixels.Enqueue(new Point(a.X - 1, a.Y));
                        pixels.Enqueue(new Point(a.X + 1, a.Y));
                        pixels.Enqueue(new Point(a.X, a.Y - 1));
                        pixels.Enqueue(new Point(a.X, a.Y + 1));
                    }
                }
            }

            g.Flush();

            return (Bitmap)tmp.Clone();
        }

        /// <summary>
        /// Returns a blank list of pixels using the correct coodrinate structure
        /// </summary>
        /// <param name="width">Pixels wide</param>
        /// <param name="height">Pixels high</param>
        /// <returns>List(Pixel)</returns>
        public static List<Pixel> GetBlankPixels(int width, int height)
        {
            var result = new List<Pixel>();

            for (int y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var pixel = new Pixel() { X = x, Y = y, PixelColour = EraserColour };

                    result.Add(pixel);
                }
            }

            return result;
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
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>Color</returns>
        public static Color GetColour(Bitmap image, int x, int y)
        {
            try
            {
                return image.GetPixel(x, y);
            }
            catch
            {
                return EraserColour;
            }
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
        /// Rainbow
        /// </summary>
        /// <param name="pixel">The position of the pixel</param>
        /// <param name="leftButton">Left button used</param>
        /// <param name="currentRainbowColour">The current rainbow colour</param>
        /// <param name="lastRainbowPixel">The last rainbox position</param>
        /// <returns></returns>
        public static Color Rainbow(Point pixel, bool leftButton, ref int currentRainbowColour, ref Point lastRainbowPixel)
        {
            var colour = RainbowColours[currentRainbowColour];

            var moveNextColour = false;

            if (leftButton)
            {
                if (pixel.X != lastRainbowPixel.X && pixel.Y != lastRainbowPixel.Y)
                {
                    moveNextColour = true;
                }
            }
            else
            {
                if (pixel.X != lastRainbowPixel.X || pixel.Y != lastRainbowPixel.Y)
                {
                    moveNextColour = true;
                }
            }

            if (moveNextColour)
            {
                currentRainbowColour = currentRainbowColour >= RainbowColours.Count - 1 ? 0 : currentRainbowColour + 1;

                lastRainbowPixel.X = pixel.X;
                lastRainbowPixel.Y = pixel.Y;
            }

            return colour;
        }

        /// <summary>
        /// Draw a shape
        /// </summary>
        /// <param name="image">The image to draw on</param>
        /// <param name="colour1">Colour 1</param>
        /// <param name="colour2">Colour 2</param>
        /// <param name="rectangle">The rectangle</param>
        /// <param name="shapeType">The shape type</param>
        /// <param name="brushSize">The brush size</param>
        /// <param name="fill">Fill or not</param>
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
                    g.DrawArc(colourPen, rectangle, 0, 180);
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
                    var star = fill? Properties.Resources.star:Properties.Resources.star_outline;
                    var tmpStar = new Bitmap(rectangle.Width, rectangle.Height);
                    var s = Graphics.FromImage(tmpStar);

                    s.Clear(Color.FromArgb(0, 0, 0, 0));

                    s.DrawImage(star, 0,0,rectangle.Width, rectangle.Height);

                    for(int y =0; y< tmpStar.Height; y++)
                    {
                        for(int x = 0; x< tmpStar.Width; x++)
                        {
                            if(tmpStar.GetPixel(x,y).R.Equals(0))
                            {
                                //Add the pixels with the new colour
                                tmp.SetPixel(x + rectangle.X, y+rectangle.Y, colour);
                            }
                        }
                    }

                    s.Flush();
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
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        var transparentColour = GetColour(transparencyMap, x, y);

                        if (transparentColour.A.Equals(0))
                        {
                            tmp.SetPixel(x, y, transparentColour);
                        }
                    }
                }
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
        /// </summary>
        /// <param name="image"></param>
        /// <param name="colour"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Image</returns>
        public static Bitmap SetColour(this Bitmap image, Color colour, int x, int y)
        {
            var tmp = new Bitmap(image.Width, image.Height);

            var g = Graphics.FromImage(tmp);

            g.DrawImageUnscaled(image, 0, 0);

            tmp.SetPixel(x, y, colour);

            g.Flush();

            return (Bitmap)tmp.Clone();
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
        /// <returns>int</returns>
        public static int Zoomify(this int value, int zoom)
        {
            return value / zoom * zoom;
        }

        #endregion Public methods
    }
}