namespace Deve.Data;

/// <summary>
/// Options related to the user requesting the data.
/// </summary>
public class DataOptions : IDataOptions
{
    /// <summary>
    /// Preferred language.
    /// </summary>
    public string LangCode { get; set; } = Constants.DefaultLangCode;

    /// <summary>
    /// Origin device Id.
    /// </summary>
    public string OriginId { get; set; } = string.Empty;
}
