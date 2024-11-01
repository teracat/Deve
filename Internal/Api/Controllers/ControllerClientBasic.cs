using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.External;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.ApiPathClientBasic)]
    public class ControllerClientBasic : ControllerBaseGet<ClientBasic, External.Client, CriteriaClientBasic>
    {
        protected override IDataGet<ClientBasic, External.Client, CriteriaClientBasic> DataGet => Core.ClientsBasic;

        public ControllerClientBasic(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }
    }
}
