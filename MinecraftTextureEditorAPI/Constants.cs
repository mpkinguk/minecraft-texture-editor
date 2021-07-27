using MinecraftTextureEditorAPI.Helpers;

namespace MinecraftTextureEditorAPI
{
    /// <summary>
    /// Constants class for file paths and stuff
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// AssetsFolder
        /// </summary>
        public static string AssetsFolder = ConfigurationHelper.LoadSetting("AssetsFolder");

        /// <summary>
        /// ProjectFolder
        /// </summary>
        public static string DocumentsFolder = ConfigurationHelper.LoadSetting("ProjectFolder");

        /// <summary>
        /// MinecraftFolder
        /// </summary>
        public static string MinecraftFolder = ConfigurationHelper.LoadSetting("MinecraftFolder");
        /// <summary>
        /// ResourcePackFolder
        /// </summary>
        public static string ResourcePackFolder = ConfigurationHelper.LoadSetting("ResourcePackFolder");

        /// <summary>
        /// Versions
        /// </summary>
        public static string Versions = ConfigurationHelper.LoadSetting("Versions");

        /// <summary>
        /// VersionsFolder
        /// </summary>
        public static string VersionsFolder = ConfigurationHelper.LoadSetting("VersionsFolder");
    }
}
