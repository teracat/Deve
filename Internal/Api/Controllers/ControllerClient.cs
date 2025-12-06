using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

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
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus) => await _data.UpdateStatus(id, newStatus);
    }
}
