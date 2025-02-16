using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Auth.TokenManagers;
using Deve.External.Data;
using Deve.Api.Controllers;
using Deve.Api.DataSourceBuilder;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathState)]
    public class ControllerState : ControllerBaseGet<State, State, CriteriaState>
    {
        protected override IDataGet<State, State, CriteriaState> DataGet => Core.States;

        public ControllerState(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
    }
}