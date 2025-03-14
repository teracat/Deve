using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Helpers;
using Deve.Internal.Data;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract partial class BaseViewModel : ReactiveValidationObject
    {
        #region Fields
        private readonly INavigationService _navigationService;
        private readonly IData _data;
        [Reactive]
        private bool _isBusy = false;

        [Reactive]
        private bool _shouldValidate = false;

        [Reactive]
        private string _errorText = string.Empty;

        [ObservableAsProperty]
        private bool _isIdle;

        [ObservableAsProperty]
        private bool _hasError;
        #endregion

        #region Properties
        protected INavigationService NavigationService => _navigationService;

        protected IData Data => _data;
        #endregion

        #region Constructor
        protected BaseViewModel(INavigationService navigationService, IData data, ISchedulerProvider scheduler)
        {
            _navigationService = navigationService;
            _data = data;

            // Properties
            _isIdleHelper = this.WhenAnyValue(vm => vm.IsBusy)
                                .Select(isBusy => !isBusy)
                                .ToProperty(this, vm => vm.IsIdle, scheduler: scheduler.MainThread);

            _hasErrorHelper = this.WhenAnyValue(x => x.ErrorText)
                                  .Select((errorText) => !string.IsNullOrWhiteSpace(errorText))
                                  .ToProperty(this, vm => vm.HasError, scheduler: scheduler.MainThread);
        }
        #endregion

        #region Virtual Methods
        public virtual bool OnViewBackButtonPressed()
        {
            if (IsBusy)
            {
                return true;
            }

            return false;
        }

        protected virtual bool Validate()
        {
            ErrorText = string.Empty;
            ShouldValidate = true;
            if (HasErrors)
            {
                var errors = GetErrors(null);
                if (errors is string[] arrayErrors)
                {
                    ErrorText = string.Join("\n", arrayErrors);
                }
                return false;
            }
            return ValidationContext.IsValid;
        }
        #endregion

        #region Helper Methods
        protected void GoBack() => _ = NavigationService.PopAsync();
        #endregion
    }
}