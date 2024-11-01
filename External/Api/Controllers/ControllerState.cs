using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.External.Api
{
    [ApiController]
    [Route(ApiConstants.ApiPathState)]
    public class ControllerState : ControllerBaseGet<State, State, CriteriaState>
    {
        protected override IDataGet<State, State, CriteriaState> DataGet => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
