using Microsoft.AspNetCore.Mvc;
using Deve.Core;

namespace Deve.Api
{
    public class ControllerBaseDeve : ControllerBase
    {
        #region Fields
        private readonly ICore _core;
        #endregion

        #region Properties
        protected ICore Core => _core;
        #endregion

        #region Constructor
        public ControllerBaseDeve(IHttpContextAccessor contextAccessor)
        {
            string? langCode = UtilsApi.GetLangCodeFromRequest(contextAccessor.HttpContext?.Request);   //IRequestCultureFeature seems to not take into account available languages to set the culture
            var options = new DataOptions()
            {
                OriginId = contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };
            if (!string.IsNullOrWhiteSpace(langCode))
                options.LangCode = langCode;

            _core = CoreFactory.Get(false, null, options);
        }
        #endregion
    }
}
