using Medicum.Tests.Sdk.Fixtures;
using Medicum.Tests.Modules.MODULE_NAME;

namespace Medicum.Tests.Sdk.Modules.MODULE_NAME;

public class METHOD_NAMESdkTest : METHOD_NAMETest, IClassFixture<SdkFixture>
{
    public METHOD_NAMESdkTest(SdkFixture fixture)
       : base(fixture)
    {
    }
}
