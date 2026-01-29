namespace Deve.Clients.Interfaces;

/// <summary>
/// Marks a type as requiring asynchronous initialization and provides the result of that initialization.
/// Based on https://blog.stephencleary.com/2013/01/async-oop-2-constructors.html
/// </summary>
public interface IAsyncInitialization
{
    /// <summary>
    /// The result of the asynchronous initialization of this instance.
    /// </summary>
    Task Initialization { get; }
}