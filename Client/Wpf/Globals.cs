using System.Reflection;
using System.Windows;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf
{
    public static class Globals
    {
        #region Constants
        public const string AmountStringFormat = "{0:###,###,##0.##}€";
        #endregion

        #region UserToken
        public static UserToken? UserToken { get; set; }
        public static string LoggerUser => UserToken?.Subject.Name ?? string.Empty;
        #endregion

        #region Helpers
        public static string AppVersion => "v" + Assembly.GetExecutingAssembly().GetName().Version?.ToString(3) ?? string.Empty;

        public static void ShowError(string message) => MessageBox.Show(message, AppResources.Error, MessageBoxButton.OK, MessageBoxImage.Error);

        public static void ShowError(IList<ResultError> errors, char separator = ',') => ShowError(Utils.ErrorsToString(errors, separator));

        public static bool ShowQuestion(string message, string caption) => MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        #endregion
    }
}
