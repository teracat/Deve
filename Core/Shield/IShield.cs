using System.Runtime.CompilerServices;
using Deve.Data;
using Deve.Model;

namespace Deve.Core.Shield
{
    public interface IShield
    {
        Task<Result> Protect(DataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "");
        Task SetAttemptResult(bool succeeded, DataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "");
    }
}