namespace Deve.Options;

public class ConnectionStringsOptions
{
    // <hooks:common-connectionstringsoptions-properties>

    public string CustomersConnection { get; set; } = string.Empty;

    public string IdentityConnection { get; set; } = string.Empty;

    public string RedisCacheConnection { get; set; } = string.Empty;
}
