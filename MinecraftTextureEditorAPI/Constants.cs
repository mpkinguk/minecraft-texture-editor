using MinecraftTextureEditorAPI.Helpers;
using System;

namespace MinecraftTextureEditorAPI
{
    /// <summary>
    /// Constants class for file paths and stuff
    /// </summary>
    public static class Constants
    {
        #region Enums

        /// <summary>
        /// Filter type
        /// </summary>
        public enum FilterType
        {
            Category,
            Name,
            Width,
            Height
        }

        #endregion Enums

        #region Folders

        /// <summary>
        /// AssetsFolder
        /// </summary>
        public static string AssetsFolder = ConfigurationHelper.LoadSetting("AssetsFolder");

        /// <summary>
        /// ProjectFolder
        /// </summary>
        public static string DocumentsFolder = ConfigurationHelper.LoadSetting("ProjectFolder");

        /// <summary>
        /// Filter
        /// </summary>
        public static string Filter = "Png Files(*.png)|*.png|Jpg Files(*.jpg)|*.jpg|All Files(*.*)|*.*";

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

        #endregion Folders

        #region Values

        /// <summary>
        /// ItemSize
        /// </summary>
        public const int ItemSize = 48;

        /// <summary>
        /// Default image height
        /// </summary>
        public static int DefaultHeight = 16;

        /// <summary>
        /// Default image width
        /// </summary>
        public static int DefaultWidth = 16;

        /// <summary>
        /// Filter type delimiter
        /// </summary>
        public static char FilterTypeDelimiter = ',';

        /// <summary>
        /// Filter value delimiter
        /// </summary>
        public static char FilterValueDelimiter = '=';

        /// <summary>
        /// The default start zoom
        /// </summary>
        public static int StartZoom = 16;

        #endregion Values

        #region Messages

        /// <summary>
        /// Assets folder not found
        /// </summary>
        public static string AssetsFolderNotFoundMessage = "This path does not contain an asset folder.\n\"OK\" to choose a different path\n\"Cancel\" to continue without using a project.";

        /// <summary>
        /// Changes have been made
        /// </summary>
        public static string ChangesMadeMessage = "Changes have been made. Do you wish to save them?\n\"Yes\" to save\n\"No\" to close without saving\n\"Cancel\" to keep the image open";

        /// <summary>
        /// File exists create backup
        /// </summary>
        public static string FileExistsCreateBackupMessage = "File already exists. Create a backup?";

        /// <summary>
        /// Minecraft font not found
        /// </summary>
        public static string MinecraftFolderNotFoundMessage = "Minecraft font needs to be installed.\nPlease click OK to install font";

        /// <summary>
        /// Options have changed
        /// </summary>
        public static string OptionsChangedMessage = "Some options have been changed.\nDo you wish to save your settings?";

        /// <summary>
        /// Options have changed
        /// </summary>
        public static string OptionsChangedRestartMessage = "Some options have been changed.\nDo you wish to reload the application to use them?";

        /// <summary>
        /// Package deployed as zip
        /// </summary>
        public static string PackageDeployedAsZipMessage = "Package deployed as zip file.\nWould you like to access this location?";

        /// <summary>
        /// Package deployed open Minecraft
        /// </summary>
        public static string PackageDeployedMessage = "Package deployed.\nPlease open Minecraft and select your texture pack to test it out!";

        /// <summary>
        /// Package exists create backup
        /// </summary>
        public static string PackageExistsCreateBackupMessage = "This pack already exists. Create a backup?";

        /// <summary>
        /// Package not deployed
        /// </summary>
        public static string PackageNotDeployedMessage = "Package not deployed";

        /// <summary>
        /// Project created
        /// </summary>
        public static string ProjectCreatedMessage = "Project created!";

        /// <summary>
        /// System directory not found
        /// </summary>
        public static string SystemDirectoryNotFoundMessage = "Could not find system directory";

        /// <summary>
        /// Less lag flag
        /// </summary>
        public static bool LessLag = Convert.ToBoolean(ConfigurationHelper.LoadSetting("LessLag"));

        /// <summary>
        /// Make all blocks the same
        /// </summary>
        public static string MakeAllBlocksTheSameMessage = "Make all blocks the same.\nThis cannot be reversed.\nAre you sure?";

        #endregion Messages

        #region Headers

        /// <summary>
        /// Complete
        /// </summary>
        public static string Complete = "Complete";

        /// <summary>
        /// Error
        /// </summary>
        public static string Error = "Error";

        /// <summary>
        /// Information
        /// </summary>
        public static string Information = "Information";

        /// <summary>
        /// Not found
        /// </summary>
        public static string NotFound = "Not Found";

        /// <summary>
        /// Warning
        /// </summary>
        public static string Warning = "Warning";

        #endregion Headers
    }
}