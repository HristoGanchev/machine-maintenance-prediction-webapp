namespace MaintenancePrediction.ApiService.Services
{
    using MaintenancePrediction.ApiService.Data;
    //Encapsulates the logic to load the ML model and make predictions.

    using MaintenancePrediction.ApiService.Services.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MaintenancePrediction.ApiService.Models;
    using Microsoft.EntityFrameworkCore;

    public class PredictionService : IPredictionService
    {
        private readonly MachineMaintenanceDbContext _context;
        private readonly ILogger<PredictionService> _logger;
        private readonly MLModelWrapper _mlModel; // A wrapper for your ML.NET model
        private string _lastStatus;

        public PredictionService(MachineMaintenanceDbContext context, 
            ILogger<PredictionService> logger, 
            MLModelWrapper mlModel)
        {
            _context = context;
            _logger = logger;
            _mlModel = mlModel;
        }

        public async Task<PredictionResult> PredictMaintenanceAsync(int machineId)
        {
            var machineData = await _context.MachineStatuses
                .Where(m => m.MachineId == machineId)
                .OrderByDescending(m => m.Timestamp)
                .FirstOrDefaultAsync();

            if (machineData == null) return null;

            var prediction = _mlModel.Predict(new MachineStatusInput
            {
                RuntimeHours = machineData.RuntimeHours,
                Cycles = machineData.Cycles,
                Temperature = machineData.Temperature
            });

            return new PredictionResult
            {
                MachineId = machineId,
                MaintenanceRequired = prediction.MaintenanceRequired,
                PredictedDate = prediction.PredictedDate
            };
        }

        public async Task RunScheduledPredictionAsync()
        {
            var allMachines = await _context.Machines.ToListAsync();
            foreach (var machine in allMachines)
            {
                var prediction = await PredictMaintenanceAsync(machine.Id);
                if (prediction != null)
                {
                    _logger.LogInformation("Prediction for Machine {MachineId}: MaintenanceRequired={MaintenanceRequired}, PredictedDate={PredictedDate}",
                        machine.Id, prediction.MaintenanceRequired, prediction.PredictedDate);

                    // Optionally store the prediction in the database
                    _context.PredictionResults.Add(new PredictionResult
                    {
                        MachineId = machine.Id,
                        MaintenanceRequired = prediction.MaintenanceRequired,
                        PredictedDate = prediction.PredictedDate,
                        Timestamp = DateTime.Now
                    });
                }
            }

            await _context.SaveChangesAsync();
            _lastStatus = "Prediction job completed successfully at " + DateTime.Now;
        }

        public string GetLastPredictionStatus()
        {
            return _lastStatus ?? "Prediction job has not run yet.";
        }
    }

}
