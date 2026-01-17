using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Customers;

namespace Deve.Tests.Core.Modules.Customers;

public class StatsCoreTest : StatsTest, IClassFixture<CoreFixture>
{
    public StatsCoreTest(CoreFixture fixtureDataCore)
       : base(fixtureDataCore)
    {
    }
}
