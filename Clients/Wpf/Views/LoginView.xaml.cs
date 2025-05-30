﻿using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Reactive.Threading.Tasks;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public partial class LoginView : BaseView, ILoginView
    {
        #region Fields
        private readonly LoginViewModel _viewModel;
        #endregion

        #region Constructors
        public LoginView(LoginViewModel viewModel)
        {
            InitializeComponent();
            
            viewModel.LoginView = this;
            ViewModel = _viewModel = viewModel;

//-:cnd
#if DEBUG
            if (string.IsNullOrEmpty(Properties.Settings.Default.Username))
            {
                SetUsernamePassword("teracat", "teracat");
            }
#endif
//+:cnd
            
            if (string.IsNullOrEmpty(_viewModel.Username))
            {
                uxUsername.Focus();
            }
            else
            {
                uxPassword.Focus();
            }
        }
        #endregion

        #region Methods
        public void SetUsernamePassword(string username, string password)
        {
            _viewModel.Username = username;
            uxPassword.Password = password;
        }
        #endregion

        #region IViewLogin
        public void ChangeCulture(CultureInfo value, string username)
        {
            ((App)Application.Current).ChangeCulture(value, username, uxPassword.Password);
        }
        #endregion

        #region Events
        private void OnUsernameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
            {
                uxPassword.Focus();
            }
        }

        private void OnPasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // The Password property is not a dependency property for security reasons.
            if (e.Key == System.Windows.Input.Key.Return)
            {
                _ = _viewModel.LoginCommand.Execute(uxPassword.Password).ToTask();
            }
        }

        private async void OnLoginClick(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoginCommand.Execute(uxPassword.Password).ToTask();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            _viewModel.Password = passwordBox.SecurePassword;
        }
        #endregion
    }
}