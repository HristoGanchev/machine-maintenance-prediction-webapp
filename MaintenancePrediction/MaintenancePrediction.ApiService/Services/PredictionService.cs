namespace MaintenancePrediction.ApiService.Services
{
    using MaintenancePrediction.ApiService.Data;
    using MaintenancePrediction.ApiService.Services.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MaintenancePrediction.ApiService.Models;
    using MaintenancePrediction.ApiService.MLModels;
    using Microsoft.EntityFrameworkCore;

    //Encapsulates the logic to load the ML model and make predictions.

    public class PredictionService : IPredictionService
    {
        private readonly MachineMaintenanceDbContext _context;
        private readonly ILogger<PredictionService> _logger;
        private readonly MLModelWrapper _mlModel; // A wrapper for your ML.NET model
        private string _lastStatus { get; set; }

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
            var machineData = await _context.MachineUsages
                .Where(m => m.MachineId == machineId)
                .OrderByDescending(m => m.LastUpdated)
                .FirstOrDefaultAsync();

            if (machineData == null) return null;

            var prediction = _mlModel.Predict(new MachineUsage
            {
                RuntimeHours = machineData.RuntimeHours,
                CycleCount = machineData.CycleCount,
                CycleTime = machineData.CycleTime
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
                var prediction = await PredictMaintenanceAsync(machine.MachineId);
                if (prediction != null)
                {
                    _logger.LogInformation("Prediction for Machine {MachineId}: MaintenanceRequired={MaintenanceRequired}, PredictedDate={PredictedDate}",
                        machine.MachineId, prediction.MaintenanceRequired, prediction.PredictedDate);

                    // Optionally store the prediction in the database
                    //_context.PredictionResults.Add(new PredictionResult
                    //{
                    //    MachineId = machine.MachineId,
                    //    MaintenanceRequired = prediction.MaintenanceRequired,
                    //    PredictedDate = prediction.PredictedDate,
                    //    Timestamp = DateTime.Now
                    //});
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
