using System.Runtime.InteropServices;

namespace SnapNET.Model.PInvoke
{
    /// <summary>
    /// Simple structure for a rectangle
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
