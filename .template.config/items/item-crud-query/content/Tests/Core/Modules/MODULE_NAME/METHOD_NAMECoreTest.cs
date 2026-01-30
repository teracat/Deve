using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.MODULE_NAME;

namespace Deve.Tests.Core.Modules.MODULE_NAME;

public class METHOD_NAMECoreTest : METHOD_NAMETest, IClassFixture<CoreFixture>
{
    public METHOD_NAMECoreTest(CoreFixture fixture)
       : base(fixture)
    {
    }
}
