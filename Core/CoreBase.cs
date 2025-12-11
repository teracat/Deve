using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Data;
using Deve.DataSource;
using Deve.Dto;

namespace Deve.Core
{
    public abstract class CoreBase
    {
        #region Properties
        protected IDataSource Source { get; }
        protected IAuth Auth { get; }
        protected IDataOptions Options { get; }
        protected IUserIdentityService UserIdentityService { get; }
        #endregion

        #region Constructor
        protected CoreBase(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService)
        {
            Source = dataSource;
            Auth = auth;
            Options = options;
            UserIdentityService = userIdentityService;
        }
        #endregion

        #region Methods
        protected virtual async Task<Result> CheckPermission(PermissionType type, PermissionDataType dataType)
        {
            var userIdentity = UserIdentityService.UserIdentity;
            var permissionResult = await Auth.IsGranted(userIdentity, type, dataType);
            if (permissionResult == PermissionResult.Granted)
            {
                return Utils.ResultOk();
            }

            return Utils.ResultError(Options.LangCode, ResultErrorType.Unauthorized);
        }
        #endregion
    }
}
