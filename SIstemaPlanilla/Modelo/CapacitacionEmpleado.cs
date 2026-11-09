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
    internal class CapacitacionEmpleado
    {
        public int IdEmpleado { get; set; }
        public int IdCapacitacion { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public DateTime FechaOriginal { get; set; }   
        public decimal? Calificacion { get; set; }

        public DataTable ObtenerTodos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM VW_EmpleadoCapacitacionLeer ORDER BY FechaRealizacion DESC", con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;

                string sql = @"SELECT * FROM VW_EmpleadoCapacitacionLeer
                               WHERE Empleado LIKE @f OR Capacitacion LIKE @f
                               ORDER BY FechaRealizacion DESC";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable ObtenerEmpleadosParaCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM EMPLEADO ORDER BY NOMBRE_COMPLETO", con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable ObtenerCapacitacionesParaCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT ID_CAPACITACION, NOMBRE_CAPACITACION FROM Capacitacion ORDER BY NOMBRE_CAPACITACION", con))
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

                    using (SqlCommand cmd = new SqlCommand("SP_EmpleadoCapacitacionInsertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", this.IdCapacitacion);

                        SqlParameter pFecha = cmd.Parameters.Add("@FECHA_REALIZACION", SqlDbType.DateTime);
                        pFecha.Value = this.FechaRealizacion;

                        SqlParameter pCal = cmd.Parameters.Add("@CALIFICACION", SqlDbType.Decimal);
                        pCal.Precision = 5; pCal.Scale = 2;
                        pCal.Value = this.Calificacion.HasValue ? (object)this.Calificacion.Value : DBNull.Value;

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

                    using (SqlCommand cmd = new SqlCommand("SP_EmpleadoCapacitacionActualizar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", this.IdCapacitacion);

                        SqlParameter pOrig = cmd.Parameters.Add("@FECHA_ORIGINAL", SqlDbType.DateTime);
                        pOrig.Value = this.FechaOriginal;

                        SqlParameter pFecha = cmd.Parameters.Add("@FECHA_REALIZACION", SqlDbType.DateTime);
                        pFecha.Value = this.FechaRealizacion;

                        SqlParameter pCal = cmd.Parameters.Add("@CALIFICACION", SqlDbType.Decimal);
                        pCal.Precision = 5; pCal.Scale = 2;
                        pCal.Value = this.Calificacion.HasValue ? (object)this.Calificacion.Value : DBNull.Value;

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

                    using (SqlCommand cmd = new SqlCommand("SP_EmpleadoCapacitacionEliminar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", this.IdCapacitacion);

                        SqlParameter pFecha = cmd.Parameters.Add("@FECHA_REALIZACION", SqlDbType.DateTime);
                        pFecha.Value = this.FechaOriginal;

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
    }
}
