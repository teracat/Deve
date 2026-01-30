using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Clients.Maui.Interfaces;
using Deve.Data;

namespace Deve.Clients.Maui.ViewModels;

internal abstract partial class BaseViewModel : ObservableValidator
{
    #region Fields
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsIdle))]
    private bool _isBusy = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasError))]
    private string _errorText = string.Empty;
    #endregion

    #region Properties
    protected INavigationService NavigationService { get; }

    protected IData Data { get; }

    public bool IsIdle => !IsBusy;

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

    protected virtual bool Validate()
    {
        ErrorText = string.Empty;
        ClearErrors();
        ValidateAllProperties();
        if (HasErrors)
        {
            ErrorText = string.Join("\n", GetErrors());
            return false;
        }
        return true;
    }
    #endregion

    #region Helper Methods
    protected void GoBack() => _ = NavigationService.PopAsync();
    #endregion
}
