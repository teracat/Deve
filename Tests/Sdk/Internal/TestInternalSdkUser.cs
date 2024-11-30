using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkUser : TestUser<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkUser(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }
    }
}