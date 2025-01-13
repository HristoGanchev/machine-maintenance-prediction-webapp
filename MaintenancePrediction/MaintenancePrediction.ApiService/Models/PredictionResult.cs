﻿namespace MaintenancePrediction.ApiService.Models
{
    public class PredictionResult
    {
        public int Id
        {
            get; set;
        }
        public int MachineId
        {
            get; set;
        }
        public bool MaintenanceRequired
        {
            get; set;
        }
        public DateTime PredictedDate
        {
            get; set;
        }
        public DateTime Timestamp
        {
            get; set;
        }
    }
}