namespace Deve.Identity;

public static class IdentityConstants
{
    public static readonly string PathIdentityV1 = ApiConstants.BasePathV1 + "Identity/";

    // <hooks:constants-path>
    public static readonly string PathUserV1 = PathIdentityV1 + "User/";

    // <hooks:constants-method>
    public static readonly string MethodPassword = "Password";
    public static readonly string MethodGetByUsernamePassword = "GetByUsernamePassword";

    // <hooks:constants-tag>
    public static readonly string TagUser = "User";
}
