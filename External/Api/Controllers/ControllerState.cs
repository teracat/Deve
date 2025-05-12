using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseGet<State, State, CriteriaState>
    {
        private readonly IDataState _data;

        protected override IDataGet<State, State, CriteriaState> DataGet => _data;

        public ControllerState(IDataState data)
        {
            _data = data;
        }
    }
}
