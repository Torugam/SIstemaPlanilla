using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaPlanilla.Modelo
{
    public class Cargo
    {
        public int IdCargo { get; set; }
        public int? IdCargoSuperior { get; set; }
        public string NombreCargo { get; set; } = string.Empty;
        public decimal SalarioMin { get; set; }
        public decimal SalarioMax { get; set; }

        //metodo para obtener todos los cargos 
        public DataTable ObtenerTodosLosCargos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM VW_CargoLeer ORDER BY Cargo", con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //metodo para buscar cargos por nombre o cargo superior
        public DataTable BuscarCargos(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;

                string sql = @"SELECT * FROM VW_CargoLeer
                               WHERE Cargo LIKE @f OR CargoSuperior LIKE @f
                               ORDER BY Cargo";
                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@f", "%" + filtro + "%");
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //meotodo para obtener los cargos para un combo box
        public DataTable ObtenerCargosParaCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return dt;
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT ID_CARGO, NOMBRE_CARGO FROM CARGO ORDER BY NOMBRE_CARGO", con))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }

        //metodo para insertar un cargo
        public bool Insertar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("SP_CargoInsertar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CAR_ID_CARGO", this.IdCargoSuperior > 0 ? (object)this.IdCargoSuperior : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NOMBRE_CARGO", this.NombreCargo);

                        SqlParameter pMin = cmd.Parameters.Add("@SALARIO_MIN_REF", SqlDbType.Decimal);
                        pMin.Precision = 10; pMin.Scale = 2; pMin.Value = this.SalarioMin;

                        SqlParameter pMax = cmd.Parameters.Add("@SALARIO_MAX_REF", SqlDbType.Decimal);
                        pMax.Precision = 10; pMax.Scale = 2; pMax.Value = this.SalarioMax;

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

        //metodo para actualizar un cargo
        public bool Actualizar(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("SP_CargoActualizar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CARGO", this.IdCargo);
                        cmd.Parameters.AddWithValue("@CAR_ID_CARGO", this.IdCargoSuperior > 0 ? (object)this.IdCargoSuperior : DBNull.Value);
                        cmd.Parameters.AddWithValue("@NOMBRE_CARGO", this.NombreCargo);

                        SqlParameter pMin = cmd.Parameters.Add("@SALARIO_MIN_REF", SqlDbType.Decimal);
                        pMin.Precision = 10; pMin.Scale = 2; pMin.Value = this.SalarioMin;

                        SqlParameter pMax = cmd.Parameters.Add("@SALARIO_MAX_REF", SqlDbType.Decimal);
                        pMax.Precision = 10; pMax.Scale = 2; pMax.Value = this.SalarioMax;

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

        //metodo para eliminar un cargo
        public bool EliminarCargo(out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) { mensaje = "No hay conexión a la base de datos."; return false; }

                    using (SqlCommand cmd = new SqlCommand("SP_CARGO_ELIMINAR", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_CARGO", this.IdCargo);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                mensaje = ex.Number == 547
                    ? "No se puede eliminar: el cargo está siendo usado en otros registros."
                    : ex.Message;
                return false;
            }
        }

        
    }
}