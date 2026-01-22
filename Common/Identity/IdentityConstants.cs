namespace Deve.Identity;

public static class IdentityConstants
{
    public const string PathIdentityV1 = ApiConstants.BasePathV1 + "Identity/";

    // <hooks:constants-path>
    public const string PathUserV1 = PathIdentityV1 + "User/";

    // <hooks:constants-method>
    public const string MethodPassword = "Password";
    public const string MethodGetByUsernamePassword = "GetByUsernamePassword";

    // <hooks:constants-tag>
    public const string TagUser = "User";
}
