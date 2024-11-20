using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreState : TestCoreBaseDataAll<State, State, CriteriaState>
    {
        #region Overrides
        protected override IDataAll<State, State, CriteriaState> GetDataAll(ICore core) => core.States;

        protected override State CreateInvalidDataToAdd() => new();

        protected override State CreateInvalidDataToUpdate() => new();

        protected override State CreateValidDataToAdd() => new()
        {
            Name = "Tests State",
            CountryId = 1,
            Country = "España",
        };

        protected override State CreateValidDataToUpdate() => new()
        {
            Id = 1,
            Name = "Barcelona",
            CountryId = 1,
            Country = "España",
        };
        #endregion
    }
}