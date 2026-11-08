using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Cargos : Form
    {
        // Ajusta tu servidor si es necesario (ej: "Server=.; Database=BD_PLANILLA; Integrated Security=true;")
        private string cadenaConexion = "Server=localhost; Database=BD_PLANILLA; Integrated Security=true;";

        public Cargos()
        {
            InitializeComponent();
        }

        private void Cargos_Load(object sender, EventArgs e)
        {
            CargarCargos();
            CargarComboCargoSuperior();
        }

        private void CargarCargos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string query = "SELECT ID_CARGO, CAR_ID_CARGO, NOMBRE_CARGO, SALARIO_MIN_REF, SALARIO_MAX_REF FROM CARGO";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvCargos.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cargos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboCargoSuperior()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string query = "SELECT ID_CARGO, NOMBRE_CARGO FROM CARGO";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_CARGO"] = DBNull.Value;
                            fila["NOMBRE_CARGO"] = "-- Ninguno (Cargo Principal) --";
                            dt.Rows.InsertAt(fila, 0);

                            cbCargoSuperior.DataSource = dt;
                            cbCargoSuperior.DisplayMember = "NOMBRE_CARGO";
                            cbCargoSuperior.ValueMember = "ID_CARGO";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cargos superiores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIdCargo.Text) || string.IsNullOrWhiteSpace(txtNombreCargo.Text))
                {
                    MessageBox.Show("El ID del Cargo y el Nombre son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Cargo_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARGO", Convert.ToInt32(txtIdCargo.Text.Trim()));

                        if (cbCargoSuperior.SelectedValue == null || cbCargoSuperior.SelectedValue == DBNull.Value)
                        {
                            cmd.Parameters.AddWithValue("@CAR_ID_CARGO", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CAR_ID_CARGO", Convert.ToInt32(cbCargoSuperior.SelectedValue));
                        }

                        cmd.Parameters.AddWithValue("@NOMBRE_CARGO", txtNombreCargo.Text.Trim());
                        cmd.Parameters.AddWithValue("@SALARIO_MIN_REF", Convert.ToDecimal(txtSalarioMin.Text.Trim()));
                        cmd.Parameters.AddWithValue("@SALARIO_MAX_REF", Convert.ToDecimal(txtSalarioMax.Text.Trim()));

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();

                        MessageBox.Show("Cargo registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarCargos();
                        CargarComboCargoSuperior();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cargo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtIdCargo.Clear();
            txtNombreCargo.Clear();
            txtSalarioMin.Clear();
            txtSalarioMax.Clear();
            if (cbCargoSuperior.Items.Count > 0) cbCargoSuperior.SelectedIndex = 0;
            txtIdCargo.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}