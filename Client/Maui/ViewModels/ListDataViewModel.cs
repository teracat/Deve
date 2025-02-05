using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract partial class ListDataViewModel : BaseViewModel
    {
        #region Fields
        [ObservableProperty]
        IEnumerable<ListData>? _listData;

        [ObservableProperty]
        ListData? _selectedData;
        #endregion

        #region OnPropertyChanged
        partial void OnSelectedDataChanged(ListData? value)
        {
            if (value is not null)
                DoSelected(value);

            // Clear the selection to allow the same item to be selected again
            SelectedData = null;
        }
        #endregion

        #region Constructor
        public ListDataViewModel(IServiceProvider serviceProvider, IDataService dataService)
            : base(serviceProvider, dataService)
        {
            _ = LoadData();
        }
        #endregion

        #region Methods
        internal async Task LoadData()
        {
            ErrorText = string.Empty;
            IsBusy = true;
            try
            {
                await GetListData();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Abstract/Virtual Methods
        protected abstract Task GetListData();
        protected virtual void DoSelected(ListData data)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "Id", data.Id }
            };
            Shell.Current.GoToAsync("details", navigationParameter);
        }
        #endregion
    }
}