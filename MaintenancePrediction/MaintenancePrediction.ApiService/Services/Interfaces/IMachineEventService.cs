using MaintenancePrediction.ApiService.Models;

namespace MaintenancePrediction.ApiService.Services.Interfaces
{
    public interface IMachineEventService
    {
        Task LogEventAsync(int machineId, string eventCode, string description);
        Task<IEnumerable<MachineEvent>> GetEventsAsync(int machineId);
    }
}
