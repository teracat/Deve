using Deve.Tests.Modules.Customers;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Customers;

public class CountrySdkTest : CountryTest, IClassFixture<SdkFixture>
{
    public CountrySdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
