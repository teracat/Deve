using Deve.Tests.Modules.Customers;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Customers;

public class StateSdkTest : StateTest, IClassFixture<SdkFixture>
{
    public StateSdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
