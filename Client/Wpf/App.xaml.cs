using System.Globalization;
using System.Windows;
using Deve.ClientApp.Wpf.Views;

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

            if (Wpf.Properties.Settings.Default.LangCode > 0)
            {
                var culture = new CultureInfo(Wpf.Properties.Settings.Default.LangCode);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        public static void ChangeCulture(CultureInfo newCulture, string username, string password)
        {
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            var oldWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = new LoginView(username, password);
            Application.Current.MainWindow.Show();

            oldWindow.Close();
        }
    }
}
