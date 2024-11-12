using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseAll<City, City, CriteriaCity>
    {
        protected override IDataAll<City, City, CriteriaCity> DataAll => Core.Cities;

        public ControllerCity(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
