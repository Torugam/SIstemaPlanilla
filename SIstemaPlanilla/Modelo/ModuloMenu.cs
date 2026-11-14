    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{

    // opciones del sistema
    public enum OpcionesSistema
    {
        Administracion = 1,
        RecursosHumanos = 2,
        GestionPlanilla = 3
    }

    // Clase para manejar los permisos de los roles en el sistema
    public static class ModuloMenu
    {
        // Método para obtener los permisos de un rol específico
        public static List<int> ObtenerPermisos(int idRol)
        {
            // Lista para almacenar los permisos del rol
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