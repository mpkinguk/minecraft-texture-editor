using System;
using System.Text.Json.Serialization;

namespace MinecraftTextureEditorAPI.Model.Bedrock
{
    /// <summary>
    /// Header class for bedrock manifest file
    /// </summary>
    public class Header
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }
        [JsonPropertyName("version")]
        public int[] Version { get; set; }
        [JsonPropertyName("min_engine_version")]
        public int[] MinEngineVersion { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Header()
        {
            Description = "Example vanilla resource pack";
            Name = "Vanilla Resource Pack";
            Uuid = Guid.NewGuid().ToString();
            Version = new int[] { 0, 0, 1 };
            MinEngineVersion = new int[] { 1, 20, 40 };
        }
    }
}
