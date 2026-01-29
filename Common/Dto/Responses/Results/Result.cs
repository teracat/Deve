using Deve.Localize;

namespace Deve.Dto.Responses.Results;

public record Result : IResult<Result>
{
    public bool Success { get; init; }

    public IReadOnlyList<ResultError>? Errors { get; init; }

    public Result()
    {
        Success = true;
        Errors = null;
    }

    public Result(bool success, IReadOnlyList<ResultError>? errors)
    {
        Success = success;
        Errors = errors;
    }

    public Result(ResultErrorType errorType, string? fieldName, string? errorDescription)
    {
        Success = false;
        Errors = [new ResultError(errorType, fieldName, errorDescription)];
    }

    public Result(IReadOnlyList<ResultError> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        Success = errors.Count == 0;
        Errors = errors;
    }

    public Result(IResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        Success = result.Success;
        Errors = result.Errors;
    }

    public static Result Ok() => new();

    public static Result Fail(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static Result Fail(string langCode, ResultErrorType errorType) => new(errorType, null, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static Result Fail(ResultErrorType errorType, string? fieldName, string? errorDescription) => new(errorType, fieldName, errorDescription);

    public static Result Fail(ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, null);

    public static Result Fail(IReadOnlyList<ResultError> errors) => new(errors);

    public static Result Fail(IResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        return new Result(result.Success, result.Errors);
    }


    public static ResultGet<T> OkGet<T>(T data) => new(data);

    public static ResultGet<T> FailGet<T>(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static ResultGet<T> FailGet<T>(string langCode, ResultErrorType errorType) => new(errorType, null, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static ResultGet<T> FailGet<T>(ResultErrorType errorType, string? fieldName, string? errorDescription) => new(errorType, fieldName, errorDescription);

    public static ResultGet<T> FailGet<T>(ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, null);

    public static ResultGet<T> FailGet<T>(ResultErrorType errorType) => new(errorType, null, null);

    public static ResultGet<T> FailGet<T>(IReadOnlyList<ResultError> errors) => new(errors);

    public static ResultGet<T> FailGet<T>(IResult result) => new(result);


    public static ResultGetList<T> OkGetList<T>(IReadOnlyList<T> data, int? offset, int? limit, string orderBy, int totalCount) => new(data, offset, limit, orderBy, totalCount);

    public static ResultGetList<T> FailGetList<T>(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static ResultGetList<T> FailGetList<T>(string langCode, ResultErrorType errorType) => new(errorType, null, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

    public static ResultGetList<T> FailGetList<T>(ResultErrorType errorType, string? fieldName, string? errorDescription) => new(errorType, fieldName, errorDescription);

    public static ResultGetList<T> FailGetList<T>(ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, null);

    public static ResultGetList<T> FailGetList<T>(ResultErrorType errorType) => new(errorType, null, null);

    public static ResultGetList<T> FailGetList<T>(IReadOnlyList<ResultError> errors) => new(errors);

    public static ResultGetList<T> FailGetList<T>(IResult result) => new(result);
}
