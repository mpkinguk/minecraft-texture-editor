using System;
using System.Text.Json.Serialization;

namespace MinecraftTextureEditorAPI.Model.Bedrock
{
    /// <summary>
    /// Modules class for bedrock manifest file
    /// </summary>
    public class Modules
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("version")]
        public int[] Version { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Modules() 
        {
            Description = "Example vanilla resource pack";
            Type = "resources";
            Uuid = Guid.NewGuid().ToString();
            Version = new int[] { 0, 0, 1 };
        }
    }
}
