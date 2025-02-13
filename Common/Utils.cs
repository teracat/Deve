using Deve.Localize;
using Deve.Model;

namespace Deve
{
    public class Utils
    {
        public static bool SomeIsNullOrWhiteSpace(params string?[] values) => values.Any(x => string.IsNullOrWhiteSpace(x));

        public static bool FindNullOrWhiteSpace(out List<Field> found, params Field[] fields)
        {
            found = [];
            foreach (var field in fields)
            {
                if (IsEmptyValue(field.Value))
                    found.Add(field);
            }
            return found.Count > 0;
        }

        public static bool IsEmptyValue(object? value)
        {
            if (value is null)
                return true;

            if (value is string strValue)
                return string.IsNullOrWhiteSpace(strValue);
            
            if (value is int iValue)
                return iValue <= 0;

            if (value is long lValue)
                return lValue <= 0;

            return false;
        }

        public static IList<ResultError> FoundFieldsToErrors(string langCode, ResultErrorType errorType, List<Field> list) => list.Select(x => new ResultError(errorType, x.Name, ErrorLocalizeFactory.Get().Localize(errorType, langCode)))
                       .ToList();

        public static T RunProtected<T>(SemaphoreSlim semaphore, Func<T> func)
        {
            semaphore.Wait();
            try
            {
                return func.Invoke();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static Task<T> RunProtectedAsync<T>(SemaphoreSlim semaphore, Func<T> func) => Task.Run(() => { return RunProtected(semaphore, func); });

        public static Result ResultOk() => new();

        public static Result ResultError(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

        public static Result ResultError(string langCode, ResultErrorType errorType) => new(errorType, null, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

        public static Result ResultError(ResultErrorType errorType, string? fieldName, string? errorDescription) => new(errorType, fieldName, errorDescription);

        public static Result ResultError(ResultErrorType errorType, string? fieldName) => new(errorType, fieldName);

        public static Result ResultError(IList<ResultError> errors) => new(errors);

        public static Result ResultError(Result result) => new(result);

        public static ResultGet<T> ResultGetOk<T>(T data) => new(data);

        public static ResultGet<T> ResultGetError<T>(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

        public static ResultGet<T> ResultGetError<T>(string langCode, ResultErrorType errorType) => ResultGetError<T>(langCode, errorType, null);

        public static ResultGet<T> ResultGetError<T>(ResultErrorType errorType, string? fieldName, string? errorDescription) => new(errorType, fieldName, errorDescription);

        public static ResultGet<T> ResultGetError<T>(ResultErrorType errorType, string? fieldName) => ResultGetError<T>(errorType, fieldName, null);

        public static ResultGet<T> ResultGetError<T>(ResultErrorType errorType) => ResultGetError<T>(errorType, null, null);

        public static ResultGet<T> ResultGetError<T>(Result result) => new(result);

        public static ResultGet<T> ResultGetError<T>(IList<ResultError> errors) => new(errors);

        public static ResultGetList<T> ResultGetListOk<T>(List<T> data, int? offset, int? limit, string orderBy, int totalCount) => new(data, offset, limit, orderBy, totalCount);

        public static ResultGetList<T> ResultGetListError<T>(string langCode, ResultErrorType errorType, string? fieldName) => new(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));

        public static ResultGetList<T> ResultGetListError<T>(string langCode, ResultErrorType errorType) => ResultGetListError<T>(langCode, errorType, null);

        public static ResultGetList<T> ResultGetListError<T>(ResultErrorType errorType, string? fieldName, string? errorDescription) => new(errorType, fieldName, errorDescription);

        public static ResultGetList<T> ResultGetListError<T>(ResultErrorType errorType, string? fieldName) => ResultGetListError<T>(errorType, fieldName, null);

        public static ResultGetList<T> ResultGetListError<T>(ResultErrorType errorType) => ResultGetListError<T>(errorType, null, null);

        public static ResultGetList<T> ResultGetListError<T>(Result result) => new(result);

        public static ResultGetList<T> ResultGetListError<T>(IList<ResultError> errors) => new(errors);

        public static T? CreateInstance<T>() => (T?)Activator.CreateInstance(typeof(T));

        public static string ErrorsToString(IList<ResultError> errors, char separator) => string.Join(separator, errors.Select(x => string.IsNullOrEmpty(x.FieldName) ? x.Description : $"{x.Description} ({x.FieldName})"));

        public static string ErrorsToString(IList<ResultError> errors) => ErrorsToString(errors, ',');
    }
}