using Deve.Api.Controllers;
using Deve.Criteria;
using Deve.External.Data;
using Deve.External.Model;
using Deve.Model;
using Microsoft.AspNetCore.Mvc;

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
