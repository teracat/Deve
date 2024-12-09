namespace Deve.External.ClientApp.Maui
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();

            ViewModel = new LoginViewModel(this);
        }
    }
}
