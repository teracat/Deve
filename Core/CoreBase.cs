using Deve.Model;
using Deve.DataSource;
using Deve.Auth.Permissions;

namespace Deve.Core
{
    internal abstract class CoreBase
    {
        #region Properties
        protected CoreMain Core { get; }
        protected IDataSource Source => Core.DataSource;
        #endregion

        #region Constructor
        protected CoreBase(CoreMain core)
        {
            Core = core;
        }
        #endregion

        #region Methods
        protected async virtual Task<Result> CheckPermission(PermissionType type, PermissionDataType dataType)
        {
            var permissionResult = await Core.Auth.IsGranted(Core.UserIdentity, type, dataType);
            if (permissionResult == PermissionResult.Granted)
            {
                return Utils.ResultOk();
            }

            return Utils.ResultError(Core.Options.LangCode, ResultErrorType.Unauthorized);
        }
        #endregion
    }
}