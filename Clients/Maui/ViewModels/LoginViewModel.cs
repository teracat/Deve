using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deve.Authenticate;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Resources.Strings;

namespace Deve.Clients.Maui.ViewModels
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

        #region Constructor
        public LoginViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
//-:cnd
#if DEBUG
            Username = "teracat";
            Password = "teracat";
#endif
//+:cnd
        }
        #endregion

        #region Methods
        [RelayCommand(CanExecute = nameof(IsIdle))]
        internal async Task Login()
        {
            if (!Validate())
            {
                return;
            }

            IsBusy = true;
            try
            {
                var resLogin = await DataService.Data.Authenticate.Login(new UserCredentials(Username, Password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                await NavigationService.NavigateToAsync("//clients");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}