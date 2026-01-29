using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Deve.Clients.Wpf.Interfaces;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Wpf.ViewModels;

internal abstract partial class BaseEditViewModel : BaseViewModel, INavigationAware
{
    #region Fields
    [Reactive]
    private Guid _id = Guid.Empty;
    #endregion

    #region Properties
    public ReactiveCommand<Unit, IResult> LoadCommand { get; }

    public Action? LoadDataDoneAction { get; set; }
    #endregion

    #region Constructor
    protected BaseEditViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
        : base(navigationService, data, messageHandler, scheduler)
    {
        // Commands
        var canExecuteIsIdle = this.WhenAnyValue(vm => vm.IsIdle);
        LoadCommand = ReactiveCommand.CreateFromTask(GetData, canExecuteIsIdle, outputScheduler: scheduler.TaskPool);

        // Properties
        _ = this.WhenAnyObservable(vm => vm.LoadCommand.IsExecuting, vm => vm.SaveCommand.IsExecuting, vm => vm.CancelCommand.IsExecuting)
            .ToProperty(this, vm => vm.IsBusy, scheduler: scheduler.TaskPool);

        // Subscriptions
        _ = LoadCommand.CombineLatest(this.WhenAnyValue(vm => vm.IsIdle))
                    .Where(tuple => tuple.Second)   // Waits until IsIdle becomes true (it allows to set the focus to the initial control)
                    .SubscribeOn(scheduler.TaskPool)
                    .ObserveOn(scheduler.MainThread)
                    .DistinctUntilChanged()
                    .Subscribe(tuple =>
                    {
                        var res = tuple.First;
                        if (res is not null && !res.Success)
                        {
                            MessageHandler.ShowError(res.Errors);
                            Close();
                            return;
                        }

                        LoadDataDoneAction?.Invoke();
                    });

        _ = SaveCommand.CombineLatest(this.WhenAnyValue(vm => vm.IsIdle))
                    .Where(tuple => tuple.Second)   // Waits until IsIdle becomes true
                    .SubscribeOn(scheduler.TaskPool)
                    .ObserveOn(scheduler.MainThread)
                    .Subscribe(tuple =>
                    {
                        var res = tuple.First;
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
    protected abstract Task<IResult> Save();

    protected abstract Task<IResult> GetData();
    #endregion

    #region INavigationAware
    public void OnNavigatedTo(object? parameter)
    {
        if (parameter is Guid id)
        {
            Id = id;
        }
        else
        {
            Id = Guid.Empty;
        }

        _ = LoadCommand.Execute().Subscribe();
    }
    #endregion
}
