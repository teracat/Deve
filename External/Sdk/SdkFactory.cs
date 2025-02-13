using Deve.Data;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    public static class SdkFactory
    {
        /// <summary>
        /// Get an instance of the Sdk main class.
        /// </summary>
        /// <param name="environment">Environment to connect to: Production or Development.</param>
        /// <param name="options">Additional options.</param>
        /// <param name="handler">To see the HTTP messages sent and received (you can use the ConsoleLoggingHandler or your own implementation).</param>
        /// <returns>The IData implementation to access the data.</returns>
        public static IData Get(EnvironmentType environment, DataOptions? options, LoggingHandlerBase? handler) => new SdkMain(environment, options, handler);

        /// <summary>
        /// Get an instance of the Sdk main class.
        /// </summary>
        /// <param name="environment">Environment to connect to: Production or Development.</param>
        /// <param name="options">Additional options.</param>
        /// <returns>The IData implementation to access the data.</returns>
        public static IData Get(EnvironmentType environment, DataOptions? options) => Get(environment, options, null);

        /// <summary>
        /// Get an instance of the Sdk main class.
        /// </summary>
        /// <param name="environment">Environment to connect to: Production or Development.</param>
        /// <returns>The IData implementation to access the data.</returns>
        public static IData Get(EnvironmentType environment) => Get(environment, null, null);

        /// <summary>
        /// Get an instance of the Sdk main class using Production as EnvironmentType.
        /// </summary>
        /// <returns>The IData implementation to access the data.</returns>
        public static IData Get() => Get(EnvironmentType.Production, null, null);
    }
}