using System.Windows;
using Deve.Model;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;

namespace Deve.Clients.Wpf.Services
{
    internal class MessageHandlerMessageBox : IMessageHandler
    {
        public void ShowError(string message) => MessageBox.Show(message, AppResources.Error, MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowError(IList<ResultError> errors, char separator) => ShowError(Utils.ErrorsToString(errors, separator));

        public void ShowError(IList<ResultError> errors) => ShowError(errors, ',');

        public bool ShowQuestion(string message, string caption) => MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}