using System.Globalization;

namespace Deve.Clients.Wpf.Interfaces;

internal interface ILoginView
{
    void ChangeCulture(CultureInfo value, string username);
}
