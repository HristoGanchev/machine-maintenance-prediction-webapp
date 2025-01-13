using MaintenancePrediction.ApiService.Models;

namespace MaintenancePrediction.ApiService.Services.Interfaces
{
    public interface IMachineUsageService
    {
        Task UpdateUsageAsync(int machineId, float additionalHours, int additionalCycles);
        Task<MachineUsage> GetUsageAsync(int machineId);
    }
}
