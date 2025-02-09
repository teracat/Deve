using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Internal.Fixtures;

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