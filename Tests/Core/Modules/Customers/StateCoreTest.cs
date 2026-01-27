using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Customers;

namespace Deve.Tests.Core.Modules.Customers;

public class StateCoreTest : StateTest, IClassFixture<CoreFixture>
{
    public StateCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
