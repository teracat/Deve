using Deve.Auth;
using Deve.Internal;

namespace Deve.Core
{
    internal class CoreCountry : CoreBaseAll<Country, Country, CriteriaCountry>, IDataAll<Country, Country, CriteriaCountry>
    {
        #region CoreBaseAll Abstract Properties
        protected override IDataAll<Country, Country, CriteriaCountry> DataAll => Source.Countries;
        protected override PermissionDataType DataType => PermissionDataType.Country;
        #endregion

        #region Constructor
        public CoreCountry(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override async Task<Result> CheckRequired(Country data)
        {
            return await Task.Run(() =>
            {
                return ResultBuilder.Create(Core.Options.LangCode)
                       .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.IsoCode))
                       .ToResult();
            });
        }

        protected override Task<Result> CheckAdd(Country data, IList<Country> list)
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
