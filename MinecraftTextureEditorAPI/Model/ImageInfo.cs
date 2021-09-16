using System.Drawing;

namespace MinecraftTextureEditorAPI.Model
{
    /// <summary>
    /// Image Info class
    /// </summary>
    public class ImageInfo
    {
        #region Properties

        /// <summary>
        /// The size of the image
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// The filename
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// The full path
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The category
        /// </summary>
        public string Category { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initialise the properties
        /// </summary>
        public ImageInfo()
        {
            Size = new Size(0,0);
            Filename = string.Empty;
            FullPath = string.Empty;
            Category = string.Empty;
        }

        #endregion
    }
}