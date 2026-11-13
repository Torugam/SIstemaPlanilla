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
    //Comentario de prueba para ver si se sube al repositorio. Ignorenlo
    internal class Departamento
    {
        public int IdDepartamento { get; set; }
        public int? IdEmpleado { get; set; }
        public string NombreDepartamento { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        public DataTable ObtenerTodosLosDepartamentos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlCommand cmd = new SqlCommand("sp_Departamento_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable BuscarDepartamentos(string filtro)
        {
            DataTable dt = ObtenerTodosLosDepartamentos();
            DataView dv = dt.DefaultView;
            dv.RowFilter = $"NOMBRE_DEPARTAMENTO LIKE '%{filtro.Replace("'", "''")}%'";
            return dv.ToTable();
        }

        public DataTable ObtenerEncargadosCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                string query = "SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM Empleado";
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public bool Insertar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Insertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado.HasValue ? (object)this.IdEmpleado.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NOMBRE_DEPARTAMENTO", this.NombreDepartamento);
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(this.Descripcion) ? (object)DBNull.Value : this.Descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public bool Actualizar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Actualizar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", this.IdDepartamento);
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado.HasValue ? (object)this.IdEmpleado.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NOMBRE_DEPARTAMENTO", this.NombreDepartamento);
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(this.Descripcion) ? (object)DBNull.Value : this.Descripcion);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }

        public bool Eliminar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Eliminar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", this.IdDepartamento);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Number == 547
                    ? "No se puede eliminar: el departamento está siendo usado en otros registros."
                    : ex.Message;
                return false;
            }
        }
    }
}
