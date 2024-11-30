using Deve.Core;

namespace Deve.Tests.Core
{
    /// <summary>
    /// Authenticate Tests for Core.
    /// </summary>
    public class TestCoreAuthenticate : TestAuthenticate<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreAuthenticate(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}