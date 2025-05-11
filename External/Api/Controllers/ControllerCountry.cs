using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseGet<Country, Country, CriteriaCountry>
    {
        private readonly IDataCountry _data;

        protected override IDataGet<Country, Country, CriteriaCountry> DataGet => _data;

        public ControllerCountry(IDataCountry data)
        {
            _data = data;
        }
    }
}
