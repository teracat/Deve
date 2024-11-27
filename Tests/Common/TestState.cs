namespace Deve.Tests
{
    public abstract class TestState<TDataType> : TestBaseDataAll<TDataType, State, State, CriteriaState> where TDataType : IDataCommon
    {
        #region Overrides
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