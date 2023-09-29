using System.Text.Json.Serialization;

namespace MinecraftTextureEditorAPI.Model.Bedrock
{
    /// <summary>
    /// Manifest class for bedrock manifest file
    /// </summary>
    public class Manifest
    {
        [JsonPropertyName("format_version")]
        public int FormatVersion { get; set; }

        [JsonPropertyName("header")]
        public Header Header { get; set; }

        [JsonPropertyName("modules")]
        public Modules[] Modules { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Manifest()
        {
            FormatVersion = 2;
            Header = new Header();
            Modules = new Modules[] { new Modules() };
        }
    }
}
