using Deve.Criteria;
using Deve.Data;
using Deve.Model;

namespace Deve.Tests
{
    public abstract class TestCountry<TDataType> : TestBaseDataAll<TDataType, Country, Country, CriteriaCountry> where TDataType : IDataCommon
    {
        #region Constructor
        protected TestCountry(IFixtureData<TDataType> fixture)
            : base(fixture)
        {
        }
        #endregion

        #region Overrides
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
            IsoCode = "ES",
        };
        #endregion
    }
}