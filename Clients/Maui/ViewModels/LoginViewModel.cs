using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using Deve.Auth;
using Deve.Auth.Login;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Resources.Strings;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Maui.ViewModels;

internal partial class LoginViewModel : BaseViewModel
{
    #region Fields
    [Reactive]
    private string _username = string.Empty;

    [Reactive]
    private string _password = string.Empty;
    #endregion

    #region Properties
    public ReactiveCommand<Unit, ResultGet<LoginResponse>?> LoginCommand { get; }
    #endregion

    #region Constructor
    public LoginViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler)
        : base(navigationService, data, scheduler)
    {
        // Commands
        var canExecuteLogin = this.WhenAnyValue(vm => vm.IsIdle);
        LoginCommand = ReactiveCommand.CreateFromTask(Login, canExecuteLogin, outputScheduler: scheduler.MainThread);

        // Validation Rules
        this.ValidationRule(vm => vm.Username,
                            this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Username,
                                              (shouldValidate, username) => !shouldValidate || !string.IsNullOrWhiteSpace(username)),
                            AppResources.MissingUsername);
        this.ValidationRule(vm => vm.Password,
                            this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Password,
                                              (shouldValidate, password) => !shouldValidate || !string.IsNullOrWhiteSpace(password)),
                            AppResources.MissingPassword);

        // Subscriptions
        this.WhenAnyObservable(vm => vm.LoginCommand.IsExecuting)
            .SubscribeOn(scheduler.TaskPool)
            .ObserveOn(scheduler.MainThread)
            .DistinctUntilChanged()
            .Subscribe(isExecuting => IsBusy = isExecuting);

        // When the login command completes, check the result and navigate to the main view if successful.
        // It waits for the command to complete and then checks if it was successful.
        LoginCommand
            .CombineLatest(this.WhenAnyObservable(vm => vm.LoginCommand.IsExecuting))
            .Where(tuple => !tuple.Second && tuple.First is not null)  // Waits until IsExecuting becomes false
            .SubscribeOn(scheduler.TaskPool)
            .ObserveOn(scheduler.MainThread)   // Ensures execution on the UI thread
            .DistinctUntilChanged()
            .Subscribe(tuple =>
            {
                var res = tuple.First;
                if (res is not null)
                {
                    if (!res.Success)
                    {
                        ErrorText = Utils.ErrorsToString(res.Errors);
                    }
                    else
                    {
                        Globals.LoginResponseData = res.Data;

                        _ = NavigationService.NavigateToAsync("//clients");
                    }
                }
            });

        //-:cnd
#if DEBUG
        Username = "teracat";
        Password = "teracat";
#endif
        //+:cnd
    }
    #endregion

    #region Methods
    private async Task<ResultGet<LoginResponse>?> Login()
    {
        if (!Validate())
        {
            return null;
        }

        return await Data.Auth.Login(new LoginRequest(Username, Password));
    }
    #endregion
}
