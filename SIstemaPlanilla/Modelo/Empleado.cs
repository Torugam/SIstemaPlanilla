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
    internal class Empleado
    {
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Dui { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int IdCargo { get; set; }
        public string Estado { get; set; } = "ACTIVO";

        // Método para obtener la lista de empleados
        public DataTable ObtenerEmpleados(string filtro = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlCommand cmd = new SqlCommand("sp_Empleado_Listar", con))
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

        // Método para obtener la lista de cargos para el combo box
        public DataTable ObtenerCargosParaCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlCommand cmd = new SqlCommand("sp_Cargo_ListarCombo", con))
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

        // Método para insertar un nuevo empleado
        public bool Insertar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Empleado_Insertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", this.NombreCompleto);
                        cmd.Parameters.AddWithValue("@DUI", this.Dui);
                        cmd.Parameters.AddWithValue("@DIRECCION", this.Direccion);
                        cmd.Parameters.AddWithValue("@TELEFONO", this.Telefono);
                        cmd.Parameters.AddWithValue("@ID_CARGO", this.IdCargo);
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

        // Método para actualizar un empleado existente
        public bool Actualizar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Empleado_Actualizar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", this.NombreCompleto);
                        cmd.Parameters.AddWithValue("@DUI", this.Dui);
                        cmd.Parameters.AddWithValue("@DIRECCION", this.Direccion);
                        cmd.Parameters.AddWithValue("@TELEFONO", this.Telefono);
                        cmd.Parameters.AddWithValue("@ID_CARGO", this.IdCargo);
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

        // Método para cambiar el estado de un empleado
        public bool CambiarEstado(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("sp_Empleado_CambiarEstado", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", this.IdEmpleado);
                        cmd.Parameters.AddWithValue("@ESTADO", this.Estado);
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
