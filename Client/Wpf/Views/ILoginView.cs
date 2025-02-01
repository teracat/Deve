using System.Globalization;

namespace Deve.ClientApp.Wpf.Views
{
    public interface ILoginView
    {
        void ChangeCulture(CultureInfo value, string username);
    }
}
