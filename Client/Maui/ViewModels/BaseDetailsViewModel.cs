using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract class BaseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        protected long _id = 0;
        #endregion

        #region Constructor
        public BaseDetailsViewModel(IServiceProvider serviceProvider, IDataService dataService)
            : base(serviceProvider, dataService)
        {
            _ = LoadData();
        }
        #endregion

        #region Abstract Methods
        protected abstract Task LoadData();
        #endregion

        #region IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _id = (long)query["Id"];
        }
        #endregion
    }
}