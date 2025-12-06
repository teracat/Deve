using Deve.Api.Controllers;
using Deve.Criteria;
using Deve.External.Data;
using Deve.Model;
using Microsoft.AspNetCore.Mvc;

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
