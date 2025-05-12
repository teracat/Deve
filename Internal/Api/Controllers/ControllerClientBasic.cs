using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.External.Data;
using Deve.External.Model;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathClientBasic)]
    public class ControllerClientBasic : ControllerBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        private readonly IDataClientBasic _data;

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => _data;

        public ControllerClientBasic(IDataClientBasic data)
        {
            _data = data;
        }
    }
}
