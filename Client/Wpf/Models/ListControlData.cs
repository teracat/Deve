using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Deve.ClientApp.Wpf.Models
{
    public partial class ListControlData : ObservableObject
    {
        #region Fields
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsIdle))]
        private bool _isBusy = false;

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _errorText = string.Empty;

        [ObservableProperty]
        private IEnumerable<ListData>? _items;

        private Func<Task>? _funcWhenSearch;
        #endregion

        #region Properties
        public bool IsIdle => !IsBusy;
        #endregion

        #region Methods
        [RelayCommand(CanExecute = nameof(IsIdle))]
        public void Search() => _funcWhenSearch?.Invoke();
        #endregion

        #region Constructors
        public ListControlData(Func<Task>? funcWhenSearch = null)
        {
            _funcWhenSearch = funcWhenSearch;
        }
        #endregion
    }
}