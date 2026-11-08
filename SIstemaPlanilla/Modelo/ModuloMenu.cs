using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaPlanilla.Modelo
{
    public class OpccionMenu
    {
        public int IdOpcion { get; set; }
        public string NombreOpcion { get; set; } = string.Empty;
        public string RutaInterface { get; set; } = string.Empty;
    }

    public static class ModuloMenu
    {
        public static List<OpccionMenu> ObtenerOpcionesPorRol(int idRol)
        {
            List<OpccionMenu> opciones = new List<OpccionMenu>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConexionBD"]?.ConnectionString
                ?? "Data Source=localhost;Initial Catalog=BD_PLANILLA;Integrated Security=True;";

            string query = @"SELECT O.ID_OPCION, O.NOMBRE_OPCION, O.RUTA_INTERFACE 
                             FROM ROL_OPCION RO
                             INNER JOIN OPCION O ON RO.ID_OPCION = O.ID_OPCION
                             WHERE RO.ID_ROL = @idRol";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idRol", idRol);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                opciones.Add(new OpccionMenu
                                {
                                    IdOpcion = Convert.ToInt32(reader["ID_OPCION"]),
                                    NombreOpcion = reader["NOMBRE_OPCION"].ToString() ?? "",
                                    RutaInterface = reader["RUTA_INTERFACE"].ToString() ?? ""
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Error al cargar menú: " + ex.Message);
                    }
                }
            }

            return opciones;
        }
    }
}