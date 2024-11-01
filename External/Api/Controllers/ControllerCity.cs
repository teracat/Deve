using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.External.Api
{
    [ApiController]
    [Route(ApiConstants.ApiPathCity)]
    public class ControllerCity : ControllerBaseGet<City, City, CriteriaCity>
    {
        protected override IDataGet<City, City, CriteriaCity> DataGet => Core.Cities;

        public ControllerCity(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
