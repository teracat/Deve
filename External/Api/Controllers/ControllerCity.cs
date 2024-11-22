using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.External.Api
{
    [ApiController]
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseGet<City, City, CriteriaCity>
    {
        protected override IDataGet<City, City, CriteriaCity> DataGet => Core.Cities;

        public ControllerCity(IHttpContextAccessor contextAccessor, IDataSourceFactory dataSourceFactory)
            : base(contextAccessor, dataSourceFactory)
        {
        }
    }
}
