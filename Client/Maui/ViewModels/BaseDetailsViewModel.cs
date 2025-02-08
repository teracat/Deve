using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract class BaseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Properties
        public long Id { get; private set; }
        #endregion

        #region Constructor
        public BaseDetailsViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IDataService dataService)
            : base(navigationService, serviceProvider, dataService)
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
            Id = (long)query["Id"];
            _ = LoadData();
        }
        #endregion
    }
}