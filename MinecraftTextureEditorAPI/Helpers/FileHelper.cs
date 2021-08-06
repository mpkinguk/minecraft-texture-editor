using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftTextureEditorAPI.Helpers
{
    /// <summary>
    /// FileHelper class
    /// </summary>
    public static class FileHelper
    {
        #region Public properties

        /// <summary>
        /// Log
        /// </summary>
        public static ILog Log { get; set; }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Return the file details for a given filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static FileInfo FileDetails(this string filename)
        {
            return File.Exists(filename) ? new FileInfo(filename) : null;
        }

        /// <summary>
        /// Returns the default assets folder
        /// </summary>
        /// <returns>string</returns>
        public static string GetAssetsFolder()
        {
            try
            {
                var path = Path.Combine(GetMineCraftFolder(), Constants.AssetsFolder);

                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
                }

                return path;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the default project folder (Documents)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultProjectFolder()
        {
            try
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"{path} not found. Please ensure you have a valid Documents folder before running this wizard");
                }

                return path;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Get files
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="searchPatterns">Search patterns</param>
        /// <param name="searchAllDirectories">Search all directories</param>
        /// <returns>List(string)</returns>
        public static IList<string> GetFiles(string path, string searchPatterns, bool searchAllDirectories)
        {
            try
            {
                var files = Enumerable.Empty<string>();

                var wildCardSplit = searchPatterns.Split(';');

                foreach (var wildCard in wildCardSplit)
                {
                    var result = SafeFileEnumerator.EnumerateFiles(path, wildCard, searchAllDirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                    files = files.Concat(result);
                }

                return files.ToList();
            }
            catch (Exception ex)
            {
                Log?.Debug(ex.Message);
                return new List<string>();
            }
        }

        /// <summary>
        /// Returns the default minecraft folder
        /// </summary>
        /// <returns>string</returns>
        public static string GetMineCraftFolder()
        {
            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Constants.MinecraftFolder);

                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
                }

                return path;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the root path of the project folder so it includes the assets path
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>String</returns>
        public static string GetProjectRootFolder(string path)
        {
            var filesPath = path;

            var directoryPath = new DirectoryInfo(filesPath);

            //We want to take the folder, not just the contents
            if (directoryPath.Name.Equals("assets", StringComparison.InvariantCultureIgnoreCase))
            {
                filesPath = directoryPath.Parent.FullName;
            }

            return filesPath;
        }

        /// <summary>
        /// Returns the default assets folder
        /// </summary>
        /// <returns>string</returns>
        public static string GetResourcePackFolder()
        {
            try
            {
                var path = Path.Combine(GetMineCraftFolder(), Constants.ResourcePackFolder);

                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
                }

                return path;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets a list of version folders from the default minecraft folder
        /// </summary>
        /// <returns>object[]</returns>
        public static object[] GetVersions()
        {
            try
            {
                var path = GetVersionsFolder();

                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
                }

                var list = Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly).Select(x => new DirectoryInfo(x).Name);

                return list.ToArray<object>();
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return new object[0];
            }
        }

        /// <summary>
        /// Returns the default minecraft folder
        /// </summary>
        /// <returns>string</returns>
        public static string GetVersionsFolder()
        {
            try
            {
                var path = Path.Combine(GetMineCraftFolder(), Constants.VersionsFolder);

                if (!Directory.Exists(path))
                {
                    throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
                }

                return path;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Try and load an image
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <returns>Image</returns>
        public static object LoadFile(string fileName = "")
        {
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? OpenFileName() : fileName;

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return null;
                }

                Image image;

                using (FileStream fs = File.OpenRead(fileName))
                {
                    image = Image.FromStream(fs);
                    fs.Close();
                }

                return image;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return new List<string>();
            }
        }

        /// <summary>
        /// Get a filename to open using a file dialog box
        /// </summary>
        /// <returns>File name</returns>
        public static string OpenFileName(string selectedPath = "")
        {
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.InitialDirectory = string.IsNullOrEmpty(selectedPath) ? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) : selectedPath;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var filename = dialog.FileName;

                        if (File.Exists(filename))
                        {
                            return filename;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return selectedPath;
            }
        }

        /// <summary>
        /// Try and save an image from a picturebox
        /// </summary>
        /// <param name="pictureBox">The picturebox</param>
        /// <returns>Bool</returns>
        public static bool SaveFile(PictureBox pictureBox, string fileName = "")
        {
            try
            {
                var filename = string.IsNullOrEmpty(fileName) ? SaveFileName() : fileName;

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return false;
                }

                var image = pictureBox.Image;

                using (var stream = File.OpenWrite(filename))
                {
                    image.Save(stream, ImageFormat.Png);
                    stream.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Try and save an image
        /// </summary>
        /// <param name="image">The image</param>
        /// <returns>Bool</returns>
        public static bool SaveFile(Image image, string fileName = "")
        {
            try
            {
                var filename = string.IsNullOrWhiteSpace(fileName) ? SaveFileName() : fileName;

                if (string.IsNullOrWhiteSpace(filename))
                {
                    return false;
                }

                if (File.Exists(filename))
                {
                    if (MessageBox.Show("File already exists. Create a backup?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        var backupFilename = $"{filename}.bak";

                        if (File.Exists(backupFilename))
                        {
                            File.Delete(backupFilename);
                        }
                        File.Move(filename, backupFilename);
                    }
                    else
                    {
                        File.Delete(filename);
                    }
                }

                using (var stream = File.OpenWrite(filename))
                {
                    image.Save(stream, ImageFormat.Png);
                    stream.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Get a filename to save using a file dialog box
        /// </summary>
        /// <returns>File name</returns>
        public static string SaveFileName(string selectedPath = "")
        {
            try
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.InitialDirectory = string.IsNullOrEmpty(selectedPath) ? Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) : selectedPath;

                    dialog.Filter = Constants.Filter;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var filename = dialog.FileName;

                        return filename;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return selectedPath;
            }
        }

        /// <summary>
        /// Get a folder name using a folder browser dialog box
        /// </summary>
        /// <param name="selectedPath">The selected path</param>
        /// <returns>Folder name</returns>
        public static string SelectFolder(string selectedPath = "")
        {
            try
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    var defaultProjectFolder = GetDefaultProjectFolder();

                    dialog.SelectedPath = string.IsNullOrEmpty(selectedPath) ? defaultProjectFolder : selectedPath;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var folderName = dialog.SelectedPath;

                        if (Directory.Exists(folderName))
                        {
                            return folderName;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return selectedPath;
            }
        }

        #endregion Public methods
    }
}