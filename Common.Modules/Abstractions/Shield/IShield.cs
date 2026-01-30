using System.Runtime.CompilerServices;

namespace Deve.Shield;

public interface IShield : IDisposable
{
    Task<Result> Protect(IDataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "");

    Task<Result> Protect(IDataOptions options, ShieldItemConfig config, [CallerFilePath] string category = "", [CallerMemberName] string method = "");

    Task SetAttemptResult(bool succeeded, IDataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "");

    Task SetAttemptResult(bool succeeded, IDataOptions options, ShieldItemConfig config, [CallerFilePath] string category = "", [CallerMemberName] string method = "");
}
