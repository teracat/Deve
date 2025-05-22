using Deve.Model;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Data;
using Deve.DataSource;
using Deve.Internal.Data;
using Deve.Cache;

namespace Deve.Core
{
    public abstract class CoreBaseAll<ModelList, Model, Criteria> : CoreBaseGet<ModelList, Model, Criteria>, IDataAll<ModelList, Model, Criteria> where Model: ModelId
    {
        #region Abstract Property
        protected abstract IDataAll<ModelList, Model, Criteria> DataAll { get; }
        #endregion

        #region CoreBaseGet Properties
        protected override IDataAll<ModelList, Model, Criteria> DataGet => DataAll;
        #endregion

        #region Constructor
        protected CoreBaseAll(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, ICache? cache)
            : base(dataSource, auth, options, userIdentityService, cache)
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
            var res = await DataAll.Add(data);
            if (res.Success)
            {
                await Added();
            }
            return res;
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
            var res = await DataAll.Update(data);
            if (res.Success)
            {
                await Updated(data.Id);
            }
            return res;
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
            var res = await DataAll.Delete(id);
            if (res.Success)
            {
                await Deleted(id);
            }
            return res;
        }
        #endregion

        #region Helper Methods
        // This method must be overridden in derived classes to perform checks on the data before adding or updating.
        protected abstract Task<Result> CheckRequired(Model data, ChecksActionType action);

        // This method must be overridden in derived classes to check for duplicates in the data.
        protected abstract Task<Result> CheckDuplicated(Model data, IList<ModelList> list, ChecksActionType action);

        // This method can be overridden in derived classes to perform additional checks before deleting an item.
        protected virtual Task<Result> CheckDelete(long id)
        {
            return Task.Run(() =>
            {
                return ResultBuilder.Create(Options.LangCode)
                                    .CheckNotNullOrEmpty(new Field(id))
                                    .ToResult();
            });
        }

        // This method can be overridden in derived classes to perform additional actions when the data changes (called when added, updated or deleted).
        protected virtual Task Changed() => Task.CompletedTask;

        // This method can be overridden in derived classes to perform additional actions after an item is added.
        protected virtual Task Added() => Changed();

        // This method can be overridden in derived classes to perform additional actions after an item is updated.
        protected virtual async Task Updated(long id)
        {
            await Changed();
            await RemoveFromCache(id);
        }

        // This method can be overridden in derived classes to perform additional actions after an item is deleted.
        protected virtual async Task Deleted(long id)
        {
            await Changed();
            await RemoveFromCache(id);
        }

        // If the cache is used, this method removes the item from the cache after it's updated or deleted.
        private Task RemoveFromCache(long id) => Task.Run(() =>
        {
            Cache?.Remove(UtilsCore.GetCacheKeyForType<Model>(id));
        });
        #endregion
    }
}
