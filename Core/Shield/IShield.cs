using System.Runtime.CompilerServices;
using Deve.Data;
using Deve.Dto;

namespace Deve.Core.Shield
{
    public interface IShield
    {
        Task<Result> Protect(IDataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "");

        Task SetAttemptResult(bool succeeded, IDataOptions options, [CallerFilePath] string category = "", [CallerMemberName] string method = "");
    }
}