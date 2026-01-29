using Deve.Tests.Modules.Customers;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Customers;

public class ClientSdkTest : ClientTest, IClassFixture<SdkFixture>
{
    public ClientSdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
