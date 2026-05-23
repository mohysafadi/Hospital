using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hospital
{
    public class PatientRepository
    {
        // إضافة مريض
        public void AddPatient(Patient p)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                string query = @"INSERT INTO Patients 
                                (Name, Address, BirthDate, PatientType, IsDischarged)
                                VALUES (@n, @a, @b, @t, @d)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@a", p.Address);
                cmd.Parameters.AddWithValue("@b", p.BirthDate);
                cmd.Parameters.AddWithValue("@t", p.PatientType);
                cmd.Parameters.AddWithValue("@d", p.IsDischarged);

                cmd.ExecuteNonQuery();
            }
        }

        // تحديث حالة المريض (تخريج)
        public void DischargePatient(int patientID)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Patients SET IsDischarged = 1 WHERE PatientID=@id", con);

                cmd.Parameters.AddWithValue("@id", patientID);
                cmd.ExecuteNonQuery();
            }
        }

        // جلب جميع المرضى
        public List<Patient> GetAllPatients()
        {
            List<Patient> list = new List<Patient>();

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Patients", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Patient
                    {
                        PatientID = (int)dr["PatientID"],
                        Name = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        BirthDate = (DateTime)dr["BirthDate"],
                        PatientType = (int)dr["PatientType"],
                        IsDischarged = (bool)dr["IsDischarged"]
                    });
                }
            }

            return list;
        }
    }
}