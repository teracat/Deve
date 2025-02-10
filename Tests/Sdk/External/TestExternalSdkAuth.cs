using Deve.External.Sdk;
using Deve.Tests.Sdk.External.Fixtures;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkAuth : TestAuthenticate<ISdk>, IClassFixture<FixtureSdkExternal>
    {
        public TestExternalSdkAuth(FixtureSdkExternal fixture)
            : base(fixture)
        {
        }
    }
}