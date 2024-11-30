using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkAuth : TestAuthenticate<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkAuth(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }
    }
}