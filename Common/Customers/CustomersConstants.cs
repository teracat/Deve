namespace Deve.Customers;

public static class CustomersConstants
{
    public static readonly string PathCustomersV1 = Constants.BasePathApiV1 + "Customers/";

    // <hooks:constants-path>
    public static readonly string PathCountryV1 = PathCustomersV1 + "Country/";
    public static readonly string PathStateV1 = PathCustomersV1 + "State/";
    public static readonly string PathCityV1 = PathCustomersV1 + "City/";
    public static readonly string PathClientV1 = PathCustomersV1 + "Client/";
    public static readonly string PathStatsV1 = PathCustomersV1 + "Stats/";

    // <hooks:constants-method>
    public static readonly string MethodGetClientStats = "GetClientStats";
    public static readonly string MethodUpdateStatus = "UpdateStatus";

    // <hooks:constants-tag>
    public static readonly string TagCountry = "Country";
    public static readonly string TagState = "State";
    public static readonly string TagCity = "City";
    public static readonly string TagClient = "Client";
    public static readonly string TagStats = "Stats";
}