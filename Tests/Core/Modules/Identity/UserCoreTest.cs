using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Identity;

namespace Deve.Tests.Core.Modules.Identity;

public class UserCoreTest : UserTest, IClassFixture<CoreFixture>
{
    public UserCoreTest(CoreFixture fixtureDataCore)
       : base(fixtureDataCore)
    {
    }
}
