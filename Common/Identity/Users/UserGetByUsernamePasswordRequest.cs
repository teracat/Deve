using Deve.Identity.Enums;

namespace Deve.Identity.Users;

public sealed record UserGetByUsernamePasswordRequest(string Username, string Password, UserActiveType ActiveType);
