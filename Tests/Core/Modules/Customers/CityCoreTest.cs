using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Customers;

namespace Deve.Tests.Core.Modules.Customers;

public class CityCoreTest : CityTest, IClassFixture<CoreFixture>
{
    public CityCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
