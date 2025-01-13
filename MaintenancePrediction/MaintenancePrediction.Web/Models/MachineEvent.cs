
namespace MaintenancePrediction.Web.Models
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

        public MachineData Machine
        {
            get; set;
        } // Navigation property
    }
}
