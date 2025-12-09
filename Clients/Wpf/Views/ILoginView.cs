using System.Globalization;

namespace Deve.Clients.Wpf.Views
{
    public interface ILoginView
    {
        void ChangeCulture(CultureInfo value, string username);
    }
}
