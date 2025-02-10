using Deve.Model;
using Deve.Criteria;
using Deve.Auth.Permissions;
using Deve.Internal.Data;

namespace Deve.Core
{
    internal class CoreCity : CoreBaseAll<City, City, CriteriaCity>
    {
        #region CoreBaseAll Abstract Properties
        protected override IDataAll<City, City, CriteriaCity> DataAll => Source.Cities;
        protected override PermissionDataType DataType => PermissionDataType.City;
        #endregion

        #region Constructor
        public CoreCity(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(City data, ChecksActionType action)
        {
            var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                                .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.StateId), new Field(data.CountryId));

            if (action == ChecksActionType.Update)
                resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));

            //Check Valid StateId
            if (!Utils.IsEmptyValue(data.StateId))
            {
                var stateRes = await Source.States.Get(data.StateId);
                if (!stateRes.Success)
                    return stateRes;
                resultBuilder.CheckNotNull(stateRes.Data, nameof(data.StateId));

                //Copy State Name
                if (stateRes.Data is not null)
                    data.State = stateRes.Data.Name;
            }

            //Check Valid CountryId
            if (!Utils.IsEmptyValue(data.CountryId))
            {
                var countryRes = await Source.Countries.Get(data.CountryId);
                if (!countryRes.Success)
                    return countryRes;
                resultBuilder.CheckNotNull(countryRes.Data, nameof(data.CountryId));

                //Copy Country Name
                if (countryRes.Data is not null)
                    data.Country = countryRes.Data.Name;
            }

            return resultBuilder.ToResult();
        }

        protected override Task<Result> CheckDuplicated(City data, IList<City> list, ChecksActionType action)
        {
            return Task.Run(() =>
            {
                if (action == ChecksActionType.Add)
                {
                    var resCheckId = UtilsCore.CheckIdWhenAdding(Core, data, list);
                    if (resCheckId is not null)
                        return resCheckId;
                }

                if (list.Any(x => x.Id != data.Id && x.Name.Equals(data.Name, StringComparison.InvariantCultureIgnoreCase)))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Name));

                return Utils.ResultOk();
            });
        }
        #endregion
    }
}