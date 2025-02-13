using Deve.External.Data;
using Deve.Model;

namespace Deve.DataSource
{
    internal abstract class DataSourceBaseGet<ModelList, Model, Criteria> : DataSourceBase, IDataGet<ModelList, Model, Criteria>
    {
        #region Static Atributes
        protected static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);
        #endregion

        #region Constructor
        protected DataSourceBaseGet(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region IDataGet
        public abstract Task<ResultGetList<ModelList>> Get(Criteria? criteria);

        public Task<ResultGetList<ModelList>> Get() => Get(default(Criteria));

        public abstract Task<ResultGet<Model>> Get(long id);
        #endregion
    }
}