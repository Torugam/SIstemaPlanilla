using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{
    public class Capacitacion
    {
        public int IdCapacitacion { get; set; }
        public string NombreCapacitacion { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int HorasDuracion { get; set; }

        public DataTable ObtenerTodasLasCapacitaciones()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM VW_CapacitacionLeer ORDER BY Capacitacion", con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable BuscarCapacitaciones(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;

                string sql = @"SELECT * FROM VW_CapacitacionLeer
                               WHERE Capacitacion LIKE @f OR Descripcion LIKE @f
                               ORDER BY Capacitacion";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
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

                    using (SqlCommand cmd = new SqlCommand("SP_CapacitacionInsertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOMBRE_CAPACITACION", this.NombreCapacitacion);
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(this.Descripcion) ? DBNull.Value : (object)this.Descripcion);
                        cmd.Parameters.AddWithValue("@HORAS_DURACION", this.HorasDuracion);

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

                    using (SqlCommand cmd = new SqlCommand("SP_CapacitacionActualizar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", this.IdCapacitacion);
                        cmd.Parameters.AddWithValue("@NOMBRE_CAPACITACION", this.NombreCapacitacion);
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(this.Descripcion) ? DBNull.Value : (object)this.Descripcion);
                        cmd.Parameters.AddWithValue("@HORAS_DURACION", this.HorasDuracion);

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

                    using (SqlCommand cmd = new SqlCommand("SP_CapacitacionEliminar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", this.IdCapacitacion);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Number == 547
                    ? "No se puede eliminar: la capacitación está siendo usada en otros registros."
                    : ex.Message;
                return false;
            }
        }
    }
}