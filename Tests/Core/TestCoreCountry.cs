using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreCountry : TestCoreBaseDataAll<Country, Country, CriteriaCountry>
    {
        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => Core.Countries;

        protected override Country CreateInvalidDataToAdd()
        {
            return new Country();
        }

        protected override Country CreateValidDataToAdd()
        {
            return new Country()
            {
                Name = "Tests Country",
                IsoCode = "TE",
            };
        }
    }
}