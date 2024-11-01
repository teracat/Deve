using Microsoft.AspNetCore.Mvc;
using Deve.Api;

namespace Deve.Internal.Api
{
    [ApiController]
    [Route(ApiConstants.ApiPathStats)]
    public class ControllerStats : ControllerBaseAuth
    {
        #region Constructor
        public ControllerStats(IHttpContextAccessor contextAccessor)
            : base( contextAccessor)
        {
        }
        #endregion

        #region Methods
        [HttpGet(Name = ApiConstants.ApiMethodGetClientStats)]
        public async Task<ResultGet<ClientStats>> GetClientStats()
        {
            return await Core.Stats.GetClientStats();
        }
        #endregion
    }
}
