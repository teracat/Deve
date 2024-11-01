using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.ApiPathClient)]
    public class ControllerClient : ControllerBaseAll<Client, Client, CriteriaClient>
    {
        protected override IDataAll<Client, Client, CriteriaClient> DataAll => Core.Clients;

        public ControllerClient(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }

        [HttpPut, Route(ApiConstants.ApiMethodUpdateStatus + "/{id}/{newStatus}")]
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return await Core.Clients.UpdateStatus(id, newStatus);
        }
    }
}
