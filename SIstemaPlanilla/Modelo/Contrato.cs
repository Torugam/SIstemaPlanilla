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
    internal class Contrato
    {
        public int IdEmpleado { get; set; }
        public string TipoContrato { get; set; } = string.Empty; 
        public decimal SalarioBase { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool EsTemporal => TipoContrato.Equals("TEMPORAL", StringComparison.OrdinalIgnoreCase);

        //metodo para obtener los contratos 
        public DataTable ObtenerContratos(string filtro = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlCommand cmd = new SqlCommand("sp_Contrato_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FILTRO", filtro.Trim());
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        //metodo para obtener los empleados sin contrato
        public DataTable ObtenerEmpleadosSinContrato()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlCommand cmd = new SqlCommand("sp_Contrato_ListarEmpleadosSinContrato", con))
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

        //metodo para insertar un contrato
        public bool Insertar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    if (this.TipoContrato == "PERMANENTE")
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ContratoPermanente_Insertar", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                            cmd.Parameters.AddWithValue("@SALARIO_BASE", this.SalarioBase);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (this.TipoContrato == "TEMPORAL")
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_ContratoTemporal_Insertar", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                            cmd.Parameters.AddWithValue("@SALARIO_BASE", this.SalarioBase);
                            cmd.Parameters.AddWithValue("@FECHA_INICIO", this.FechaInicio);
                            cmd.Parameters.AddWithValue("@FECHA_FIN", this.FechaFin);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        mensaje = "Tipo de contrato no válido.";
                        return false;
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

        //metodo para actualizar un contrato
        public bool Actualizar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Actualizar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.Parameters.AddWithValue("@SALARIO_BASE", this.SalarioBase);
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", this.FechaInicio);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", this.FechaFin);
                        cmd.Parameters.AddWithValue("@ES_TEMPORAL", this.EsTemporal);
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

        //metodo para eliminar un contrato
        public bool Eliminar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Eliminar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Number == 547
                    ? "No se puede eliminar: el contrato está siendo usado en otros registros."
                    : ex.Message;
                return false;
            }
        }
    }
}
