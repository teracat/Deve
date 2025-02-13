using Microsoft.AspNetCore.Mvc;
using Deve.Core;
using Deve.Data;
using Deve.Api.DataSourceBuilder;
using Deve.DataSource;

namespace Deve.Api.Controllers
{
    public class ControllerBaseDeve : ControllerBase
    {
        #region Fields
        private readonly IDataSource _dataSource;
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
            {
                options.LangCode = langCode;
            }

            _dataSource = dataSourceBuilder.Create(options);
            _core = CoreFactory.Get(false, _dataSource, options);

            // Register for dispose when the request is finished
            contextAccessor.HttpContext?.Response.RegisterForDispose(_core);
            contextAccessor.HttpContext?.Response.RegisterForDispose(_dataSource);
        }
        #endregion
    }
}