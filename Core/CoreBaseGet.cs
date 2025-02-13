using Deve.Auth.Permissions;
using Deve.External.Data;
using Deve.Model;

namespace Deve.Core
{
    internal abstract class CoreBaseGet<ModelList, Model, Criteria> : CoreBase, IDataGet<ModelList, Model, Criteria>
    {
        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        protected abstract PermissionDataType DataType { get; }
        #endregion

        #region Constructor
        public CoreBaseGet(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region IDataGet Methods
        public virtual async Task<ResultGetList<ModelList>> Get(Criteria? criteria)
        {
            var resPerm = await CheckPermission(PermissionType.GetList);
            if (!resPerm.Success)
                return Utils.ResultGetListError<ModelList>(resPerm);

            return await DataGet.Get(criteria);
        }

        public virtual async Task<ResultGetList<ModelList>> Get() => await Get(default(Criteria));

        public virtual async Task<ResultGet<Model>> Get(long id)
        {
            var resPerm = await CheckPermission(PermissionType.Get);
            if (!resPerm.Success)
                return Utils.ResultGetError<Model>(resPerm);

            if (id <= 0)
                return Utils.ResultGetError<Model>(Core.Options.LangCode, ResultErrorType.MissingRequiredField, nameof(id));

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