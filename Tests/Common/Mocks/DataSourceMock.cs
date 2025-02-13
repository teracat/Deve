using Moq;
using Deve.DataSource;
using Deve.Criteria;
using Deve.Model;
using Deve.Internal.Criteria;
using Deve.Internal.Model;

namespace Deve.Tests.Mocks
{
    internal class DataSourceMock : Mock<IDataSource>
    {
        public DataSourceMock()
        {
            // Users
            Setup(d => d.Users.Get(It.IsAny<CriteriaUser?>())).Returns<CriteriaUser>((c) => GetListUser(DataMock.Users, c));
            Setup(d => d.Users.Get()).Returns(() => GetListUser(DataMock.Users, null));
            Setup(d => d.Users.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Users, id));
            Setup(d => d.Users.Add(It.IsAny<User>())).Returns<User>((c) => Add(DataMock.Users, c));
            Setup(d => d.Users.Update(It.IsAny<User>())).Returns<User>((c) => CheckIdExists(DataMock.Users, c.Id));
            Setup(d => d.Users.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Users, id));

            // Countries
            Setup(d => d.Countries.Get(It.IsAny<CriteriaCountry?>())).Returns<CriteriaCountry>((_) => GetList(DataMock.Countries));
            Setup(d => d.Countries.Get()).Returns(() => GetList(DataMock.Countries));
            Setup(d => d.Countries.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Countries, id));
            Setup(d => d.Countries.Add(It.IsAny<Country>())).Returns<Country>((c) => Add(DataMock.Countries, c));
            Setup(d => d.Countries.Update(It.IsAny<Country>())).Returns<Country>((c) => CheckIdExists(DataMock.Countries, c.Id));
            Setup(d => d.Countries.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Countries, id));

            // States
            Setup(d => d.States.Get(It.IsAny<CriteriaState?>())).Returns<CriteriaState>((_) => GetList(DataMock.States));
            Setup(d => d.States.Get()).Returns(() => GetList(DataMock.States));
            Setup(d => d.States.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.States, id));
            Setup(d => d.States.Add(It.IsAny<State>())).Returns<State>((c) => Add(DataMock.States, c));
            Setup(d => d.States.Update(It.IsAny<State>())).Returns<State>((c) => CheckIdExists(DataMock.States, c.Id));
            Setup(d => d.States.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.States, id));

            // Cities
            Setup(d => d.Cities.Get(It.IsAny<CriteriaCity?>())).Returns<CriteriaCity>((_) => GetList(DataMock.Cities));
            Setup(d => d.Cities.Get()).Returns(() => GetList(DataMock.Cities));
            Setup(d => d.Cities.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Cities, id));
            Setup(d => d.Cities.Add(It.IsAny<City>())).Returns<City>((c) => Add(DataMock.Cities, c));
            Setup(d => d.Cities.Update(It.IsAny<City>())).Returns<City>((c) => CheckIdExists(DataMock.Cities, c.Id));
            Setup(d => d.Cities.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Cities, id));

            // Clients
            Setup(d => d.Clients.Get(It.IsAny<CriteriaClient?>())).Returns<CriteriaClient>((_) => GetList(DataMock.Clients));
            Setup(d => d.Clients.Get()).Returns(() => GetList(DataMock.Clients));
            Setup(d => d.Clients.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Clients, id));
            Setup(d => d.Clients.Add(It.IsAny<Client>())).Returns<Client>((c) => Add(DataMock.Clients, c));
            Setup(d => d.Clients.Update(It.IsAny<Client>())).Returns<Client>((c) => CheckIdExists(DataMock.Clients, c.Id));
            Setup(d => d.Clients.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Clients, id));
            Setup(d => d.Clients.UpdateStatus(It.IsAny<long>(), It.IsAny<ClientStatus>())).Returns<long, ClientStatus>((id, _) => CheckIdExists(DataMock.Clients, id));

            // ClientsBasic
            var clientsBasic = DataMock.Clients.Select(x => new ClientBasic()
            {
                Id = x.Id,
                Name = x.Name,
                TradeName = x.TradeName,
                City = x.Location.City,
                Country = x.Location.Country,
                State = x.Location.State,
                Latitude = x.Location.Latitude,
                Longitude = x.Location.Longitude,
            }).ToList();
            Setup(d => d.ClientsBasic.Get(It.IsAny<CriteriaClientBasic?>())).Returns<CriteriaClientBasic>((_) => GetList(clientsBasic));
            Setup(d => d.ClientsBasic.Get()).Returns(() => GetList(clientsBasic));
            Setup(d => d.ClientsBasic.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Clients.Select(x => x as External.Model.Client).ToList(), id));
        }

        #region Helper DataAll methods
        private static Task<ResultGetList<T>> GetList<T>(List<T> list) => Task.Run(() => Utils.ResultGetListOk<T>(list, 0, 0, string.Empty, list.Count));

        // User must implement the Criteria search by Username and OnlyActive so the Auth test methods don't fail
        private static Task<ResultGetList<User>> GetListUser(List<User> list, CriteriaUser? criteria)
        {
            return Task.Run(() =>
            {
                if (criteria is not null)
                {
                    var qry = list.AsQueryable();

                    switch (criteria.OnlyActive)
                    {
                        case CriteriaActiveType.OnlyActive:
                            qry = qry.Where(x => x.IsActive);
                            break;
                        case CriteriaActiveType.OnlyInactive:
                            qry = qry.Where(x => !x.IsActive);
                            break;
                        case CriteriaActiveType.All:
                        default:
                            // Filter by IsActive is not needed
                            break;
                    }

                    if (!string.IsNullOrWhiteSpace(criteria.Username))
                    {
                        qry = qry.Where(x => x.Username.Equals(criteria.Username, StringComparison.InvariantCultureIgnoreCase));
                    }

                    var listFound = qry.ToList();
                    return Utils.ResultGetListOk(listFound, 0, 0, string.Empty, listFound.Count);
                }

                return Utils.ResultGetListOk(list, 0, 0, string.Empty, list.Count);
            });
        }

        private static Task<ResultGet<T>> Get<T>(List<T> list, long id) where T: ModelId
        {
            return Task.Run(() =>
            {
                if (id <= 0)
                {
                    return Utils.ResultGetError<T>(ResultErrorType.InvalidId, nameof(id));
                }

                var item = list.FirstOrDefault(x => x.Id == id);
                if (item is null)
                {
                    return Utils.ResultGetError<T>(ResultErrorType.NotFound);
                }

                return Utils.ResultGetOk<T>(item);
            });
        }

        private static Task<ResultGet<ModelId>> Add<T,F>(List<T> list, F data) where F: ModelId => Task.Run(() => Utils.ResultGetOk<ModelId>(data));

        private static Task<Result> CheckIdExists<T>(List<T> list, long id) where T : ModelId
        {
            return Task.Run(() =>
            {
                //We must check if the Id exists so the tests with invalid Id don't Fail
                var item = list.FirstOrDefault(x => x.Id == id);
                if (item is null)
                {
                    return Utils.ResultGetError<T>(ResultErrorType.NotFound);
                }

                return Utils.ResultOk();
            });
        }
        #endregion
    }
}