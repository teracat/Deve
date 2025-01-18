using Deve.Localize;

namespace Deve
{
    public class Utils
    {
        public static bool SomeIsNullOrWhiteSpace(params string?[] values)
        {
            return values.Any(x => string.IsNullOrWhiteSpace(x));
        }

        public static bool FindNullOrWhiteSpace(out List<Field> found, params Field[] fields)
        {
            found = new List<Field>();
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

        public static IList<ResultError> FoundFieldsToErrors(string langCode, ResultErrorType errorType, List<Field> list)
        {
            return list.Select(x => new ResultError(errorType, x.Name, ErrorLocalizeFactory.Get().Localize(errorType, langCode)))
                       .ToList();
        }

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

        public static Task<T> RunProtectedAsync<T>(SemaphoreSlim semaphore, Func<T> func)
        {
            return Task.Run(() =>
            {
                return RunProtected(semaphore, func);
            });
        }

        public static Result ResultOk()
        {
            return new Result();
        }

        public static Result ResultError(string langCode, ResultErrorType errorType, string? fieldName = null)
        {
            return new Result(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));
        }

        public static Result ResultError(ResultErrorType errorType, string? fieldName = null, string? errorDescription = null)
        {
            return new Result(errorType, fieldName, errorDescription);
        }

        public static Result ResultError(IList<ResultError> errors)
        {
            return new Result(errors);
        }

        public static Result ResultError(Result result)
        {
            return new Result(result);
        }

        public static ResultGet<T> ResultGetOk<T>(T data)
        {
            return new ResultGet<T>(data);
        }

        public static ResultGet<T> ResultGetError<T>(string langCode, ResultErrorType errorType, string? fieldName = null)
        {
            return new ResultGet<T>(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));
        }

        public static ResultGet<T> ResultGetError<T>(ResultErrorType errorType, string? fieldName = null, string? errorDescription = null)
        {
            return new ResultGet<T>(errorType, fieldName, errorDescription);
        }

        public static ResultGet<T> ResultGetError<T>(Result result)
        {
            return new ResultGet<T>(result);
        }

        public static ResultGet<T> ResultGetError<T>(IList<ResultError> errors)
        {
            return new ResultGet<T>(errors);
        }

        public static ResultGetList<T> ResultGetListOk<T>(List<T> data, int? offset, int? limit, string orderBy, int totalCount)
        {
            return new ResultGetList<T>(data, offset, limit, orderBy, totalCount);
        }

        public static ResultGetList<T> ResultGetListError<T>(string langCode, ResultErrorType errorType, string? fieldName = null)
        {
            return new ResultGetList<T>(errorType, fieldName, ErrorLocalizeFactory.Get().Localize(errorType, langCode));
        }

        public static ResultGetList<T> ResultGetListError<T>(ResultErrorType errorType, string? fieldName = null, string? errorDescription = null)
        {
            return new ResultGetList<T>(errorType, fieldName, errorDescription);
        }

        public static ResultGetList<T> ResultGetListError<T>(Result result)
        {
            return new ResultGetList<T>(result);
        }

        public static ResultGetList<T> ResultGetListError<T>(IList<ResultError> errors)
        {
            return new ResultGetList<T>(errors);
        }

        public static T? CreateInstance<T>()
        {
            return (T?)Activator.CreateInstance(typeof(T));
        }

        public static string ErrorsToString(IList<ResultError> errors, char separator = ',')
        {
            return string.Join(separator, errors.Select(x => string.IsNullOrEmpty(x.FieldName) ? x.Description : $"{x.Description} ({x.FieldName})"));
        }
    }
}