using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deve.ClientApp.Maui.Resources.Strings;

namespace Deve.ClientApp.Maui.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        #region Fields
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingUsername))]
        private string _username = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingPassword))]
        private string _password = string.Empty;
        #endregion

        #region Overrides
//-:cnd
#if DEBUG
        public override void OnViewAppearing()
        {
            base.OnViewAppearing();
            Username = "teracat";
            Password = "teracat";
        }
#endif
//+:cnd
        #endregion

        #region Methods
        [RelayCommand(CanExecute = nameof(IsIdle))]
        internal async Task Login()
        {
            if (!Validate())
                return;

            IsBusy = true;
            try
            {
                var resLogin = await Globals.Data.Authenticate.Login(new UserCredentials(Username, Password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                ((App?)Application.Current)?.GoToMain();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}