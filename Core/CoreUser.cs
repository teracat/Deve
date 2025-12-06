using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Core.DataSourceWrappers;
using Deve.Data;
using Deve.DataSource;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

namespace Deve.Core
{
    public class CoreUser : CoreBaseAll<UserBase, UserPlainPassword, CriteriaUser>, IDataUser
    {
        #region Fields
        private readonly DataSourceWrapperUser _wrapperUser;
        #endregion

        #region CoreBaseAll Abstract Properties
        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> DataAll => _wrapperUser;
        protected override PermissionDataType DataType => PermissionDataType.User;
        #endregion

        #region Constructor
        public CoreUser(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService)
            : base(dataSource, auth, options, userIdentityService, null)
        {
            _wrapperUser = new DataSourceWrapperUser(dataSource, auth);
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override Task<Result> CheckRequired(UserPlainPassword data, ChecksActionType action)
        {
            return Task.Run(() =>
            {
                var resultBuilder = ResultBuilder.Create(Options.LangCode)
                                                 .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.Username));

                switch (action)
                {
                    case ChecksActionType.Add:
                        _ = resultBuilder.CheckNotNullOrEmpty(new Field(data.Password));
                        break;

                    case ChecksActionType.Update:
                        _ = resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));
                        break;

                    default:
                        // Nothing to be checked
                        break;
                }

                return resultBuilder.ToResult();
            });
        }

        protected override Task<Result> CheckDuplicated(UserPlainPassword data, IList<UserBase> list, ChecksActionType action)
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

                if (!string.IsNullOrWhiteSpace(data.Username))
                {
                    if (list.Any(x => x.Id != data.Id && !string.IsNullOrWhiteSpace(x.Username) && x.Username.Equals(data.Username, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        return Utils.ResultError(Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Username));
                    }
                }

                if (!string.IsNullOrWhiteSpace(data.Email))
                {
                    if (list.Any(x => x.Id != data.Id && !string.IsNullOrWhiteSpace(x.Email) && x.Email.Equals(data.Email, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        return Utils.ResultError(Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Email));
                    }
                }

                return Utils.ResultOk();
            });
        }

        protected override async Task<Result> CheckDelete(long id)
        {
            var result = await base.CheckDelete(id);
            if (!result.Success)
            {
                return result;
            }

            //A User can't delete its own user
            if (UserIdentityService.UserIdentity is not null && UserIdentityService.UserIdentity.Id == id)
            {
                return Utils.ResultError(Options.LangCode, ResultErrorType.NotAllowed, nameof(id));
            }

            return Utils.ResultOk();
        }
        #endregion
    }
}
