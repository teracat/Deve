using Deve.Auth;
using Deve.Auth.Hash;
using Deve.Auth.TokenManagers;
using Deve.Core;
using Deve.Data;
using Deve.DataSource;
using Deve.DataSource.Config;
using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;

namespace Deve.Clients
{
    public static class ClientSampleExecutors
    {
        #region External
        public static void ExternalSdk(DataOptions options)
        {
            using var data = new External.Sdk.SdkMain(EnvironmentType.Staging, options, new LoggingHandlerLog());
            ExternalData(data);
        }

        public static void ExternalData(External.Data.IData data)
        {
            var sample = new ClientSampleExternal(data);
            sample.Execute();
        }
        #endregion

        #region Internal
        public static void InternalSdk(DataOptions options)
        {
            using var data = new Internal.Sdk.SdkMain(EnvironmentType.Staging, options, new LoggingHandlerLog());
            InternalData(data);
        }

        public static void InternalEmbedded(DataOptions options)
        {
            using var hash = new HashSha512();
            using var dataSource = new DataSourceMain(new DataSourceConfig());
            using var tokenManager = new TokenManagerCrypt();
            using var auth = new AuthMain(tokenManager, dataSource, hash, options);
            using var data = new CoreMain(dataSource, auth, options);
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