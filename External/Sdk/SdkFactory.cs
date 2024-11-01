using Deve.Sdk;

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
        public static IData Get(EnvironmentType environment = EnvironmentType.Production, DataOptions? options = null, LoggingHandlerBase? handler = null) => new SdkMain(environment, options, handler);
    }
}