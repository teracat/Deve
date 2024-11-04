using Deve.Auth;
using Deve.Internal;

namespace Deve.Core
{
    internal class CoreState : CoreBaseAll<State, State, CriteriaState>, IDataAll<State, State, CriteriaState>
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
        protected override async Task<Result> CheckRequired(State data)
        {
            var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                               .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.CountryId));
            if (resultBuilder.HasErrors)
                return resultBuilder.ToResult();

            //Check Valid CountryId
            var countryRes = await Source.Countries.Get(data.CountryId);
            if (!countryRes.Success)
                return countryRes;
            resultBuilder.CheckNotNull(countryRes.Data, nameof(data.CountryId));

            //Copy Country Name
            if (countryRes.Data is not null)
                data.Country = countryRes.Data.Name;

            return resultBuilder.ToResult();
        }

        protected override Task<Result> CheckAdd(State data, IList<State> list)
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
