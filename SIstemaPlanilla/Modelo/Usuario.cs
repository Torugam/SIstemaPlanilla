using SistemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstemaPlanilla.Modelo
{
    internal class Usuario
    {
        
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public int? IdEmpleado { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Estado { get; set; } = "ACTIVO";
        public int IntentosFallidos { get; set; }

        public string NombreRol { get; set; } = string.Empty;
        public string NombreEmpleado { get; set; } = string.Empty;

        
        //metodo leer tabla
        public DataTable Leer()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM vw_Usuarios", con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //metodo buscar usuario
        public DataTable Buscar(string textoBuscado)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;

                // Llamamos al SP de búsqueda
                using (SqlCommand cmd = new SqlCommand("SP_BuscarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Filtro", textoBuscado);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //metodo insertar usuario
        public bool Insertar()
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return false;

                using (SqlCommand cmd = new SqlCommand("SP_InsertarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdRol", this.IdRol);
                    cmd.Parameters.AddWithValue("@IdEmpleado", this.IdEmpleado > 0 ? (object)this.IdEmpleado : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Username", this.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", this.PasswordHash);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // metodo actualizar usuario
        public bool Actualizar(bool actualizaPassword)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return false;

                using (SqlCommand cmd = new SqlCommand("SP_ActualizarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", this.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdRol", this.IdRol);
                    cmd.Parameters.AddWithValue("@IdEmpleado", this.IdEmpleado > 0 ? (object)this.IdEmpleado : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Username", this.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", this.PasswordHash);
                    cmd.Parameters.AddWithValue("@ActualizaPassword", actualizaPassword); 

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        //metodo desactivar usuario
        public bool Desactivar()
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return false;

                // El SP que ya tenías creado
                using (SqlCommand cmd = new SqlCommand("SP_GestionarEstadoUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_usuario", this.IdUsuario);
                    cmd.Parameters.AddWithValue("@p_nuevo_estado", "INACTIVO");
                    cmd.Parameters.AddWithValue("@p_reiniciar_intentos", 1);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
