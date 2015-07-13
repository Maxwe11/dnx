// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;

namespace Microsoft.Framework.Runtime
{
    public class ApplicationEnvironment : IApplicationEnvironment
    {
        private readonly Project _project;
        private readonly FrameworkName _targetFramework;

        public ApplicationEnvironment(Project project, FrameworkName targetFramework, string configuration)
        {
            _project = project;
            _targetFramework = targetFramework;
            Configuration = configuration;
        }

        public string ApplicationName
        {
            get
            {
                return _project.EntryPoint ?? _project.Name;
            }
        }

        public string ApplicationBasePath
        {
            get
            {
                return _project.ProjectDirectory;
            }
        }

        public string Version
        {
            get { return _project.Version.ToString(); }
        }

        public string Configuration { get; private set; }


        public FrameworkName RuntimeFramework
        {
            get { return _targetFramework; }
        }

#if DNX451
        public object GetData(string name)
        {
            return AppDomain.CurrentDomain.GetData(name);
        }

        public void SetData(string name, object value)
        {
            AppDomain.CurrentDomain.SetData(name, value);
        }
#else
        // NOTE(anurse): ConcurrentDictionary seems overkill here. This data is rarely used, and a global lock seems safer.
        private object _lock = new object();
        private Dictionary<string, object> _appGlobalData = new Dictionary<string, object>();
        public object GetData(string name)
        {
            lock (_lock)
            {
                object val;
                if (_appGlobalData.TryGetValue(name, out val))
                {
                    return val;
                }
                return null;
            }
        }

        public void SetData(string name, object value)
        {
            lock (_lock)
            {
                _appGlobalData[name] = value;
            }
        }
#endif
    }
}
