using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.ViewModels
{
    public abstract class BaseEditViewModel : BaseViewModel, INavigationAware
    {
        #region Fields
        public Action? LoadDataDoneAction { get; set; }

        private AsyncCommand? _saveCommand;
        private Command? _cancelCommand;
        #endregion

        #region Properties
        public long Id { get; set; } = 0;
        #endregion

        #region Constructor
        protected BaseEditViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler)
            : base(navigationService, data, messageHandler)
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
        public Command Cancel => _cancelCommand ??= new Command(DoCancel, () => IsIdle);
        public AsyncCommand Save => _saveCommand ??= new AsyncCommand(DoSave, () => IsIdle);
        #endregion
    }
}