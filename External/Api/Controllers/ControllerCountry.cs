using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Core;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseGet<Country, Country, CriteriaCountry>
    {
        protected override IDataGet<Country, Country, CriteriaCountry> DataGet => Core.Countries;

        public ControllerCountry(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }
    }
}