using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Internal.Fixtures;

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