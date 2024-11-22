using Microsoft.AspNetCore.Mvc;
using Deve.Core;
using Deve.DataSource;

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
        public ControllerBaseDeve(IHttpContextAccessor contextAccessor, IDataSourceBuilder dataSourceBuilder)
        {
            string? langCode = UtilsApi.GetLangCodeFromRequest(contextAccessor.HttpContext?.Request);   //IRequestCultureFeature seems to not take into account available languages to set the culture
            var options = new DataOptions()
            {
                OriginId = contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty
            };
            if (!string.IsNullOrWhiteSpace(langCode))
                options.LangCode = langCode;

            var dataSource = dataSourceBuilder.Create(options);

            _core = CoreFactory.Get(false, dataSource, options);
        }
        #endregion
    }
}
