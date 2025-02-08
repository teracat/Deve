using Deve.ClientApp.Maui.Helpers;
using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract class ListDataViewModel : BaseViewModel
    {
        #region Fields
        IEnumerable<ListData>? _listData;
        #endregion

        #region Properties
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
        public ListDataViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IDataService dataService)
            : base(navigationService, serviceProvider, dataService)
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
            var navigationParameter = new NavigationParameters
            {
                { "Id", data.Id }
            };
            NavigationService.NavigateToAsync("details", navigationParameter);
        }
        #endregion
    }
}