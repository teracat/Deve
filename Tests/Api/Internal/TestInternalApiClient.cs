using Deve.Internal.Dto;
using Deve.Dto;
using Deve.Tests.Api.Internal.Fixture;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiClient : TestInternalApiBaseAll<Client>, IClassFixture<FixtureApiInternal>
    {
        protected override string Path => ApiConstants.PathClient;

        public TestInternalApiClient(FixtureApiInternal fixture)
            : base(fixture)
        {
        }

        protected override Client CreateInvalidDataToAdd() => new();

        protected override Client CreateInvalidDataToUpdate() => new();

        protected override Client CreateValidDataToAdd() => new()
        {
            Name = "Tests Client",
            TradeName = "Tests",
            Balance = 12,
            Status = ClientStatus.Active,
            TaxName = "Tests Client Corporation",
            Location = new Location()
            {
                CityId = 1,
                City = "Santedor",
                CountryId = 1,
                Country = "España",
                StateId = 1,
                State = "Barcelona",
            }
        };

        protected override Client CreateValidDataToUpdate() => new()
        {
            Id = 1,
            Name = "Jordi Badia",
            TradeName = "Teracat",
            Balance = 50,
            Status = ClientStatus.Active,
            TaxName = "Jordi Badia Santaulària",
            Location = new Location()
            {
                CityId = 1,
                City = "Santedor",
                CountryId = 1,
                Country = "España",
                StateId = 1,
                State = "Barcelona",
            }
        };

        #region UpdateStatus Tests
        [Fact]
        public async Task UpdateStatus_Unauthorized_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.PutAsync(Path + ApiConstants.MethodUpdateStatus + $"/0/{(int)ClientStatus.Inactive}", null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateStatus_Zero_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.PutAsync(Path + ApiConstants.MethodUpdateStatus + $"/0/{(int)ClientStatus.Inactive}", null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateStatus_InvalidId_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.PutAsync(Path + ApiConstants.MethodUpdateStatus + $"/{InvalidId}/{(int)ClientStatus.Inactive}", null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdateStatus_Valid_SuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.PutAsync(Path + ApiConstants.MethodUpdateStatus + $"/{ValidId}/{(int)ClientStatus.Inactive}", null);

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion
    }
}