using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Auth.TokenManagers;
using Deve.Internal.Data;
using Deve.Api.Controllers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseAll<State, State, CriteriaState>
    {
        protected override IDataAll<State, State, CriteriaState> DataAll => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
    }
}