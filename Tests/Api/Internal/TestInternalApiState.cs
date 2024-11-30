namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiState : TestInternalApiBaseAll<State>, IClassFixture<FixtureApiInternal>
    {
        protected override string Path => ApiConstants.PathState;

        public TestInternalApiState(FixtureApiInternal fixture)
            : base(fixture)
        {
        }

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
    }
}