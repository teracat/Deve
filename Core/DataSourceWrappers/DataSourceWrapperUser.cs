using Deve.Model;
using Deve.DataSource;
using Deve.Auth;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;

namespace Deve.Core.DataSourceWrappers
{
    /// <summary>
    /// Is the converter from the class used in the Api & Core to the class used in the DataSource.
    /// UserPlainPassword <-> User
    /// To add or update a user, we use the plain password, but we store the hash.
    /// </summary>
    internal class DataSourceWrapperUser : DataSourceWrapperBase, IDataAll<UserBase, UserPlainPassword, CriteriaUser>
    {
        #region Properties
        public IAuth Auth { get; }
        #endregion

        #region Constructor
        public DataSourceWrapperUser(IDataSource dataSource, IAuth auth)
            : base(dataSource)
        {
            Auth = auth;
        }
        #endregion

        #region IDataAll
        public async Task<ResultGetList<UserBase>> Get(CriteriaUser? criteria)
        {
            var resUsers = await Source.Users.Get(criteria);
            if (!resUsers.Success)
            {
                return Utils.ResultGetListError<UserBase>(resUsers);
            }

            var usersBase = resUsers.Data
                                    .Select(x => (UserBase)x)
                                    .ToList();

            return Utils.ResultGetListOk(usersBase, resUsers.Offset, resUsers.Limit, resUsers.OrderBy, resUsers.TotalCount);
        }

        public async Task<ResultGetList<UserBase>> Get() => await Get(null);

        public async Task<ResultGet<UserPlainPassword>> Get(long id)
        {
            var resUser = await Source.Users.Get(id);
            if (!resUser.Success || resUser.Data is null)
            {
                return Utils.ResultGetError<UserPlainPassword>(resUser);
            }

            return Utils.ResultGetOk(new UserPlainPassword(resUser.Data));
        }

        public async Task<ResultGet<ModelId>> Add(UserPlainPassword data)
        {
            //The password will not be null because is already checked in the CoreUser.CheckRequired
            var user = new User(data)
            {
                PasswordHash = Auth.Hash.Calc(data.Password ?? data.Username)
            };

            return await Source.Users.Add(user);
        }

        public async Task<Result> Update(UserPlainPassword data)
        {
            //The DataSource will only update the Password if is not Empty.
            string newPasswordHash = string.Empty;
            if (!string.IsNullOrWhiteSpace(data.Password))
            {
                newPasswordHash = Auth.Hash.Calc(data.Password);
            }

            var user = new User(data)
            {
                PasswordHash = newPasswordHash
            };

            return await Source.Users.Update(user);
        }

        public async Task<Result> Delete(long id)
        {
            return await Source.Users.Delete(id);
        }
        #endregion
    }
}