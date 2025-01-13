using System.Reflection.PortableExecutable;

namespace MaintenancePrediction.ApiService.Data.Models
{
    public class MachineUsage
    {
        public int MachineId
        {
            get; set;
        }
        public ApiService.Models.MachineData Machine
        {
            get; set;
        } // Navigation property

        public float RuntimeHours
        {
            get; set;
        }
        public int CycleCount
        {
            get; set;
        }
        public DateTime LastUpdated
        {
            get; set;
        }
    }
}
