using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Core;
using Deve.External.Model;
using Deve.External.Data;
using Deve.Api.Controllers;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathClient)]
    public class ControllerClient : ControllerBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => Core.ClientsBasic;

        public ControllerClient(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }
    }
}