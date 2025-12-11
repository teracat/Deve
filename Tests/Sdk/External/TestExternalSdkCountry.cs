using Deve.Dto;
using Deve.External.Data;
using Deve.External.Sdk;
using Deve.Tests.Sdk.External.Fixtures;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkCountry : TestBaseDataGet<ISdk, Country, Country, CriteriaCountry>, IClassFixture<FixtureSdkExternal>
    {
        public TestExternalSdkCountry(FixtureSdkExternal fixture)
            : base(fixture)
        {
        }

        protected override IDataGet<Country, Country, CriteriaCountry> GetDataGet(ISdk sdk) => sdk.Countries;
    }
}