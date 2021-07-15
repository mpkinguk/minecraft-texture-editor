using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MinecraftTextureEditorAPI.Helpers
{
    /// <summary>
    /// Enumerate files whilst reducing impact of protected files
    /// </summary>
    public static class SafeFileEnumerator
    {
        public static ILog Log;

        /// <summary>
        /// Enumerate directories
        /// </summary>
        /// <param name="parentDirectory">The parent directory</param>
        /// <param name="searchPattern">The search pattern</param>
        /// <param name="searchOpt">Search options</param>
        /// <returns>String enumerable</returns>
        public static IEnumerable<string> EnumerateDirectories(string parentDirectory, string searchPattern, SearchOption searchOpt)
        {
            try
            {
                var directories = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories)
                {
                    directories = Directory.EnumerateDirectories(parentDirectory)
                        .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOpt));
                }
                return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern));
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// Enumerate files
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="searchPattern">The search pattern</param>
        /// <param name="searchOpt">Search options</param>
        /// <returns>String enumerable</returns>
        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOpt)
        {
            try
            {
                var dirFiles = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories)
                {
                    dirFiles = Directory.EnumerateDirectories(path)
                                        .SelectMany(x => EnumerateFiles(x, searchPattern, searchOpt));
                }
                return dirFiles.Concat(Directory.EnumerateFiles(path, searchPattern));
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return Enumerable.Empty<string>();
            }
        }
    }
}