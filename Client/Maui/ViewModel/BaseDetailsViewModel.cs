using System.ComponentModel;

namespace Deve.ClientApp.Maui
{
    public abstract class BaseDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        #region Fields
        protected long _id = 0;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public BaseDetailsViewModel(BasePage page) 
            : base(page)
        {
        }
        #endregion

        #region Overrides
        public override void OnViewAppearing()
        {
            base.OnViewAppearing();
            _ = LoadData();
        }
        #endregion

        #region Abstract Methods
        protected abstract Task LoadData();
        #endregion

        #region INotifyPropertyChanged
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _id = (long)query["Id"];
        }
        #endregion
    }
}