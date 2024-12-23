﻿using System.Globalization;
using Deve.Common.Resources;

namespace Deve.Localize
{
    internal class ErrorLocalize : IErrorLocalize
    {
        private Dictionary<string, CultureInfo> _cultureInfos = [];

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
                }
            }
            return ErrorTypesResource.ResourceManager.GetString(errorType.ToString(), cultureInfo) ?? errorType.ToString();
        }
    }
}
