using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace MinecraftTextureEditorAPI.Helpers
{
    public static class ConfigurationHelper
    {
        public static ILog Log;

        /// <summary>
        /// Load a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>string</returns>
        public static string LoadSetting(string key)
        {
            string result = ConfigurationManager.AppSettings[key];

            return result;
        }

        /// <summary>
        /// Save a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public static void SaveSetting(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                Log?.Debug(ex.Message);
            }
        }

        /// <summary>
        /// Get all settings
        /// </summary>
        /// <returns>Dictionary(string,string)</returns>
        public static Dictionary<string, string> GetAllSettings()
        {
            var result = new Dictionary<string, string>();

            try
            {
                for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
                {
                    result.Add(ConfigurationManager.AppSettings.Keys[i], ConfigurationManager.AppSettings[i]);
                }

                return result;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return result;
            }
        }
    }
}