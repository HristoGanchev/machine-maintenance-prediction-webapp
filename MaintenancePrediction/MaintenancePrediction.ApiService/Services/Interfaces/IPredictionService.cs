namespace MaintenancePrediction.ApiService.Services.Interfaces
{
    using System.Threading.Tasks;
    using MaintenancePrediction.ApiService.Models;

    public interface IPredictionService
    {
        Task<PredictionResult> PredictMaintenanceAsync(int machineId);
        Task RunScheduledPredictionAsync();
        string GetLastPredictionStatus();
    }

}
