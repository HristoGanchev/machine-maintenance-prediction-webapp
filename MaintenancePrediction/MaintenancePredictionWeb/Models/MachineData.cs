namespace MaintenancePrediction.Web.Models
{
    public class MachineData
    {
        public string MachineName
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

    }
}
