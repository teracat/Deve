using Deve.Internal;
using Deve.Internal.Sdk;

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