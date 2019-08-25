using System.Collections.Generic;

namespace SnapNET.Model.Settings
{
    /// <summary>
    /// Class to serialize the settings
    /// </summary>
    internal class GlobalSettings
    {
        // ***** Public members *****

        /// <summary>
        /// Flag if software should be started with windows
        /// </summary>
        public bool StartWithWindows { get; set; } = true;

        /// <summary>
        /// Grid settings per monitor
        /// </summary>
        public Dictionary<string, GridSettings> GridSettings { get; set; }


        // Shortcuts

        // Application defaults


        // ***** Public methods *****

        /// <summary>
        /// Constructor 
        /// </summary>
        public GlobalSettings()
        {
            GridSettings = new Dictionary<string, GridSettings>();
        }

    }
}
