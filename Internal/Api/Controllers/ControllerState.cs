using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseAll<State, State, CriteriaState>
    {
        private readonly IDataState _data;

        protected override IDataAll<State, State, CriteriaState> DataAll => _data;

        public ControllerState(IDataState data)
        {
            _data = data;
        }
    }
}
