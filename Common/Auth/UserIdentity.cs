using Deve.Identity.Enums;

namespace Deve.Auth;

/// <summary>
/// User information used internally (can contain private information).
/// When new properties are needed, you should also change the UserIdentityMapper methods.
/// </summary>
public sealed record UserIdentity(Guid Id, string Username, Role Role);
