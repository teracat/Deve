﻿using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Auth
{
    public static class AuthFactory
    {
        public static IAuth Get(IDataSource dataSource, DataOptions? options = null, ITokenManager? tokenManager = null) => new AuthMain(dataSource, options, tokenManager);
    }
}
