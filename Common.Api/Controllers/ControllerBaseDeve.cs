using Microsoft.AspNetCore.Mvc;
using Deve.Core;

namespace Deve.Api.Controllers
{
    public class ControllerBaseDeve : ControllerBase
    {
        #region Properties
        protected ICore Core { get; private set; }
        #endregion

        #region Constructor
        public ControllerBaseDeve(ICore core)
        {
            Core = core;
        }
        #endregion
    }
}