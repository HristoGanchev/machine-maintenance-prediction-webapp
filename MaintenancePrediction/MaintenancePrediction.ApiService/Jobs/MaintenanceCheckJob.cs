using MaintenancePrediction.ApiService.Services.Interfaces;

namespace MaintenancePrediction.ApiService.Jobs
{
    // Use for gathering machine events, usage and maintenance check.
    public class MaintenanceCheckJob: BackgroundService
    {
        private readonly IServiceProvider _maintenanceCheckService;
        private readonly ILogger<PredictionJob> _logger;

        public MaintenanceCheckJob(IServiceProvider maintenanceCheckService, ILogger<PredictionJob> logger)
        {
            _maintenanceCheckService = maintenanceCheckService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _maintenanceCheckService.CreateScope())
                {
                    try
                    {
                        var maintenanceCheckService = scope.ServiceProvider.GetRequiredService<IMachineMaintenanceCheckResultService>();

                        _logger.LogInformation("Running Maintenance Check Job at: {time}", DateTimeOffset.Now);
                        await maintenanceCheckService.RunMachinesMaintenanceCheckResultMixinAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while running the Maintenance check job.");
                    }
                }

                // Wait for the next run (e.g., every hour)
                //await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Debug
            }
        }
    }
}
