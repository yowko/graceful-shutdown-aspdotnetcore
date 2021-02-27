using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GracefulShutdown
{
    public class SlowHostedService: IHostedService
    {
        private readonly ILogger<SlowHostedService> _logger;

        public SlowHostedService(ILogger<SlowHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("SlowHostedService started");
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SlowHostedService stopping..."); 
            await Task.Delay(10_000, cancellationToken);
            _logger.LogInformation("SlowHostedService stopped");
        }
    }
}