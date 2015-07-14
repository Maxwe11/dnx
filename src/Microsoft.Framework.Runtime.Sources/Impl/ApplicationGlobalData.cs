using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Framework.Runtime.Sources.Impl
{
    internal class ApplicationGlobalData
    {
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
        private readonly object _lock = new object();
        private readonly Dictionary<string, object> _appGlobalData = 
            new Dictionary<string, object>(StringComparer.Ordinal); // Case-sensitive, just like AppDomain.Get/SetData
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
