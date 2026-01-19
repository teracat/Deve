namespace Deve.Identity;

public static class IdentityConstants
{
    public const string PathIdentityV1 = ApiConstants.BasePathV1 + "Identity/";
    public const string PathUserV1 = PathIdentityV1 + "User/";

    public const string MethodPassword = "Password";
    public const string MethodGetByUsernamePassword = "GetByUsernamePassword";

    public const string TagUser = "User";
}
