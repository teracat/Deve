using Deve.Tests.Sdk.Fixtures;
using Deve.Tests.Modules.MODULE_NAME;

namespace Deve.Tests.Sdk.Modules.MODULE_NAME;

public class METHOD_NAMESdkTest : METHOD_NAMETest, IClassFixture<SdkFixture>
{
    public METHOD_NAMESdkTest(SdkFixture fixture)
       : base(fixture)
    {
    }
}
