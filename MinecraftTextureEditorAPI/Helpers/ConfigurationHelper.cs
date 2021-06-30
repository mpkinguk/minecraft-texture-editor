using System.Collections.Generic;
using System.Configuration;

namespace MinecraftTextureEditorAPI.Helpers
{
    public static class ConfigurationHelper
    {
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
            ConfigurationManager.AppSettings.Set(key, value);
        }

        /// <summary>
        /// Get all settings
        /// </summary>
        /// <returns>Dictionary(string,string)</returns>
        public static Dictionary<string, string> GetAllSettings()
        {
            var result = new Dictionary<string, string>();

            for(int i=0; i<ConfigurationManager.AppSettings.Count; i++)
            {
                result.Add(ConfigurationManager.AppSettings.Keys[i], ConfigurationManager.AppSettings[i]);
            }

            return result;
        }
    }
}
