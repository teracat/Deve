using Deve.Clients.Interfaces;
using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract partial class ListDataViewModel : BaseViewModel, IAsyncInitialization
    {
        #region Fields
        [ObservableProperty]
        IEnumerable<ListData>? _listData;

        [ObservableProperty]
        ListData? _selectedData;
        #endregion

        #region Properties
        public Task Initialization { get; private set; }
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
        public ListDataViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
            Initialization = InitializeAsync();
        }
        #endregion

        #region Methods
        protected virtual async Task InitializeAsync()
        {
            await LoadData();
        }

        public async Task LoadData()
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
            var navigationParameter = new NavigationParameters
            {
                { nameof(BaseDetailsViewModel.Id), data.Id }
            };
            NavigationService.NavigateToAsync("details", navigationParameter);
        }
        #endregion
    }
}