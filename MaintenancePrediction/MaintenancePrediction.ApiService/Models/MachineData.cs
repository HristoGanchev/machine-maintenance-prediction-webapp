﻿using System.ComponentModel.DataAnnotations;

namespace MaintenancePrediction.ApiService.Models
{
    public class MachineData
    {
        [Key]
        public int MachineId
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
        public string Location
        {
            get; set;
        }

        public double RuntimeThreshold
        {
            get; set;
        }

        public double CycleTimeThreshold
        {
            get; set;
        }

        public int CycleCountThreshold
        {
            get; set;
        }

    }
}
