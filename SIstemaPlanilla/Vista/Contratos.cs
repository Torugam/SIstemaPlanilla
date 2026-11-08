using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Contratos : Form
    {
        // Controles que adaptaremos/crearemos por código de forma totalmente segura
        private ComboBox cbTipoContrato = null!;
        private TextBox txtBuscar = null!;

        public Contratos()
        {
            InitializeComponent();

            // 1. Transformamos el TextBox de tipo contrato del diseñador en un ComboBox desplegable
            if (txtTipoContrato != null)
            {
                cbTipoContrato = new ComboBox
                {
                    Location = txtTipoContrato.Location,
                    Size = txtTipoContrato.Size,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = txtTipoContrato.Font,
                    TabIndex = txtTipoContrato.TabIndex
                };

                // Reemplazamos el TextBox viejo por el nuevo ComboBox en el formulario
                this.Controls.Remove(txtTipoContrato);
                txtTipoContrato.Dispose();
                this.Controls.Add(cbTipoContrato);
            }

            // 2. Creamos dinámicamente el cuadro de búsqueda en la parte superior junto al título
            txtBuscar = new TextBox
            {
                Location = new Point(220, 20),
                Size = new Size(280, 27),
                ForeColor = Color.Gray,
                Text = "Buscar por empleado o tipo..."
            };
            this.Controls.Add(txtBuscar);
            txtBuscar.BringToFront();

            // Configuración del ComboBox de tipo de contrato
            if (cbTipoContrato != null)
            {
                cbTipoContrato.Items.Clear();
                cbTipoContrato.Items.AddRange(new string[] { "PERMANENTE", "TEMPORAL" });
                if (cbTipoContrato.Items.Count > 0) cbTipoContrato.SelectedIndex = 0;
                cbTipoContrato.SelectedIndexChanged += CbTipoContrato_SelectedIndexChanged;
            }

            // Eventos del buscador
            txtBuscar.Enter += TxtBuscar_Enter;
            txtBuscar.Leave += TxtBuscar_Leave;
            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;

            // Formato visual para diferenciar filas en el DataGridView
            if (dgvContratos != null)
            {
                dgvContratos.CellFormatting += DgvContratos_CellFormatting;
            }

            this.Load += Contratos_Load;
        }

        private void Contratos_Load(object? sender, EventArgs e)
        {
            CargarContratos();
            CargarComboEmpleados();
            ActualizarEstadoFechaFin();
        }

        private void CbTipoContrato_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarEstadoFechaFin();
        }

        private void ActualizarEstadoFechaFin()
        {
            if (dtpFechaFin != null && cbTipoContrato != null)
            {
                if (cbTipoContrato.SelectedItem?.ToString() == "PERMANENTE")
                {
                    dtpFechaFin.Enabled = false; // Los permanentes no llevan fecha de fin
                    dtpFechaFin.Checked = false;
                }
                else
                {
                    dtpFechaFin.Enabled = true;
                }
            }
        }

        private void CargarContratos()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dgvContratos != null)
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

        private void CargarComboEmpleados()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return; 

                    string query = "SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM EMPLEADO WHERE ESTADO = 'ACTIVO'";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_EMPLEADO"] = 0;
                            fila["NOMBRE_COMPLETO"] = "-- Seleccione un Empleado --";
                            dt.Rows.InsertAt(fila, 0);

                            if (cbEmpleados != null)
                            {
                                cbEmpleados.DataSource = dt;
                                cbEmpleados.DisplayMember = "NOMBRE_COMPLETO";
                                cbEmpleados.ValueMember = "ID_EMPLEADO";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para separar visualmente con colores los tipos de contrato en la tabla
        private void DgvContratos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvContratos != null && e.RowIndex >= 0 && e.ColumnIndex < dgvContratos.Columns.Count)
            {
                if (dgvContratos.Columns[e.ColumnIndex].Name == "TIPO_CONTRATO" && e.Value != null)
                {
                    string tipo = e.Value.ToString() ?? "";

                    if (tipo == "PERMANENTE")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(220, 248, 220); // Verde suave
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                    else if (tipo == "TEMPORAL")
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 205); // Amarillo / Naranja suave
                        e.CellStyle.ForeColor = Color.DarkOrange;
                    }
                }
            }
        }

        private void TxtBuscar_Enter(object? sender, EventArgs e)
        {
            if (txtBuscar != null && txtBuscar.Text == "Buscar por empleado o tipo...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.Black;
            }
        }

        private void TxtBuscar_Leave(object? sender, EventArgs e)
        {
            if (txtBuscar != null && string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = "Buscar por empleado o tipo...";
                txtBuscar.ForeColor = Color.Gray;
            }
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (dgvContratos != null && dgvContratos.DataSource is DataTable dt && txtBuscar != null)
            {
                string filtro = txtBuscar.Text.Trim();

                if (string.IsNullOrEmpty(filtro) || filtro == "Buscar por empleado o tipo...")
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    string filtroEscapado = filtro.Replace("'", "''");
                    dt.DefaultView.RowFilter = $"NOMBRE_COMPLETO LIKE '%{filtroEscapado}%' OR TIPO_CONTRATO LIKE '%{filtroEscapado}%'";
                }
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cbEmpleados == null || cbEmpleados.SelectedValue == null || Convert.ToInt32(cbEmpleados.SelectedValue) == 0 || string.IsNullOrWhiteSpace(txtSalarioBase?.Text))
                {
                    MessageBox.Show("Debe seleccionar un empleado y registrar el salario base.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tipoContrato = cbTipoContrato?.SelectedItem?.ToString() ?? "PERMANENTE";

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", Convert.ToInt32(cbEmpleados.SelectedValue));
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", dtpFechaInicio?.Value ?? DateTime.Now);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", tipoContrato == "TEMPORAL" ? (object)(dtpFechaFin?.Value ?? DateTime.Now) : DBNull.Value);
                        cmd.Parameters.AddWithValue("@SALARIO_BASE", Convert.ToDecimal(txtSalarioBase.Text.Trim()));
                        cmd.Parameters.AddWithValue("@TIPO_CONTRATO", tipoContrato);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Contrato registrado y asociado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            if (cbEmpleados != null && cbEmpleados.Items.Count > 0) cbEmpleados.SelectedIndex = 0;
            if (cbTipoContrato != null && cbTipoContrato.Items.Count > 0) cbTipoContrato.SelectedIndex = 0;
            if (txtSalarioBase != null) txtSalarioBase.Clear();

            if (txtBuscar != null)
            {
                txtBuscar.Text = "Buscar por empleado o tipo...";
                txtBuscar.ForeColor = Color.Gray;
            }

            if (dtpFechaInicio != null) dtpFechaInicio.Value = DateTime.Now;
            if (dtpFechaFin != null) dtpFechaFin.Value = DateTime.Now;
            if (cbEmpleados != null) cbEmpleados.Focus();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}