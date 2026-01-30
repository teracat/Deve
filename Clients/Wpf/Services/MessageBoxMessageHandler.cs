using System.Windows;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Wpf.Services;

internal sealed class MessageBoxMessageHandler : IMessageHandler
{
    public void ShowError(string message) => MessageBox.Show(message, AppResources.Error, MessageBoxButton.OK, MessageBoxImage.Error);

    public void ShowError(IReadOnlyList<ResultError>? errors, char separator) => ShowError(Utils.ErrorsToString(errors, separator));

    public void ShowError(IReadOnlyList<ResultError>? errors) => ShowError(errors, ',');

    public bool ShowQuestion(string message, string caption) => MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
}
