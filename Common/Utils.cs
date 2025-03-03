using Deve.Localize;
using Deve.Model;

namespace Deve
{
    /// <summary>
    /// Provides utility methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Determines if any of the given strings are null, empty, or contain only whitespace.
        /// </summary>
        /// <param name="values">The string values to check.</param>
        /// <returns>true if at least one value is null, empty, or whitespace; otherwise, false.</returns>
        public static bool SomeIsNullOrWhiteSpace(params string?[] values) => values.Any(x => string.IsNullOrWhiteSpace(x));

        /// <summary>
        /// Finds fields where the value is null or whitespace.
        /// </summary>
        /// <param name="fields">The fields to check.</param>
        /// <returns>A list of fields that contain null or whitespace values.</returns>
        public static List<Field> FindNullOrWhiteSpace(params Field[] fields)
        {
            List<Field> found = [];
            foreach (var field in fields)
            {
                if (IsEmptyValue(field.Value))
                {
                    found.Add(field);
                }
            }
            return found;
        }

        /// <summary>
        /// Determines if the given value is empty based on its type.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if the value is empty or null; otherwise, false.</returns>
        public static bool IsEmptyValue(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is string strValue)
            {
                return string.IsNullOrWhiteSpace(strValue);
            }

            if (value is int iValue)
            {
                return iValue <= 0;
            }

            if (value is long lValue)
            {
                return lValue <= 0;
            }

            return false;
        }

        /// <summary>
        /// Converts a list of fields with null or whitespace values into a list of result errors.
        /// </summary>
        /// <param name="langCode">The language code for localization.</param>
        /// <param name="errorType">The error type.</param>
        /// <param name="list">The list of fields with errors.</param>
        /// <returns>A list of result errors.</returns>
        public static IList<ResultError> FoundFieldsToErrors(string langCode, ResultErrorType errorType, List<Field> list) => list.Select(x => new ResultError(errorType, x.Name, ErrorLocalizeFactory.Get().Localize(errorType, langCode)))
                       .ToList();

        /// <summary>
        /// Executes a function within a semaphore lock.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <param name="semaphore">The semaphore to control concurrent execution.</param>
        /// <param name="func">The function to execute.</param>
        /// <returns>The result of the function execution.</returns>
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

        /// <summary>
        /// Executes a function asynchronously within a semaphore lock.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <param name="semaphore">The semaphore to control concurrent execution.</param>
        /// <param name="func">The function to execute.</param>
        /// <returns>A task representing the asynchronous execution.</returns>
        public static Task<T> RunProtectedAsync<T>(SemaphoreSlim semaphore, Func<T> func) => Task.Run(() => { return RunProtected(semaphore, func); });

        public static Result ResultOk() => new();

        public static Result ResultError() => new(false);

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

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>A new instance of the specified type, or <c>null</c> if creation fails.</returns>
        public static T? CreateInstance<T>() => (T?)Activator.CreateInstance(typeof(T));

        /// <summary>
        /// Converts a list of result errors into a single string with a specified separator.
        /// </summary>
        /// <param name="errors">The list of result errors.</param>
        /// <param name="separator">The character used to separate error messages.</param>
        /// <returns>A formatted string containing all errors.</returns>
        public static string ErrorsToString(IList<ResultError> errors, char separator) => string.Join(separator, errors.Select(x => string.IsNullOrEmpty(x.FieldName) ? x.Description : $"{x.Description} ({x.FieldName})"));

        /// <summary>
        /// Converts a list of result errors into a single string, separated by commas.
        /// </summary>
        /// <param name="errors">The list of result errors.</param>
        /// <returns>A formatted string containing all errors.</returns>
        public static string ErrorsToString(IList<ResultError> errors) => ErrorsToString(errors, ',');
    }
}