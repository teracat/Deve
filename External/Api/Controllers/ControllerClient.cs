using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.External.Model;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathClient)]
    public class ControllerClient : ControllerBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        private readonly IDataClientBasic _data;

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => _data;

        public ControllerClient(IDataClientBasic data)
        {
            _data = data;
        }
    }
}
