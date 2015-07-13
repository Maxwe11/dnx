// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Framework.Runtime
{
    /// <summary>
    /// Exposes information about a library which can be an assembly, project, or a package.
    /// </summary>
    public sealed class Library
    {
        /// <summary>
        /// Gets the name of the library.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///  Gets the version of the library.
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets the path to the library. For projects, this is a path to the project.json file.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the type of library. Common values include Project, Package, and Assembly.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets a list of dependencies for the library. The dependencies are names are other <see cref="ILibrary"/> objects.
        /// </summary>
        public IEnumerable<string> Dependencies { get; }

        /// <summary>
        /// Gets a list of assembly names from the library that can be loaded. Packages can contain multiple assemblies.
        /// </summary>
        public IEnumerable<AssemblyName> LoadableAssemblies { get; }

        public Library(string name)
            : this(name, string.Empty, string.Empty, string.Empty, EmptyEnum<string>(), EmptyEnum<AssemblyName>())
        {
        }

        public Library(string name, IEnumerable<string> dependencies)
            : this(name, string.Empty, string.Empty, string.Empty, dependencies, EmptyEnum<AssemblyName>())
        {
        }

        public Library(string name, string version, string path, string type, IEnumerable<string> dependencies, IEnumerable<AssemblyName> loadableAssemblies)
        {
            Name = name;
            Version = version;
            Path = path;
            Type = type;
            Dependencies = dependencies;
            LoadableAssemblies = loadableAssemblies;
        }

        // REVIEW(anurse): Maybe this is over-board, but I'm trying to avoid a dependency on System.Linq just to get Enumerable.Empty
        //  (and also avoid allocating an empty array)
        private static IEnumerable<T> EmptyEnum<T>()
        {
            yield break;
        }
    }
}
