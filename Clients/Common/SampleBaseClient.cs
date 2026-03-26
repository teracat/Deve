using System.Globalization;
using System.Text;
using Deve.Dto.Responses.Results;
using Deve.Logging;

namespace Deve.Clients;

public abstract class SampleBaseClient
{
    public static void LogCharacters(ILog log, char character, int count)
    {
        if (log is null)
        {
            return;
        }

        log.Debug(new string(character, count));
    }

    public static void LogTitle(ILog log, string title)
    {
        if (log is null)
        {
            return;
        }

        LogCharacters(log, '#', 80);
        log.Debug("# " + title);
        LogCharacters(log, '#', 80);
    }

    public static void LogResult(ILog log, string data)
    {
        if (log is null)
        {
            return;
        }

        LogCharacters(log, '*', 50);
        log.Debug("* Result:");
        LogCharacters(log, '*', 50);
        log.Debug("\n" + data + "\n");
        LogCharacters(log, '*', 50);
    }

    public static void LogError(ILog log, IResult result)
    {
        if (result?.Errors is null)
        {
            return;
        }

        var msg = new StringBuilder("Errors: ");
        foreach (var error in result.Errors)
        {
            _ = msg.AppendLine(CultureInfo.InvariantCulture, $"{error.Type} - {error.Description} [{error.FieldName}]");
        }
        LogResult(log, msg.ToString());
    }

    public abstract Task Execute(CancellationToken cancellationToken);
}
