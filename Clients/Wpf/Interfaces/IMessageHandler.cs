using Deve.Dto.Responses.Results;

namespace Deve.Clients.Wpf.Interfaces;

internal interface IMessageHandler
{
    void ShowError(string message);

    void ShowError(IReadOnlyList<ResultError>? errors, char separator);

    void ShowError(IReadOnlyList<ResultError>? errors);

    bool ShowQuestion(string message, string caption);
}
