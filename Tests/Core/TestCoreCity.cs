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
            StateId = 2,
            State = "Washington",
            CountryId = 2,
            Country = "USA",
        };

        protected override City CreateValidDataToUpdate() => new()
        {
            Id = 3,
            Name = "Washington DC",
            StateId = 2,
            State = "Washington",
            CountryId = 2,
            Country = "USA"
        };
        #endregion
    }
}