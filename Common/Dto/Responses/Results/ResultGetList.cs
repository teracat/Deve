using Deve.Localize;

namespace Deve.Dto.Responses.Results;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Factory-style static methods on result are intentional here")]
public record ResultGetList<T> : IResult<ResultGetList<T>>
{
    public bool Success { get; init; }

    public IReadOnlyList<ResultError>? Errors { get; init; }

    public IReadOnlyList<T> Data { get; init; }

    public int? Offset { get; init; }

    public int? Limit { get; init; }

    public string OrderBy { get; init; } = string.Empty;

    public int TotalCount { get; init; }

    public ResultGetList()
    {
        Success = true;
        Errors = null;
        Data = [];
    }

    public ResultGetList(IReadOnlyList<T> data, int? offset, int? limit, string orderBy, int totalCount)
    {
        Success = true;
        Errors = [];
        Data = data;
        Offset = offset;
        Limit = limit;
        OrderBy = orderBy;
        TotalCount = totalCount;
    }

    public ResultGetList(bool success, IReadOnlyList<ResultError>? errors)
    {
        Success = success;
        Errors = errors;
        Data = [];
    }

    public ResultGetList(ResultErrorType errorType, string? fieldName, string? errorDescription)
    {
        Success = false;
        Errors = [new ResultError(errorType, fieldName, errorDescription)];
        Data = [];
    }

    public ResultGetList(IReadOnlyList<ResultError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        Success = errors.Count == 0;
        Errors = errors;
        Data = [];
    }

    public ResultGetList(ResultGetList<T> result)
    {
        ArgumentNullException.ThrowIfNull(result);

        Success = result.Success;
        Errors = result.Errors;
        Data = result.Data;
        Offset = result.Offset;
        Limit = result.Limit;
        OrderBy = result.OrderBy;
        TotalCount = result.TotalCount;
    }

    public ResultGetList(IResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        Success = result.Success;
        Errors = result.Errors;
        Data = [];
    }

    public static ResultGetList<T> Fail(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static ResultGetList<T> Fail(IReadOnlyList<ResultError> errors) => new(errors);
}
