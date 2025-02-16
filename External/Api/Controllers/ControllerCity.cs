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
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseGet<City, City, CriteriaCity>
    {
        protected override IDataGet<City, City, CriteriaCity> DataGet => Core.Cities;

        public ControllerCity(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
    }
}