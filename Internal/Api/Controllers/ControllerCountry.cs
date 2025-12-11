using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Dto;
using Deve.Internal.Data;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathCountry)]
    public class ControllerCountry : ControllerBaseAll<Country, Country, CriteriaCountry>
    {
        private readonly IDataCountry _data;

        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => _data;

        public ControllerCountry(IDataCountry data)
        {
            _data = data;
        }
    }
}
