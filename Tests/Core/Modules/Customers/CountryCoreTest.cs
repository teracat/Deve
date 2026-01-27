using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Customers;

namespace Deve.Tests.Core.Modules.Customers;

public class CountryCoreTest : CountryTest, IClassFixture<CoreFixture>
{
    public CountryCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
