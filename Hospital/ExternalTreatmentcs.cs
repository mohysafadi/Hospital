using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class ExternalTreatment
    {
        public int ExternalTreatmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string TreatmentName { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
        public DateTime VisitDate { get; set; }
    }
}