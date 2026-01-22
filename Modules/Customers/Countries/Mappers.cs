namespace Deve.Customers.Countries;

internal static class Mappers
{
    public static CountryResponse ToResponse(this Country country) => new(country.Id, country.Name, country.IsoCode);

    public static GetList.Query ToQuery(this CountryGetListRequest? request) => new(request?.Id, request?.Name, request?.IsoCode, request?.Offset, request?.Limit, request?.OrderBy);

    public static Add.Command ToCommand(this CountryAddRequest request) => new(request.Name, request.IsoCode);

    public static Update.Command ToCommand(this CountryUpdateRequest request, Guid id) => new(id, request.Name, request.IsoCode);

    // <hooks:mappers>
}
