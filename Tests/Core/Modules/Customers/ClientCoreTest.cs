using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Customers;

namespace Deve.Tests.Core.Modules.Customers;

public class ClientCoreTest : ClientTest, IClassFixture<CoreFixture>
{
    public ClientCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
