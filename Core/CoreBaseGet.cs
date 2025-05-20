using Deve.Model;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Data;
using Deve.DataSource;
using Deve.Cache;
using Deve.External.Data;

namespace Deve.Core
{
    public abstract class CoreBaseGet<ModelList, Model, Criteria> : CoreBase, IDataGet<ModelList, Model, Criteria>
    {
        #region Properties
        protected ICache? Cache { get; }
        #endregion

        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        protected abstract PermissionDataType DataType { get; }
        #endregion

        #region Constructor
        protected CoreBaseGet(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, ICache? cache)
            : base(dataSource, auth, options, userIdentityService)
        {
            Cache = cache;
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

            // Example of using cache: if the cache is enabled and the item is already in the cache, return it directly.
            if (Cache is not null && Cache.TryGet(UtilsCore.GetCacheKeyForType<Model>(id), out Model value))
            {
                return Utils.ResultGetOk(value);
            }

            var resGet = await DataGet.Get(id);
            if (Cache is not null && resGet.Success)
            {
                // If the item is not in the cache, add it.
                Cache.Set(UtilsCore.GetCacheKeyForType<Model>(id), resGet.Data);
            }

            return resGet;
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
