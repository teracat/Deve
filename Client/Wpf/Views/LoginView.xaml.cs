﻿using System.Globalization;
using Deve.ClientApp.Wpf.ViewModels;

namespace Deve.ClientApp.Wpf.Views
{
    public partial class LoginView : BaseView, ILoginView
    {
        #region Fields
        private readonly LoginViewModel _viewModel;
        #endregion

        #region Constructors
        public LoginView()
        {
            InitializeComponent();
            string? username = null;
//-:cnd
#if DEBUG
            if (string.IsNullOrEmpty(Properties.Settings.Default.Username))
            {
                username = "teracat";
                uxPassword.Password = "teracat";
            }
#endif
//+:cnd
            ViewModel = _viewModel = new LoginViewModel(this, username);
        }

        public LoginView(string? username, string? password)
        {
            InitializeComponent();

            uxPassword.Password = password;

            ViewModel = _viewModel = new LoginViewModel(this, username);

            if (string.IsNullOrEmpty(uxUsername.Text))
                uxUsername.Focus();
            else
                uxPassword.Focus();
        }
        #endregion

        #region IViewLogin
        public void ChangeCulture(CultureInfo value, string username)
        {
            App.ChangeCulture(value, username, uxPassword.Password);
        }
        #endregion

        #region Events
        private void OnUsernameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                uxPassword.Focus();
        }

        private void OnPasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // The Password property is not a dependency property for security reasons.
            if (e.Key == System.Windows.Input.Key.Return)
                _ = _viewModel.DoLogin(uxPassword.Password);
        }

        private void OnLoginClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _ = _viewModel.DoLogin(uxPassword.Password);
        }
        #endregion
    }
}