namespace MaintenancePrediction.ApiService.Services
{
    //Encapsulates the logic to load the ML model and make predictions.
    public class PredictionService
    {

        public bool CheckForMaintenance(float runtimeHours, int cycleCount, float runtimeThreshold, int cycleThreshold)
        {
            return runtimeHours >= runtimeThreshold || cycleCount >= cycleThreshold;
        }
    }
}
