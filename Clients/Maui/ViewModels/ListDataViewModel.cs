using System.Reactive;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Deve.Model;
using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract partial class ListDataViewModel : BaseViewModel, IAsyncInitialization
    {
        #region Fields
        [Reactive]
        IEnumerable<ListData>? _listData;

        [Reactive]
        ListData? _selectedData;
        #endregion

        #region Properties
        public Task Initialization { get; private set; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        #endregion

        #region Constructor
        protected ListDataViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
            : base(navigationService, data, scheduler)
        {
            // Commands
            var canExecuteIsIdle = this.WhenAnyValue(vm => vm.IsIdle);
            LoadCommand = ReactiveCommand.CreateFromTask(LoadData, canExecuteIsIdle, outputScheduler: scheduler.TaskPool);

            // Properties
            this.WhenAnyObservable(vm => vm.LoadCommand.IsExecuting)
                .ToProperty(this, vm => vm.IsBusy, scheduler: scheduler.TaskPool);

            // Subscriptions
            this.WhenAnyValue(vm => vm.SelectedData)
                .Subscribe((value) =>
                {
                    if (value is not null)
                    {
                        DoSelected(value);

                        // Clear the selection to allow the same item to be selected again
                        SelectedData = null;
                    }
                });

            // Initialization
            Initialization = InitializeAsync();
        }
        #endregion

        #region Methods
        protected async Task InitializeAsync()
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            ErrorText = string.Empty;
            var res = await GetListData();
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
            }
        }
        #endregion

        #region Abstract/Virtual Methods
        protected abstract Task<Result> GetListData();

        protected virtual void DoSelected(ListData data)
        {
            var navigationParameter = new NavigationParameters
            {
                { nameof(BaseDetailsViewModel.Id), data.Id }
            };
            NavigationService.NavigateToAsync("details", navigationParameter);
        }
        #endregion
    }
}