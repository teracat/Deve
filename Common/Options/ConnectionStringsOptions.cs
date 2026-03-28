namespace Deve.Options;

public class ConnectionStringsOptions
{
    // <hooks:common-connectionstringsoptions-properties>

    public string CustomersConnectionRead { get; set; } = string.Empty;
    public string CustomersConnectionWrite { get; set; } = string.Empty;

    public string IdentityConnectionRead { get; set; } = string.Empty;
    public string IdentityConnectionWrite { get; set; } = string.Empty;

    public string RedisCacheConnection { get; set; } = string.Empty;
}
