using Deve.DataSource.CriteriaHandlers;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

namespace Deve.DataSource
{
    internal class DataSourceClient : DataSourceBaseAll<Client, Client, CriteriaClient>, IDataClient
    {
        #region Constructor
        public DataSourceClient(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region DataSourceBaseAll Implementation
        public override Task<ResultGetList<Client>> Get(CriteriaClient? criteria = default)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (criteria is null)
                {
                    var list = Data.Clients
                                   .OrderBy(x => x.Name)
                                   .Take(Constants.DefaultCriteriaLimit)
                                   .ToList();
                    return Utils.ResultGetListOk(list, null, Constants.DefaultCriteriaLimit, nameof(Client.Name), Data.Clients.Count);
                }

                //Apply Filters
                var qry = CriteriaHandlerClient.Apply(Data.Clients.AsQueryable(), criteria, out string orderBy);

                //Total Count
                int totalCount = qry.Count();

                //Limit & Offset
                if (criteria.Offset.HasValue)
                    qry = qry.Skip(criteria.Offset.Value);
                if (criteria.Limit.HasValue)
                    qry = qry.Take(criteria.Limit.Value);

                //Execute Query
                var data = qry.ToList();

                //Return result
                return Utils.ResultGetListOk(data, criteria.Offset, criteria.Limit, orderBy, totalCount);
            });
        }

        public override Task<ResultGet<Client>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                var client = Data.Clients.FirstOrDefault(x => x.Id == id);
                if (client is null)
                    return Utils.ResultGetError<Client>(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                return Utils.ResultGetOk(client);
            });
        }

        public override Task<ResultGet<ModelId>> Add(Client client)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                Data.Clients.Add(client);

                return Utils.ResultGetOk((ModelId)client);
            });
        }

        public override Task<Result> Update(Client client)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(client.Id);
                if (found is null)
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                //Update
                found.Name = client.Name;
                found.TaxName = client.TaxName;
                found.TaxId = client.TaxId;
                found.TradeName = client.TradeName;

                found.Location.Address = client.Location.Address;
                found.Location.CountryId = client.Location.CountryId;
                found.Location.Country = client.Location.Country;
                found.Location.StateId = client.Location.StateId;
                found.Location.State = client.Location.State;
                found.Location.CityId = client.Location.CityId;
                found.Location.City = client.Location.City;
                found.Location.PostalCode = client.Location.PostalCode;
                found.Location.Latitude = client.Location.Latitude;
                found.Location.Longitude = client.Location.Longitude;

                return Utils.ResultOk();
            });
        }

        public override Task<Result> Delete(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(id);
                if (found is null)
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                //Remove
                if (!Data.Clients.Remove(found))
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.Unknown);

                return Utils.ResultOk();
            });
        }
        #endregion

        #region IDataClient Implementation
        public Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(id);
                if (found is null)
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);

                //Update Status
                found.Status = newStatus;

                return Utils.ResultOk();
            });
        }
        #endregion

        #region Methods
        private Client? FindLocal(long id)
        {
            return Data.Clients.FirstOrDefault(x => x.Id == id);
        }
        #endregion
    }
}