using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Internal.Data;

namespace Deve.Api.Controllers
{
    public abstract class ControllerBaseAll<ModelList, Model, Criteria> : ControllerBaseAuth
    {
        #region Abstract Property
        protected abstract IDataAll<ModelList, Model, Criteria> DataAll { get; }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<ResultGetList<ModelList>> GetList([FromQuery] Criteria? criteria)
        {
            return await DataAll.Get(criteria);
        }

        [HttpGet, Route("{id}")]
        public async Task<ResultGet<Model>> Get(long id)
        {
            return await DataAll.Get(id);
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] Model request)
        {
            return await DataAll.Add(request);
        }

        [HttpPut]
        public async Task<Result> Put([FromBody] Model request)
        {
            return await DataAll.Update(request);
        }

        [HttpDelete, Route("{id}")]
        public async Task<Result> Delete(long id)
        {
            return await DataAll.Delete(id);
        }
        #endregion
    }
}
