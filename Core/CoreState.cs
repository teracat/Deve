using Deve.Model;
using Deve.Criteria;
using Deve.Auth.Permissions;
using Deve.Internal.Data;

namespace Deve.Core
{
    internal class CoreState : CoreBaseAll<State, State, CriteriaState>
    {
        #region CoreBaseAll Abstract Properties
        protected override IDataAll<State, State, CriteriaState> DataAll => Source.States;
        protected override PermissionDataType DataType => PermissionDataType.State;
        #endregion

        #region Constructor
        public CoreState(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(State data, ChecksActionType action)
        {
            var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                                             .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.CountryId));

            if (action == ChecksActionType.Update)
                resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));

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

        protected override Task<Result> CheckDuplicated(State data, IList<State> list, ChecksActionType action)
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