﻿using Deve.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkClientAll : SdkBaseAll<Client, Client, CriteriaClient>, IDataClient
    {
        #region Constructor
        public SdkClientAll(ISdk sdk)
            : base(ApiConstants.PathClient, sdk)
        {
        }
        #endregion

        #region IDataClient
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return await Put(Path + ApiConstants.MethodUpdateStatus + $"/{id}/{(int)newStatus}", RequestAuthType.Default, null);
        }
        #endregion
    }
}