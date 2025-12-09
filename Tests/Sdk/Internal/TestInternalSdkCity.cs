using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Sdk;
using Deve.Model;
using Deve.Tests.Sdk.Internal.Fixtures;

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