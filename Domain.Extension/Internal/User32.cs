using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Extension.Internal
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    /// <summary>
    /// API methods in user32.dll.
    /// </summary>
    public static class User32
    {
        [SuppressUnmanagedCodeSecurity]
        class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
            
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
            
            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow([In] IntPtr hWnd);


            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);
            
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindowVisible(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        }
        public static bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint) => NativeMethods.MoveWindow(hWnd, x, y, w, h, repaint);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns></returns>
        public static bool SetForegroundWindow(IntPtr hWnd)
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Changes the parent window of the specified child window.
        /// </summary>
        /// <param name="hWndChild">A handle to the child window.</param>
        /// <param name="hWndNewParent">A handle to the new parent window. If this parameter is NULL, the desktop window becomes the new parent window.</param>
        /// <returns>If the function succeeds, the return value is a handle to the previous parent window.</returns>
        public static IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent)
        {
            return NativeMethods.SetParent(hWndChild, hWndNewParent);
        }

        /// <summary>
        /// Destroys an icon and frees any memory the icon occupied.
        /// </summary>
        /// <param name="hIcon">A handle to the icon to be destroyed. The icon must not be in use.</param>
        /// <returns></returns>
        public static bool DestroyIcon(IntPtr hIcon)
        {
            return NativeMethods.DestroyIcon(hIcon);
        }

        /// <summary>
        /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.</param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        public static bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.PostMessage(hWnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Sends the specified message to a window or windows. 
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. </param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        public static IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.SendMessage(hWnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Calls the default window procedure to provide default processing for any window messages that an application does not process. This function ensures that every message is processed. DefWindowProc is called with the same parameters received by the window procedure.
        /// </summary>
        /// <param name="hWnd">A handle to the window procedure that received the message.</param>
        /// <param name="msg">The message.</param>
        /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
        /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
        /// <returns></returns>
        public static IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }
        
        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings. This function does not search child windows. This function does not perform a case-sensitive search.
        /// To search child windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns></returns>
        public static IntPtr FindWindow(string lpClassName, string lpWindowName)
        {
            return NativeMethods.FindWindow(lpClassName, lpWindowName);
        }

        /// <summary>
        /// The SetWindowRgn function sets the window region of a window. The window region determines the area within the window where the system permits drawing. The system does not display any portion of a window that lies outside of the window region
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window region is to be set.</param>
        /// <param name="hRgn">A handle to a region. The function sets the window region of the window to this region.</param>
        /// <param name="bRedraw">Specifies whether the system redraws the window after setting the window region. If bRedraw is TRUE, the system does so; otherwise, it does not. Typically, you set bRedraw to TRUE if the window is visible.</param>
        /// <returns></returns>
        public static bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw)
        {
            return NativeMethods.SetWindowRgn(hWnd, hRgn, bRedraw);
        }
        

        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>
        /// </returns>
        public static bool IsWindowVisible(IntPtr hWnd)
        {
            return NativeMethods.IsWindowVisible(hWnd);
        }

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="rect">RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns></returns>
        public static bool GetWindowRect(IntPtr hWnd, out RECT rect)
        {
            return NativeMethods.GetWindowRect(hWnd, out rect);
        }
    }
}
