namespace Deve.Customers;

public static class CustomersConstants
{
    public const string PathCustomersV1 = ApiConstants.BasePathV1 + "Customers/";

    public const string PathCountryV1 = PathCustomersV1 + "Country/";
    public const string PathStateV1 = PathCustomersV1 + "State/";
    public const string PathCityV1 = PathCustomersV1 + "City/";
    public const string PathClientV1 = PathCustomersV1 + "Client/";
    public const string PathStatsV1 = PathCustomersV1 + "Stats/";

    public const string MethodGetClientStats = "GetClientStats";
    public const string MethodUpdateStatus = "UpdateStatus";

    public const string TagCustomersCountry = "Country";
    public const string TagCustomersState = "State";
    public const string TagCustomersCity = "City";
    public const string TagCustomersClient = "Client";
    public const string TagCustomersStats = "Stats";
}