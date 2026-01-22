using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.MODULE_NAME;

namespace Deve.Tests.Core.Modules.MODULE_NAME;

public class ITEM_NAME_SINGULARCoreTest : ITEM_NAME_SINGULARTest, IClassFixture<CoreFixture>
{
    public ITEM_NAME_SINGULARCoreTest(CoreFixture fixtureDataCore)
       : base(fixtureDataCore)
    {
    }
}
