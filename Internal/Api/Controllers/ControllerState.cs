using Deve.Api.Controllers;
using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Model;
using Microsoft.AspNetCore.Mvc;

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
