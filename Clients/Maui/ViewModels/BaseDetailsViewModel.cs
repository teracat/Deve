using System.Reactive;
using ReactiveUI;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract partial class BaseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Properties
        public long Id { get; private set; }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; }
        #endregion

        #region Constructor
        protected BaseDetailsViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
            : base(navigationService, data, scheduler)
        {
            // Commands
            var canExecuteIsIdle = this.WhenAnyValue(vm => vm.IsIdle);
            LoadCommand = ReactiveCommand.CreateFromTask(LoadData, canExecuteIsIdle, outputScheduler: scheduler.TaskPool);

            // Properties
            _ = this.WhenAnyObservable(vm => vm.LoadCommand.IsExecuting)
                .ToProperty(this, vm => vm.IsBusy, scheduler: scheduler.TaskPool);
        }
        #endregion

        #region Abstract Methods
        protected abstract Task GetData();
        #endregion

        #region Methods
        private async Task LoadData()
        {
            ErrorText = string.Empty;
            await GetData();
        }
        #endregion

        #region IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = (long)query[nameof(Id)];
            _ = LoadData();
        }
        #endregion
    }
}