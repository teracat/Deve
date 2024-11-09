using System.Runtime.CompilerServices;
using Deve.Localize;

namespace Deve.Core.Shield
{
    internal class ShieldMain : IShield
    {
        #region Static Fields
        private static readonly Dictionary<string, ShieldOriginData> _origins = [];  //Key: originId
        private static readonly Dictionary<string, ShieldOriginMethodData> _originsMethods = [];  //Key: originId-methodKey
        #endregion

        #region Static Methods
        public static string BuildConfigKey(string category, string method)
        {
            //If we receive the CallerFilePath, get the Filename to have the class name
            if (Path.IsPathFullyQualified(category))
                category = Path.GetFileNameWithoutExtension(category);

            return category + "-" + method;
        }

        public static string BuildItemKey(string originId, string category, string method)
        {
            return BuildConfigKey(category, method) + "-" + originId;
        }

        private static ShieldItemConfig GetItemConfig(string category, string method)
        {
            string configKey = BuildConfigKey(category, method);

            ShieldItemConfig config;
            if (ShieldConfig.Items.TryGetValue(configKey, out ShieldItemConfig? notDefaultConfig))
                config = notDefaultConfig;
            else
                config = new ShieldItemConfig();
            return config;
        }

        private static ShieldOriginData GetOriginData(string originId)
        {
            ShieldOriginData originata;
            if (_origins.TryGetValue(originId, out ShieldOriginData? existingOrigin))
                originata = existingOrigin;
            else
            {
                originata = new ShieldOriginData();
                _origins.Add(originId, originata);
            }
            return originata;
        }

        private static ShieldOriginMethodData GetOriginMethodData(string originId, string category, string method)
        {
            string itemKey = BuildItemKey(originId, category, method);

            ShieldOriginMethodData methodData;
            if (_originsMethods.TryGetValue(originId, out ShieldOriginMethodData? existingItem))
                methodData = existingItem;
            else
            {
                methodData = new ShieldOriginMethodData();
                _originsMethods.Add(originId, methodData);
            }
            return methodData;
        }
        #endregion

        #region Constructor
        public ShieldMain()
        {
        }
        #endregion

        #region IShield
        public Task<Result> Protect(DataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "")
        {
            return Task.Run(() =>
            {
                if (Utils.SomeIsNullOrWhiteSpace(options.OriginId, category, method))
                    return Utils.ResultOk();

                var config = GetItemConfig(category, method);
                var originData = GetOriginData(options.OriginId);
                var methodData = GetOriginMethodData(options.OriginId, category, method);

                //Check if origin is locked
                if (originData.Status == ShieldLockStatus.Locked)
                {
                    //Check if it should be unlocked
                    var secondsLocked = (int)(DateTimeOffset.UtcNow - originData.LastAttempt).TotalSeconds;
                    if (secondsLocked > config.LockSeconds)
                        originData.Status = ShieldLockStatus.Unlocked;
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
                        methodData.Status = ShieldLockStatus.Unlocked;
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
                    return;

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
                        switch (config.MaxAttemptsLockType)
                        {
                            case ShieldMaxAttemptsLockType.Origin:
                                if (originData.Status != ShieldLockStatus.Locked)
                                {
                                    originData.Status = ShieldLockStatus.Locked;
                                }
                                break;
                            case ShieldMaxAttemptsLockType.OnlyMethod:
                                if (methodData.Status != ShieldLockStatus.Locked)
                                {
                                    methodData.Status = ShieldLockStatus.Locked;
                                }
                                break;
                        }
                    }
                }
            });
        }
        #endregion
    }
}