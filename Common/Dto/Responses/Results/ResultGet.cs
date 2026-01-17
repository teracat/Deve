using Deve.Localize;

namespace Deve.Dto.Responses.Results;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "Factory-style static methods on result are intentional here")]
public record ResultGet<T> : IResult<ResultGet<T>>
{
    public bool Success { get; init; }

    public IReadOnlyList<ResultError>? Errors { get; init; }

    public T? Data { get; init; }

    public ResultGet()
    {
        Success = true;
        Errors = null;
        Data = default;
    }

    public ResultGet(T data)
    {
        Success = true;
        Errors = null;
        Data = data;
    }

    public ResultGet(bool success, IReadOnlyList<ResultError>? errors)
    {
        Success = success;
        Errors = errors;
        Data = default;
    }

    public ResultGet(ResultErrorType errorType, string? fieldName, string? errorDescription)
    {
        Success = false;
        Errors = [new ResultError(errorType, fieldName, errorDescription)];
        Data = default;
    }

    public ResultGet(IReadOnlyList<ResultError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        Success = errors.Count == 0;
        Errors = errors;
        Data = default;
    }

    public ResultGet(IResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        Success = result.Success;
        Errors = result.Errors;
        Data = default;
    }

    public static ResultGet<T> Fail(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static ResultGet<T> Fail(IReadOnlyList<ResultError> errors) => new(errors);
}
