using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathCity)]
    public class ControllerCity : ControllerBaseAll<City, City, CriteriaCity>
    {
        private readonly IDataCity _data;

        protected override IDataAll<City, City, CriteriaCity> DataAll => _data;

        public ControllerCity(IDataCity data)
        {
            _data = data;
        }
    }
}
