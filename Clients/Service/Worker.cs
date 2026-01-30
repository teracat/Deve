using Deve.Data;
using Deve.Logging;

namespace Deve.Clients;

internal sealed class Worker : BackgroundService
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
                // Sdk (Api must be running)
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                SampleBaseClient.LogTitle("Sdk...");
                await SampleExecutorsClient.Sdk(options);

                //////////////////////////////////////////////////////////////////////////////////////////////////////
                // Embedded (uses Core, no other projects must be running)
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                SampleBaseClient.LogTitle("Embedded...");
                await SampleExecutorsClient.Embedded(options);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}
