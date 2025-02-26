using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Core;
using Deve.Internal.Model;
using Deve.Api.Controllers;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathStats)]
    public class ControllerStats : ControllerBaseAuth
    {
        #region Constructor
        public ControllerStats(IHttpContextAccessor contextAccessor, ICore core)
            : base(contextAccessor, core)
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