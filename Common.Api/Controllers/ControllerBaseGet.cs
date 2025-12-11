using Microsoft.AspNetCore.Mvc;
using Deve.External.Data;
using Deve.Dto;

namespace Deve.Api.Controllers
{
    public abstract class ControllerBaseGet<ModelList, Model, Criteria> : ControllerBaseDeve
    {
        #region Abstract Property
        protected abstract IDataGet<ModelList, Model, Criteria> DataGet { get; }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<ResultGetList<ModelList>> Get([FromQuery] Criteria? criteria) => await DataGet.Get(criteria);

        [HttpGet, Route("{id}")]
        public async Task<ResultGet<Model>> Get(long id) => await DataGet.Get(id);
        #endregion
    }
}
