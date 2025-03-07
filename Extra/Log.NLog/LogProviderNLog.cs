﻿using NLog;

namespace Deve.Logging
{
    /// <summary>
    /// Use NLog as a log provider
    /// https://github.com/nlog/nlog/wiki/Tutorial
    /// </summary>
    internal class LogProviderNLog : ILogProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Debug(string text)
        {
            Logger.Debug(text);
        }

        public void Error(string text)
        {
            Logger.Error(text);
        }
    }

    public static class LogProviderNLogExtension
    {
        private static LogProviderNLog? _instance;

        public static void AddNLog(this LogProviders logProviders)
        {
            if (_instance is null)
            {
                _instance = new LogProviderNLog();
                logProviders.Add(_instance);
            }
        }

        public static void RemoveNLog(this LogProviders logProviders)
        {
            if (_instance is not null)
            {
                logProviders.Remove(_instance);
                _instance = null;
            }
        }
    }
}
