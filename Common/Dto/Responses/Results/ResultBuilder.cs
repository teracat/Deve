using System.Runtime.CompilerServices;
using Deve.Localize;

namespace Deve.Dto.Responses.Results;

public class ResultBuilder
{
    #region Fields
    private readonly string _langCode;
    private readonly List<ResultError> _errors = [];
    #endregion

    #region Properties
    public IList<ResultError> Errors => _errors;

    public bool HasErrors => Errors.Count > 0;
    #endregion

    #region Static Method
    public static ResultBuilder Create(string langCode) => new(langCode);

    public static ResultBuilder Create() => Create(Constants.DefaultLangCode);
    #endregion

    #region Constructors
    public ResultBuilder(string langCode)
    {
        _langCode = langCode;
    }

    public ResultBuilder()
    {
        _langCode = Constants.DefaultLangCode;
    }
    #endregion

    #region Add Methods
    public ResultBuilder Add(ResultError error)
    {
        Errors.Add(error);
        return this;
    }

    public ResultBuilder Add(ResultErrorType type, string? fieldName, string? description)
    {
        Errors.Add(new ResultError(type, fieldName, description ?? ErrorLocalizeFactory.Get().Localize(type, _langCode)));
        return this;
    }

    public ResultBuilder Add(ResultErrorType type, string? fieldName) => Add(type, fieldName, null);

    public ResultBuilder Add(ResultErrorType type) => Add(type, null, null);

    public ResultBuilder AddRange(IEnumerable<ResultError> errors)
    {
        _errors.AddRange(errors);
        return this;
    }
    #endregion

    #region Checks Methods
    public ResultBuilder CheckNotNull(object? value, [CallerArgumentExpression(nameof(value))] string fieldName = "") => CheckNotNull(value, ResultErrorType.InvalidId, fieldName);

    public ResultBuilder CheckNotNull(object? value, ResultErrorType errorType, [CallerArgumentExpression(nameof(value))] string fieldName = "")
    {
        if (value is null)
        {
            _ = Add(errorType, fieldName);
        }
        return this;
    }

    public ResultBuilder CheckNotNullOrEmpty(params Field[] fields)
    {
        var found = Utils.FindNullOrWhiteSpace(fields);
        if (found.Count > 0)
        {
            return AddRange(Utils.FoundFieldsToErrors(_langCode, ResultErrorType.MissingRequiredField, found));
        }
        return this;
    }

    public ResultBuilder CheckStringMaxLength(string? value, int maxLength, [CallerArgumentExpression(nameof(value))] string fieldName = "")
    {
        if (value is not null && value.Length > maxLength)
        {
            var errorFactory = ErrorLocalizeFactory.Get();
            var msg = string.Format(errorFactory.GetCulture(_langCode), errorFactory.Localize(ResultErrorType.FieldValueTooLong, _langCode), maxLength);
            _ = Add(ResultErrorType.FieldValueTooLong, fieldName, msg);
        }
        return this;
    }

    public ResultBuilder CheckStringMinLength(string? value, int minLength, [CallerArgumentExpression(nameof(value))] string fieldName = "")
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < minLength)
        {
            var errorFactory = ErrorLocalizeFactory.Get();
            var msg = string.Format(errorFactory.GetCulture(_langCode), errorFactory.Localize(ResultErrorType.FieldValueTooShort, _langCode), minLength);
            _ = Add(ResultErrorType.FieldValueTooShort, fieldName, msg);
        }
        return this;
    }
    #endregion

    #region ToResult Method
    public Result ToResult()
    {
        if (HasErrors)
        {
            return Result.Fail(_errors);
        }
        else
        {
            return Result.Ok();
        }
    }
    #endregion
}
