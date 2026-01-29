namespace Deve.Dto.Responses.Results;

public record ResultError(ResultErrorType Type, string? FieldName, string? Description)
{
    public ResultError() : this(ResultErrorType.Unknown, null, null) { }

    public ResultError(ResultErrorType type, string? fieldName) : this(type, fieldName, null) { }

    public ResultError(ResultErrorType type) : this(type, null, null) { }
}
