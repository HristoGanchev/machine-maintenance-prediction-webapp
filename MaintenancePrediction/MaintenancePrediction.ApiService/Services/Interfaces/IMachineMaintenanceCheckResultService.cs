using MaintenancePrediction.ApiService.Models;

namespace MaintenancePrediction.ApiService.Services.Interfaces
{
    public interface IMachineMaintenanceCheckResultService
    {
        Task<MachineMaintenanceCheckResult> GetMachineMaintenanceCheckResultAsync(int machineId);
        //Task<MachineMaintenanceCheckResult> GetMachineMaintenanceCheckResultAsync(int id);
        //Task<MachineMaintenanceCheckResult> CreateMachineMaintenanceCheckResultAsync(MachineMaintenanceCheckResult machineMaintenanceCheckResult);
        //Task<MachineMaintenanceCheckResult> UpdateMachineMaintenanceCheckResultAsync(int id, MachineMaintenanceCheckResult machineMaintenanceCheckResult);
        //Task DeleteMachineMaintenanceCheckResultAsync(int id);

        Task RunMachinesMaintenanceCheckResultMixinAsync();
    }
}
