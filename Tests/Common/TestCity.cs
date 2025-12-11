using Deve.Dto;
using Deve.Data;

namespace Deve.Tests
{
    public abstract class TestCity<TDataType> : TestBaseDataAll<TDataType, City, City, CriteriaCity> where TDataType : IDataCommon
    {
        #region Constructor
        protected TestCity(IFixtureData<TDataType> fixture)
            : base(fixture)
        {
        }
        #endregion

        #region Overrides
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