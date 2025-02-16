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
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseAll<Country, Country, CriteriaCountry>
    {
        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => Core.Countries;

        public ControllerCountry(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
    }
}