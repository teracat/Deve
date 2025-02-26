using Deve.Core;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    /// <summary>
    /// Authenticate Tests for Core.
    /// </summary>
    public class TestCoreAuthenticate : TestAuthenticate<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreAuthenticate(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}