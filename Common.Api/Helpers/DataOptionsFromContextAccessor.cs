using Microsoft.AspNetCore.Http;
using Deve.Data;

namespace Deve.Api.Helpers
{
    /// <summary>
    /// Options related to the user requesting the data, retrieved using the IHttpContextAccessor.
    /// </summary>
    public class DataOptionsFromContextAccessor : DataOptions
    {
        public DataOptionsFromContextAccessor(IHttpContextAccessor contextAccessor)
        {
            OriginId = contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            string? langCode = UtilsApi.GetLangCodeFromRequest(contextAccessor.HttpContext?.Request);   //IRequestCultureFeature seems to not take into account available languages to set the culture
            if (!string.IsNullOrWhiteSpace(langCode))
            {
                LangCode = langCode;
            }
        }
    }
}
