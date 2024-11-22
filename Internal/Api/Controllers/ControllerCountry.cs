using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseAll<Country, Country, CriteriaCountry>
    {
        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => Core.Countries;

        public ControllerCountry(IHttpContextAccessor contextAccessor, IDataSourceFactory dataSourceFactory)
            : base(contextAccessor, dataSourceFactory)
        {
        }
    }
}
