using Microsoft.AspNetCore.Mvc;
using Deve.Internal.Data;
using Deve.Dto;

namespace Deve.Api.Controllers
{
    public abstract class ControllerBaseAll<ModelList, Model, Criteria> : ControllerBaseDeve
    {
        #region Abstract Property
        protected abstract IDataAll<ModelList, Model, Criteria> DataAll { get; }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<ResultGetList<ModelList>> GetList([FromQuery] Criteria? criteria) => await DataAll.Get(criteria);

        [HttpGet, Route("{id}")]
        public async Task<ResultGet<Model>> Get(long id) => await DataAll.Get(id);

        [HttpPost]
        public async Task<Result> Post([FromBody] Model request) => await DataAll.Add(request);

        [HttpPut]
        public async Task<Result> Put([FromBody] Model request) => await DataAll.Update(request);

        [HttpDelete, Route("{id}")]
        public async Task<Result> Delete(long id) => await DataAll.Delete(id);
        #endregion
    }
}
