﻿using Deve.Sdk;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    /// <summary>
    /// Sdk definitions available only for External use.
    /// </summary>
    public interface ISdk : IData, ISdkCommon
    {
    }
}