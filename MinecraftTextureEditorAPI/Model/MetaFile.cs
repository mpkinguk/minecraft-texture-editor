using System.Text.Json.Serialization;

namespace MinecraftTextureEditorAPI.Model
{
    public class MetaFile
    {
        [JsonPropertyName("pack")]
        public Pack Pack { get; set; }

        public MetaFile()
        {
            Pack = new Pack() { Format = 5, Description = "[Any description you want to give your pack]" };
        }
    }
}