using Deve.Identity;
using Deve.Identity.Enums;
using Deve.Identity.Users;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Identity;

public class UserApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => IdentityConstants.PathUserV1;

    public UserApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    protected override object CreateInvalidRequestToAdd() => new UserAddRequest
    (
        Name: string.Empty,
        Username: string.Empty,
        Password: string.Empty,
        Status: UserStatus.Inactive,
        Role: Role.User,
        Email: null,
        Birthday: null
    );

    protected override object CreateInvalidRequestToUpdate() => new UserUpdateRequest
    (
        Name: string.Empty,
        Username: string.Empty,
        Status: UserStatus.Inactive,
        Role: Role.User,
        Email: null,
        Birthday: null
    );

    protected override object CreateValidRequestToAdd() => new UserAddRequest
    (
        Name: "Tests User",
        Username: "new.user",
        Password: "new-user",
        Status: UserStatus.Active,
        Role: Role.User,
        Email: null,
        Birthday: null
    );

    protected override object CreateValidRequestToUpdate() => new UserUpdateRequest
    (
        Name: "Fake User 2",
        Username: "fake2",
        Status: UserStatus.Inactive,
        Role: Role.User,
        Email: null,
        Birthday: null
    );

    #region UpdatePassword Tests
    [Fact]
    public async Task UpdatePassword_Unauthorized_NotSuccessStatusCode()
    {
        var request = new UserUpdatePasswordRequest(string.Empty);

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientNoAuth.PutAsync(Path + $"{Guid.Empty}/" + IdentityConstants.MethodPassword, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdatePassword_NullRequest_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + $"{ValidId}/" + IdentityConstants.MethodPassword, null);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdatePassword_EmptyId_NotSuccessStatusCode()
    {
        var request = new UserUpdatePasswordRequest("pwd");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + $"{Guid.Empty}/" + IdentityConstants.MethodPassword, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdatePassword_InvalidId_NotSuccessStatusCode()
    {
        var request = new UserUpdatePasswordRequest("pwd");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + $"{InvalidId}/" + IdentityConstants.MethodPassword, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdatePassword_ValidId_SuccessStatusCode()
    {
        var request = new UserUpdatePasswordRequest("new-pwd-test");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{TestsConstants.UpdateUserId}/" + IdentityConstants.MethodPassword, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion
}
