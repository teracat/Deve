namespace Deve.Auth.Login;

public sealed record LoginResponse
{
    public string Name { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public DateTimeOffset Joined { get; init; }
    public UserToken Token { get; init; } = new UserToken();
}
