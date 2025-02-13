using Deve.Model;
using Deve.Criteria;
using Deve.Internal.Criteria;
using Deve.Internal.Model;
using System.Globalization;

namespace Deve.DataSource
{
    internal class DataSourceUser : DataSourceBaseAll<User, User, CriteriaUser>
    {
        #region Constructor
        public DataSourceUser(DataSourceMain dataSourceMain)
            : base(dataSourceMain)
        {
        }
        #endregion

        #region DataSourceBaseAll Implementation
        public override Task<ResultGetList<User>> Get(CriteriaUser? criteria = default)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                if (criteria is null)
                {
                    var list = Data.Users
                                   .OrderBy(x => x.Name)
                                   .Take(Constants.DefaultCriteriaLimit)
                                   .ToList();
                    return Utils.ResultGetListOk(list, null, Constants.DefaultCriteriaLimit, nameof(User.Name), Data.Users.Count);
                }

                var qry = Data.Users.AsQueryable();

                //Apply Filters
                if (criteria.Id.HasValue)
                {
                    qry = qry.Where(x => x.Id == criteria.Id.Value);
                }

                if (!string.IsNullOrWhiteSpace(criteria.Name))
                {
                    qry = qry.Where(x => x.Name.Contains(criteria.Name, StringComparison.InvariantCultureIgnoreCase));
                }

                switch (criteria.OnlyActive)
                {
                    case CriteriaActiveType.OnlyActive:
                        qry = qry.Where(x => x.IsActive);
                        break;
                    case CriteriaActiveType.OnlyInactive:
                        qry = qry.Where(x => !x.IsActive);
                        break;
                    case CriteriaActiveType.All:
                    default:
                        // Filter by IsActive is not needed
                        break;
                }   

                if (!string.IsNullOrWhiteSpace(criteria.Username))
                {
                    qry = qry.Where(x => x.Username.Equals(criteria.Username, StringComparison.InvariantCultureIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(criteria.PasswordHash))
                {
                    qry = qry.Where(x => x.PasswordHash.Equals(criteria.PasswordHash, StringComparison.InvariantCultureIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(criteria.Email))
                {
                    qry = qry.Where(x => !string.IsNullOrWhiteSpace(x.Email) && x.Email.Contains(criteria.Email, StringComparison.InvariantCultureIgnoreCase));
                }

                //OrderBy
                string orderBy = criteria.OrderBy ?? nameof(User.Name);
                switch (orderBy.ToLower(CultureInfo.InvariantCulture))
                {
                    case "id":
                        qry = qry.OrderBy(x => x.Id);
                        break;
                    case "email":
                        qry = qry.OrderBy(x => x.Email);
                        break;
                    case "username":
                        qry = qry.OrderBy(x => x.Username);
                        break;
                    case "birthday":
                        qry = qry.OrderBy(x => x.Birthday).ThenBy(x => x.Name);
                        break;
                    case "joined":
                        qry = qry.OrderBy(x => x.Joined).ThenBy(x => x.Name);
                        break;
                    case "isactive":
                        qry = qry.OrderBy(x => x.IsActive).ThenBy(x => x.Name);
                        break;
                    default:
                        qry = qry.OrderBy(x => x.Name);
                        break;
                }

                return ApplyOffsetAndLimit(qry, criteria, orderBy);
            });
        }

        public override Task<ResultGet<User>> Get(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                var city = Data.Users.FirstOrDefault(x => x.Id == id);
                if (city is null)
                {
                    return Utils.ResultGetError<User>(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);
                }

                return Utils.ResultGetOk(city);
            });
        }

        public override Task<ResultGet<ModelId>> Add(User user)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                Data.Users.Add(user);
                return Utils.ResultGetOk((ModelId)user);
            });
        }

        public override Task<Result> Update(User user)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(user.Id);
                if (found is null)
                {
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);
                }

                //Update
                found.Name = user.Name;
                found.Username = user.Username;
                found.Email = user.Email;
                found.IsActive = user.IsActive;
                found.Joined = user.Joined;
                found.Birthday = user.Birthday;
                if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                {
                    found.PasswordHash = user.PasswordHash;
                }

                return Utils.ResultOk();
            });
        }

        public override Task<Result> Delete(long id)
        {
            return Utils.RunProtectedAsync(Semaphore, () =>
            {
                //Search the object in memory
                var found = FindLocal(id);
                if (found is null)
                {
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);
                }

                //Remove
                if (!Data.Users.Remove(found))
                {
                    return Utils.ResultError(DataSourceMain.Options.LangCode, ResultErrorType.NotFound);
                }

                return Utils.ResultOk();
            });
        }
        #endregion

        #region Methods
        private User? FindLocal(long id) => Data.Users.FirstOrDefault(x => x.Id == id);
        #endregion
    }
}