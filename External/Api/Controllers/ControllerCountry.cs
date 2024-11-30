using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.External.Api
{
    [ApiController]
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseGet<Country, Country, CriteriaCountry>
    {
        protected override IDataGet<Country, Country, CriteriaCountry> DataGet => Core.Countries;

        public ControllerCountry(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder)
            : base(contextAccessor, dataSourceBuilder)
        {
        }
    }
}
