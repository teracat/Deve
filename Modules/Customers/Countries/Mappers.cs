namespace Deve.Customers.Countries;

internal static class Mappers
{
    public static CountryResponse ToResponse(this Country country) => new()
    {
        Id = country.Id,
        Name = country.Name,
        IsoCode = country.IsoCode
    };

    public static GetList.Query ToQuery(this CountryGetListRequest? request) => new()
    {
        Id = request?.Id,
        Name = request?.Name,
        IsoCode = request?.IsoCode,
        Offset = request?.Offset,
        Limit = request?.Limit,
        OrderBy = request?.OrderBy
    };

    public static Add.Command ToCommand(this CountryAddRequest request) => new()
    {
        Name = request.Name,
        IsoCode = request.IsoCode
    };

    public static Update.Command ToCommand(this CountryUpdateRequest request, Guid id) => new()
    {
        Id = id,
        Name = request.Name,
        IsoCode = request.IsoCode
    };

    // <hooks:mappers>
}
