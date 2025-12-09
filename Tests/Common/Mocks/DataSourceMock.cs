using Deve.Criteria;
using Deve.DataSource;
using Deve.Internal.Criteria;
using Deve.Internal.Model;
using Deve.Model;
using Moq;

namespace Deve.Tests.Mocks
{
    internal class DataSourceMock : Mock<IDataSource>
    {
        public DataSourceMock()
        {
            // Users
            _ = Setup(d => d.Users.Get(It.IsAny<CriteriaUser?>())).Returns<CriteriaUser>((c) => GetListUser(DataMock.Users, c));
            _ = Setup(d => d.Users.Get()).Returns(() => GetListUser(DataMock.Users, null));
            _ = Setup(d => d.Users.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Users, id));
            _ = Setup(d => d.Users.Add(It.IsAny<User>())).Returns<User>((c) => Add(c));
            _ = Setup(d => d.Users.Update(It.IsAny<User>())).Returns<User>((c) => CheckIdExists(DataMock.Users, c.Id));
            _ = Setup(d => d.Users.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Users, id));

            // Countries
            _ = Setup(d => d.Countries.Get(It.IsAny<CriteriaCountry?>())).Returns<CriteriaCountry>((_) => GetList(DataMock.Countries));
            _ = Setup(d => d.Countries.Get()).Returns(() => GetList(DataMock.Countries));
            _ = Setup(d => d.Countries.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Countries, id));
            _ = Setup(d => d.Countries.Add(It.IsAny<Country>())).Returns<Country>((c) => Add(c));
            _ = Setup(d => d.Countries.Update(It.IsAny<Country>())).Returns<Country>((c) => CheckIdExists(DataMock.Countries, c.Id));
            _ = Setup(d => d.Countries.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Countries, id));

            // States
            _ = Setup(d => d.States.Get(It.IsAny<CriteriaState?>())).Returns<CriteriaState>((_) => GetList(DataMock.States));
            _ = Setup(d => d.States.Get()).Returns(() => GetList(DataMock.States));
            _ = Setup(d => d.States.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.States, id));
            _ = Setup(d => d.States.Add(It.IsAny<State>())).Returns<State>((c) => Add(c));
            _ = Setup(d => d.States.Update(It.IsAny<State>())).Returns<State>((c) => CheckIdExists(DataMock.States, c.Id));
            _ = Setup(d => d.States.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.States, id));

            // Cities
            _ = Setup(d => d.Cities.Get(It.IsAny<CriteriaCity?>())).Returns<CriteriaCity>((_) => GetList(DataMock.Cities));
            _ = Setup(d => d.Cities.Get()).Returns(() => GetList(DataMock.Cities));
            _ = Setup(d => d.Cities.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Cities, id));
            _ = Setup(d => d.Cities.Add(It.IsAny<City>())).Returns<City>((c) => Add(c));
            _ = Setup(d => d.Cities.Update(It.IsAny<City>())).Returns<City>((c) => CheckIdExists(DataMock.Cities, c.Id));
            _ = Setup(d => d.Cities.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Cities, id));

            // Clients
            _ = Setup(d => d.Clients.Get(It.IsAny<CriteriaClient?>())).Returns<CriteriaClient>((_) => GetList(DataMock.Clients));
            _ = Setup(d => d.Clients.Get()).Returns(() => GetList(DataMock.Clients));
            _ = Setup(d => d.Clients.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Clients, id));
            _ = Setup(d => d.Clients.Add(It.IsAny<Client>())).Returns<Client>((c) => Add(c));
            _ = Setup(d => d.Clients.Update(It.IsAny<Client>())).Returns<Client>((c) => CheckIdExists(DataMock.Clients, c.Id));
            _ = Setup(d => d.Clients.Delete(It.IsAny<long>())).Returns<long>((id) => CheckIdExists(DataMock.Clients, id));
            _ = Setup(d => d.Clients.UpdateStatus(It.IsAny<long>(), It.IsAny<ClientStatus>())).Returns<long, ClientStatus>((id, _) => CheckIdExists(DataMock.Clients, id));

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
            _ = Setup(d => d.ClientsBasic.Get(It.IsAny<CriteriaClientBasic?>())).Returns<CriteriaClientBasic>((_) => GetList(clientsBasic));
            _ = Setup(d => d.ClientsBasic.Get()).Returns(() => GetList(clientsBasic));
            _ = Setup(d => d.ClientsBasic.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Clients.Select(x => x as External.Model.Client).ToList(), id));
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

        private static Task<ResultGet<T>> Get<T>(List<T> list, long id) where T : ModelId
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

        private static Task<ResultGet<ModelId>> Add<F>(F data) where F : ModelId => Task.Run(() => Utils.ResultGetOk<ModelId>(data));

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