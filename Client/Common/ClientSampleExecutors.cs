﻿using Deve.Sdk;

namespace Deve.ClientApp
{
    public static class ClientSampleExecutors
    {
        #region External
        public static void ExternalSdk(DataOptions? options = null)
        {
            var data = External.Sdk.SdkFactory.Get(EnvironmentType.Staging, options, new LoggingHandlerLog());
            ExternalData(data);
        }

        public static void ExternalData(External.IData data)
        {
            var sample = new ClientSampleExternal(data);
            sample.Execute();
        }
        #endregion

        #region Internal
        public static void InternalSdk(DataOptions? options = null)
        {
            var data = Internal.Sdk.SdkFactory.Get(EnvironmentType.Staging, options, new LoggingHandlerLog());
            InternalData(data);
        }

        public static void InternalEmbedded(DataOptions? options = null)
        {
            var data = Core.CoreFactory.Get(true, null, options);
            InternalData(data);
        }

        public static void InternalData(Internal.IData data)
        {
            var sample = new ClientSampleInternal(data);
            sample.Execute();
        }
        #endregion
    }
}
