using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.External.Api
{
    [ApiController]
    [Route(ApiConstants.PathClient)]
    public class ControllerClient : ControllerBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => Core.ClientsBasic;

        public ControllerClient(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
