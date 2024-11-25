using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiCountry : TestInternalApiBaseAll<Country>
    {
        protected override string Path => ApiConstants.PathCountry;

        public TestInternalApiCountry(WebApplicationFactory<Program> factory)
            : base(factory)
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