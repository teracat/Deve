using Deve.Logging;
using Deve.Model;

namespace Deve.Clients
{
    public abstract class ClientSampleBase
    {
        public static void LogCharacters(char character, int count)
        {
            Log.Debug(new string(character, count));
        }

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

        public static void LogError(Result result)
        {
            var msg = "Errors: ";
            foreach (var error in result.Errors)
                msg += $"{error.Type} - {error.Description} [{error.FieldName}]\n";
            LogResult(msg);
        }

        public abstract void Execute();
    }
}