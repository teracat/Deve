using Deve.Internal;
using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkCity : TestCity<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkCity(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }

        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ISdk sdk) => sdk.Cities;
    }
}