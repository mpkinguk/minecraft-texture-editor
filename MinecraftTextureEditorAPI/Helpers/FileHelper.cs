using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MinecraftTextureEditorAPI
{
    public static class FileHelper
    {
        /// <summary>
        /// Get files
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="searchPatterns">Search patterns</param>
        /// <param name="searchAllDirectories">Search all directories</param>
        /// <returns>List(string)</returns>
        public static List<string> GetFiles(string path, string searchPatterns, bool searchAllDirectories)
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

        /// <summary>
        /// Try and load an image
        /// </summary>
        /// <param name="fileName">The filename</param>
        /// <returns>Image</returns>
        public static object LoadFile(string fileName = "")
        {
            fileName = string.IsNullOrEmpty(fileName) ? OpenFileName() : fileName;

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            var image = new Bitmap(fileName);

            return image;
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

                pictureBox.Image.Save(filename, ImageFormat.Png);

                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
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
                        File.Move(filename, string.Concat(filename, ".bak"));
                    }
                }

                image.Save(filename, ImageFormat.Png);

                return true;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
                return false;
            }
        }

        /// <summary>
        /// Get a folder name using a folder browser dialog box
        /// </summary>
        /// <returns>Folder name</returns>
        public static string OpenFolderName(string selectedPath = "")
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = string.IsNullOrEmpty(selectedPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : selectedPath;

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

        /// <summary>
        /// Get a filename to open using a file dialog box
        /// </summary>
        /// <returns>File name</returns>
        public static string OpenFileName(string selectedPath = "")
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = string.IsNullOrEmpty(selectedPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : selectedPath;

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

        /// <summary>
        /// Get a filename to save using a file dialog box
        /// </summary>
        /// <returns>File name</returns>
        public static string SaveFileName(string selectedPath = "")
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = string.IsNullOrEmpty(selectedPath) ? Environment.GetFolderPath(Environment.SpecialFolder.Personal) : selectedPath;

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
        /// Gets a list of version folders from the default minecraft folder
        /// </summary>
        /// <returns>object[]</returns>
        public static object[] GetVersions()
        {
            var path = Path.Combine((string)GetMineCraftFolder().Clone(), "Versions");

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
            }

            var list = Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly).Select(x=> new DirectoryInfo(x).Name);

            return list.ToArray<object>();
        }

        /// <summary>
        /// Returns the default minecraft folder
        /// </summary>
        /// <returns>string</returns>
        public static string GetMineCraftFolder()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".minecraft");

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException($"{path} not found. Please ensure you have installed minecraft before running this wizard");
            }

            return path;
        }
    }
}