using MinecraftTextureEditorAPI.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftTextureEditorAPI.Helpers
{
    public static class DrawingHelper
    {
        #region Enums

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
            Rainbow,
            MirrorX,
            MirrorY,
            TransparencyLock
        }

        /// <summary>
        /// Colour selection types
        /// </summary>
        public enum ColourSelectionType
        {
            ColourWheel,
            Saturation,
            Alpha
        }

        #endregion Enums

        public static List<Color> RainbowColours = new List<Color> { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };

        /// <summary>
        /// The eraser colour
        /// </summary>
        public static Color EraserColour = Color.FromArgb(0, 0, 0, 0);

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
        public static Color GetColour(Image image, int x, int y)
        {
            try
            {
                using (var tmp = new Bitmap(1, 1))
                {
                    var rectangle = new Rectangle(0, 0, 1, 1);

                    using (var g = Graphics.FromImage(tmp))
                    {
                        g.DrawImage(image, rectangle, new Rectangle(x, y, 1, 1), GraphicsUnit.Pixel);
                    }

                    return tmp.GetPixel(0, 0);
                }
            }
            catch
            {
                return EraserColour;
            }
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
        /// Converts an image to a pixel list
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>List(Pixel)</returns>
        public static List<Pixel> GetPixelsFromImage(Image image)
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
        public static Texture GetTextureFromImage(Image image)
        {
            return new Texture(image);
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
        /// Stack-based floodfill routine
        /// </summary>
        /// <param name="currentColour">The current colour</param>
        /// <param name="newColour">The new colour</param>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        /// <param name="texture">The texture</param>
        public static double FloodFill(Color currentColour, Color newColour, int x, int y, Texture texture)
        {
            var start = DateTime.Now;

            var width = texture.Width;
            var height = texture.Height;

            var pixelCount = Convert.ToInt32(texture.PixelList.Count * 1.75F);

            Queue<Point> pixels = new Queue<Point>();
            pixels.Enqueue(new Point(x, y));

            Bitmap bmp = BitmapFromTexture(texture);

            if (texture.PixelList.Any(o => !ColourMatch(o.PixelColour, currentColour)))
            {
                // The *1.75F denotes the various offshoots from the main coordinates
                while (pixels.Count > 0 && pixels.Count <= pixelCount)
                {
                    Point a = pixels.Dequeue();

                    //make sure we stay within bounds
                    if (InBounds(a.X, a.Y, width, height))
                    {
                        var currentPixelColour = bmp.GetPixel(a.X, a.Y);

                        if (ColourMatch(currentPixelColour, currentColour) && currentColour != newColour)
                        {
                            bmp.SetPixel(a.X, a.Y, newColour);

                            //GetPixel(texture, a.X, a.Y).PixelColour = newColour;

                            // Check bounds and colour before queuing next point
                            pixels.Enqueue(new Point(a.X - 1, a.Y));
                            pixels.Enqueue(new Point(a.X + 1, a.Y));
                            pixels.Enqueue(new Point(a.X, a.Y - 1));
                            pixels.Enqueue(new Point(a.X, a.Y + 1));
                        }
                    }
                }

                texture.PixelList = GetTextureFromImage(bmp).PixelList;
            }
            else
            {
                foreach (var pixel in texture.PixelList)
                {
                    pixel.PixelColour = newColour;
                }
            }

            TimeSpan ts = DateTime.Now - start;

            return ts.TotalSeconds;
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
        /// Does the color match exactly
        /// </summary>
        /// <param name="a">Colour a</param>
        /// <param name="b">Colour b</param>
        /// <returns>Bool</returns>
        public static bool ColourMatch(Color a, Color b)
        {
            return (a.ToArgb() & 0xffffff) == (b.ToArgb() & 0xffffff);
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
    }
}