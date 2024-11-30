using Deve.Internal.Sdk;

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