using Deve.Auth.Crypt;
using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients
{
    public static class ClientSampleExecutors
    {
        #region External
        public static void ExternalSdk(DataOptions? options)
        {
            using var data = External.Sdk.SdkFactory.Get(EnvironmentType.Staging, options, new LoggingHandlerLog());
            ExternalData(data);
        }

        public static void ExternalData(External.Data.IData data)
        {
            var sample = new ClientSampleExternal(data);
            sample.Execute();
        }
        #endregion

        #region Internal
        public static void InternalSdk(DataOptions? options)
        {
            using var data = Internal.Sdk.SdkFactory.Get(EnvironmentType.Staging, options, new LoggingHandlerLog());
            InternalData(data);
        }

        public static void InternalEmbedded(DataOptions? options)
        {
            using var tokenManager = new TokenManagerCrypt();
            using var data = Core.CoreFactory.Get(true, tokenManager, options);
            InternalData(data);
        }

        public static void InternalData(Internal.Data.IData data)
        {
            var sample = new ClientSampleInternal(data);
            sample.Execute();
        }
        #endregion
    }
}