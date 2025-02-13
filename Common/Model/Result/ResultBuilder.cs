using Deve.Localize;
using System.Runtime.CompilerServices;

namespace Deve.Model
{
    public class ResultBuilder
    {
        #region Fields
        private string _langCode;
        #endregion

        #region Properties
        public List<ResultError> Errors { get; private set; } = [];

        public bool HasErrors => Errors.Count > 0;
        #endregion

        #region Static Method
        public static ResultBuilder Create(string langCode = Constants.DefaultLangCode)
        {
            return new ResultBuilder(langCode);
        }
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
            Errors.AddRange(errors);
            return this;
        }
        #endregion

        #region Checks Methods
        public void CheckNotNull(object? value, [CallerArgumentExpression(nameof(value))] string fieldName = "")
        {
            CheckNotNull(value, ResultErrorType.InvalidId, fieldName);
        }

        public void CheckNotNull(object? value, ResultErrorType errorType, [CallerArgumentExpression(nameof(value))] string fieldName = "")
        {
            if (value is null)
                Add(errorType, fieldName);
        }

        public ResultBuilder CheckNotNullOrEmpty(params Field[] fields)
        {
            if (Utils.FindNullOrWhiteSpace(out var found, fields))
                AddRange(Utils.FoundFieldsToErrors(_langCode, ResultErrorType.MissingRequiredField, found));
            return this;
        }
        #endregion

        #region ToResult Method
        public Result ToResult()
        {
            if (HasErrors)
                return Utils.ResultError(Errors);
            else
                return Utils.ResultOk();
        }
        #endregion
    }
}