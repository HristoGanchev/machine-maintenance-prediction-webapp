using MaintenancePrediction.Web.Models;

namespace MaintenancePrediction.Web.Services
{
    using System.Net.Http.Json;

    public class MachineStatusService
    {
        private readonly HttpClient _httpClient;

        public MachineStatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MachineData>?> GetMachinesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MachineData>>("api/machine-status/machines");
        }

        public async Task<List<MachineEvent>?> GetMachineEventsAsync(int machineId)
        {
            return await _httpClient.GetFromJsonAsync<List<MachineEvent>>($"api/machine-status/events/{machineId}");
        }

        public async Task<MachineUsage?> GetMachineUsageAsync(int machineId)
        {
            return await _httpClient.GetFromJsonAsync<MachineUsage>($"api/machine-status/usage/{machineId}");
        }

        public async Task<MachineMaintenanceCheckResult?> GetMaintenanceCheckAsync(int machineId)
        {
            return await _httpClient.GetFromJsonAsync<MachineMaintenanceCheckResult>($"api/machine-status/maintenance-check/{machineId}");
        }
    }

}
