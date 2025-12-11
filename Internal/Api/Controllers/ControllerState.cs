using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Dto;
using Deve.Internal.Data;

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
