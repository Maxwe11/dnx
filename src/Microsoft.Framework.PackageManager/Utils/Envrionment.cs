// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Microsoft.Framework.Runtime;

// TODO: delete this after System.Environment.GetFolderPath() is available on CoreCLR
#if DNXCORE50
namespace Microsoft.Framework.PackageManager.Internal
{
    internal static class Environment
    {
        public static string NewLine { get; } = System.Environment.NewLine;

        public static string GetEnvironmentVariable(string variable)
        {
            return System.Environment.GetEnvironmentVariable(variable);
        }

        public static string ExpandEnvironmentVariables(string name)
        {
            return System.Environment.ExpandEnvironmentVariables(name);
        }

        public static string GetFolderPath(SpecialFolder folder)
        {
            switch (folder)
            {
                case SpecialFolder.ProgramFilesX86:
                    return GetEnvironmentVariable("PROGRAMFILES(X86)");
                case SpecialFolder.ProgramFiles:
                    return GetEnvironmentVariable("PROGRAMFILES");
                case SpecialFolder.UserProfile:
                    return GetHome();
                case SpecialFolder.CommonApplicationData:
                    if (RuntimeEnvironmentHelper.IsWindows)
                    {
                        return FirstNonEmpty(
                            () => GetEnvironmentVariable("PROGRAMDATA"),
                            () => GetEnvironmentVariable("ALLUSERSPROFILE"));
                    }
                    else
                    {
                        return "/usr/share";
                    }
                case SpecialFolder.ApplicationData:
                    if (RuntimeEnvironmentHelper.IsWindows)
                    {
                        return GetEnvironmentVariable("APPDATA");
                    }
                    else
                    {
                        return FirstNonEmpty(
                            () => GetEnvironmentVariable("XDG_CONFIG_HOME"),
                            () => Path.Combine(GetHome(), ".config"));
                    }
                case SpecialFolder.LocalApplicationData:
                    if (RuntimeEnvironmentHelper.IsWindows)
                    {
                        return GetEnvironmentVariable("LOCALAPPDATA");
                    }
                    else
                    {
                        return FirstNonEmpty(
                            () => GetEnvironmentVariable("XDG_DATA_HOME"),
                            () => Path.Combine(GetHome(), ".local", "share"));
                    }
                default:
                    return null;
            }
        }

        private static string GetHome()
        {
            if (RuntimeEnvironmentHelper.IsWindows)
            {
                return FirstNonEmpty(
                    () => GetEnvironmentVariable("USERPROFILE"),
                    () => GetEnvironmentVariable("HOMEDRIVE") + GetEnvironmentVariable("HOMEPATH"));
            }
            else
            {
                return GetEnvironmentVariable("HOME");
            }
        }

        private static string FirstNonEmpty(params Func<string>[] providers)
        {
            foreach (var p in providers)
            {
                var value = p();
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }
            return null;
        }

        internal enum SpecialFolder
        {
            ProgramFilesX86,
            ProgramFiles,
            UserProfile,
            CommonApplicationData,
            ApplicationData,
            LocalApplicationData
        }
    }
}
#endif