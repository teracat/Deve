using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseAll<Country, Country, CriteriaCountry>
    {
        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => Core.Countries;

        public ControllerCountry(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder)
            : base(contextAccessor, dataSourceBuilder)
        {
        }
    }
}
