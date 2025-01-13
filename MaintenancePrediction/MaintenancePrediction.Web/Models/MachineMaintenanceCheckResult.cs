namespace MaintenancePrediction.Web.Models
{
    public class MachineMaintenanceCheckResult
    {
        public int Id
        {
            get;
            set;
        }
        public int MachineId
        {
            get;
            set;
        }

        public double RuntimeHours
        {
            get;
            set;
        }

        public double RuntimeThreshold
        {
            get;
            set;
        }

        public int CycleCount
        {
            get;
            set;
        }

        public int CycleThreshold
        {
            get;
            set;
        }

        public bool RequiresMaintenance
        {
            get;
            set;
        }

        public string Reason
        {
            get;
            set;
        }
    }
}
