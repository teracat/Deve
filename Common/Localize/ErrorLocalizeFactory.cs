namespace Deve.Localize;

/// <summary>
/// Factory class for providing an instance of <see cref="IErrorLocalize"/>.
/// </summary>
public static class ErrorLocalizeFactory
{
    /// <summary>
    /// Holds the singleton instance of the error localization service.
    /// </summary>
    private static IErrorLocalize? _localize;

    /// <summary>
    /// Gets an instance of <see cref="IErrorLocalize"/>.
    /// </summary>
    /// <returns>
    /// A singleton instance of <see cref="IErrorLocalize"/>, creating a new one if none exists.
    /// </returns>
    public static IErrorLocalize Get() => _localize ??= new ErrorLocalize();
}
