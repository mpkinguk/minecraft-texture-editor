using MinecraftTextureEditorAPI.Model;
using static MinecraftTextureEditorAPI.Helpers.DrawingHelper;
using System.Drawing;

namespace MinecraftTextureEditorUI
{
    /// <summary>
    /// Static class for storing application states that can be used across forms and classes
    /// </summary>
    public static class State
    {
        /// <summary>
        /// The current alpha setting
        /// </summary>
        public static int Alpha;

        /// <summary>
        /// The  brush size
        /// </summary>
        public static int BrushSize = 1;

        /// <summary>
        /// The  colour 1
        /// </summary>
        public static Color Colour1;

        /// <summary>
        /// The  colour 2
        /// </summary>
        public static Color Colour2;

        /// <summary>
        /// The current rainbow colour index
        /// </summary>
        public static int CurrentRainbowColourIndex;

        /// <summary>
        /// Drawing Tools form
        /// </summary>
        public static DrawingToolsForm DrawingTools;

        /// <summary>
        /// Editor form
        /// </summary>
        public static EditorForm Editor;

        /// <summary>
        /// The current eraser colour
        /// </summary>
        public static Color EraserColor;

        /// <summary>
        /// The path
        /// </summary>
        public static string Path;

        /// <summary>
        /// Pixel clipboard
        /// </summary>
        public static Texture PixelClipboard;

        /// <summary>
        /// Texture Picker form
        /// </summary>
        public static TexturePickerForm TexturePicker;
        /// <summary>
        /// The  tooltype
        /// </summary>
        public static ToolType ToolType;
        /// <summary>
        /// The  transparency lock setting
        /// </summary>
        public static bool TransparencyLock;
    }
}
