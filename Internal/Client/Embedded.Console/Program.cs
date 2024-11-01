using Deve.Core;

namespace Deve.Internal.ClientApp
{
    /// <summary>
    /// Internal App using the InternalClientSampleBasic implementation and the Core as the IData.
    /// The Deve.Core is referenced.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientApp = new InternalClientSampleBasic(CoreFactory.Get());
            clientApp.Execute();

            Console.WriteLine("\nPress a key to finish...");
            Console.ReadKey();
        }
    }
}
