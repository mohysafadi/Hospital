using Hospital;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hospital
{
    public class InternalTreatmentRepository
    {
        private string connectionString;

        public InternalTreatmentRepository(string cs)
        {
            connectionString = cs;
        }

        // إضافة معالجة
        public int AddInternalTreatment(InternalTreatment t)
        {
            int newID = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO InternalTreatment
                    (PatientID, RoomNumber, StartDate, EndDate, TreatmentName, Cost, Notes)
                    VALUES (@p, @r, @s, @e, @t, @c, @n);
                    SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@p", t.PatientID);
                cmd.Parameters.AddWithValue("@r", t.RoomNumber);
                cmd.Parameters.AddWithValue("@s", t.StartDate);
                cmd.Parameters.AddWithValue("@e", (object)t.EndDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@t", t.TreatmentName);
                cmd.Parameters.AddWithValue("@c", t.Cost);
                cmd.Parameters.AddWithValue("@n", t.Notes);

                con.Open();
                newID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return newID;
        }

        // تعديل معالجة
        public void UpdateInternalTreatment(InternalTreatment t)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    UPDATE InternalTreatment SET
                        PatientID=@p,
                        RoomNumber=@r,
                        StartDate=@s,
                        EndDate=@e,
                        TreatmentName=@t,
                        Cost=@c,
                        Notes=@n
                    WHERE InternalTreatmentID=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", t.InternalTreatmentID);
                cmd.Parameters.AddWithValue("@p", t.PatientID);
                cmd.Parameters.AddWithValue("@r", t.RoomNumber);
                cmd.Parameters.AddWithValue("@s", t.StartDate);
                cmd.Parameters.AddWithValue("@e", (object)t.EndDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@t", t.TreatmentName);
                cmd.Parameters.AddWithValue("@c", t.Cost);
                cmd.Parameters.AddWithValue("@n", t.Notes);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // حذف معالجة
        public void DeleteInternalTreatment(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd1 = new SqlCommand(
                    "DELETE FROM InternalTreatmentDoctor WHERE InternalTreatmentID=@id", con);
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(
                    "DELETE FROM InternalTreatment WHERE InternalTreatmentID=@id", con);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd2.ExecuteNonQuery();
            }
        }

        // حذف الأطباء المرتبطين
        public void DeleteDoctorsForTreatment(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM InternalTreatmentDoctor WHERE InternalTreatmentID=@id", con);

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // إضافة أطباء
        public void AddDoctorsToTreatment(int treatmentID, List<int> doctors)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                foreach (int docID in doctors)
                {
                    string query = @"
                        INSERT INTO InternalTreatmentDoctor (InternalTreatmentID, DoctorID)
                        VALUES (@t, @d)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@t", treatmentID);
                    cmd.Parameters.AddWithValue("@d", docID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // جلب الأطباء المرتبطين
        public List<int> GetDoctorsForTreatment(int treatmentID)
        {
            List<int> list = new List<int>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT DoctorID 
                    FROM InternalTreatmentDoctor 
                    WHERE InternalTreatmentID=@id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", treatmentID);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add((int)dr["DoctorID"]);
                }
            }

            return list;
        }

        // جلب المعالجات + أسماء الأطباء
        public DataTable GetAllInternalTreatments()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        it.InternalTreatmentID,
                        it.PatientID,
                        it.RoomNumber,
                        it.StartDate,
                        it.EndDate,
                        it.TreatmentName,
                        it.Cost,
                        it.Notes,
                        STRING_AGG(d.Name, ', ') AS Doctors
                    FROM InternalTreatment it
                    LEFT JOIN InternalTreatmentDoctor itd
                        ON it.InternalTreatmentID = itd.InternalTreatmentID
                    LEFT JOIN Doctors d
                        ON itd.DoctorID = d.DoctorID
                    GROUP BY 
                        it.InternalTreatmentID,
                        it.PatientID,
                        it.RoomNumber,
                        it.StartDate,
                        it.EndDate,
                        it.TreatmentName,
                        it.Cost,
                        it.Notes
                    ORDER BY it.InternalTreatmentID DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }

            return dt;
        }
    }
}