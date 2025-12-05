using Microsoft.AspNetCore.Mvc;
using Deve.Api.Controllers;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

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
        public async Task<ResultGet<ClientStats>> GetClientStats() => await _data.GetClientStats();
    }
}
