using Deve.Core;

namespace Deve.Tests.Core
{
    internal static class TestsCoreHelpers
    {
        //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
        public static ICore CreateCore() => new CoreMain(true, TestsHelpers.CreateDataSourceMock());
    }
}
