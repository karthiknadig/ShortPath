// Copyright (c) Karthik Nadig
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Globalization;

namespace ShortPath {
    public static class StringExtensions {
        public static string FormatInvariant(this string format, object arg) =>
            string.Format(CultureInfo.InvariantCulture, format, arg);

        public static string FormatInvariant(this string format, params object[] args) =>
            string.Format(CultureInfo.InvariantCulture, format, args);

        public static string PrefixPathIfNeeded(this string path) =>
            (path.Length > 256 && !path.StartsWith(@"\\?\")) ? $@"\\?\{path}" : path;

    }
}
