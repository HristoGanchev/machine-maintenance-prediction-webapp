namespace MaintenancePrediction.ApiService.Jobs
{
    // Implements periodic predictions using Quartz.NET or BackgroundService.

    using MaintenancePrediction.ApiService.Services.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class PredictionJob : BackgroundService
    {
        private readonly IServiceProvider _predictionService;
        private readonly ILogger<PredictionJob> _logger;

        public PredictionJob(IServiceProvider predictionService, ILogger<PredictionJob> logger)
        {
            _predictionService = predictionService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _predictionService.CreateScope())
                {
                    try
                    {
                        var predictionService = scope.ServiceProvider.GetRequiredService<IPredictionService>();

                        _logger.LogInformation("Running Prediction Job at: {time}", DateTimeOffset.Now);
                        await predictionService.RunScheduledPredictionAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while running the prediction job.");
                    }
                }

                // Wait for the next run (e.g., every hour)
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }

}
