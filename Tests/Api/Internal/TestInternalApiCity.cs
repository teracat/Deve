using Deve.Model;
using Deve.Tests.Api.Internal.Fixture;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiCity : TestInternalApiBaseAll<City>, IClassFixture<FixtureApiInternal>
    {
        protected override string Path => ApiConstants.PathCity;

        public TestInternalApiCity(FixtureApiInternal fixture)
            : base(fixture)
        {
        }

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
    }
}