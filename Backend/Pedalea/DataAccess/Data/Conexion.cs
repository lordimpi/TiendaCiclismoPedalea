using System.Data.SqlClient;

namespace DataAccess.Data
{
    public class Conexion
    {
        private static Conexion Conection = null;

        public SqlConnection CreateConnection()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Data Source=SANTIAGO;Initial Catalog=PEDALEA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            catch (Exception ex)
            {
                Cadena = null;
                string msg = ex.Message;
            }
            return Cadena;
        }

        public static Conexion GetInstancia()
        {
            if (Conection == null)
            {
                Conection = new Conexion();
            }
            return Conection;
        }
    }
}
