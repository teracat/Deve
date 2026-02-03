using Deve.Tests.Modules.MODULE_NAME;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.MODULE_NAME;

public class FEATURE_SINGULARSdkTest : FEATURE_SINGULARTest, IClassFixture<SdkFixture>
{
    public FEATURE_SINGULARSdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
