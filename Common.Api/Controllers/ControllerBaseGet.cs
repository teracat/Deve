using Microsoft.AspNetCore.Mvc;
using Deve.External;
using Deve.DataSource;

namespace Deve.Api
{
    public abstract class ControllerBaseGet<ModelList, Model, Criteria> : ControllerBaseAuth
    {
        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        #endregion

        #region Constructor
        public ControllerBaseGet(IHttpContextAccessor contextAccessor, IDataSourceFactory dataSourceFactory)
            : base(contextAccessor, dataSourceFactory)
        {
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<ResultGetList<ModelList>> Get([FromQuery] Criteria? criteria)
        {
            return await DataGet.Get(criteria);
        }

        [HttpGet, Route("{id}")]
        public async Task<ResultGet<Model>> Get(long id)
        {
            return await DataGet.Get(id);
        }
        #endregion
    }
}
