using Deve.Internal;

namespace Deve.Tests
{
    public abstract class TestClient : TestBaseDataAll<IData, Client, Client, CriteriaClient>
    {
        #region Overrides
        protected override IDataAll<Client, Client, CriteriaClient> GetDataAll(IData data) => data.Clients;

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
        #endregion

        #region Custom Tests
        private IDataClient GetDataClient(IData data) => data.Clients;

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnNotSuccess()
        {
            var data = CreateData();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnErrorNotNull()
        {
            var data = CreateData();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var data = CreateData();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_NoAuthValidData_ReturnErrorType()
        {
            var data = CreateData();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnNotSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnErrorsNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnErrorsType()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_InvalidData_ReturnErrorsNotEmpty()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(0, ClientStatus.Active);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task UpdateStatus_ValidData_ReturnSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataClient = GetDataClient(data);

            var res = await dataClient.UpdateStatus(ValidId, ClientStatus.Active);

            Assert.True(res.Success);
        }
        #endregion
    }
}