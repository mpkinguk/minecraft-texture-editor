using System.Text.Json.Serialization;

namespace MinecraftTextureEditorAPI.Model
{
    /// <summary>
    /// The pack details
    /// </summary>
    public class Pack
    {
        /// <summary>
        /// The pack description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The pack format
        /// </summary>
        [JsonPropertyName("pack_format")]
        public int Format { get; set; }
    }
}