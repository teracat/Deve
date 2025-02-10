using System.Globalization;

namespace Deve.Clients.Wpf.Interfaces
{
    public interface ILoginView
    {
        void ChangeCulture(CultureInfo value, string username);
    }
}