using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Identity.Enums;
using Deve.Identity.Users;

namespace Deve.Tests.Modules.Identity;

public abstract class UserTest : DataAllBaseTest<IData, UserResponse, UserResponse>
{
    #region Constructor
    protected UserTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => new UserGetListRequest();

    protected override object CreateInvalidRequestToAdd() => new UserAddRequest
    {
        Name = "",
        Username = "",
        Password = "",
        Status = UserStatus.Inactive,
        Role = Role.User,
    };

    protected override object CreateInvalidRequestToUpdate() => new UserUpdateRequest
    {
        Name = "",
        Username = "",
        Status = UserStatus.Inactive,
        Role = Role.User
    };

    protected override object CreateValidRequestToAdd() => new UserAddRequest
    {
        Name = "New Tests User",
        Username = "tests.new",
        Password = "Tests.New",
        Status = UserStatus.Active,
        Role = Role.User
    };

    protected override object CreateValidRequestToUpdate() => new UserUpdateRequest
    {
        Name = "Fake User Updated",
        Username = "fake",
        Status = UserStatus.Active,
        Role = Role.Admin
    };

    protected override Task<ResultGetList<UserResponse>> GetListAsync(IData data, object? request) => data.Identity.Users.GetAsync((UserGetListRequest?)request);
    protected override Task<ResultGet<UserResponse>> GetByIdAsync(IData data, Guid? id) => data.Identity.Users.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.Identity.Users.AddAsync((UserAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.Identity.Users.UpdateAsync(id, (UserUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.Identity.Users.DeleteAsync(id);
    #endregion
}
