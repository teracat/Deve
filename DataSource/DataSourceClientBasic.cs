using Deve.Model;
using Deve.Criteria;
using Deve.DataSource.CriteriaHandlers;
using Deve.External.Model;

namespace Deve.DataSource
{
    internal class DataSourceClientBasic : DataSourceBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        #region Constructor
        public DataSourceClientBasic(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region DataSourceBaseGet Implementation
        public override Task<ResultGetList<ClientBasic>> Get(CriteriaClientBasic? criteria = default)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (criteria is null)
                {
                    var list = Data.Clients
                                   .Where(x => x.Status == Internal.Model.ClientStatus.Active)
                                   .OrderBy(x => x.Name)
                                   .Take(Constants.DefaultCriteriaLimit)
                                   .Select(x => new ClientBasic()
                                   {
                                       Id = x.Id,
                                       Name = x.Name,
                                       TradeName = x.TradeName,
                                       City = x.Location.City,
                                       Country = x.Location.Country,
                                       State = x.Location.State,
                                       Latitude = x.Location.Latitude,
                                       Longitude = x.Location.Longitude,
                                   })
                                   .ToList();
                    return Utils.ResultGetListOk(list, null, Constants.DefaultCriteriaLimit, nameof(Client.Name), Data.Clients.Count);
                }

                //Apply Filters
                var criteriaClient = new Internal.Criteria.CriteriaClient(criteria)
                {
                    Status = Internal.Model.ClientStatus.Active
                };
                var qry = CriteriaHandlerClient.Apply(Data.Clients.AsQueryable(), criteriaClient, out string orderBy);

                //Client -> ClientBasic
                var qryBasic = qry.Select(x => new ClientBasic()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TradeName = x.TradeName,
                    City = x.Location.City,
                    Country = x.Location.Country,
                    State = x.Location.State,
                    Latitude = x.Location.Latitude,
                    Longitude = x.Location.Longitude,
                });

                return ApplyOffsetAndLimit(qryBasic, criteria, orderBy);
            });
        }

        public override Task<ResultGet<Client>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (id <= 0)
                {
                    return Utils.ResultGetError<Client>(DataSourceMain.Options.LangCode, ResultErrorType.MissingRequiredField, nameof(ClientBasic.Id));
                }

                var client = Data.Clients.FirstOrDefault(x => x.Id == id) as Client;
                if (client is null)
                {
                    return Utils.ResultGetError<Client>(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);
                }

                return Utils.ResultGetOk(client);
            });
        }
        #endregion
    }
}