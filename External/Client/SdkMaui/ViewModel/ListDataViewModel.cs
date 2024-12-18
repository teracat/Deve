namespace Deve.External.ClientApp.Maui
{
    public abstract class ListDataViewModel : BaseViewModel
    {
        #region Fields
        IList<ListData>? _listData;
        #endregion

        #region Properties
        public IList<ListData>? ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        #endregion

        #region Constructor
        public ListDataViewModel(ListDataPage page) 
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

        #region Methods
        internal async Task LoadData()
        {
            ErrorText = string.Empty;
            IsBusy = true;
            try
            {
                await LoadListData();
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected abstract Task LoadListData();
        #endregion
    }
}