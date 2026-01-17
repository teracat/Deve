namespace Deve.Auth.Login;

public sealed record LoginResponse(string Name, string Username, DateTimeOffset Joined, UserToken Token);
