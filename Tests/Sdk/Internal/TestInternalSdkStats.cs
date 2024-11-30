using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkStats : TestStats<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkStats(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }
    }
}