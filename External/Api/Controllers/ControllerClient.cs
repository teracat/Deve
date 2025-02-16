using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Criteria;
using Deve.Auth.TokenManagers;
using Deve.External.Model;
using Deve.External.Data;
using Deve.Api.Controllers;
using Deve.Api.DataSourceBuilder;

namespace Deve.External.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathClient)]
    public class ControllerClient : ControllerBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => Core.ClientsBasic;

        public ControllerClient(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base(contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
    }
}