using Deve.Model;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Internal.Criteria;
using Deve.Data;
using Deve.DataSource;
using Deve.Cache;

namespace Deve.Core
{
    public class CoreClient : CoreBaseAll<Client, Client, CriteriaClient>, IDataClient
    {
        #region Fields
        private readonly IDataCity _dataCity;
        private readonly IDataState _dataState;
        private readonly IDataCountry _dataCountry;
        #endregion

        #region CoreBaseAll Abstract Properties
        protected override IDataAll<Client, Client, CriteriaClient> DataAll => Source.Clients;
        protected override PermissionDataType DataType => PermissionDataType.Client;
        #endregion

        #region Constructor
        public CoreClient(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, IDataCity dataCity, IDataState dataState, IDataCountry dataCountry, ICache cache)
            : base(dataSource, auth, options, userIdentityService, cache)
        {
            _dataCity = dataCity;
            _dataState = dataState;
            _dataCountry = dataCountry;
        }
        #endregion

        #region Overrides
        protected override Task Changed()
        {
            // Clear the cache for ClientStats when cient data changes
            Cache?.Remove(nameof(ClientStats));
            return base.Changed();
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(Client data, ChecksActionType action)
        {
            var resultBuilder = ResultBuilder.Create(Options.LangCode)
                                .CheckNotNullOrEmpty(new Field(data.Name));

            if (action == ChecksActionType.Update)
            {
                resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));
            }

            //Check Valid CityId
            City? city = null;
            if (data.Location.CityId.HasValue)
            {
                var cityRes = await _dataCity.Get(data.Location.CityId.Value);
                if (!cityRes.Success)
                {
                    return cityRes;
                }

                resultBuilder.CheckNotNull(cityRes.Data, nameof(data.Location.CityId));
                city = cityRes.Data;
            }

            //Check Valid StateId
            State? state = null;
            if (data.Location.StateId.HasValue)
            {
                var stateRes = await _dataState.Get(data.Location.StateId.Value);
                if (!stateRes.Success)
                {
                    return stateRes;
                }

                resultBuilder.CheckNotNull(stateRes.Data, nameof(data.Location.StateId));
                state = stateRes.Data;
            }

            //Check Valid CountryId
            Country? country = null;
            if (data.Location.CountryId.HasValue)
            {
                var countryRes = await _dataCountry.Get(data.Location.CountryId.Value);
                if (!countryRes.Success)
                {
                    return countryRes;
                }

                resultBuilder.CheckNotNull(countryRes.Data, nameof(data.Location.CountryId));
                country = countryRes.Data;
            }

            //Copy City, State & Country Name
            data.Location.Country = country?.Name;
            data.Location.State = state?.Name;
            data.Location.City = city?.Name;

            return resultBuilder.ToResult();
        }

        protected override Task<Result> CheckDuplicated(Client data, IList<Client> list, ChecksActionType action)
        {
            return Task.Run(() =>
            {
                if (action == ChecksActionType.Add)
                {
                    var resCheckId = UtilsCore.CheckIdWhenAdding(Options, data, list);
                    if (resCheckId is not null)
                    {
                        return resCheckId;
                    }
                }

                if (list.Any(x => x.Id != data.Id && x.Name.Equals(data.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return Utils.ResultError(Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Name));
                }

                return Utils.ResultOk();
            });
        }
        #endregion

        #region IDataClient
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            var resPerm = await CheckPermission(PermissionType.Update);
            if (!resPerm.Success)
            {
                return Utils.ResultError(resPerm);
            }

            return await Source.Clients.UpdateStatus(id, newStatus);
        }
        #endregion
    }
}
