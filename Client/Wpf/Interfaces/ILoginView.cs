using System.Globalization;

namespace Deve.ClientApp.Wpf.Interfaces
{
    public interface ILoginView
    {
        void ChangeCulture(CultureInfo value, string username);
    }
}
