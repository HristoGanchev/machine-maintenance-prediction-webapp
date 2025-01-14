using MaintenancePrediction.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace MaintenancePrediction.ApiService.MLModels
{
    public class MLModelWrapper
    {

        //private readonly PredictionEngine<MachineStatusInput, MachineStatusOutput> _predictionEngine;

        //public MLModelWrapper(ITransformer mlModel, MLContext mlContext)
        //{
        //    _predictionEngine = mlContext.Model.CreatePredictionEngine<MachineStatusInput, MachineStatusOutput>(mlModel);
        //}

        public PredictionResult Predict(MachineUsage input)
        {
            // Add prediction logic here
            return new PredictionResult() { MaintenanceRequired = true};
        }
    }
}
