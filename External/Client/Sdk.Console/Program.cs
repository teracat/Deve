namespace Deve.External.ClientApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Providers.AddConsole();
#if DEBUG
            Log.Providers.AddDebug();
#endif

            var clientSample = new SdkClientSample();
            clientSample.Execute();

            Console.WriteLine("\nPress a key to finish...");
            Console.ReadKey();
        }
    }
}
