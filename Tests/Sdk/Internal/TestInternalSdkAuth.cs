using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Internal.Fixtures;

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