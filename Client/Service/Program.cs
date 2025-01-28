namespace Deve.ClientApp
{
    /// <summary>
    /// Main Sdk Client Service program.
    /// https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Providers.AddLog4net();
            //Log.Providers.AddNLog();
//-:cnd
#if DEBUG
            Log.Providers.AddDebug();
#endif
//+:cnd

            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddWindowsService(options =>
            {
                options.ServiceName = "Deve Client";
            });
            //builder.Logging.AddDebug();
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}