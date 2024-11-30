using Deve.Internal;

namespace Deve.Tests
{
    public abstract class TestUser<TDataType> : TestBaseDataAll<TDataType, UserBase, UserPlainPassword, CriteriaUser> where TDataType : IData
    {
        #region Constructor
        public TestUser(IFixtureData<TDataType> fixture)
            : base(fixture)
        {
        }
        #endregion

        #region Overrides
        protected override IDataAll<UserBase, UserPlainPassword, CriteriaUser> GetDataAll(TDataType data) => data.Users;

        protected override UserPlainPassword CreateInvalidDataToAdd() => new();

        protected override UserPlainPassword CreateInvalidDataToUpdate() => new();

        protected override UserPlainPassword CreateValidDataToAdd() => new()
        {
            Name = "New Tests User",
            Role = Role.User,
            Username = "tests.new",
            IsActive = false,
            Joined = new DateTime(2024, 11, 19),
            Password = "Tests.New",
        };

        // We update the user "tests" which is used to authenticate tests, so we must keep it Active and with the role Admin so the others tests don't fail
        protected override UserPlainPassword CreateValidDataToUpdate() => new()
        {
            Id = 1,
            Role = Role.Admin,
            Name = "Valid Tests User",
            Username = "tests",
            IsActive = true,
            Joined = new DateTime(2024, 9, 19),
        };
        #endregion
    }
}