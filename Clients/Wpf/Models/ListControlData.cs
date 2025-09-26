using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deve.Clients.Wpf.Helpers;

namespace Deve.Clients.Wpf.Models
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
        public ListControlData()
        {
        }

        public ListControlData(Func<Task>? funcWhenSearch)
        {
            _funcWhenSearch = funcWhenSearch;
        }
        #endregion
    }
}