﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;

namespace MinecraftTextureEditorAPI.Model
{
    /// <summary>
    /// Texture class
    /// </summary>
    public class Texture
    {
        #region Properties

        /// <summary>
        /// The height
        /// </summary>
        public int Height => PixelList.Max(o => o.Y) + 1;

        public List<Pixel> PixelList { get; set; }

        /// <summary>
        /// The width
        /// </summary>
        public int Width => PixelList.Max(o => o.X) + 1;

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public Texture()
        {
            PixelList = new List<Pixel>();
        }

        /// <summary>
        /// Initialise empty pixel list using dimensions
        /// </summary>
        /// <param name="width">The width</param>
        /// <param name="height">The height</param>
        public Texture(int width, int height)
        {
            PixelList = GetBlankPixels(width, height);
        }

        /// <summary>
        /// Initialise pixel list using image
        /// </summary>
        /// <param name="image">The image</param>
        public Texture(Bitmap image)
        {
            PixelList = GetPixelsFromImage(image);
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Clones a new instance of the current texture
        /// </summary>
        /// <returns>Texture</returns>
        public Texture Clone()
        {
            var newPixelList = new List<Pixel>();

            foreach (var pixel in PixelList)
            {
                var newPixel = new Pixel() { PixelColour = pixel.PixelColour, X = pixel.X, Y = pixel.Y };
                newPixelList.Add(newPixel);
            }

            var newTexture = new Texture() { PixelList = newPixelList };

            return newTexture;
        }

        #endregion Public methods
    }
}