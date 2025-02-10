using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Internal.Fixtures;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkClient : TestClient<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkClient(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }
    }
}