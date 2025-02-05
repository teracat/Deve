using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Interfaces;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public abstract partial class BaseEditViewModel : BaseViewModel, INavigationAware
    {
        #region Fields
        public Action? LoadDataDoneAction { get; set; }
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
        [RelayCommand(CanExecute = nameof(IsIdle))]
        internal void Cancel()
        {
            SetResult(false);
            Close();
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        internal abstract Task Save();

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

        #region Overrides
        protected override void OnIsBusyChanged()
        {
            CancelCommand.NotifyCanExecuteChanged();
            SaveCommand.NotifyCanExecuteChanged();
        }
        #endregion
    }
}