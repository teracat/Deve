using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Core;
using Deve.Internal.Data;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseAll<State, State, CriteriaState>
    {
        protected override IDataAll<State, State, CriteriaState> DataAll => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }
    }
}