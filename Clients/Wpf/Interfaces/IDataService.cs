﻿using Deve.Internal.Data;

namespace Deve.Clients.Wpf.Interfaces
{
    public interface IDataService : IDisposable
    {
        IData Data { get; }
    }
}