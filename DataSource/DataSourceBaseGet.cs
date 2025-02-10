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
        public DataSourceBaseGet(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region IDataGet
        public abstract Task<ResultGetList<ModelList>> Get(Criteria? criteria = default);
        public abstract Task<ResultGet<Model>> Get(long id);
        #endregion
    }
}