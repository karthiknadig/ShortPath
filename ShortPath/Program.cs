// Copyright (c) Karthik Nadig
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ShortPath {
    class Program {
        #region Win32
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszLongPath,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszShortPath,
            uint cchBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetFullPathName(
            [MarshalAs(UnmanagedType.LPTStr)] string lpFileName,
            uint nBufferLength,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpBuffer, 
            IntPtr lpFilePart);

        const uint MAX_UNICODE_PATH = 32767;
        #endregion Win32

        private static bool _showFullPath;
        static void Main(string[] args) {

            string[] paths = args;
            if (paths.Contains("-?") || paths.Contains("--help")) {
                ShowHelp();
                return;
            }

            if (args.Contains("-f") || args.Contains("--full")) {
                _showFullPath = true;
                paths = (from a in args where File.Exists(a) || Directory.Exists(a) select a).ToArray();
            } else {
                _showFullPath = false;
            }

            if (paths.Length == 0) {
                string line;
                while ((line = Console.ReadLine()) != null) {
                    GetShortPath(line);
                }
            } else {
                foreach (string path in paths) {
                    GetShortPath(path);
                }
            }
        }

        private static void ShowHelp() {
            Console.WriteLine(Resources.Info_HelpText);
        }

        private static void GetShortPath(string path) {
            if (File.Exists(path) || Directory.Exists(path)) {
                if (_showFullPath) {
                    StringBuilder fullPath = new StringBuilder((int)MAX_UNICODE_PATH);
                    string tempPath = path.PrefixPathIfNeeded();

                    if(GetFullPathName(tempPath, MAX_UNICODE_PATH, fullPath, IntPtr.Zero) == 0) {
                        ErrorMessage(Resources.Error_ShortPathFailedWithError.FormatInvariant(path, Marshal.GetHRForLastWin32Error().ToString("X")));
                        return;
                    }

                    path = fullPath.ToString();
                }
                
                StringBuilder shortPath = new StringBuilder((int)MAX_UNICODE_PATH);
                string longPath = path.PrefixPathIfNeeded();

                if (GetShortPathName(longPath, shortPath, MAX_UNICODE_PATH) == 0) {
                    ErrorMessage(Resources.Error_ShortPathFailedWithError.FormatInvariant(path, Marshal.GetHRForLastWin32Error().ToString("X")));
                } else {
                    Console.WriteLine(shortPath.ToString());
                }
            }
        }

        private static void ErrorMessage(string message) {
            ConsoleColor b = Console.BackgroundColor;
            ConsoleColor f = Console.ForegroundColor;
            try {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
            } finally {
                Console.BackgroundColor = b;
                Console.ForegroundColor = f;
            }

        }
    }
}
