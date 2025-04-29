using Deve.Model;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Data;
using Deve.DataSource;
using Deve.External.Data;

namespace Deve.Core
{
    public abstract class CoreBaseGet<ModelList, Model, Criteria> : CoreBase, IDataGet<ModelList, Model, Criteria>
    {
        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        protected abstract PermissionDataType DataType { get; }
        #endregion

        #region Constructor
        public CoreBaseGet(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentity? userIdentity)
            : base(dataSource, auth, options, userIdentity)
        {
        }
        #endregion

        #region IDataGet Methods
        public virtual async Task<ResultGetList<ModelList>> Get(Criteria? criteria)
        {
            var resPerm = await CheckPermission(PermissionType.GetList);
            if (!resPerm.Success)
            {
                return Utils.ResultGetListError<ModelList>(resPerm);
            }

            return await DataGet.Get(criteria);
        }

        public virtual async Task<ResultGetList<ModelList>> Get() => await Get(default(Criteria));

        public virtual async Task<ResultGet<Model>> Get(long id)
        {
            var resPerm = await CheckPermission(PermissionType.Get);
            if (!resPerm.Success)
            {
                return Utils.ResultGetError<Model>(resPerm);
            }

            if (id <= 0)
            {
                return Utils.ResultGetError<Model>(Options.LangCode, ResultErrorType.MissingRequiredField, nameof(id));
            }

            return await DataGet.Get(id);
        }
        #endregion

        #region Methods
        protected async virtual Task<Result> CheckPermission(PermissionType type)
        {
            return await CheckPermission(type, DataType);
        }
        #endregion
    }
}
