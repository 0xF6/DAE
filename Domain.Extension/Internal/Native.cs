namespace Domain.Extension.Internal
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int left;
        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int top;
        /// <summary>
        /// The x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int right;
        /// <summary>
        /// The y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int bottom;

        /// <summary>
        /// Gets the calculated width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get { return right - left; } }
        /// <summary>
        /// Gets the calculated height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get { return bottom - top; } }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Left = {0}, Top = {1}, Width = {2}, Height = {3}.", left, top, Width, Height);
        }

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="POINT"/> struct.
        /// </summary>
        /// <param name="x">The x location.</param>
        /// <param name="y">The y location.</param>
        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// The x-coordinate of the point.
        /// </summary>
        public int x;
        /// <summary>
        /// The y-coordinate of the point.
        /// </summary>
        public int y;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}.", x, y);
        }
    }
    internal static class Native
    {
        /// <summary>
        /// Defines the coordinates of the upper-left and lower-right corners of a rectangle.
        /// </summary>
        
        public class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary(string lpFileName);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string defaultValue, StringBuilder lpReturnedString, Int32 bufferSize, string lpFileName);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int defaultValue, string lpFileName);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool AllocConsole();

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeConsole();

            [DllImport("kernel32", SetLastError = true)]
            public static extern bool AttachConsole(int dwProcessId);

            [DllImport("user32.dll")]
            public static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetConsoleWindow();
        }
        public static void CenterConsole()
        {
            IntPtr hWin = NativeMethods.GetConsoleWindow();
            RECT rc;
            User32.GetWindowRect(hWin, out rc);
            Screen scr = Screen.FromPoint(new Point(rc.left, rc.top));
            int x = scr.WorkingArea.Left + (scr.WorkingArea.Width - (rc.right - rc.left)) / 2;
            int y = scr.WorkingArea.Top + (scr.WorkingArea.Height - (rc.bottom - rc.top)) / 2;
            User32.MoveWindow(hWin, x, y, rc.right - rc.left, rc.bottom - rc.top, false);
        }
        public static bool AllocConsole() => NativeMethods.AllocConsole();
        public static bool FreeConsole() => NativeMethods.FreeConsole();
    }
}
