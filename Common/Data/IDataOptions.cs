namespace Deve.Data;

/// <summary>
/// Options related to the user requesting the data.
/// </summary>
public interface IDataOptions
{
    /// <summary>
    /// Preferred language.
    /// </summary>
    string LangCode { get; }

    /// <summary>
    /// Origin device Id.
    /// </summary>
    string OriginId { get; }
}