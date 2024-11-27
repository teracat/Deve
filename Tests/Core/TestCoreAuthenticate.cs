using Deve.Core;

namespace Deve.Tests.Core
{
    /// <summary>
    /// Authenticate Tests for Core.
    /// </summary>
    public class TestCoreAuthenticate : TestAuthenticate<ICore>
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();
    }
}