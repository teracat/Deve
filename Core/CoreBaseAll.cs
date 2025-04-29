using Deve.Model;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Data;
using Deve.DataSource;
using Deve.Internal.Data;

namespace Deve.Core
{
    public abstract class CoreBaseAll<ModelList, Model, Criteria> : CoreBaseGet<ModelList, Model, Criteria>, IDataAll<ModelList, Model, Criteria>
    {
        #region Abstract Property
        protected abstract IDataAll<ModelList, Model, Criteria> DataAll { get; }
        #endregion

        #region CoreBaseGet Properties
        protected override IDataAll<ModelList, Model, Criteria> DataGet => DataAll;
        #endregion

        #region Constructor
        public CoreBaseAll(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentity? userIdentity)
            : base(dataSource, auth, options, userIdentity)
        {
        }
        #endregion

        #region IDataAll Methods
        public virtual async Task<ResultGet<ModelId>> Add(Model data)
        {
            var resPerm = await CheckPermission(PermissionType.Add);
            if (!resPerm.Success)
            {
                return Utils.ResultGetError<ModelId>(resPerm);
            }

            //Some basic checks
            if (data is null)
            {
                return Utils.ResultGetError<ModelId>(Options.LangCode, ResultErrorType.MissingRequiredField);
            }

            var resRequired = await CheckRequired(data, ChecksActionType.Add);
            if (!resRequired.Success)
            {
                return Utils.ResultGetError<ModelId>(resRequired);
            }

            //Check duplicated
            var resList = await DataAll.Get();
            if (!resList.Success)
            {
                return Utils.ResultGetError<ModelId>(resList);
            }

            if (resList.Data is null)
            {
                return Utils.ResultGetError<ModelId>(Options.LangCode, ResultErrorType.Unknown);
            }

            var resDuplicated = await CheckDuplicated(data, resList.Data, ChecksActionType.Add);
            if (!resDuplicated.Success)
            {
                return Utils.ResultGetError<ModelId>(resDuplicated);
            }

            //Add
            return await DataAll.Add(data);
        }

        public virtual async Task<Result> Update(Model data)
        {
            var resPerm = await CheckPermission(PermissionType.Update);
            if (!resPerm.Success)
            {
                return Utils.ResultGetError<Model>(resPerm);
            }

            //Some basic checks
            if (data is null)
            {
                return Utils.ResultGetError<ModelId>(Options.LangCode, ResultErrorType.MissingRequiredField);
            }

            var result = await CheckRequired(data, ChecksActionType.Update);
            if (!result.Success)
            {
                return result;
            }

            //Check duplicated
            var resList = await DataAll.Get();
            if (!resList.Success)
            {
                return resList;
            }

            if (resList.Data is null)
            {
                return Utils.ResultError(Options.LangCode, ResultErrorType.Unknown);
            }

            var resChecksAdd = await CheckDuplicated(data, resList.Data, ChecksActionType.Update);
            if (!resChecksAdd.Success)
            {
                return resChecksAdd;
            }

            //Update
            return await DataAll.Update(data);
        }

        public virtual async Task<Result> Delete(long id)
        {
            var resPerm = await CheckPermission(PermissionType.Delete);
            if (!resPerm.Success)
            {
                return Utils.ResultGetError<Model>(resPerm);
            }

            //Some basic checks
            var errorBuilder = ResultBuilder.Create(Options.LangCode)
                                            .CheckNotNullOrEmpty(new Field(id));
            if (errorBuilder.HasErrors)
            {
                return errorBuilder.ToResult();
            }

            var result = await CheckDelete(id);
            if (!result.Success)
            {
                return result;
            }

            //Remove
            return await DataAll.Delete(id);
        }
        #endregion

        #region Helper Methods
        protected abstract Task<Result> CheckRequired(Model data, ChecksActionType action);
        protected abstract Task<Result> CheckDuplicated(Model data, IList<ModelList> list, ChecksActionType action);
        protected virtual Task<Result> CheckDelete(long id)
        {
            return Task.Run(() =>
            {
                return ResultBuilder.Create(Options.LangCode)
                                    .CheckNotNullOrEmpty(new Field(id))
                                    .ToResult();
            });
        }
        #endregion
    }
}
