using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Core;
using Deve.Internal.Model;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathClient)]
    public class ControllerClient : ControllerBaseAll<Client, Client, CriteriaClient>
    {
        protected override IDataAll<Client, Client, CriteriaClient> DataAll => Core.Clients;

        public ControllerClient(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
        {
        }

        [HttpPut, Route(ApiConstants.MethodUpdateStatus + "/{id}/{newStatus}")]
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return await Core.Clients.UpdateStatus(id, newStatus);
        }
    }
}