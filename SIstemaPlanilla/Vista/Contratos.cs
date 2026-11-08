using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Contratos : Form
    {
        private string cadenaConexion = "Server=.; Database=BD_PLANILLA; Integrated Security=true;";

        public Contratos()
        {
            InitializeComponent();
        }

        private void Contratos_Load(object sender, EventArgs e)
        {
            CargarContratos();
            CargarComboEmpleados();
        }

        // 1. Listar Contratos en el DataGridView
        private void CargarContratos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvContratos.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los contratos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Llenar el ComboBox de Empleados
        private void CargarComboEmpleados()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string query = "SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM Empleado";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_EMPLEADO"] = DBNull.Value;
                            fila["NOMBRE_COMPLETO"] = "-- Seleccione un Empleado --";
                            dt.Rows.InsertAt(fila, 0);

                            cbEmpleados.DataSource = dt;
                            cbEmpleados.DisplayMember = "NOMBRE_COMPLETO";
                            cbEmpleados.ValueMember = "ID_EMPLEADO";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbEmpleados.SelectedValue == null || cbEmpleados.SelectedValue == DBNull.Value || string.IsNullOrWhiteSpace(txtSalarioBase.Text) || string.IsNullOrWhiteSpace(txtTipoContrato.Text))
                {
                    MessageBox.Show("Debe seleccionar un empleado, ingresar el salario base y el tipo de contrato.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", Convert.ToInt32(cbEmpleados.SelectedValue));
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", dtpFechaInicio.Value);

                        // Si el checkbox de indefinido o sin fecha fin está activo, mandamos DBNull
                        cmd.Parameters.AddWithValue("@FECHA_FIN", dtpFechaFin.Checked ? (object)dtpFechaFin.Value : DBNull.Value);

                        cmd.Parameters.AddWithValue("@SALARIO_BASE", Convert.ToDecimal(txtSalarioBase.Text.Trim()));
                        cmd.Parameters.AddWithValue("@TIPO_CONTRATO", txtTipoContrato.Text.Trim());

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();

                        MessageBox.Show("Contrato registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarContratos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            if (cbEmpleados.Items.Count > 0) cbEmpleados.SelectedIndex = 0;
            txtSalarioBase.Clear();
            txtTipoContrato.Clear();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Checked = false;
            cbEmpleados.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}