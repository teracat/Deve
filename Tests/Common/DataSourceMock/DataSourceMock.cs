﻿using Moq;
using Deve.DataSource;
using Deve.Internal;

namespace Deve.Tests
{
    internal class DataSourceMock : Mock<IDataSource>
    {
        public DataSourceMock()
        {
            // Users
            Setup(d => d.Users.Get(It.IsAny<CriteriaUser?>())).Returns<CriteriaUser>((c) => GetListUser(DataMock.Users, c));
            Setup(d => d.Users.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Users, id));
            Setup(d => d.Users.Add(It.IsAny<User>())).Returns<User>((c) => Add(DataMock.Users, c));
            Setup(d => d.Users.Update(It.IsAny<User>())).Returns<User>((c) => Update(DataMock.Users, c.Id));
            Setup(d => d.Users.Delete(It.IsAny<long>())).Returns<long>((id) => Delete(DataMock.Users, id));

            // Countries
            Setup(d => d.Countries.Get(It.IsAny<CriteriaCountry?>())).Returns<CriteriaCountry>((c) => GetList(DataMock.Countries));
            Setup(d => d.Countries.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Countries, id));
            Setup(d => d.Countries.Add(It.IsAny<Country>())).Returns<Country>((c) => Add(DataMock.Countries, c));
            Setup(d => d.Countries.Update(It.IsAny<Country>())).Returns<Country>((c) => Update(DataMock.Countries, c.Id));
            Setup(d => d.Countries.Delete(It.IsAny<long>())).Returns<long>((id) => Delete(DataMock.Countries, id));

            // States
            Setup(d => d.States.Get(It.IsAny<CriteriaState?>())).Returns<CriteriaState>((c) => GetList(DataMock.States));
            Setup(d => d.States.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.States, id));
            Setup(d => d.States.Add(It.IsAny<State>())).Returns<State>((c) => Add(DataMock.States, c));
            Setup(d => d.States.Update(It.IsAny<State>())).Returns<State>((c) => Update(DataMock.States, c.Id));
            Setup(d => d.States.Delete(It.IsAny<long>())).Returns<long>((id) => Delete(DataMock.States, id));

            // Cities
            Setup(d => d.Cities.Get(It.IsAny<CriteriaCity?>())).Returns<CriteriaCity>((c) => GetList(DataMock.Cities));
            Setup(d => d.Cities.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Cities, id));
            Setup(d => d.Cities.Add(It.IsAny<City>())).Returns<City>((c) => Add(DataMock.Cities, c));
            Setup(d => d.Cities.Update(It.IsAny<City>())).Returns<City>((c) => Update(DataMock.Cities, c.Id));
            Setup(d => d.Cities.Delete(It.IsAny<long>())).Returns<long>((id) => Delete(DataMock.Cities, id));

            // Clients
            Setup(d => d.Clients.Get(It.IsAny<CriteriaClient?>())).Returns<CriteriaClient>((c) => GetList(DataMock.Clients));
            Setup(d => d.Clients.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Clients, id));
            Setup(d => d.Clients.Add(It.IsAny<Client>())).Returns<Client>((c) => Add(DataMock.Clients, c));
            Setup(d => d.Clients.Update(It.IsAny<Client>())).Returns<Client>((c) => Update(DataMock.Clients, c.Id));
            Setup(d => d.Clients.Delete(It.IsAny<long>())).Returns<long>((id) => Delete(DataMock.Clients, id));
            Setup(d => d.Clients.UpdateStatus(It.IsAny<long>(), It.IsAny<ClientStatus>())).Returns<long, ClientStatus>((id, status) => Update(DataMock.Clients, id));

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
            Setup(d => d.ClientsBasic.Get(It.IsAny<CriteriaClientBasic?>())).Returns<CriteriaClientBasic>((c) => GetList(clientsBasic));
            Setup(d => d.ClientsBasic.Get(It.IsAny<long>())).Returns<long>((id) => Get(DataMock.Clients.Select(x => x as External.Client).ToList(), id));
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
                    }

                    if (!string.IsNullOrWhiteSpace(criteria.Username))
                        qry = qry.Where(x => x.Username.Equals(criteria.Username, StringComparison.InvariantCultureIgnoreCase));

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
                    return Utils.ResultGetError<T>(ResultErrorType.InvalidId, nameof(id));

                var item = list.FirstOrDefault(x => x.Id == id);
                if (item is null)
                    return Utils.ResultGetError<T>(ResultErrorType.NotFound);

                return Utils.ResultGetOk<T>(item);
            });
        }

        private static Task<ResultGet<ModelId>> Add<T,F>(List<T> list, F data) where F: ModelId => Task.Run(() => Utils.ResultGetOk<ModelId>(data));

        private static Task<Result> Update<T>(List<T> list, long id) where T : ModelId
        {
            return Task.Run(() =>
            {
                //We must check if the Id exists so the tests with invalid Id don't Fail
                var item = list.FirstOrDefault(x => x.Id == id);
                if (item is null)
                    return Utils.ResultGetError<T>(ResultErrorType.NotFound);

                return Utils.ResultOk();
            });
        }

        private static Task<Result> Delete<T>(List<T> list, long id) where T : ModelId
        {
            return Task.Run(() =>
            {
                //We must check if the Id exists so the tests with invalid Id don't Fail
                var item = list.FirstOrDefault(x => x.Id == id);
                if (item is null)
                    return Utils.ResultGetError<T>(ResultErrorType.NotFound);
                
                return Utils.ResultOk();
            });
        }
        #endregion
    }
}
