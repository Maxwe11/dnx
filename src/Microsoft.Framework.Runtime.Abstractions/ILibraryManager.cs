// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.Framework.Runtime.Compilation;

namespace Microsoft.Framework.Runtime
{
    /// <summary>
    /// Provides access to the complete graph of dependencies for the application.
    /// </summary>
    public interface ILibraryManager
    {
        ILibraryExport GetLibraryExport(string name);

        ILibraryExport GetAllExports(string name);

        IEnumerable<Library> GetReferencingLibraries(string name);

        Library GetLibraryInformation(string name);

        IEnumerable<Library> GetLibraries();

        ILibraryExport GetLibraryExport(string name, string aspect);

        ILibraryExport GetAllExports(string name, string aspect);

        IEnumerable<Library> GetReferencingLibraries(string name, string aspect);

        Library GetLibraryInformation(string name, string aspect);

        IEnumerable<Library> GetLibraries(string aspect);
    }
}
