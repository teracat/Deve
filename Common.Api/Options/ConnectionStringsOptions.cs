namespace Deve.Api.Options;

public class ConnectionStringsOptions
{
    // <hooks:api-connectionstringsoptions-properties>

    public string CustomersConnection { get; set; } = string.Empty;

    public string IdentityConnection { get; set; } = string.Empty;
}
