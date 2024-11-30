using Deve.External;
using Deve.External.Sdk;

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