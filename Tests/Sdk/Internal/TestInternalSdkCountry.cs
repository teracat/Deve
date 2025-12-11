using Deve.Dto;
using Deve.Internal.Data;
using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Internal.Fixtures;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkCountry : TestCountry<ISdk>, IClassFixture<FixtureSdkInternal>
    {
        public TestInternalSdkCountry(FixtureSdkInternal fixture)
            : base(fixture)
        {
        }

        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ISdk sdk) => sdk.Countries;
    }
}