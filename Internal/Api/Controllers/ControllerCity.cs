using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseAll<City, City, CriteriaCity>
    {
        protected override IDataAll<City, City, CriteriaCity> DataAll => Core.Cities;

        public ControllerCity(IHttpContextAccessor contextAccessor, IDataSourceFactory dataSourceFactory)
            : base(contextAccessor, dataSourceFactory)
        {
        }
    }
}
