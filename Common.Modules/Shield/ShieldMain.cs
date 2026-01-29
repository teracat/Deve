using System.Runtime.CompilerServices;
using Deve.Localize;

namespace Deve.Shield;

internal sealed class ShieldMain : IShield
{
    #region Constants
    /// <summary>
    /// Executes the clean process every X minutes specified here.
    /// </summary>
    private const int CleanEveryMinutes = 5;

    /// <summary>
    /// If the last attempt was made before the minutes specified here, it will be removed from memory.
    /// Take into account to set a value big enough to keep the locks active.
    /// The lock of the Login method lasts 5 minutes, so it can be removed after 15 minutes from the last call.
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

    public static string BuildOriginMethodKey(string originId, string category, string method) => originId + "-" + BuildConfigKey(category, method);
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
        var originsToRemove = new List<string>();
        foreach (var origin in _origins)
        {
            if ((DateTimeOffset.UtcNow - origin.Value.LastAttempt).TotalMinutes >= RemoveAfterMinutesLastAttempt)
            {
                originsToRemove.Add(origin.Key);
            }
        }
        foreach (var originKey in originsToRemove)
        {
            _ = _origins.Remove(originKey);
        }

        var originMethodsToRemove = new List<string>();
        foreach (var originMethod in _originsMethods)
        {
            if ((DateTimeOffset.UtcNow - originMethod.Value.LastAttempt).TotalMinutes >= RemoveAfterMinutesLastAttempt)
            {
                originMethodsToRemove.Add(originMethod.Key);
            }
        }
        foreach (var originMethodKey in originMethodsToRemove)
        {
            _ = _originsMethods.Remove(originMethodKey);
        }
    }
    #endregion

    #region IShield
    public Task<Result> Protect(IDataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "") => Protect(options, new ShieldItemConfig(), category, method);

    public Task<Result> Protect(IDataOptions options, ShieldItemConfig config, [CallerFilePath] string category = "", [CallerMemberName] string method = "")
    {
        return Task.Run(() =>
        {
            if (Utils.SomeIsNullOrWhiteSpace(options.OriginId, category, method))
            {
                return Result.Ok();
            }

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
                    var errorFactory = ErrorLocalizeFactory.Get();
                    string lockedMsg = string.Format(errorFactory.GetCulture(options.LangCode), errorFactory.Localize(ResultErrorType.Locked, options.LangCode), config.LockSeconds - secondsLocked);
                    return Result.Fail(ResultErrorType.Locked, null, lockedMsg);
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
                    var errorFactory = ErrorLocalizeFactory.Get();
                    string lockedMsg = string.Format(errorFactory.GetCulture(options.LangCode), errorFactory.Localize(ResultErrorType.Locked, options.LangCode), config.LockSeconds - secondsLocked);
                    return Result.Fail(ResultErrorType.Locked, null, lockedMsg);
                }
            }

            return Result.Ok();
        });
    }

    public Task SetAttemptResult(bool succeeded, IDataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "") => SetAttemptResult(succeeded, options, new ShieldItemConfig(), category, method);

    public Task SetAttemptResult(bool succeeded, IDataOptions options, ShieldItemConfig config, [CallerFilePath] string category = "", [CallerMemberName] string method = "")
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
                methodData.NumAttemptsInvalid++;
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

    #region IDisposable
    public void Dispose() => _timer.Dispose();
    #endregion
}
