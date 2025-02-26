using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Core;
using Deve.External.Data;

namespace Deve.Api.Controllers
{
    public abstract class ControllerBaseGet<ModelList, Model, Criteria> : ControllerBaseAuth
    {
        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        #endregion

        #region Constructor
        protected ControllerBaseGet(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
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