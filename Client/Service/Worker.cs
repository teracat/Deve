namespace Deve.ClientApp
{
    public class Worker : BackgroundService
    {
        public Worker(ILogger<Worker> logger)
        {
            Log.Providers.AddNetCore(logger);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var options = new DataOptions()
                    {
                        LangCode = Constants.LanguageCodeSpanish
                    };

                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // External Sdk (External Api must be running)
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    ClientSampleBase.LogTitle("External Sdk...");
                    ClientSampleExecutors.ExternalSdk(options);

                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Internal Sdk (Internal Api must be running)
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    ClientSampleBase.LogTitle("Internal Sdk...");
                    ClientSampleExecutors.InternalSdk(options);

                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Embedded (uses Core, no other projects must be running)
                    //////////////////////////////////////////////////////////////////////////////////////////////////////
                    ClientSampleBase.LogTitle("Embedded...");
                    ClientSampleExecutors.InternalEmbedded(options);

                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
        }
    }
}
