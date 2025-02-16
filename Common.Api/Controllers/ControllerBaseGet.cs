using Microsoft.AspNetCore.Mvc;
using Deve.External.Data;
using Deve.Model;
using Deve.Auth.TokenManagers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Api.Controllers
{
    public abstract class ControllerBaseGet<ModelList, Model, Criteria> : ControllerBaseAuth
    {
        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        #endregion

        #region Constructor
        protected ControllerBaseGet(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
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