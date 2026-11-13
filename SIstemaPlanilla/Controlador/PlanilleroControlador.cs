using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;

namespace SIstemaPlanilla.Controlador
{
    public class PlanilleroControlador
    {
        // 1. PARÁMETROS DE LEY (ISSS, AFP, RENTA)
        public DataTable ListarParametrosLey(string filtro = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerParametrosLey", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_filtro", filtro);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public bool ActualizarParametroLey(int idParametro, decimal porcentajeOMonto, decimal? techoMaximo, decimal? tramoDesde, decimal? tramoHasta)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarParametroLey", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_parametro", idParametro);
                    cmd.Parameters.AddWithValue("@p_porcentaje_o_monto", porcentajeOMonto);
                    cmd.Parameters.AddWithValue("@p_techo_maximo", (object?)techoMaximo ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p_tramo_desde", (object?)tramoDesde ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p_tramo_hasta", (object?)tramoHasta ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        // 2. PRÉSTAMOS INTERNOS
        public DataTable ListarEmpleadosActivosCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ListarEmpleadosActivosCombo", con))
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

        public DataTable ListarPrestamos(string filtro = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ListarPrestamos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_filtro", filtro);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public int RegistrarPrestamo(int idEmpleado, decimal montoTotal, decimal cuotaQuincenal)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_RegistrarPrestamo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_empleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@p_monto_total", montoTotal);
                    cmd.Parameters.AddWithValue("@p_cuota_quincenal", cuotaQuincenal);

                    SqlParameter paramOut = new SqlParameter("@p_id_prestamo_generado", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramOut);

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(paramOut.Value);
                }
            }
        }

        public DataTable ConsultarHistorialPagosPrestamo(int idPrestamo)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ConsultarHistorialPagosPrestamo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_prestamo", idPrestamo);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        
        // 3. GENERACIÓN Y CONSULTA DE PLANILLAS QUINCENALES

        public int GenerarPlanillaQuincenal(int periodoQuincenal, int mes, int anio)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_GenerarPlanillaQuincenal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_periodo_quincenal", periodoQuincenal);
                    cmd.Parameters.AddWithValue("@p_mes", mes);
                    cmd.Parameters.AddWithValue("@p_anio", anio);

                    SqlParameter paramOut = new SqlParameter("@p_id_planilla_generada", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramOut);

                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(paramOut.Value);
                }
            }
        }

        public DataTable ConsultarPlanillaPorPeriodo(int periodoQuincenal, int mes, int anio, string filtro = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ConsultarPlanillaPorPeriodo", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_periodo_quincenal", periodoQuincenal);
                    cmd.Parameters.AddWithValue("@p_mes", mes);
                    cmd.Parameters.AddWithValue("@p_anio", anio);
                    cmd.Parameters.AddWithValue("@p_filtro", filtro);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public bool ActualizarHorasExtraBonos(int idPlanilla, int idEmpleado, decimal horasExtra, decimal bonificaciones)
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                using (SqlCommand cmd = new SqlCommand("SP_ActualizarHorasExtraBonos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_planilla", idPlanilla);
                    cmd.Parameters.AddWithValue("@p_id_empleado", idEmpleado);
                    cmd.Parameters.AddWithValue("@p_horas_extra", horasExtra);
                    cmd.Parameters.AddWithValue("@p_bonificaciones", bonificaciones);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }
        
        // 4. BOLETA DE PAGO INDIVIDUAL  PARAVISTA VW_BoletaPagoIndividual
        public DataTable ConsultarBoletaIndividual(int idPlanilla, int idEmpleado)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) throw new Exception("No se pudo conectar a la base de datos.");
                string query = "SELECT * FROM VW_BoletaPagoIndividual WHERE ID_PLANILLA = @idPlanilla AND ID_EMPLEADO = @idEmpleado";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@idPlanilla", idPlanilla);
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }








    }
}