using Deve.Tests.Modules.Customers;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Customers;

public class StatsSdkTest : StatsTest, IClassFixture<SdkFixture>
{
    public StatsSdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
