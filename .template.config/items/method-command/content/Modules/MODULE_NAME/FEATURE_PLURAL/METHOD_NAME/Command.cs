namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed record Command : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
