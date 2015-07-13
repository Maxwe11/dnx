using System;
using System.Runtime.Versioning;

namespace Microsoft.Framework.Runtime
{
    /// <summary>
    /// Summary description for LibraryKey
    /// </summary>
    internal static class LibraryKeyExtensions
    {
        public static LibraryKey ChangeName(this LibraryKey target, string name)
        {
            return new LibraryKey(name, target.TargetFramework, target.Configuration, target.Aspect);
        }

        public static LibraryKey ChangeTargetFramework(this LibraryKey target, FrameworkName targetFramework)
        {
            return new LibraryKey(target.Name, targetFramework, target.Configuration, target.Aspect);
        }

        public static LibraryKey ChangeAspect(this LibraryKey target, string aspect)
        {
            return new LibraryKey(target.Name, target.TargetFramework, target.Configuration, aspect);
        }
    }
}