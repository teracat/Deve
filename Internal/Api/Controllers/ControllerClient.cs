using Microsoft.AspNetCore.Mvc;
using Deve.Model;
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
        private readonly IDataClient _data;

        protected override IDataAll<Client, Client, CriteriaClient> DataAll => _data;

        public ControllerClient(IDataClient data)
        {
            _data = data;
        }

        [HttpPut, Route(ApiConstants.MethodUpdateStatus + "/{id}/{newStatus}")]
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return await _data.UpdateStatus(id, newStatus);
        }
    }
}
