using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreCity : TestCoreBaseDataAll<City, City, CriteriaCity>
    {
        #region Overrides
        protected override IDataAll<City, City, CriteriaCity> GetDataAll(ICore core) => core.Cities;

        protected override City CreateInvalidDataToAdd() => new();

        protected override City CreateInvalidDataToUpdate() => new();

        protected override City CreateValidDataToAdd() => new()
        {
            Name = "Tests City",
            StateId = 1,
            State = "Barcelona",
            CountryId = 1,
            Country = "España",
        };

        protected override City CreateValidDataToUpdate() => new()
        {
            Id = 1,
            Name = "Santpedor",
            StateId = 1,
            State = "Barcelona",
            CountryId = 1,
            Country = "España",
        };
        #endregion
    }
}