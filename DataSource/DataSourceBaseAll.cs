using Deve.Model;
using Deve.Internal.Data;

namespace Deve.DataSource
{
    internal abstract class DataSourceBaseAll<ModelList, Model, Criteria> : DataSourceBaseGet<ModelList, Model, Criteria>, IDataAll<ModelList, Model, Criteria> where Criteria: Deve.Criteria.Criteria
    {
        #region Constructor
        protected DataSourceBaseAll(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region IDataAll
        public abstract Task<ResultGet<ModelId>> Add(Model data);
        public abstract Task<Result> Update(Model data);
        public abstract Task<Result> Delete(long id);
        #endregion
    }
}