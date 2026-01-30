namespace Deve.Dto.Responses.Results;

public interface IResult
{
    bool Success { get; }

    IReadOnlyList<ResultError>? Errors { get; }
}

public interface IResult<TSelf> : IResult where TSelf : IResult<TSelf>
{
    static abstract TSelf Fail(string langCode, ResultErrorType errorType, string? fieldName);

    static abstract TSelf Fail(IReadOnlyList<ResultError> errors);
}
