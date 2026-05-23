using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Hospital
{
    public class HospitalSystem
    {
        private string connectionString =
          @"Data Source=(LocalDB)\MSSQLLocalDB;
         AttachDbFilename=C:\USERS\MOHY\SOURCE\REPOS\HOSPITAL\HOSPITAL\DATABASE1.MDF;
         Integrated Security=True;Connect Timeout=30";
        // إضافة طبيب
        public void AddDoctor(Doctor d)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Doctors 
                                (Name, Address, BirthDate, DoctorType, Salary, StartTraining, EndTraining) 
                                VALUES (@Name, @Address, @BirthDate, @DoctorType, @Salary, @StartTraining, @EndTraining)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", d.Name);
                cmd.Parameters.AddWithValue("@Address", d.Address);
                cmd.Parameters.AddWithValue("@BirthDate", d.BirthDate);
                cmd.Parameters.AddWithValue("@DoctorType", d.DoctorType);
                cmd.Parameters.AddWithValue("@Salary", d.Salary);
                cmd.Parameters.AddWithValue("@StartTraining", d.StartTraining ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EndTraining", d.EndTraining ?? (object)DBNull.Value);

                   con.Open();
                   cmd.ExecuteNonQuery();
            }
        }

        // جلب كل الأطباء
        public DataTable GetAllDoctors()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Doctors";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // حذف طبيب
        public void DeleteDoctor(int doctorID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Doctors WHERE DoctorID = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", doctorID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // تعديل بيانات طبيب
        public void UpdateDoctor(Doctor d)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Doctors 
                                 SET Name=@Name, Address=@Address, BirthDate=@BirthDate, DoctorType=@DoctorType, 
                                     Salary=@Salary, StartTraining=@StartTraining, EndTraining=@EndTraining
                                 WHERE DoctorID=@DoctorID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@DoctorID", d.DoctorID);
                cmd.Parameters.AddWithValue("@Name", d.Name);
                cmd.Parameters.AddWithValue("@Address", d.Address);
                cmd.Parameters.AddWithValue("@BirthDate", d.BirthDate);
                cmd.Parameters.AddWithValue("@DoctorType", d.DoctorType);
                cmd.Parameters.AddWithValue("@Salary", d.Salary);
                cmd.Parameters.AddWithValue("@StartTraining", d.StartTraining ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EndTraining", d.EndTraining ?? (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void AddPatient(Patient p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Patients (Name, Address, BirthDate, PatientType, IsDischarged) " +
                               "VALUES (@Name, @Address, @BirthDate, @PatientType, @IsDischarged)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Address", p.Address);
                cmd.Parameters.AddWithValue("@BirthDate", p.BirthDate);
                cmd.Parameters.AddWithValue("@PatientType", p.PatientType);
                cmd.Parameters.AddWithValue("@IsDischarged", p.IsDischarged);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable GetAllPatients()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Patients";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public void DeletePatient(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Patients WHERE PatientID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdatePatient(Patient p)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Patients SET Name=@Name, Address=@Address, BirthDate=@BirthDate, " +
                               "PatientType=@PatientType, IsDischarged=@IsDischarged WHERE PatientID=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", p.PatientID);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Address", p.Address);
                cmd.Parameters.AddWithValue("@BirthDate", p.BirthDate);
                cmd.Parameters.AddWithValue("@PatientType", p.PatientType);
                cmd.Parameters.AddWithValue("@IsDischarged", p.IsDischarged);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}