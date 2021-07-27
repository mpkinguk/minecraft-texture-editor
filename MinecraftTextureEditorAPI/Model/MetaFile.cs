using System.Text.Json.Serialization;

namespace MinecraftTextureEditorAPI.Model
{
    /// <summary>
    /// MetaFile class for packaging assets during deployment
    /// </summary>
    public class MetaFile
    {
        #region Properties

        /// <summary>
        /// The pack details
        /// </summary>
        [JsonPropertyName("pack")]
        public Pack Pack { get; set; }

        #endregion Properties

        #region Public methods

        /// <summary>
        /// Constructor
        /// </summary>
        public MetaFile()
        {
            Pack = new Pack() { Format = 5, Description = "[Any description you want to give your pack]" };
        }

        #endregion Public methods
    }
}