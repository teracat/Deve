using System.Runtime.CompilerServices;
using Deve.Data;
using Deve.Localize;
using Deve.Model;

namespace Deve.Core.Shield
{
    internal class ShieldMain : IShield
    {
        #region Constants
        /// <summary>
        /// Executes the clean process every X minutes specified here.
        /// </summary>
        private const int CleanEveryMinutes = 5;

        /// <summary>
        /// If the last attempt was made before the minutes specified here, it will be removed from memory.
        /// Take into account to set a value big enough to keep the locks active.
        /// The lock of the Login method lasts 5 minutes, so it can be removed after 15 minuts from the last call.
        /// </summary>
        private const int RemoveAfterMinutesLastAttempt = 15;
        #endregion

        #region Fields
        private readonly Dictionary<string, ShieldOriginData> _origins = [];  //Key: originId
        private readonly Dictionary<string, ShieldOriginMethodData> _originsMethods = [];  //Key: originId-methodKey
        private readonly Timer _timer;  //Used to remove old data in _origins and _originsMethods
        #endregion

        #region Static Methods
        public static string BuildConfigKey(string category, string method)
        {
            //If we receive the CallerFilePath, get the Filename to have the class name
            if (Path.IsPathFullyQualified(category))
            {
                category = Path.GetFileNameWithoutExtension(category);
            }

            return category + "-" + method;
        }

        public static string BuildOriginMethodKey(string originId, string category, string method)
        {
            return originId + "-" + BuildConfigKey(category, method);
        }

        private static ShieldItemConfig GetItemConfig(string category, string method)
        {
            string configKey = BuildConfigKey(category, method);

            ShieldItemConfig config;
            if (ShieldConfig.Items.TryGetValue(configKey, out ShieldItemConfig? notDefaultConfig))
            {
                config = notDefaultConfig;
            }
            else
            {
                config = new ShieldItemConfig();
            }

            return config;
        }
        #endregion

        #region Constructor
        public ShieldMain()
        {
            _timer = new Timer(new TimerCallback(CleanOldData), null, TimeSpan.FromMinutes(CleanEveryMinutes), TimeSpan.FromMinutes(CleanEveryMinutes));
        }
        #endregion

        #region Methods
        private ShieldOriginData GetOriginData(string originId)
        {
            ShieldOriginData originata;
            if (_origins.TryGetValue(originId, out ShieldOriginData? existingOrigin))
            {
                originata = existingOrigin;
            }
            else
            {
                originata = new ShieldOriginData();
                _origins.Add(originId, originata);
            }
            return originata;
        }

        private ShieldOriginMethodData GetOriginMethodData(string originId, string category, string method)
        {
            string itemKey = BuildOriginMethodKey(originId, category, method);

            ShieldOriginMethodData methodData;
            if (_originsMethods.TryGetValue(itemKey, out ShieldOriginMethodData? existingItem))
            {
                methodData = existingItem;
            }
            else
            {
                methodData = new ShieldOriginMethodData();
                _originsMethods.Add(itemKey, methodData);
            }
            return methodData;
        }

        private void CleanOldData(object? state)
        {
            foreach (var origin in _origins)
            {
                if ((DateTimeOffset.UtcNow - origin.Value.LastAttempt).TotalMinutes >= RemoveAfterMinutesLastAttempt)
                {
                    _origins.Remove(origin.Key);
                }
            }
            foreach (var originMethod in _originsMethods)
            {
                if ((DateTimeOffset.UtcNow - originMethod.Value.LastAttempt).TotalMinutes >= RemoveAfterMinutesLastAttempt)
                {
                    _originsMethods.Remove(originMethod.Key);
                }
            }
        }
        #endregion

        #region IShield
        public Task<Result> Protect(DataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "")
        {
            return Task.Run(() =>
            {
                if (Utils.SomeIsNullOrWhiteSpace(options.OriginId, category, method))
                {
                    return Utils.ResultOk();
                }

                var config = GetItemConfig(category, method);
                var originData = GetOriginData(options.OriginId);
                var methodData = GetOriginMethodData(options.OriginId, category, method);

                //Check if origin is locked
                if (originData.Status == ShieldLockStatus.Locked)
                {
                    //Check if it should be unlocked
                    var secondsLocked = (int)(DateTimeOffset.UtcNow - originData.LastAttempt).TotalSeconds;
                    if (secondsLocked > config.LockSeconds)
                    {
                        originData.Status = ShieldLockStatus.Unlocked;
                    }
                    else
                    {
                        string lockedMsg = string.Format(ErrorLocalizeFactory.Get().Localize(ResultErrorType.Locked, options.LangCode), config.LockSeconds - secondsLocked);
                        return Utils.ResultError(ResultErrorType.Locked, null, lockedMsg);
                    }
                }

                //Check if the method is locked
                if (methodData.Status == ShieldLockStatus.Locked)
                {
                    //Check if it should be unlocked
                    var secondsLocked = (int)(DateTimeOffset.UtcNow - methodData.LastAttempt).TotalSeconds;
                    if (secondsLocked > config.LockSeconds)
                    {
                        methodData.Status = ShieldLockStatus.Unlocked;
                    }
                    else
                    {
                        string lockedMsg = string.Format(ErrorLocalizeFactory.Get().Localize(ResultErrorType.Locked, options.LangCode), config.LockSeconds - secondsLocked);
                        return Utils.ResultError(ResultErrorType.Locked, null, lockedMsg);
                    }
                }

                return Utils.ResultOk();
            });
        }

        public Task SetAttemptResult(bool succeeded, DataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "")
        {
            return Task.Run(() =>
            {
                if (Utils.SomeIsNullOrWhiteSpace(options.OriginId, category, method))
                {
                    return;
                }

                var originData = GetOriginData(options.OriginId);
                var methodData = GetOriginMethodData(options.OriginId, category, method);
                originData.LastAttempt = methodData.LastAttempt = DateTimeOffset.UtcNow;

                if (succeeded)
                {
                    methodData.NumAttemptsInvalid = 0;
                }
                else
                {
                    methodData.NumAttemptsInvalid += 1;

                    var config = GetItemConfig(category, method);
                    if (methodData.NumAttemptsInvalid >= config.MaxInvalidAttempts)
                    {
                        methodData.NumAttemptsInvalid = 0;
                        if (config.MaxAttemptsLockType == ShieldMaxAttemptsLockType.OnlyMethod)
                        {
                            methodData.Status = ShieldLockStatus.Locked;
                        }
                        else
                        {
                            originData.Status = ShieldLockStatus.Locked;
                        }
                    }
                }
            });
        }
        #endregion
    }
}