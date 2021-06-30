﻿using MinecraftTextureEditorAPI.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MinecraftTextureEditorAPI
{
    public static class DrawingHelper
    {
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
    }
}