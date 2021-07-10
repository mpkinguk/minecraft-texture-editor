using System.Drawing;

namespace MinecraftTextureEditorAPI.Model
{
    /// <summary>
    /// Pixel Class
    /// </summary>
    public class Pixel
    {
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

        /// <summary>
        /// Constructor
        /// </summary>
        public Pixel()
        {
            PixelColour = Color.FromArgb(0, 0, 0, 0);
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Clone this pixel
        /// </summary>
        /// <returns></returns>
        public Pixel Clone()
        {
            return new Pixel { PixelColour = PixelColour, X = X, Y = Y };
        }
    }
}