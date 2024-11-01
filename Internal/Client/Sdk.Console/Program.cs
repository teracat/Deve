using Deve.Sdk;
using Deve.Internal.Sdk;

namespace Deve.Internal.ClientApp
{
    /// <summary>
    /// Internal App using the InternalClientSampleBasic implementation and the Sdk as the IData.
    /// The Deve.Internal.Sdk is referenced.
    /// This sample needs the Deve.Internal.Api to be running to work.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientApp = new InternalClientSampleBasic(SdkFactory.Get(Deve.Sdk.EnvironmentType.Staging, null, new LoggingHandlerLog()));
            clientApp.Execute();

            Console.WriteLine("\nPress a key to finish...");
            Console.ReadKey();
        }
    }
}
