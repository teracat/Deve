using System.Windows.Input;
using Deve.Clients.Wpf.Helpers;

namespace Deve.Clients.Wpf.Models
{
    public class ListControlData : UIBase
    {
        #region Fields
        private bool _isBusy = false;
        private string _searchText = string.Empty;
        private string _errorText = string.Empty;
        private IEnumerable<ListData>? _items;
        private readonly Func<Task>? _funcWhenSearch;
        private ICommand? _searchCommand;
        #endregion

        #region Properties
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public string ErrorText
        {
            get => _errorText;
            set => SetProperty(ref _errorText, value);
        }

        public IEnumerable<ListData>? Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public ICommand SearchCommand => _searchCommand ??= new Command(() =>
        {
            _funcWhenSearch?.Invoke();
        }, () => !_isBusy);
        #endregion

        #region Constructors
        public ListControlData(Func<Task>? funcWhenSearch = null)
        {
            _funcWhenSearch = funcWhenSearch;
        }
        #endregion
    }
}