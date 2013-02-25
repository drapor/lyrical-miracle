using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using iTunesLib;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;

namespace LyricalMiracle
{
    public class Bootstrap
    {
        public void StartBootstrap(IntPtr windowHandle)
        {
            IntPtr iTunesHandle = FindWindow(null, "iTunes");

            if (iTunesHandle != null)
            {
                ShowWindow(iTunesHandle, 4);
            }

            StartItunes(windowHandle);
        }

        public void StartBootstrapItunes(IntPtr windowHandle)
        {
            System.Diagnostics.Process.Start("iTunes");
            StartItunes(windowHandle);
        }

        #region Private Functions

        private void StartItunes(IntPtr windowHandle)
        {
            System.Diagnostics.Process[] itunesProcesses = System.Diagnostics.Process.GetProcessesByName("iTunes");

            if (itunesProcesses.Length > 0)
            {
                int count = 0;
                while (itunesProcesses.FirstOrDefault().MainWindowHandle == IntPtr.Zero && count < 100)
                {
                    itunesProcesses.FirstOrDefault().Refresh();
                    count++;
                    Thread.Sleep(200);
                }

                SetWindowPos(new IntPtr(itunesProcesses.FirstOrDefault().MainWindowHandle.ToInt32()), windowHandle, 0, 0,
                            (int)(SystemParameters.WorkArea.Width * 0.8),
                            (int)(SystemParameters.WorkArea.Height), 0);
            }
            else
            {
                //Message erreur pas capable de partir itunes
            }
        }

        #endregion

        #region Win32Api

        [DllImportAttribute("user32.dll")]
        private static extern IntPtr FindWindow(String ClassName, String WindowName);

        [DllImport("user32.dll")]
        private extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            out uint lpdwProcessId);

        // When you don't want the ProcessId, use this overload and pass 
        // IntPtr.Zero for the second parameter
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd,
            IntPtr ProcessId);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        /// The GetForegroundWindow function returns a handle to the 
        /// foreground window.
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach,
            uint idAttachTo, bool fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

        #endregion
    }
}
