using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace ZipFileManagerAPI
{
    /// <summary>
    /// ZipFileManager class
    /// </summary>
    public class ZipFileManager
    {
        #region Public delegates

        public delegate void FileProcessedEventHandler(string filename);

        #endregion Public delegates

        #region Public events

        public event FileProcessedEventHandler FileProcessed;

        #endregion Public events

        #region private properties

        private readonly ILog _log;

        #endregion private properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log"></param>
        public ZipFileManager(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// Extract zip file to the chosen folder
        /// </summary>
        /// <param name="inputFilename">The input filename</param>
        /// <param name="outputPath">The output path</param>
        /// <returns></returns>
        public async Task<bool> UnZipFiles(string inputFilename, string outputPath)
        {
            try
            {
                var fileInfo = new FileInfo(inputFilename);

                var archiveFolderName = fileInfo.Name.Replace(fileInfo.Extension, "");

                var outputFolder = Path.Combine(outputPath, archiveFolderName);

                // run async so we free up the UI
                using (var t = Task.Run(() =>
                {
                    if (fileInfo.Extension.ToLower() == "zip")
                    {
                        ZipFile.ExtractToDirectory(inputFilename, outputFolder);
                    }
                    else
                    {
                        string zPath = @"7zip\7za.exe"; //add to proj and set CopyToOuputDir

                        ProcessStartInfo processInfo = new ProcessStartInfo
                        {
                            WindowStyle = ProcessWindowStyle.Hidden,
                            FileName = zPath,
                            Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", inputFilename, outputFolder)
                        };
                        Process x = Process.Start(processInfo);
                        x.WaitForExit();
                    }
                }))
                {
                    await t.ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Create zip files from individual files
        /// </summary>
        /// <param name="outputFilename">The output filename</param>
        /// <param name="path">The path</param>
        /// <param name="fileNames">The filenames to zip</param>
        /// <returns>bool</returns>
        public async Task<bool> ZipFiles(string outputFilename, string path, IList<string> fileNames)
        {
            try
            {
                if (fileNames.Count == 0)
                {
                    throw new Exception("No files selected...");
                }

                using (var zip = ZipFile.Open(outputFilename, ZipArchiveMode.Create))
                {
                    foreach (var file in fileNames)
                    {
                        using (var t = Task.Run(() =>
                        {
                            var newPath = file.Replace(path, "").TrimStart("\\".ToCharArray());
                            zip.CreateEntryFromFile(file, newPath, CompressionLevel.Optimal);
                        }))
                        {
                            await t.ConfigureAwait(false);
                        }

                        OnFileProcessed($"Adding {file}...");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _log?.Error(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Invoke file processed event
        /// </summary>
        /// <param name="filename"></param>
        private void OnFileProcessed(string filename)
        {
            FileProcessed?.Invoke(filename);
        }
    }
}