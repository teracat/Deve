namespace Deve.Identity.Users.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required UserStatus Status { get; init; }
    public required Role Role { get; init; }

    public string? Email { get; init; }
    public DateTime? Birthday { get; init; }
}
