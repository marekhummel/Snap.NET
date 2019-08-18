using SnapNET.Model.PInvoke;
using System;
using System.Text;

namespace SnapNET.Model.Window
{
    internal static class Window
    {

        internal static string GetCurrentForegroundWindow()
        {
            var hWnd = User32.GetForegroundWindow();
            return GetWindowTitle(hWnd);
        }


        private static string GetWindowTitle(IntPtr hWnd)
        {
            var sbTitle = new StringBuilder(255);
            User32.GetWindowText(hWnd, sbTitle, sbTitle.Capacity + 1);
            return sbTitle.ToString();
        }


        /*
        /// <summary>
        /// entry point of the program
        /// </summary>
        public static IEnumerable<string> GetAllWindows()
        {
            bool callback(IntPtr hWnd, int lParam)
            {
                var strbTitle = new StringBuilder(255);
                int nLength = User32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string strTitle = strbTitle.ToString();

                if (User32.IsWindowVisible(hWnd) && !String.IsNullOrEmpty(strTitle)) {
                    _titleCollection.Add(strTitle);
                }
                return true;
            }

            if (User32.EnumDesktopWindows(IntPtr.Zero, KeepWindowHandleInAltTabList, IntPtr.Zero)) {
                foreach (string item in _titleCollection) {
                    yield return item;
                }
            }
        }


        private static readonly List<string> _titleCollection = new List<string>();
        private static bool KeepWindowHandleInAltTabList(IntPtr window, int lParam)
        {
            //// Desktop
            //if (window == User32.GetShellWindow())
            //    return false;

            //http://stackoverflow.com/questions/210504/enumerate-windows-like-alt-tab-does
            //http://blogs.msdn.com/oldnewthing/archive/2007/10/08/5351207.aspx
            //1. For each visible window, walk up its owner chain until you find the root owner. 
            //2. Then walk back down the visible last active popup chain until you find a visible window.
            //3. If you're back to where you're started, (look for exceptions) then put the window in the Alt+Tab list.
            var root = User32.GetAncestor(window, User32.GetAncestorFlags.GA_ROOTOWNER);
            if (GetLastVisibleActivePopUpOfWindow(root) != window)
                return false;


            var sbClassName = new StringBuilder(255);
            User32.GetClassName(window, sbClassName, sbClassName.Capacity + 1);
            string className = sbClassName.ToString();

            var sbTitle = new StringBuilder(255);
            User32.GetWindowText(window, sbTitle, sbTitle.Capacity + 1);
            string title = sbTitle.ToString();

            //if (className == "Shell_TrayWnd" ||                          //Windows taskbar
            //    className == "DV2ControlHost" ||                         //Windows startmenu, if open
            //    (className == "Button" && title == "Start") ||           //Windows startmenu-button.
            //    className == "MsgrIMEWindowClass" ||                     //Live messenger's notifybox i think
            //    className == "SysShadow" ||                              //Live messenger's shadow-hack
            //    className.StartsWith("WMP9MediaBarFlyout"))              //WMP's "now playing" taskbar-toolbar
            //    return false;

            if (!String.IsNullOrEmpty(title))
                _titleCollection.Add(title);
            return true;
        }

        private static IntPtr GetLastVisibleActivePopUpOfWindow(IntPtr hwndWalk)
        {
            IntPtr hwndTry;

            while ((hwndTry = User32.GetLastActivePopup(hwndWalk)) != hwndTry) {
                if (User32.IsWindowVisible(hwndTry))
                    break;
                hwndWalk = hwndTry;
            }

            return hwndWalk;

            //var lastPopUp = User32.GetLastActivePopup(window);
            //if (User32.IsWindowVisible(lastPopUp))
            //    return lastPopUp;
            //else if (lastPopUp == window)
            //    return IntPtr.Zero;
            //else
            //    return GetLastVisibleActivePopUpOfWindow(lastPopUp);
        }
    }
    */
    }
}
