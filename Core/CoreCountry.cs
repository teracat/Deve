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
    public class CoreCountry : CoreBaseAll<Country, Country, CriteriaCountry>, IDataCountry, External.Data.IDataCountry
    {
        #region CoreBaseAll Abstract Properties
        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => Source.Countries;
        protected override PermissionDataType DataType => PermissionDataType.Country;
        #endregion

        #region Constructor
        public CoreCountry(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService)
            : base(dataSource, auth, options, userIdentityService, null)
        {
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(Country data, ChecksActionType action)
        {
            return await Task.Run(() =>
            {
                var resultBuilder = ResultBuilder.Create(Options.LangCode)
                                                 .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.IsoCode));

                if (action == ChecksActionType.Update)
                {
                    resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));
                }

                return resultBuilder.ToResult();
            });
        }

        protected override Task<Result> CheckDuplicated(Country data, IList<Country> list, ChecksActionType action)
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

                if (list.Any(x => x.Id != data.Id && x.IsoCode.Equals(data.IsoCode, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return Utils.ResultError(Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.IsoCode));
                }

                return Utils.ResultOk();
            });
        }
        #endregion
    }
}
