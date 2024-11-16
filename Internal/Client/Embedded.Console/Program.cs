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
            //Uncomment the following lines and set the ConnectionString for your DataSource, if needed
            /*var config = new DataSource.DataSourceConfig()
            {
                ConnectionString = ""
            };
            DataSource.DataSourceFactory.SetConfig(config);*/

            var clientApp = new InternalClientSampleBasic(CoreFactory.Get());
            clientApp.Execute();

            Console.WriteLine("\nPress a key to finish...");
            Console.ReadKey();
        }
    }
}
