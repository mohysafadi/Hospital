using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public int DoctorType { get; set; }
        public decimal Salary { get; set; }
        public DateTime? StartTraining { get; set; }
        public DateTime? EndTraining { get; set; }
        
    }
}