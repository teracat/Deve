using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Model;

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
