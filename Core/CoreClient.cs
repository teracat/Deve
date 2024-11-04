using Deve.Auth;
using Deve.Internal;

namespace Deve.Core
{
    internal class CoreClient : CoreBaseAll<Client, Client, CriteriaClient>, IDataClient
    {
        #region CoreBaseAll Abstract Properties
        protected override IDataAll<Client, Client, CriteriaClient> DataAll => Source.Clients;
        protected override PermissionDataType DataType => PermissionDataType.Client;
        #endregion

        #region Constructor
        public CoreClient(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(Client data)
        {
            var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                                .CheckNotNullOrEmpty(new Field(data.Id), new Field(data.Name));

            //Check Valid CityId
            City? city = null;
            if (data.Location.CityId.HasValue)
            {
                var cityRes = await Source.Cities.Get(data.Location.CityId.Value);
                if (!cityRes.Success)
                    return cityRes;
                resultBuilder.CheckNotNull(cityRes.Data, nameof(data.Location.CityId));
                city = cityRes.Data;
            }

            //Check Valid StateId
            State? state = null;
            if (data.Location.StateId.HasValue)
            {
                var stateRes = await Source.States.Get(data.Location.StateId.Value);
                if (!stateRes.Success)
                    return stateRes;
                resultBuilder.CheckNotNull(stateRes.Data, nameof(data.Location.StateId));
                state = stateRes.Data;
            }

            //Check Valid CountryId
            Country? country = null;
            if (data.Location.CountryId.HasValue)
            {
                var countryRes = await Source.Countries.Get(data.Location.CountryId.Value);
                if (!countryRes.Success)
                    return countryRes;
                resultBuilder.CheckNotNull(countryRes.Data, nameof(data.Location.CountryId));
                country = countryRes.Data;
            }

            //Copy City, State & Country Name
            data.Location.Country = country?.Name;
            data.Location.State = state?.Name;
            data.Location.City = city?.Name;

            return resultBuilder.ToResult();
        }

        protected override Task<Result> CheckAdd(Client data, IList<Client> list)
        {
            return Task.Run(() =>
            {
                if (list.Any(x => x.Id == data.Id))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Id));

                if (list.Any(x => x.Name.Equals(data.Name, StringComparison.InvariantCultureIgnoreCase)))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Name));

                return Utils.ResultOk();
            });
        }
        #endregion

        #region IDataClient
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            var resPerm = await CheckPermission(PermissionType.Update);
            if (!resPerm.Success)
                return Utils.ResultError(resPerm);

            return await Source.Clients.UpdateStatus(id, newStatus);
        }
        #endregion
    }
}
