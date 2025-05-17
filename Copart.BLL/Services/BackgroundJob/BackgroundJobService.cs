using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Copart.BLL.Services.BackgroundJob
{

    public class BackgroundJobService : BackgroundService
    {
        private readonly ILogger<BackgroundJobService> _logger;
        private readonly IBackgroundJobQueue _jobQueue;

        public BackgroundJobService(ILogger<BackgroundJobService> logger, IBackgroundJobQueue jobQueue)
        {
            _logger = logger;
            _jobQueue = jobQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BJ Started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var job = await _jobQueue.DequeueAsync(stoppingToken);
                    await job(stoppingToken);
                    _logger.LogInformation("BJ Completed");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error from BJ");
                }
            }
        }
    }
}
