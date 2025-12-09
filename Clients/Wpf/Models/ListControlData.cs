using System.Windows.Input;
using Deve.Clients.Wpf.Helpers;

namespace Deve.Clients.Wpf.Models
{
    public class ListControlData : UIBase
    {
        #region Fields
        private readonly Func<Task>? _funcWhenSearch;
        #endregion

        #region Properties
        public bool IsBusy
        {
            get;
            set => SetProperty(ref field, value);
        } = false;

        public string SearchText
        {
            get;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public string ErrorText
        {
            get;
            set => SetProperty(ref field, value);
        } = string.Empty;

        public IEnumerable<ListData>? Items
        {
            get;
            set => SetProperty(ref field, value);
        }

        public ICommand SearchCommand => field ??= new Command(() =>
        {
            _ = (_funcWhenSearch?.Invoke());
        }, () => !IsBusy);
        #endregion

        #region Constructors
        public ListControlData()
        {
        }

        public ListControlData(Func<Task>? funcWhenSearch)
        {
            _funcWhenSearch = funcWhenSearch;
        }
        #endregion
    }
}