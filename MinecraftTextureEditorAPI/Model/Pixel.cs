using System.Drawing;

namespace MinecraftTextureEditorAPI.Model
{
    /// <summary>
    /// Pixel Class
    /// </summary>
    public class Pixel
    {
        #region Properties

        /// <summary>
        /// Pixel colour
        /// </summary>
        public Color PixelColour { get; set; }

        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; set; }

        #endregion Properties

        /// <summary>
        /// Constructor
        /// </summary>
        public Pixel()
        {
            PixelColour = Color.FromArgb(0, 0, 0, 0);
            X = 0;
            Y = 0;
        }

        #region Public methods

        /// <summary>
        /// Clone this pixel
        /// </summary>
        /// <returns></returns>
        public Pixel Clone()
        {
            return new Pixel { PixelColour = PixelColour, X = X, Y = Y };
        }

        #endregion Public methods
    }
}