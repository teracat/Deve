﻿using Deve.Internal;

namespace Deve.DataSource
{
    internal abstract class DataSourceBaseAll<ModelList, Model, Criteria> : DataSourceBaseGet<ModelList, Model, Criteria>, IDataAll<ModelList, Model, Criteria>
    {
        #region Constructor
        public DataSourceBaseAll(DataSourceMain dataSourceMain)
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
