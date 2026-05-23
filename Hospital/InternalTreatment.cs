using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class InternalTreatment
    {
        public int InternalTreatmentID { get; set; }
        public int PatientID { get; set; }
        public string RoomNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TreatmentName { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
    }
}