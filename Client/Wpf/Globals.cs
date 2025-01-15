﻿using System.Reflection;
using Deve.Core;
using Deve.Internal;

namespace Deve.ClientApp.Wpf
{
    public static class Globals
    {
        #region Constants
        public const string AmountStringFormat = "{0:###,###,##0.##}€";
        #endregion

        #region IData
        private static IData? _data;
        public static IData Data => _data ??= CoreFactory.Get();
        #endregion

        #region UserToken
        public static UserToken? UserToken { get; set; }
        public static string LoggerUser => UserToken?.Subject.Name ?? string.Empty;
        #endregion

        #region Helpers
        public static string AppVersion => "v" + Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? string.Empty;
        #endregion
    }
}
