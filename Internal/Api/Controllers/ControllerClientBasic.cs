using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Dto;
using Deve.External.Data;
using Deve.External.Dto;

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
