using Microsoft.AspNetCore.Mvc;
using Deve.Api;
using Deve.DataSource;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.PathStats)]
    public class ControllerStats : ControllerBaseAuth
    {
        #region Constructor
        public ControllerStats(IHttpContextAccessor contextAccessor, IDataSourceFactory dataSourceFactory)
            : base( contextAccessor, dataSourceFactory)
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
