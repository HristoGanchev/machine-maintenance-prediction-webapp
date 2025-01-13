using System.Reflection.PortableExecutable;

namespace MaintenancePrediction.Web.Models
{
    public class MachineUsage
    {
        public int Id
        {
            get; set;
        }

        public int MachineId
        {
            get; set;
        }

        public float RuntimeHours
        {
            get; set;
        }

        public int CycleTime
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
        public MachineData Machine
        {
            get; set;
        } // Navigation property
    }
}
