using System;

namespace SnapNET.Model
{
    internal static class Util
    {

        public static double Clamp(double min, double max, double val)
            => Math.Min(Math.Max(val, min), max);

    }
}
