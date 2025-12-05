using Deve.Internal.Data;
using Deve.Model;

namespace Deve.DataSource
{
    internal abstract class DataSourceBaseAll<ModelList, Model, Criteria> : DataSourceBaseGet<ModelList, Model, Criteria>, IDataAll<ModelList, Model, Criteria> where Criteria : Deve.Criteria.Criteria
    {
        #region Constructor
        protected DataSourceBaseAll(IDataSource dataSource)
            : base(dataSource)
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