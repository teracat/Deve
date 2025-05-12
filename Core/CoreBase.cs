using Deve.Model;
using Deve.Auth.Permissions;
using Deve.Auth;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Core
{
    public abstract class CoreBase
    {
        #region Properties
        protected IDataSource Source { get; }
        protected IAuth Auth { get; }
        protected IDataOptions Options { get; }
        protected IUserIdentity? UserIdentity { get; }
        #endregion

        #region Constructor
        protected CoreBase(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentity? userIdentity)
        {
            Source = dataSource;
            Auth = auth;
            Options = options;
            UserIdentity = userIdentity;
        }
        #endregion

        #region Methods
        protected async virtual Task<Result> CheckPermission(PermissionType type, PermissionDataType dataType)
        {
            var permissionResult = await Auth.IsGranted(UserIdentity, type, dataType);
            if (permissionResult == PermissionResult.Granted)
            {
                return Utils.ResultOk();
            }

            return Utils.ResultError(Options.LangCode, ResultErrorType.Unauthorized);
        }
        #endregion
    }
}
