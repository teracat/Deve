using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiCity : TestInternalApiBaseAll<City>
    {
        protected override string Path => ApiConstants.PathCity;

        public TestInternalApiCity(WebApplicationFactory<Program> factory)
            : base(factory)
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