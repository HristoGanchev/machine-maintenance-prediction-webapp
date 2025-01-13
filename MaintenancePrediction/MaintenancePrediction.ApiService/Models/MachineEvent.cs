using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MaintenancePrediction.ApiService.Models
{
    public class MachineEvent
    {
        [Key]
        public int EventId
        {
            get; set;
        }
        public int MachineId
        {
            get; set;
        }
        public MachineData Machine
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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DefaultValue("GetDate()")]
        public DateTime Timestamp
        {
            get; set;
        }
    }
}
