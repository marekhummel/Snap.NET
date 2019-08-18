using System.Collections.Generic;

namespace SnapNET.Model.Settings
{
    /// <summary>
    /// Class to serialize the settings
    /// </summary>
    internal class GlobalSettings
    {

        /// <summary>
        /// Flag if software should be started with windows
        /// </summary>
        public bool StartWithWindows { get; set; }

        /// <summary>
        /// Grid settings per monitor
        /// </summary>
        public List<GridSettings> GridSettings { get; set; }


        // Shortcuts

        // Application defaults
    }
}
