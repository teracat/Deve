using System.Globalization;
using System.Text;
using Deve.Logging;
using Deve.Dto.Responses.Results;

namespace Deve.Clients;

public abstract class SampleBaseClient
{
    public static void LogCharacters(char character, int count) => Log.Debug(new string(character, count));

    public static void LogTitle(string title)
    {
        LogCharacters('#', 80);
        Log.Debug("# " + title);
        LogCharacters('#', 80);
    }

    public static void LogResult(string data)
    {
        LogCharacters('*', 50);
        Log.Debug("* Result:");
        LogCharacters('*', 50);
        Log.Debug("\n" + data + "\n");
        LogCharacters('*', 50);
    }

    public static void LogError(IResult result)
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
        LogResult(msg.ToString());
    }

    public abstract Task Execute();
}
