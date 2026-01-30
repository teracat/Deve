using Deve.Tests.Modules.Customers;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Customers;

public class CitySdkTest : CityTest, IClassFixture<SdkFixture>
{
    public CitySdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
