using System.Text.RegularExpressions;
using Deve.Dto;
using Deve.Dto.Responses.Results;
using Deve.Localize;

namespace Deve;

/// <summary>
/// Provides utility methods.
/// </summary>
public static partial class Utils
{
    /// <summary>
    /// Determines if any of the given strings are null, empty, or contain only whitespace.
    /// </summary>
    /// <param name="values">The string values to check.</param>
    /// <returns>true if at least one value is null, empty, or whitespace; otherwise, false.</returns>
    public static bool SomeIsNullOrWhiteSpace(params string?[] values) => values.Any(string.IsNullOrWhiteSpace);

    /// <summary>
    /// Finds fields where the value is null or whitespace.
    /// </summary>
    /// <param name="fields">The fields to check.</param>
    /// <returns>A list of fields that contain null or whitespace values.</returns>
    public static IList<Field> FindNullOrWhiteSpace(params Field[] fields)
    {
        if (fields is null)
        {
            return [];
        }

        return fields.Where(x => x is not null && IsEmptyValue(x.Value))
                     .ToList();
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

        if (value is Guid gValue)
        {
            return gValue == Guid.Empty;
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
    public static IList<ResultError> FoundFieldsToErrors(string langCode, ResultErrorType errorType, IList<Field> list)
    {
        if (list is null)
        {
            return [];
        }

        return list.Select(x => new ResultError(errorType, x.Name, ErrorLocalizeFactory.Get().Localize(errorType, langCode)))
                   .ToList();
    }

    /// <summary>
    /// Executes a function within a semaphore lock.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="semaphore">The semaphore to control concurrent execution.</param>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result of the function execution.</returns>
    public static T RunProtected<T>(SemaphoreSlim semaphore, Func<CancellationToken, T> func, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(semaphore);
        ArgumentNullException.ThrowIfNull(func);

        semaphore.Wait(cancellationToken);
        try
        {
            return func(cancellationToken);
        }
        finally
        {
            _ = semaphore.Release();
        }
    }

    /// <summary>
    /// Executes a function asynchronously within a semaphore lock.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="semaphore">The semaphore to control concurrent execution.</param>
    /// <param name="func">The function to execute.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous execution.</returns>
    public static Task<T> RunProtectedAsync<T>(SemaphoreSlim semaphore, Func<CancellationToken, T> func, CancellationToken cancellationToken) => Task.Run(() => RunProtected(semaphore, func, cancellationToken), cancellationToken);

    public static Task<T> RunProtectedAsync<T>(SemaphoreSlim semaphore, Func<CancellationToken, T> func) => Task.Run(() => RunProtected(semaphore, func, default));

    /// <summary>
    /// Creates an instance of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    /// <returns>A new instance of the specified type, or <c>null</c> if creation fails.</returns>
    public static T? CreateInstance<T>() => Activator.CreateInstance<T>();

    /// <summary>
    /// Converts a list of result errors into a single string with a specified separator.
    /// </summary>
    /// <param name="errors">The list of result errors.</param>
    /// <param name="separator">The character used to separate error messages.</param>
    /// <returns>A formatted string containing all errors.</returns>
    public static string ErrorsToString(IReadOnlyList<ResultError>? errors, char separator)
    {
        if (errors is null)
        {
            return string.Empty;
        }
        return string.Join(separator, errors.Select(x => string.IsNullOrEmpty(x.FieldName) ? x.Description : $"{x.Description} ({x.FieldName})"));
    }

    /// <summary>
    /// Converts a list of result errors into a single string, separated by commas.
    /// </summary>
    /// <param name="errors">The list of result errors.</param>
    /// <returns>A formatted string containing all errors.</returns>
    public static string ErrorsToString(IReadOnlyList<ResultError>? errors) => ErrorsToString(errors, ',');

    /// <summary>
    /// Converts an string with named arguments into a composite format string with indexed arguments.
    /// </summary>
    /// <param name="text">The string with named arguments to convert.</param>
    /// <returns>A composite format string with indexed arguments.</returns>
    public static string ConvertNameArgumentsToIndexed(string text)
    {
        var keys = new List<string>();
        string result = ConvertNameArgumentsToIndexedRegex().Replace(text, match =>
        {
            string key = match.Groups[1].Value;
            if (!keys.Contains(key))
            {
                keys.Add(key);
            }
            int index = keys.IndexOf(key);
            return $"{{{index}}}";
        });

        return result;
    }

    public static string GetCacheKeyForType<T>(Guid id) => $"{typeof(T).Name}-{id}";

    [GeneratedRegex(@"\{(\w+)\}")]
    private static partial Regex ConvertNameArgumentsToIndexedRegex();
}
