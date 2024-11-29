using Deve.Internal;

namespace Deve.Tests
{
    public abstract class TestClient<TDataType> : TestBaseDataAll<TDataType, Client, Client, CriteriaClient> where TDataType : IData
    {
        #region Constructor
        public TestClient(IFixtureData<TDataType> fixtureData, IFixtureDataLogged<TDataType> fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }
        #endregion

        #region Overrides
        protected override IDataAll<Client, Client, CriteriaClient> GetDataAll(TDataType data) => data.Clients;

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
                Country = "Espa�a",
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
            TaxName = "Jordi Badia Santaul�ria",
            Location = new Location()
            {
                CityId = 1,
                City = "Santedor",
                CountryId = 1,
                Country = "Espa�a",
                StateId = 1,
                State = "Barcelona",
            }
        };
        #endregion

        #region Custom Tests
        private IDataClient GetDataClient(IData data) => data.Clients;

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnNotSuccess()
        {
            var dataClient = GetDataClient(FixtureData.Data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnErrorNotNull()
        {
            var dataClient = GetDataClient(FixtureData.Data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var dataClient = GetDataClient(FixtureData.Data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnErrorType()
        {
            var dataClient = GetDataClient(FixtureData.Data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnNotSuccess()
        {
            var dataClient = GetDataClient(FixtureDataLogged.Data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnErrorsNotNull()
        {
            var dataClient = GetDataClient(FixtureDataLogged.Data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnErrorsType()
        {
            var dataClient = GetDataClient(FixtureDataLogged.Data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnErrorsNotEmpty()
        {
            var dataClient = GetDataClient(FixtureDataLogged.Data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_ValidData_ReturnSuccess()
        {
            var dataClient = GetDataClient(FixtureDataLogged.Data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.True(res.Success);
        }
        #endregion
    }
}