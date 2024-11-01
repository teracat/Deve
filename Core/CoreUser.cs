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
        protected override Task<Result> CheckRequired(User data)
        {
            return Task.Run(() =>
            {
                return ResultBuilder.Create()
                       .CheckNotNullOrEmpty(new Field(data.Name), new Field(data.Username), new Field(data.PasswordHash))
                       .ToResult();
            });
        }

        protected override Task<Result> CheckAdd(User data, IList<User> list)
        {
            return Task.Run(() =>
            {
                if (list.Any(x => x.Id == data.Id))
                    return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Id));

                if (!string.IsNullOrWhiteSpace(data.Email))
                {
                    if (list.Any(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.Equals(data.Email, StringComparison.InvariantCultureIgnoreCase)))
                        return Utils.ResultError(Core.Options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Email));
                }

                return Utils.ResultOk();
            });
        }
        #endregion
    }
}
