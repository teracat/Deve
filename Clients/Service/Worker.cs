using Deve.Data;
using Deve.Logging;

namespace Deve.Clients;

internal sealed class Worker(ILog log) : BackgroundService
{
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
                SampleBaseClient.LogTitle(log, "Sdk...");
                await SampleExecutorsClient.Sdk(options, log, stoppingToken);

                //////////////////////////////////////////////////////////////////////////////////////////////////////
                // Embedded (uses Core, no other projects must be running)
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                SampleBaseClient.LogTitle(log, "Embedded...");
                await SampleExecutorsClient.Embedded(options, log, stoppingToken);

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
