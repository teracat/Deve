using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.External.Api
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseGet<State, State, CriteriaState>
    {
        protected override IDataGet<State, State, CriteriaState> DataGet => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder)
            : base(contextAccessor, dataSourceBuilder)
        {
        }
    }
}
