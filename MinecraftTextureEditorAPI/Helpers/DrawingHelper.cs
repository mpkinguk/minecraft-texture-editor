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
            MirrorY
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
        /// 
        public static List<Pixel> FloodFill(Color currentColour, Color newColour, int x, int y, Texture texture)
        {
            var output = new List<Pixel>();

            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(new Point(x, y));

            if (texture.PixelList.Any(o => o.PixelColour != currentColour))
            {
                // The *1.75F denotes the various offshoots from the main coordinates
                while (pixels.Count > 0 && pixels.Count <= Convert.ToInt32(texture.PixelList.Count * 1.75F))
                {
                    Point a = pixels.Pop();
                    if (a.X >= 0 && a.X < texture.Width &&
                         a.Y >= 0 && a.Y < texture.Height)//make sure we stay within bounds
                    {
                        var currentPixel = texture.PixelList.FirstOrDefault(o => o.X.Equals(a.X) && o.Y.Equals(a.Y));

                        if (currentPixel.PixelColour.Equals(currentColour) && currentColour != newColour)
                        {
                            currentPixel.PixelColour = newColour;

                            // Add to output for undo tracking
                            output.Add(new Pixel() { PixelColour = newColour, X = currentPixel.X, Y = currentPixel.Y });

                            pixels.Push(new Point(a.X - 1, a.Y));
                            pixels.Push(new Point(a.X + 1, a.Y));
                            pixels.Push(new Point(a.X, a.Y - 1));
                            pixels.Push(new Point(a.X, a.Y + 1));
                        }
                    }
                }
            }
            else
            {
                foreach (var pixel in texture.PixelList)
                {
                    output.Add(new Pixel() { PixelColour = newColour, X = pixel.X, Y = pixel.Y });
                }
            }

            return output;
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

            var a = colour.A;
            var r = (rnd.Next(-30, 30) + colour.R).Clamp(0, 255);
            var g = (rnd.Next(-30, 30) + colour.G).Clamp(0, 255);
            var b = (rnd.Next(-30, 30) + colour.B).Clamp(0, 255);

            var newColour = Color.FromArgb(a, r, g, b);

            return newColour;
        }

        /// <summary>
        /// Clones a pixel list
        /// </summary>
        /// <param name="pixelList">The pixel list</param>
        /// <returns>List(Pixel)</returns>
        public static List<Pixel> Clone(this List<Pixel> pixelList)
        {
            var output = new List<Pixel>();

            foreach (Pixel item in pixelList)
            {
                output.Add(new Pixel { PixelColour = item.PixelColour, X = item.X, Y = item.Y });
            }

            return output;
        }
    }
}