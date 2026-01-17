using System.Collections.Specialized;

namespace Deve.Sdk;

/// <summary>
/// Format Url Query parameters and values.
/// </summary>
internal class UriQuery
{
    private readonly NameValueCollection collection;

    public UriQuery()
    {
        collection = System.Web.HttpUtility.ParseQueryString(string.Empty);
    }

    /// <summary>
    /// Add new parameter with its value.
    /// </summary>
    /// <param name="key">Paramater name.</param>
    /// <param name="value">Parameter value.</param>
    public void AddParameter(string key, string value) => collection.Add(key, value);

    /// <summary>
    /// Get the parameters and values in Http Query String.
    /// </summary>
    /// <returns></returns>
    public string ToQueryString() => "?" + collection;
}
