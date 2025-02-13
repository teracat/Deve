using Deve.Model;

namespace Deve.Clients.Wpf.Interfaces
{
    public interface IMessageHandler
    {
        void ShowError(string message);

        void ShowError(IList<ResultError> errors, char separator);

        void ShowError(IList<ResultError> errors);

        bool ShowQuestion(string message, string caption);
    }
}