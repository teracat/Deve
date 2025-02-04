using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract partial class BaseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        protected long _id = 0;
        #endregion

        #region Constructor
        public BaseDetailsViewModel(IServiceProvider serviceProvider, IDataService dataService)
            : base(serviceProvider, dataService)
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
            _id = (long)query["Id"];
            _ = LoadData();
        }
        #endregion
    }
}