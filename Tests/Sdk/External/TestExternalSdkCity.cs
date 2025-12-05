using Deve.Criteria;
using Deve.External.Data;
using Deve.External.Sdk;
using Deve.Model;
using Deve.Tests.Sdk.External.Fixtures;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkCity : TestBaseDataGet<ISdk, City, City, CriteriaCity>, IClassFixture<FixtureSdkExternal>
    {
        public TestExternalSdkCity(FixtureSdkExternal fixture)
            : base(fixture)
        {
        }

        protected override IDataGet<City, City, CriteriaCity> GetDataGet(ISdk sdk) => sdk.Cities;
    }
}