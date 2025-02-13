using Deve.Data;
using Deve.Logging;

namespace Deve.Clients
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            Log.Providers.AddConsole();
            //Log.Providers.AddLog4net();
            //Log.Providers.AddNLog();

//-:cnd
#if DEBUG
            Log.Providers.AddDebug();
#endif
//+:cnd

            var options = new DataOptions()
            {
                LangCode = Constants.LanguageCodeSpanish
            };

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // External Sdk (External Api must be running)
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ClientSampleBase.LogTitle("External Sdk...");

            ClientSampleExecutors.ExternalSdk(options);

            Console.WriteLine("External Sdk finished. Press a key to continue...");
            Console.ReadKey();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // Internal Sdk (Internal Api must be running)
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ClientSampleBase.LogTitle("Internal Sdk...");

            ClientSampleExecutors.InternalSdk(options);

            Console.WriteLine("Internal Sdk finished. Press a key to continue...");
            Console.ReadKey();


            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // Embedded (uses Core, no other projects must be running)
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            ClientSampleBase.LogTitle("Embedded...");

            ClientSampleExecutors.InternalEmbedded(options);

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            // Finish
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Press a key to finish...");
            Console.ReadKey();
        }
    }
}