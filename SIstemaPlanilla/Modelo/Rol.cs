using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        //metodo para obtener todos los roles
        public DataTable ObtenerTodosLosRoles()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlCommand cmd = new SqlCommand("SP_ListarRoles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }

        //metodo para obtener los permisos asignados a un rol
        public List<int> ObtenerPermisosAsignados(int idRol)
        {
            List<int> permisos = new List<int>();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return permisos;
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerPermisosPorRol", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_rol", idRol);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) { permisos.Add(Convert.ToInt32(reader["ID_OPCION"])); }
                    }
                }
            }
            return permisos;
        }

        //metodo para guardar un rol con sus permisos
        public bool GuardarRolConPermisos(List<int> idsOpcionesSeleccionadas)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return false;

                using (SqlCommand cmdRol = new SqlCommand("SP_InsertarRol", con))
                {
                    cmdRol.CommandType = CommandType.StoredProcedure;
                    cmdRol.Parameters.AddWithValue("@p_nombre_rol", this.NombreRol);

                    SqlParameter outIdRol = new SqlParameter("@p_id_rol_generado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmdRol.Parameters.Add(outIdRol);

                    cmdRol.ExecuteNonQuery();
                    this.IdRol = Convert.ToInt32(outIdRol.Value);
                }

                foreach (int idOpcion in idsOpcionesSeleccionadas)
                {
                    using (SqlCommand cmdPermiso = new SqlCommand("SP_AsignarPermisoRol", con))
                    {
                        cmdPermiso.CommandType = CommandType.StoredProcedure;
                        cmdPermiso.Parameters.AddWithValue("@p_id_rol", this.IdRol);
                        cmdPermiso.Parameters.AddWithValue("@p_id_opcion", idOpcion);
                        cmdPermiso.ExecuteNonQuery();
                    }
                }
                return true;
            }
        }

        //metodo para actualizar un rol con sus permisos
        public bool ActualizarRolConPermisos(List<int> nuevosPermisos)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return false;

                using (SqlCommand cmdRol = new SqlCommand("SP_ActualizarRol", con))
                {
                    cmdRol.CommandType = CommandType.StoredProcedure;
                    cmdRol.Parameters.AddWithValue("@p_id_rol", this.IdRol);
                    cmdRol.Parameters.AddWithValue("@p_nombre_rol", this.NombreRol);
                    cmdRol.ExecuteNonQuery();
                }

                using (SqlCommand cmdDel = new SqlCommand("SP_EliminarPermisosRol", con))
                {
                    cmdDel.CommandType = CommandType.StoredProcedure;
                    cmdDel.Parameters.AddWithValue("@p_id_rol", this.IdRol);
                    cmdDel.ExecuteNonQuery();
                }

                foreach (int idOpcion in nuevosPermisos)
                {
                    using (SqlCommand cmdPermiso = new SqlCommand("SP_AsignarPermisoRol", con))
                    {
                        cmdPermiso.CommandType = CommandType.StoredProcedure;
                        cmdPermiso.Parameters.AddWithValue("@p_id_rol", this.IdRol);
                        cmdPermiso.Parameters.AddWithValue("@p_id_opcion", idOpcion);
                        cmdPermiso.ExecuteNonQuery();
                    }
                }
                return true;
            }
        }

        //metodo para buscar roles por nombre
        public DataTable BuscarRoles(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;

                string sql = "SELECT * FROM vw_Roles WHERE Rol LIKE @f ORDER BY Rol";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}