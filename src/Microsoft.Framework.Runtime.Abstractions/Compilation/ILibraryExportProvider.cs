// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Runtime.Versioning;

namespace Microsoft.Framework.Runtime.Compilation
{
    public interface ILibraryExportProvider
    {
        ILibraryExport GetLibraryExport(LibraryKey target);
    }
}
