namespace Deve.Auth.Hash
{
    /// <summary>
    /// Provides hashing functionality for text input.
    /// </summary>
    public interface IHash : IDisposable
    {
        /// <summary>
        /// Computes the hash of the given text.
        /// </summary>
        /// <param name="text">The input text to hash.</param>
        /// <returns>The computed hash as a string.</returns>
        string Calc(string text);
    }
}