using Deve.Auth;
using Deve.DataSource;

namespace Deve.Core
{
    internal abstract class CoreBase
    {
        #region Fields
        private IAuth? _auth;
        #endregion

        #region Properties
        protected CoreMain Core { get; }
        protected IDataSource Source => Core.DataSource;
        protected IAuth Auth => _auth ??= AuthFactory.Get(Source, Core.Options);
        #endregion

        #region Constructor
        public CoreBase(CoreMain core)
        {
            Core = core;
        }
        #endregion

        #region Methods
        protected async virtual Task<Result> CheckPermission(PermissionType type, PermissionDataType dataType)
        {
            var permissionResult = await Auth.IsGranted(Core.UserIdentity, type, dataType);
            switch (permissionResult)
            {
                case PermissionResult.Granted:
                    return Utils.ResultOk();
                default:
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.Unauthorized);
            }
        }
        #endregion
    }
}
