using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DesktopScreenshotsDemo {
    class DSSDemoTools {
        [StructLayout(LayoutKind.Sequential)]
        struct POINT { public int x, y; }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        /// <summary>
        /// Fetches desktop-space mouse coordinates.
        /// You can find no less than 3 assets that do this in a more advanced way.
        /// </summary>
        public static Vector2Int mousePosition {
            get {
                POINT pt;
                if (GetCursorPos(out pt)) {
                    return new Vector2Int(pt.x, pt.y);
                } else return Vector2Int.zero;
            }
        }

        [Flags]
        enum SetWindowPosFlags : uint {
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000,
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int w, int h, SetWindowPosFlags uFlags);

        static IntPtr __hwnd;
        static IntPtr hwnd {
            get {
                if (__hwnd == IntPtr.Zero) {
                    __hwnd = GetActiveWindow();
                }
                return __hwnd;
            }
        }

        public static bool SetWindowVisible(bool visible) {
            var flags = SetWindowPosFlags.NOZORDER | SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOSIZE;
            flags |= visible ? SetWindowPosFlags.SHOWWINDOW : SetWindowPosFlags.HIDEWINDOW;
            return SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, flags);
        }
    }
}