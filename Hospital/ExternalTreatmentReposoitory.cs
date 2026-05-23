using Hospital;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ExternalTreatmentRepository
{
    private readonly string _connectionString;

    public ExternalTreatmentRepository(string cs)
    {
        _connectionString = cs;
    }

    public List<ExternalTreatment> GetAll()
    {
        List<ExternalTreatment> list = new List<ExternalTreatment>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM ExternalTreatment";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new ExternalTreatment
                {
                    ExternalTreatmentID = (int)dr["ExternalTreatmentID"],
                    PatientID = (int)dr["PatientID"],
                    DoctorID = (int)dr["DoctorID"],
                    TreatmentName = dr["TreatmentName"].ToString(),
                    Cost = (decimal)dr["Cost"],
                    Notes = dr["Notes"].ToString(),
                    VisitDate = (DateTime)dr["VisitDate"]
                });
            }
        }

        return list;
    }

    public int Add(ExternalTreatment t)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO ExternalTreatment 
                             (PatientID, DoctorID, TreatmentName, Cost, Notes, VisitDate)
                             VALUES (@p, @d, @n, @c, @notes, @v)";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@p", t.PatientID);
            cmd.Parameters.AddWithValue("@d", t.DoctorID);
            cmd.Parameters.AddWithValue("@n", t.TreatmentName);
            cmd.Parameters.AddWithValue("@c", t.Cost);
            cmd.Parameters.AddWithValue("@notes", t.Notes ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@v", t.VisitDate);

            con.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    public int Update(ExternalTreatment t)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"UPDATE ExternalTreatment SET
                             PatientID=@p, DoctorID=@d, TreatmentName=@n,
                             Cost=@c, Notes=@notes, VisitDate=@v
                             WHERE ExternalTreatmentID=@id";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", t.ExternalTreatmentID);
            cmd.Parameters.AddWithValue("@p", t.PatientID);
            cmd.Parameters.AddWithValue("@d", t.DoctorID);
            cmd.Parameters.AddWithValue("@n", t.TreatmentName);
            cmd.Parameters.AddWithValue("@c", t.Cost);
            cmd.Parameters.AddWithValue("@notes", t.Notes ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@v", t.VisitDate);

            con.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    public int Delete(int id)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM ExternalTreatment WHERE ExternalTreatmentID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}