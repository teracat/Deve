using System.Runtime.CompilerServices;

namespace Deve.Core
{
    internal class Shield : IShield
    {
        private static readonly Dictionary<string, ShieldOrigin> _origins = [];  //Key: originId
        private static readonly Dictionary<string, ShieldItem> _items = [];  //Key: originId-methodKey

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

        public Shield()
        {
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

        private static ShieldOrigin GetOrigin(string originId)
        {
            ShieldOrigin origin;
            if (_origins.TryGetValue(originId, out ShieldOrigin? existingOrigin))
                origin = existingOrigin;
            else
            {
                origin = new ShieldOrigin();
                _origins.Add(originId, origin);
            }
            return origin;
        }

        private static ShieldItem GetItem(string originId, string category, string method)
        {
            string itemKey = BuildItemKey(originId, category, method);

            ShieldItem item;
            if (_items.TryGetValue(originId, out ShieldItem? existingItem))
                item = existingItem;
            else
            {
                item = new ShieldItem();
                _items.Add(originId, item);
            }
            return item;
        }

        public Task<Result> Protect(DataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "")
        {
            return Task.Run(() =>
            {
                if (Utils.SomeIsNullOrWhiteSpace(options.OriginId, category, method))
                    return Utils.ResultOk();

                var config = GetItemConfig(category, method);
                var origin = GetOrigin(options.OriginId);
                var item = GetItem(options.OriginId, category, method);

                //Check if origin is locked
                if (origin.Status == ShieldLockStatus.Locked)
                {
                    //Check if the lock should be unlocked
                    if ((DateTimeOffset.UtcNow - origin.LastAttempt).TotalSeconds > config.LockSeconds)
                        origin.Status = ShieldLockStatus.Unlocked;
                    else
                        return Utils.ResultError(options.LangCode, ResultErrorType.TooManyAttempts);
                }

                //check if the method is locked
                if (item.Status == ShieldLockStatus.Locked)
                {
                    //Check if the lock should be unlocked
                    if ((DateTimeOffset.UtcNow - item.LastAttempt).TotalSeconds > config.LockSeconds)
                        item.Status = ShieldLockStatus.Unlocked;
                    else
                        return Utils.ResultError(options.LangCode, ResultErrorType.TooManyAttempts);
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

                var item = GetItem(options.OriginId, category, method);
                item.LastAttempt = DateTimeOffset.UtcNow;
                if (succeeded)
                    item.NumAttemptsInvalid = 0;
                else
                {
                    item.NumAttemptsInvalid += 1;

                    var origin = GetOrigin(options.OriginId);
                    var config = GetItemConfig(category, method);
                    if (item.NumAttemptsInvalid >= config.MaxInvalidAttempts)
                    {
                        item.NumAttemptsInvalid = 0;
                        switch (config.MaxAttemptsLockType)
                        {
                            case ShieldMaxAttemptsLockType.Origin:
                                if (origin.Status != ShieldLockStatus.Locked)
                                {
                                    origin.Status = ShieldLockStatus.Locked;
                                    origin.LastAttempt = DateTime.UtcNow;
                                }
                                break;
                            case ShieldMaxAttemptsLockType.OnlyMethod:
                                if (item.Status != ShieldLockStatus.Locked)
                                    item.Status = ShieldLockStatus.Locked;
                                break;
                        }
                    }
                }
            });
        }
    }
}
