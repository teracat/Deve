using Deve.Auth;
using Deve.Internal;

namespace Deve.Core
{
    internal abstract class CoreBaseAll<ModelList, Model, Criteria> : CoreBaseGet<ModelList, Model, Criteria>, IDataAll<ModelList, Model, Criteria>
    {
        #region Abstract Property
        protected abstract IDataAll<ModelList, Model, Criteria> DataAll { get; }
        #endregion

        #region CoreBaseGet Properties
        protected override IDataAll<ModelList, Model, Criteria> DataGet => DataAll;
        #endregion

        #region Constructor
        public CoreBaseAll(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region IDataAll Methods
        public virtual async Task<Result> Add(Model data)
        {
            var resPerm = await CheckPermission(PermissionType.Add);
            if (!resPerm.Success)
                return Utils.ResultGetError<Model>(resPerm);

            //Some basic checks
            var result = await CheckRequired(data);
            if (!result.Success)
                return result;

            //Check duplicated
            var resList = await DataAll.Get();
            if (!resList.Success)
                return resList;

            if (resList.Data is null)
                return Utils.ResultError(Core.Options.LangCode, ResultErrorType.Unknown);

            var resChecksAdd = await CheckAdd(data, resList.Data);
            if (!resChecksAdd.Success)
                return resChecksAdd;

            //Add
            return await DataAll.Add(data);
        }

        public virtual async Task<Result> Update(Model data)
        {
            var resPerm = await CheckPermission(PermissionType.Update);
            if (!resPerm.Success)
                return Utils.ResultGetError<Model>(resPerm);

            //Some basic checks
            var result = await CheckRequired(data);
            if (!result.Success)
                return result;

            //Update
            return await DataAll.Update(data);
        }

        public virtual async Task<Result> Delete(long id)
        {
            var resPerm = await CheckPermission(PermissionType.Delete);
            if (!resPerm.Success)
                return Utils.ResultGetError<Model>(resPerm);

            //Some basic checks
            var errorBuilder = ResultBuilder.Create(Core.Options.LangCode)
                               .CheckNotNullOrEmpty(new Field(id));
            if (errorBuilder.HasErrors)
                return errorBuilder.ToResult();

            var result = await CheckDelete(id);
            if (!result.Success)
                return result;

            //Remove
            return await DataAll.Delete(id);
        }
        #endregion

        #region Helper Methods
        protected abstract Task<Result> CheckRequired(Model data);
        protected abstract Task<Result> CheckAdd(Model data, IList<ModelList> list);
        protected virtual Task<Result> CheckDelete(long id)
        {
            return Task.Run(() =>
            {
                return ResultBuilder.Create(Core.Options.LangCode)
                                    .CheckNotNullOrEmpty(new Field(id))
                                    .ToResult();
            });
        }
        #endregion
    }
}
