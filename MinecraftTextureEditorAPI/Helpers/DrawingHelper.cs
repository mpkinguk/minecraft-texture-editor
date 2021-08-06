using MinecraftTextureEditorAPI.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        /// <summary>
        /// Tool types
        /// </summary>
        public enum ToolType
        {
            Pen,
            Eraser,
            Dropper,
            Texturiser,
            FloodFill,
            Rainbow
        }

        [Flags]
        public enum Modifier
        {
            MirrorX = 1,
            MirrorY = 2,
            TransparencyLock = 4
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
        /// Generate a bitmap from a texture
        /// </summary>
        /// <param name="texture">The texture</param>
        /// <returns>Bitmap</returns>
        public static Bitmap BitmapFromTexture(Texture texture)
        {
            var width = texture.Width;
            var height = texture.Height;

            var tmp = new Bitmap(width, height);

            foreach (Pixel pixel in texture.PixelList)
            {
                tmp.SetPixel(pixel.X, pixel.Y, pixel.PixelColour);
            }

            return tmp;
        }

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

            //var found = false;

            //for (int py = 0; py < height; py++)
            //{
            //    for (int px = 0; px < width; px++)
            //    {
            //        if(ColourMatch((image).GetPixel(x, y), currentColour))
            //        {
            //            found = true;
            //            break;
            //        }
            //    }

            //    if (found)
            //    {
            //        break;
            //    }
            //}           

            //if (!found)
            //{
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
            //}
            //else
            //{
            //    var g = Graphics.FromImage(image);

            //    g.Clear(newColour);

            //    g.Flush();
            //}

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
        /// Returns a blank texture
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        /// <returns>Texture</returns>
        public static Texture GetBlankTexture(int width, int height)
        {
            return new Texture(width, height);
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
                //using (var tmp = new Bitmap(1, 1))
                //{
                //    var rectangle = new Rectangle(0, 0, 1, 1);

                //    using (var g = Graphics.FromImage(tmp))
                //    {
                //        g.DrawImage(image, rectangle, new Rectangle(x, y, 1, 1), GraphicsUnit.Pixel);
                //    }

                    return image.GetPixel(x, y);
                //}
            }
            catch
            {
                return EraserColour;
            }
        }
        /// <summary>
        /// Returns a colour at the coordinates provided
        /// </summary>
        /// <param name="texture">The texture</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>Color</returns>
        public static Color GetColour(Texture texture, int x, int y)
        {
            return GetPixel(texture, x, y).PixelColour;
        }

        /// <summary>
        /// Returns a pixel at the coordinates provided
        /// </summary>
        /// <param name="texture">The texture</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <returns>Pixel</returns>
        public static Pixel GetPixel(Texture texture, int x, int y)
        {
            return texture.PixelList.FirstOrDefault(o => o.X.Equals(x) && o.Y.Equals(y));
        }

        /// <summary>
        /// Converts an image to a pixel list
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>List(Pixel)</returns>
        public static List<Pixel> GetPixelsFromImage(Bitmap image)
        {
            var result = new List<Pixel>();

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var colour = GetColour(image, x, y);
                    var pixel = new Pixel() { PixelColour = colour, X = x, Y = y };
                    result.Add(pixel);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a texture from an image
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>Texture</returns>
        public static Texture GetTextureFromImage(Bitmap image)
        {
            return new Texture(image);
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
        /// <param name="pixel">The pixel</param>
        /// <param name="leftButton">Left button used</param>
        /// <returns>Color</returns>
        public static Color Rainbow(Pixel pixel, bool leftButton, ref int currentRainbowColour, ref Pixel lastRainbowPixel)
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

        #endregion Public methods
    }
}