using System;

namespace SnapNET.Model
{
    /// <summary>
    /// Utility functions
    /// </summary>
    internal static class Util
    {

        /// <summary>
        ///  Clamps a value between two bounds
        /// </summary>
        /// <param name="min">The min bound</param>
        /// <param name="max">The max bound</param>
        /// <param name="val">The value</param>
        /// <returns></returns>
        public static double Clamp(double min, double max, double val)
            => Math.Min(Math.Max(val, min), max);
    }
}
