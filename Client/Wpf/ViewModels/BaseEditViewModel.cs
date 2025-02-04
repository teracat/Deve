using System.Windows.Input;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Interfaces;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public abstract class BaseEditViewModel : BaseViewModel, INavigationAware
    {
        #region Fields
        public Action? LoadDataDoneAction { get; set; }

        private ICommand? _saveCommand;
        private ICommand? _cancelCommand;
        #endregion

        #region Properties
        public long Id { get; set; } = 0;
        #endregion

        #region Constructor
        public BaseEditViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
        }
        #endregion

        #region Methods
        internal virtual void DoCancel()
        {
            SetResult(false);
            Close();
        }

        internal abstract Task DoSave();

        protected async Task LoadData()
        {
            IsBusy = true;
            try
            {
                await GetData();
            }
            finally
            {
                IsBusy = false;
            }

            LoadDataDoneAction?.Invoke();
        }

        protected abstract Task GetData();
        #endregion

        #region INavigationAware
        public void OnNavigatedTo(object? parameter)
        {
            if (parameter is not null && parameter is long id)
            {
                Id = id;
            }
            else
            {
                Id = 0;
            }

            _ = LoadData();
        }
        #endregion

        #region Commands
        public ICommand Cancel => _cancelCommand ??= new Command(() => DoCancel(), () => IsIdle);
        public ICommand Save => _saveCommand ??= new Command(() => _ = DoSave(), () => IsIdle);
        #endregion
    }
}