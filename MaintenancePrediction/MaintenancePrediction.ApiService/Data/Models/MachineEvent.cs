using System.Reflection.PortableExecutable;

namespace MaintenancePrediction.ApiService.Data.Models
{
    public class MachineEvent
    {
        public int EventId
        {
            get; set;
        }
        public int MachineId
        {
            get; set;
        }
        public ApiService.Models.MachineData Machine
        {
            get; set;
        } // Navigation property
        public string EventCode
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public DateTime Timestamp
        {
            get; set;
        }
    }
}
