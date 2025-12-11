using Deve.Dto;
using Deve.Tests.Api.Internal.Fixture;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiCountry : TestInternalApiBaseAll<Country>, IClassFixture<FixtureApiInternal>
    {
        protected override string Path => ApiConstants.PathCountry;

        public TestInternalApiCountry(FixtureApiInternal fixture)
            : base(fixture)
        {
        }

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
    }
}