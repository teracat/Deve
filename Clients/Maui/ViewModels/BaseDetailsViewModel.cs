using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract class BaseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Properties
        public long Id { get; private set; }
        #endregion

        #region Constructor
        public BaseDetailsViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
        }
        #endregion

        #region Abstract Methods
        protected abstract Task GetData();
        #endregion

        #region Methods
        protected async Task LoadData()
        {
            ErrorText = string.Empty;
            IsBusy = true;
            try
            {
                await GetData();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = (long)query[nameof(Id)];
            _ = LoadData();
        }
        #endregion
    }
}