﻿using System.Globalization;
using System.Security;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Helpers;
using ReactiveUI.Validation.Extensions;
using Deve.Model;
using Deve.Authenticate;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Views;

namespace Deve.Clients.Wpf.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        #region Fields
        [Reactive]
        private List<CultureInfo> _languages = [new CultureInfo("en"), new CultureInfo("es-ES")];

        [Reactive]
        private CultureInfo? _selectedLanguage;

        [Reactive]
        private string _username = string.Empty;

        [Reactive]
        private bool _remember = false;

        [Reactive]
        private SecureString _password = new();

        private readonly ValidationHelper _passwordValidation;

        [ObservableAsProperty]
        private bool _hasErrorPassword;
        #endregion

        #region Properties
        public ILoginView? LoginView { get; set; }

        public ReactiveCommand<string, ResultGet<UserToken>?> LoginCommand { get; }
        #endregion

        #region Constructor
        public LoginViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
            : base(navigationService, data, messageHandler, scheduler)
        {
            _selectedLanguage = _languages.FirstOrDefault(x => x.LCID == Thread.CurrentThread.CurrentCulture.LCID) ?? _languages.FirstOrDefault();
            _username = Properties.Settings.Default.Username;
            _remember = !string.IsNullOrEmpty(Properties.Settings.Default.Username);

            // Commands
            var canExecuteLogin = this.WhenAnyValue(vm => vm.IsIdle);
            LoginCommand = ReactiveCommand.CreateFromTask<string, ResultGet<UserToken>?>(Login, canExecuteLogin, outputScheduler: scheduler.MainThread);

            // Validation Rules
            this.ValidationRule(vm => vm.Username,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Username,
                                                  (shouldValidate,username) => !shouldValidate || !string.IsNullOrWhiteSpace(username)),
                                AppResources.MissingUsername);

            _passwordValidation = this.ValidationRule(vm => vm.Password,
                                                      this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Password,
                                                                        (shouldValidate, password) => !shouldValidate || (password is not null && password.Length > 0)),
                                                      AppResources.MissingPassword);

            // Properties
            _hasErrorPasswordHelper = this.WhenAnyValue(vm => vm._passwordValidation.IsValid)
                                          .Select(isValid => !isValid)
                                          .ToProperty(this, vm => vm.HasErrorPassword, scheduler: scheduler.TaskPool, initialValue: false);

            // Subscriptions
            this.WhenAnyObservable(vm => vm.LoginCommand.IsExecuting)
                .SubscribeOn(scheduler.TaskPool)
                .ObserveOn(scheduler.MainThread)
                .DistinctUntilChanged()
                .Subscribe(isExecuting => IsBusy = isExecuting);

            _passwordValidation.ValidationChanged
                               .SubscribeOn(scheduler.TaskPool)
                               .ObserveOn(scheduler.MainThread)
                               .Subscribe(_ => this.RaisePropertyChanged(nameof(HasErrorPassword)));

            this.WhenAnyValue(vm => vm.SelectedLanguage)
                .SubscribeOn(scheduler.TaskPool)
                .ObserveOn(scheduler.MainThread)
                .DistinctUntilChanged()
                .Subscribe(newLang =>
                {
                    if (newLang is not null && newLang.LCID != Properties.Settings.Default.LangCode)
                    {
                        Properties.Settings.Default.LangCode = newLang.LCID;
                        Properties.Settings.Default.Save();

                        LoginView?.ChangeCulture(newLang, Username);
                    }
                });

            // When the login command completes, check the result and navigate to the main view if successful.
            // It waits for the command to complete and then checks if it was successful.
            // It's done this way to be able to close this window after the navigation (if IsBusy is true it won't be closed -check OnClosing in BaseView-).
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
                            Globals.UserToken = res.Data;

                            Properties.Settings.Default.Username = Remember ? Username : string.Empty;
                            Properties.Settings.Default.Save();

                            NavigationService.NavigateTo<MainView>();
                            Close();
                        }
                    }
                });
        }
        #endregion

        #region Methods
        private async Task<ResultGet<UserToken>?> Login(string password)
        {
            if (!Validate())
            {
                return null;
            }

            return await Data.Authenticate.Login(new UserCredentials(Username, password));
        }
        #endregion
    }
}