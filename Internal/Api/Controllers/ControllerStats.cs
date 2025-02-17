using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Auth.TokenManagers;
using Deve.Internal.Model;
using Deve.Api.Controllers;
using Deve.Api.DataSourceBuilder;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathStats)]
    public class ControllerStats : ControllerBaseAuth
    {
        #region Constructor
        public ControllerStats(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder, ITokenManager tokenManager)
            : base( contextAccessor, dataSourceBuilder, tokenManager)
        {
        }
        #endregion

        #region Methods
        [HttpGet(), Route(ApiConstants.MethodGetClientStats)]
        public async Task<ResultGet<ClientStats>> GetClientStats()
        {
            return await Core.Stats.GetClientStats();
        }
        #endregion
    }
}