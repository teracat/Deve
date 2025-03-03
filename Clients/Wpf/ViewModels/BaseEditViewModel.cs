using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Deve.Model;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.ViewModels
{
    public abstract partial class BaseEditViewModel : BaseViewModel, INavigationAware
    {
        #region Fields
        [Reactive]
        private long _id = 0;
        #endregion

        #region Properties
        public ReactiveCommand<Unit, Result> LoadCommand { get; }

        public Action? LoadDataDoneAction { get; set; }
        #endregion

        #region Constructor
        protected BaseEditViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
            : base(navigationService, data, messageHandler, scheduler)
        {
            // Commands
            var canExecuteIsIdle = this.WhenAnyValue(vm => vm.IsIdle);
            LoadCommand = ReactiveCommand.CreateFromTask(GetData, canExecuteIsIdle, outputScheduler: scheduler.TaskPool);

            // Properties
            this.WhenAnyObservable(vm => vm.LoadCommand.IsExecuting, vm => vm.SaveCommand.IsExecuting, vm => vm.CancelCommand.IsExecuting)
                .ToProperty(this, vm => vm.IsBusy, scheduler: scheduler.TaskPool);

            // Subscriptions
            LoadCommand.SubscribeOn(scheduler.TaskPool)
                       .ObserveOn(scheduler.MainThread)
                       .Subscribe(res =>
                        {
                            if (res is not null)
                            {
                                if (!res.Success)
                                {
                                    MessageHandler.ShowError(res.Errors);
                                    Close();
                                    return;
                                }
                            }

                            LoadDataDoneAction?.Invoke();
                        });

            SaveCommand.SubscribeOn(scheduler.TaskPool)
                       .ObserveOn(scheduler.MainThread)
                       .Subscribe(res =>
                        {
                            if (!res.Success)
                            {
                                if (res.Errors is not null && res.Errors.Count > 0)
                                {
                                    MessageHandler.ShowError(res.Errors);
                                }
                                return;
                            }

                            SetResult(true);
                            Close();
                        });
        }
        #endregion

        #region Methods
        [ReactiveCommand(CanExecute = nameof(IsIdle))]
        protected void Cancel()
        {
            SetResult(false);
            Close();
        }

        [ReactiveCommand(CanExecute = nameof(IsIdle))]
        protected abstract Task<Result> Save();

        protected abstract Task<Result> GetData();
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is not null && parameter is long id)
            {
                Id = id;
            }
            else
            {
                Id = 0;
            }

            LoadCommand.Execute().Subscribe();
        }
        #endregion
    }
}