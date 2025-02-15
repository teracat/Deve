using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Auth.TokenManagers;
using Deve.Internal.Model;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Api.Controllers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathClient)]
    public class ControllerClient : ControllerBaseAll<Client, Client, CriteriaClient>
    {
        protected override IDataAll<Client, Client, CriteriaClient> DataAll => Core.Clients;

        public ControllerClient(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }

        [HttpPut, Route(ApiConstants.MethodUpdateStatus + "/{id}/{newStatus}")]
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return await Core.Clients.UpdateStatus(id, newStatus);
        }
    }
}