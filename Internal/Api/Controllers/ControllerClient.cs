using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Dto;
using Deve.Internal.Dto;
using Deve.Internal.Data;

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
