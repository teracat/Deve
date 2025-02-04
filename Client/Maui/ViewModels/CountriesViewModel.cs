﻿using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class CountriesViewModel : ListDataViewModel
    {
        #region Constructor
        public CountriesViewModel(IServiceProvider serviceProvider, IDataService dataService)
            : base(serviceProvider, dataService)
        {
        }
        #endregion

        #region Overrides
        protected override async Task LoadListData()
        {
            CriteriaCountry? criteria = null;
            var res = await DataService.Data.Countries.Get(criteria);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.IsoCode,
            }).ToArray();
        }
        #endregion
    }
}