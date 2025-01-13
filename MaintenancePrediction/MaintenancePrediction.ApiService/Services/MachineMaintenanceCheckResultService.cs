using MaintenancePrediction.ApiService.Data;
using Microsoft.EntityFrameworkCore;
using MaintenancePrediction.ApiService.Models;
using MaintenancePrediction.ApiService.Services.Interfaces;

namespace MaintenancePrediction.ApiService.Services
{
    public class MachineMaintenanceCheckResultService : IMachineMaintenanceCheckResultService
    {
        private readonly MachineMaintenanceDbContext _context;

        public MachineMaintenanceCheckResultService(MachineMaintenanceDbContext context)
        {
            _context = context;
        }

        public async Task<MachineMaintenanceCheckResult> GetMachineMaintenanceCheckResultAsync(int machineId)
        {
            var responce = await _context.MachineMaintenanceChecks
                .FirstOrDefaultAsync(e => e.MachineId == machineId);

            if (responce == null)
            {
                return new MachineMaintenanceCheckResult();
            }
            return responce;
        }
    }
}
