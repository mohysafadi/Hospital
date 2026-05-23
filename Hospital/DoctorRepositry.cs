using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Hospital
{
    public class DoctorRepository
    {
        // إضافة طبيب
        public void AddDoctor(Doctor d)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                string query = @"INSERT INTO Doctors 
                                (Name, Address, BirthDate, DoctorType, Salary, StartTraining, EndTraining)
                                VALUES (@n, @a, @b, @t, @s, @st, @et)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@n", d.Name);
                cmd.Parameters.AddWithValue("@a", d.Address);
                cmd.Parameters.AddWithValue("@b", d.BirthDate);
                cmd.Parameters.AddWithValue("@t", d.DoctorType);
                cmd.Parameters.AddWithValue("@s", d.Salary);
                cmd.Parameters.AddWithValue("@st", (object)d.StartTraining ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@et", (object)d.EndTraining ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        // حذف طبيب
        public void DeleteDoctor(int doctorID)
        {
            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Doctors WHERE DoctorID=@id", con);
                cmd.Parameters.AddWithValue("@id", doctorID);

                cmd.ExecuteNonQuery();
            }
        }

        // جلب جميع الأطباء
        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> list = new List<Doctor>();

            using (SqlConnection con = DatabaseHelper.GetConnection())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Doctors", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Doctor
                    {
                        DoctorID = (int)dr["DoctorID"],
                        Name = dr["Name"].ToString(),
                        Address = dr["Address"].ToString(),
                        BirthDate = (DateTime)dr["BirthDate"],
                        DoctorType = (int)dr["DoctorType"],
                        Salary = (decimal)dr["Salary"],
                        StartTraining = dr["StartTraining"] == DBNull.Value ? null : (DateTime?)dr["StartTraining"],
                        EndTraining = dr["EndTraining"] == DBNull.Value ? null : (DateTime?)dr["EndTraining"]
                    });
                }
            }

            return list;
        }
    }
}