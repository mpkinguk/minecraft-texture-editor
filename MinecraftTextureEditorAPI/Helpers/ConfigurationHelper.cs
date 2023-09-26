using log4net;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace MinecraftTextureEditorAPI.Helpers
{
    public static class ConfigurationHelper
    {
        #region Public properties

        public static ILog Log;

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Return seetings object from custom section
        /// </summary>
        /// <param name="sectionName">The section name</param>
        /// <returns>NameValueCollection</returns>
        private static NameValueCollection GetSectionSettings(string sectionName)
        {
            var result = new NameValueCollection();

            try
            {
                result = ConfigurationManager.GetSection(sectionName) as NameValueCollection;

                return result;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return result;
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

                var javaSettings = GetSectionSettings("javaSettings");
                var bedrockSettings = GetSectionSettings("bedrockSettings");

                // Add Java Settings
                for (int i = 0; i < javaSettings.Count; i++)
                {
                    result.Add(javaSettings.Keys[i], javaSettings[i]);
                }

                // Add Bedrock Settings
                for (int i = 0; i < bedrockSettings.Count; i++)
                {
                    result.Add(bedrockSettings.Keys[i], bedrockSettings[i]);
                }

                return result;
            }
            catch (Exception ex)
            {
                Log?.Error(ex.Message);
                return result;
            }
        }

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
        /// Load a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="sectionName">The sectionName</param>        
        /// <returns>string</returns>
        public static string LoadSetting(string key, string sectionName)
        {
            string result = GetSectionSettings(sectionName)[key];

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
        /// Save a setting
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public static void SaveSetting(string key, string value, string SectionName)
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

        #endregion Public methods
    }
}