﻿namespace Deve.Core
{
    /// <summary>
    /// Used to define Shield Config for some method that need some value different from the defaults.
    /// If some method is not configured here, the defaults defined in ShieldItemConfig will be used.
    /// </summary>
    internal static class ShieldConfig
    {
        public static readonly Dictionary<string, ShieldItemConfig> Items = new()
        {
            //CoreAuth.Login Shield Config
            {
                Shield.BuildConfigKey(nameof(CoreAuth), nameof(CoreAuth.Login)),
                new ShieldItemConfig()
                {
                    MaxInvalidAttempts = 3,
                }
            }
        };
    }
}