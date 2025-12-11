using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Dto;
using Deve.External.Data;
using Deve.External.Dto;

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
