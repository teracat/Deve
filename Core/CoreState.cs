using Deve.Model;
using Deve.Criteria;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Internal.Data;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Core
{
    public class CoreState : CoreBaseAll<State, State, CriteriaState>, IDataState, External.Data.IDataState
    {
        #region Fields
        private readonly IDataCountry _dataCountry;
        #endregion

        #region CoreBaseAll Abstract Properties
        protected override IDataAll<State, State, CriteriaState> DataAll => Source.States;
        protected override PermissionDataType DataType => PermissionDataType.State;
        #endregion

        #region Constructor
        public CoreState(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, IDataCountry dataCountry)
            : base(dataSource, auth, options, userIdentityService, null)
        {
            _dataCountry = dataCountry;
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(State data, ChecksActionType action)
        {
            var resultBuilder = ResultBuilder.Create(Options.LangCode)
                                             .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.CountryId));

            if (action == ChecksActionType.Update)
            {
                resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));
            }

            //Check Valid CountryId
            if (!Utils.IsEmptyValue(data.CountryId))
            {
                var countryRes = await _dataCountry.Get(data.CountryId);
                if (!countryRes.Success)
                {
                    return countryRes;
                }

                resultBuilder.CheckNotNull(countryRes.Data, nameof(data.CountryId));

                //Copy Country Name
                if (countryRes.Data is not null)
                {
                    data.Country = countryRes.Data.Name;
                }
            }

            return resultBuilder.ToResult();
        }

        protected override Task<Result> CheckDuplicated(State data, IList<State> list, ChecksActionType action)
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
    }
}
