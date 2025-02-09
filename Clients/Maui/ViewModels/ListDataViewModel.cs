using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract class ListDataViewModel : BaseViewModel, IAsyncInitialization
    {
        #region Fields
        IEnumerable<ListData>? _listData;
        #endregion

        #region Properties
        public Task Initialization { get; private set; }

        public IEnumerable<ListData>? ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }

        public ListData? SelectedData
        {
            get => null;
            set
            {
                if (value is not null)
                    DoSelected(value);
                OnPropertyChanged();
            }
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