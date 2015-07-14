using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace dnx.host.Tests
{
    public class ApplicationEnvironmentFacts
    {
        [Fact]
        public void GetDataReturnsNullForNonExistantKey()
        {
            var appEnv = new ApplicationEnvironment(null, null, null, null);
            Assert.Null(appEnv.GetData("not-a-real-key"));
        }

        [Fact]
        public void GetSetDataPersistDataWithinASingleApplicationEnvironmentInstance()
        {
            var appEnv = new ApplicationEnvironment(null, null, null, null);
            var obj = new object();
            var key = "testGetSetHost:" + Guid.NewGuid().ToString("N");
            appEnv.SetData(key, obj);
            Assert.Same(obj, appEnv.GetData(key));
        }

#if DNX451
        [Fact]
        public void GetSetDataPersistDataGloballyAcrossAppDomainInDesktopClr()
        {
            var appEnv1 = new ApplicationEnvironment(null, null, null, null);
            var appEnv2 = new ApplicationEnvironment(null, null, null, null);
            var obj = new object();
            var key = "testGetSetGlobal:" + Guid.NewGuid().ToString("N");
            appEnv1.SetData(key, obj);
            Assert.Same(obj, appEnv2.GetData(key));
            Assert.Same(obj, AppDomain.CurrentDomain.GetData(key));
        }
#endif
    }
}
