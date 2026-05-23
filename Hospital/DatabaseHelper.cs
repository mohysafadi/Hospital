using System.Data.SqlClient;

namespace Hospital
{
    public class DatabaseHelper
    {

        private static string connectionString =
      @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=|DataDirectory|\Database1.mdf;
      Integrated Security=True;";


        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}