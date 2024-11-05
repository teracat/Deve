using Deve.Auth;
using Deve.Internal;

namespace Deve.Core
{
    internal class CoreUser : CoreBaseAll<User, User, CriteriaUser>, IDataAll<User, User, CriteriaUser>
    {
        #region CoreBaseAll Abstract Properties
        protected override IDataAll<User, User, CriteriaUser> DataAll => Source.Users;
        protected override PermissionDataType DataType => PermissionDataType.User;
        #endregion

        #region Constructor
        public CoreUser(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region CoreBaseAll Implementation
        protected override Task<Result> CheckRequired(User data, CheckRequiredActionType action)
        {
            return Task.Run(() =>
            {
                var resultBuilder = ResultBuilder.Create(Core.Options.LangCode)
                                                 .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.Username), new Field(data.PasswordHash));

                if (action == CheckRequiredActionType.Update)
                    resultBuilder.CheckNotNullOrEmpty(new Field(data.Id));

                return resultBuilder.ToResult();
            });
        }

        protected override Task<Result> CheckAdd(User data, IList<User> list)
        {
            return Task.Run(() =>
            {
                if (data.Id > 0 && list.Any(x => x.Id == data.Id))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Id));

                if (!string.IsNullOrWhiteSpace(data.Email))
                {
                    if (list.Any(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.Equals(data.Email, StringComparison.InvariantCultureIgnoreCase)))
                        return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Email));
                }

                return Utils.ResultOk();
            });
        }

        protected override async Task<Result> CheckDelete(long id)
        {
            var result = await base.CheckDelete(id);
            if (!result.Success)
                return result;

            //A User can't delete its own user
            if (Core.UserIdentity is not null && Core.UserIdentity.Id == id)
                return Utils.ResultError(Core.Options.LangCode, ResultErrorType.NotAllowed, nameof(id));

            return Utils.ResultOk();
        }
        #endregion
    }
}
