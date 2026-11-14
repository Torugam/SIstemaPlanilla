using System;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{
    public class Conexion
    {
        //clase conexion para conectarse a la base de datos
        public static SqlConnection GetConexion()
        {
            //cadena de conexion a la base de datos
            string CConexion = "Server=.;Database=BD_PLANILLA;Integrated Security=True;";


            try
            {//crear una nueva conexion a la base de datos
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