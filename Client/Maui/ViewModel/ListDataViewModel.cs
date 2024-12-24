namespace Deve.ClientApp.Maui
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
        #endregion

        #region Abstract/Virtual Methods
        protected abstract Task LoadListData();
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