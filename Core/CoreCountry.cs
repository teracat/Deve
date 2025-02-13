using Deve.Model;
using Deve.Criteria;
using Deve.Auth.Permissions;
using Deve.Internal.Data;

namespace Deve.Core
{
    internal class CoreCountry : CoreBaseAll<Country, Country, CriteriaCountry>
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
        protected override async Task<Result> CheckRequired(Country data, ChecksActionType action)
        {
            return await Task.Run(() =>
            {
                var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                                                 .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.IsoCode));

                if (action == ChecksActionType.Update)
                {
                    resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));
                }

                return resultBuilder.ToResult();
            });
        }

        protected override Task<Result> CheckDuplicated(Country data, IList<Country> list, ChecksActionType action)
        {
            return Task.Run(() =>
            {
                if (action == ChecksActionType.Add)
                {
                    var resCheckId = UtilsCore.CheckIdWhenAdding(Core, data, list);
                    if (resCheckId is not null)
                    {
                        return resCheckId;
                    }
                }

                if (list.Any(x => x.Id != data.Id && x.Name.Equals(data.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Name));
                }

                if (list.Any(x => x.Id != data.Id && x.IsoCode.Equals(data.IsoCode, StringComparison.InvariantCultureIgnoreCase)))
                {
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.IsoCode));
                }

                return Utils.ResultOk();
            });
        }
        #endregion
    }
}