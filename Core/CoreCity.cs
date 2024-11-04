using Deve.Auth;
using Deve.Internal;

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
        protected override async Task<Result> CheckRequired(City data)
        {
            var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                                .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.StateId), new Field(data.CountryId));
            if (resultBuilder.HasErrors)
                return resultBuilder.ToResult();

            //Check Valid StateId
            var stateRes = await Source.States.Get(data.StateId);
            if (!stateRes.Success)
                return stateRes;
            resultBuilder.CheckNotNull(stateRes.Data, nameof(data.StateId));

            //Check Valid CountryId
            var countryRes = await Source.Countries.Get(data.CountryId);
            if (!countryRes.Success)
                return countryRes;
            resultBuilder.CheckNotNull(countryRes.Data, nameof(data.CountryId));

            //Copy State & Country Name
            if (stateRes.Data is not null)
                data.State = stateRes.Data.Name;
            if (countryRes.Data is not null)
                data.Country = countryRes.Data.Name;

            return resultBuilder.ToResult();
        }

        protected override Task<Result> CheckAdd(City data, IList<City> list)
        {
            return Task.Run(() =>
            {
                if (list.Any(x => x.Id == data.Id))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Id));

                if (list.Any(x => x.Name.Equals(data.Name, StringComparison.InvariantCultureIgnoreCase)))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Name));

                return Utils.ResultOk();
            });
        }
        #endregion
    }
}
