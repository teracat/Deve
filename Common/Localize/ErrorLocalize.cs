using System.Globalization;
using Deve.Model;
using Deve.Resources;

namespace Deve.Localize
{
    internal class ErrorLocalize : IErrorLocalize
    {
        private readonly Dictionary<string, CultureInfo> _cultureInfos = [];

        public string Localize(ResultErrorType errorType, string langCode)
        {
            _cultureInfos.TryGetValue(langCode, out CultureInfo? cultureInfo);
            if (cultureInfo is null)
            {
                try
                {
                    cultureInfo = new CultureInfo(langCode);
                    _cultureInfos.Add(langCode, cultureInfo);
                }
                catch
                {
                    // If the langCode is invalid, use the default culture
                    cultureInfo = CultureInfo.InvariantCulture;
                }
            }
            return ErrorTypesResource.ResourceManager.GetString(errorType.ToString(), cultureInfo) ?? errorType.ToString();
        }
    }
}
