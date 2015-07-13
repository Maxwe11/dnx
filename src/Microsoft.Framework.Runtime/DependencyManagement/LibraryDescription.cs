// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Runtime.Versioning;

namespace Microsoft.Framework.Runtime
{
    public class LibraryDescription
    {
        public LibraryRange LibraryRange { get; set; }
        public LibraryIdentity Identity { get; set; }
        public IEnumerable<LibraryDependency> Dependencies { get; set; }

        public bool Resolved { get; set; } = true;
        public bool Compatible { get; set; } = true;

        public string Path { get; set; }
        public string Type { get; set; }
        public FrameworkName Framework { get; set; }
        public IEnumerable<string> LoadableAssemblies { get; set; }
    }
}
