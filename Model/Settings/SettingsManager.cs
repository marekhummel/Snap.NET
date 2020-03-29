using System;
using System.IO;
using System.Web.Script.Serialization;

namespace SnapNET.Model.Settings
{
    internal static class SettingsManager
    {
        // ***** Public members *****

        internal static GlobalSettings Settings { get; private set; }



        // ***** Private members *****

        // Path to the settings json
        private static readonly string _settingsDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SnapNET");
        private static readonly string _settingsFile = "settings.json";



        // ***** Public methods *****

        /// <summary>
        /// Load settings if available
        /// </summary>
        internal static void LoadSettings()
        {
            string fullpath = Path.Combine(_settingsDir, _settingsFile);

            // No settings exist yet
            if (!File.Exists(fullpath)) {
                Settings = new GlobalSettings();

                // Create gridsettings for each monitor
                var mons = Monitor.Monitor.GetAllMonitors();
                foreach (var mon in mons)
                    Settings.GridSettings.Add(mon.Name, new GridSettings());

                return;
            }

            // Load json file
            string json = File.ReadAllText(fullpath);
            Settings = (GlobalSettings)new JavaScriptSerializer().Deserialize(json, typeof(GlobalSettings));
        }


        /// <summary>
        /// Save settings
        /// </summary>
        internal static void StoreSettings()
        {
            string json = new JavaScriptSerializer().Serialize(Settings);

            if (!Directory.Exists(_settingsDir))
                _ = Directory.CreateDirectory(_settingsDir);

            File.WriteAllText(Path.Combine(_settingsDir, _settingsFile), json);
        }


        internal static GridSettings GetGridSettings(string mon) 
            => Settings.GridSettings.ContainsKey(mon) ? Settings.GridSettings[mon] : new GridSettings();
    }
}
