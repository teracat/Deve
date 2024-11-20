using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreUser : TestCoreBaseDataAll<UserBase, UserPlainPassword, CriteriaUser>
    {
        #region Overrides
        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> GetDataAll(ICore core) => core.Users;

        protected override UserPlainPassword CreateInvalidDataToAdd() => new();

        protected override UserPlainPassword CreateInvalidDataToUpdate() => new();

        protected override UserPlainPassword CreateValidDataToAdd() => new()
        {
            Name = "Tests User",
            Role = Role.User,
            Username = "tests",
            IsActive = false,
            Joined = new DateTime(2024, 11, 19),
            Password = "Tests",
        };

        // We update the user "teracat" which is used to authenticate tests, so we must keep it Active and with the Role Admin so the others tests don't fail
        protected override UserPlainPassword CreateValidDataToUpdate() => new()
        {
            Id = 1,
            Role = Role.Admin,
            Name = "Jordi Badia",
            Username = "teracat",
            Email = "jordib@teracat.com",
            IsActive = true,
            Joined = new DateTime(2024, 9, 19),
        };
        #endregion
    }
}