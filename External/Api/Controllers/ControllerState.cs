using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Core;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseGet<State, State, CriteriaState>
    {
        protected override IDataGet<State, State, CriteriaState> DataGet => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }
    }
}