using Deve.External.Data;
using Deve.Dto;

namespace Deve.DataSource
{
    internal abstract class DataSourceBaseGet<ModelList, Model, Criteria> : DataSourceBase, IDataGet<ModelList, Model, Criteria> where Criteria : Deve.Dto.Criteria
    {
        #region Static Atributes
        protected static SemaphoreSlim Semaphore { get; } = new SemaphoreSlim(1);
        #endregion

        #region Constructor
        protected DataSourceBaseGet(IDataSource dataSource)
            : base(dataSource)
        {
        }
        #endregion

        #region Methods
        protected virtual ResultGetList<ModelList> ApplyOffsetAndLimit(IQueryable<ModelList> query, Criteria criteria, string orderBy)
        {
            //Total Count
            int totalCount = query.Count();

            //Limit & Offset
            if (criteria.Offset.HasValue)
            {
                query = query.Skip(criteria.Offset.Value);
            }

            if (criteria.Limit.HasValue)
            {
                query = query.Take(criteria.Limit.Value);
            }

            //Execute Query
            var data = query.ToList();

            //Return result
            return Utils.ResultGetListOk(data, criteria.Offset, criteria.Limit, orderBy, totalCount);
        }
        #endregion

        #region IDataGet
        public abstract Task<ResultGetList<ModelList>> Get(Criteria? criteria);

        public Task<ResultGetList<ModelList>> Get() => Get(default(Criteria));

        public abstract Task<ResultGet<Model>> Get(long id);
        #endregion
    }
}