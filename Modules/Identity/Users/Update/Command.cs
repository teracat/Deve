namespace Deve.Identity.Users.Update;

internal sealed record Command : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Username { get; init; }
    public required UserStatus Status { get; init; }
    public required Role Role { get; init; }

    public string? Email { get; init; }
    public DateTime? Birthday { get; init; }
}
