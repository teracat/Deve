using Microsoft.AspNetCore.Mvc;
using Deve.Model;
using Deve.Internal.Model;
using Deve.Api.Controllers;
using Deve.Internal.Data;

namespace Deve.Internal.Api.Controllers
{
    [ApiController]
    [Route(ApiConstants.PathStats)]
    public class ControllerStats : ControllerBaseDeve
    {
        private readonly IDataStats _data;

        public ControllerStats(IDataStats data)
        {
            _data = data;
        }

        [HttpGet(), Route(ApiConstants.MethodGetClientStats)]
        public async Task<ResultGet<ClientStats>> GetClientStats()
        {
            return await _data.GetClientStats();
        }
    }
}
