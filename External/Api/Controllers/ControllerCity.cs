using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Core;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseGet<City, City, CriteriaCity>
    {
        protected override IDataGet<City, City, CriteriaCity> DataGet => Core.Cities;

        public ControllerCity(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }
    }
}