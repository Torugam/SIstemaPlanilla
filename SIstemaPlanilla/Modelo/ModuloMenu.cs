    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{
    // Catálogo estático de tus pantallas (Enum)
    public enum OpcionesSistema
    {
        Administracion = 1,
        RecursosHumanos = 2,
        GestionPlanilla = 3
    }

    public static class ModuloMenu
    {
        public static List<int> ObtenerPermisos(int idRol)
        {
            List<int> permisos = new List<int>();
            string query = "SELECT ID_OPCION FROM ROL_OPCION WHERE ID_ROL = @idRol";

            using (SqlConnection conn = Conexion.GetConexion())
            {
                if (conn == null) return permisos;

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idRol", idRol);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            permisos.Add(Convert.ToInt32(reader["ID_OPCION"]));
                        }
                    }
                }
            }
            return permisos;
        }
    }
}