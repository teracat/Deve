using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Dto;
using Deve.External.Data;

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
