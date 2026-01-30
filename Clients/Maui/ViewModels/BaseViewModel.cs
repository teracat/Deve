using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Data;

namespace Deve.Clients.Maui.ViewModels;

internal abstract class BaseViewModel : UIBase
{
    #region Properties
    protected INavigationService NavigationService { get; }

    protected IData Data { get; }

    public bool IsBusy
    {
        get; set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsIdle));
                OnIsBusyChanged();
            }
        }
    }

    public bool IsIdle
    {
        get => !IsBusy;
        set => IsBusy = !value;
    }

    public string ErrorText
    {
        get; set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(HasError));
            }
        }
    } = string.Empty;

    public bool HasError => !string.IsNullOrWhiteSpace(ErrorText);
    #endregion

    #region Constructor
    protected BaseViewModel(INavigationService navigationService, IData data)
    {
        NavigationService = navigationService;
        Data = data;
    }
    #endregion

    #region Virtual Methods
    protected virtual void OnIsBusyChanged() { }

    public virtual bool OnViewBackButtonPressed() => IsBusy;
    #endregion

    #region Helper Methods
    protected void GoBack() => _ = NavigationService.PopAsync();
    #endregion
}
