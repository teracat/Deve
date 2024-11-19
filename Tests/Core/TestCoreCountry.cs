using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreCountry : TestCoreBaseDataAll<Country, Country, CriteriaCountry>
    {
        #region Overrides
        protected override IDataAll<Country, Country, CriteriaCountry> GetDataAll(ICore core) => core.Countries;

        protected override Country CreateInvalidDataToAdd() => new();

        protected override Country CreateInvalidDataToUpdate() => new();

        protected override Country CreateValidDataToAdd() => new()
        {
            Name = "Tests Country",
            IsoCode = "TE",
        };

        protected override Country CreateValidDataToUpdate() => new()
        {
            Id = 1,
            Name = "España",
            IsoCode = "ES"
        };
        #endregion
    }
}