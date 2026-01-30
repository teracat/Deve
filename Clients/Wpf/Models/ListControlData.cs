using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Deve.Clients.Wpf.Models;

internal sealed partial class ListControlData : ReactiveObject
{
    #region Fields
    [Reactive]
    private bool _isBusy = false;

    [Reactive]
    private string _searchText = string.Empty;

    [Reactive]
    private string _errorText = string.Empty;

    [Reactive]
    private IEnumerable<ListData>? _items;

    private readonly Func<Task>? _funcWhenSearch;

    [ObservableAsProperty]
    private bool _isIdle;
    #endregion

    #region Methods
    [ReactiveCommand(CanExecute = nameof(IsIdle))]
    public void Search() => _funcWhenSearch?.Invoke();
    #endregion

    #region Constructors
    public ListControlData(Func<Task>? funcWhenSearch)
    {
        _funcWhenSearch = funcWhenSearch;

        _isIdleHelper = this.WhenAnyValue(vm => vm.IsBusy)
                            .Select(isBusy => !isBusy)
                            .ToProperty(this, x => x.IsIdle, initialValue: true);
    }
    #endregion
}