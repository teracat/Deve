using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.MODULE_NAME;

namespace Deve.Tests.Core.Modules.MODULE_NAME;

public class FEATURE_SINGULARCoreTest : FEATURE_SINGULARTest, IClassFixture<CoreFixture>
{
    public FEATURE_SINGULARCoreTest(CoreFixture fixture)
       : base(fixture)
    {
    }
}
