using System.Globalization;

namespace Deve.Clients.Wpf.Views;

internal interface ILoginView
{
    void ChangeCulture(CultureInfo value, string username);
}
