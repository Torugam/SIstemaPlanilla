using System;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{
    public class Conexion
    {
        public static SqlConnection GetConexion()
        {
            string CConexion = "Server=.;Database=BD_PLANILLA;Integrated Security=True;";


            try
            {
                SqlConnection con = new SqlConnection(CConexion);
                
                con.Open(); 
                
                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error de conexión: " + e.Message);
                return null;
            }
        }
    }
}