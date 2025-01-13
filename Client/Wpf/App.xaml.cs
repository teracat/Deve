using System.Globalization;
using System.Windows;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
        }

        public static void ChangeCulture(CultureInfo newCulture)
        {
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            var oldWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = new LoginWindow();
            Application.Current.MainWindow.Show();

            oldWindow.Close();
        }
    }
}
