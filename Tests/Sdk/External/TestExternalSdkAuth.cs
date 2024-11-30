using Deve.External.Sdk;

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