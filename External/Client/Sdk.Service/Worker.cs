namespace Deve.External.ClientApp
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
                    var clientSample = new SdkClientSample();
                    clientSample.Execute();

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
