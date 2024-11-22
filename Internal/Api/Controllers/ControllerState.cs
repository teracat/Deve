using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseAll<State, State, CriteriaState>
    {
        protected override IDataAll<State, State, CriteriaState> DataAll => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder)
            : base(contextAccessor, dataSourceBuilder)
        {
        }
    }
}
