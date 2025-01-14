using MaintenancePrediction.ApiService.Models;
namespace MaintenancePrediction.ApiService.Services.Interfaces
{
    public interface IMachineDataService
    {
        Task<IEnumerable<MachineData>> GetMachinesAsync();
        //Task<MachineData> GetMachineAsync(int machineId);
        //Task<MachineData> AddMachineAsync(MachineData machine);
        //Task<MachineData> UpdateMachineAsync(MachineData machine);
        //Task DeleteMachineAsync(int machineId);
    }
}
