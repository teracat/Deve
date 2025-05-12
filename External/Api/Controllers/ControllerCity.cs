using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseGet<City, City, CriteriaCity>
    {
        private readonly IDataCity _data;

        protected override IDataGet<City, City, CriteriaCity> DataGet => _data;

        public ControllerCity(IDataCity data)
        {
            _data = data;
        }
    }
}
