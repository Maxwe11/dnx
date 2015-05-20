// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Framework.Runtime;
#if DNXCORE50
using Environment = Microsoft.Framework.PackageManager.Internal.Environment;
#endif

namespace NuGet
{
    public static class NuGetPathUtility
    {
        public static string GetMachineWideSettingBaseDirectory()
        {
            if (RuntimeEnvironmentHelper.IsWindows)
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
            else
            {
                // Only super users have write access to common app data folder on *nix,
                // so we use roaming local app data folder instead
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }
    }
}
