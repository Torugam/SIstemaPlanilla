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
        private int idEmpleadoSeleccionado = 0;
        private TextBox txtBuscar = null!;

        // Propiedad inteligente y convertidor automático para el ComboBox del tipo de contrato
        private ComboBox? cbTipoContrato
        {
            get
            {
                Control[] matches = this.Controls.Find("cbTipoContrato", true);
                if (matches.Length > 0)
                {
                    if (matches[0] is ComboBox cb) return cb;

                    // Si por error quedó como TextBox en el diseñador, lo convertimos dinámicamente a ComboBox
                    if (matches[0] is TextBox tb)
                    {
                        Point loc = tb.Location;
                        Size size = tb.Size;
                        this.Controls.Remove(tb);

                        ComboBox nuevoCb = new ComboBox
                        {
                            Name = "cbTipoContrato",
                            Location = loc,
                            Size = size,
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        this.Controls.Add(nuevoCb);
                        nuevoCb.BringToFront();
                        return nuevoCb;
                    }
                }
                return null;
            }
        }

        public Contratos()
        {
            InitializeComponent();

            bool enModoDiseno = this.DesignMode || (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

            if (!enModoDiseno)
            {
                CrearBuscadorPorCodigo();
            }

            // Enlace seguro de botones
            Control[] bGuardar = this.Controls.Find("btnGuardar", true);
            if (bGuardar.Length > 0 && bGuardar[0] is Button bg) bg.Click += btnGuardar_Click;

            Control[] bModificar = this.Controls.Find("btnModificar", true);
            if (bModificar.Length > 0 && bModificar[0] is Button bm) bm.Click += btnModificar_Click;

            Control[] bEliminar = this.Controls.Find("btnEliminar", true);
            if (bEliminar.Length > 0 && bEliminar[0] is Button be) be.Click += btnEliminar_Click;

            Control[] bLimpiar = this.Controls.Find("btnLimpiar", true);
            if (bLimpiar.Length > 0 && bLimpiar[0] is Button bl) bl.Click += btnLimpiar_Click;

            if (dgvContratos != null)
            {
                dgvContratos.CellClick += dgvContratos_CellClick;
                dgvContratos.CellContentClick += dgvContratos_CellContentClick;
            }

            this.Load += Contratos_Load;
        }

        private void CrearBuscadorPorCodigo()
        {
            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Font = new Font(this.Font, FontStyle.Bold),
                Location = new Point(175, 20)
            };

            txtBuscar = new TextBox
            {
                Width = 230,
                Location = new Point(230, 17)
            };
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtBuscar);

            lblBuscar.BringToFront();
            txtBuscar.BringToFront();
        }

        private void Contratos_Load(object? sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarContratos();
            CargarComboEmpleados();
            CargarComboTipoContrato();
            LimpiarCampos();
        }

        private void ConfigurarDataGridView()
        {
            if (dgvContratos != null)
            {
                dgvContratos.AllowUserToAddRows = false;
                dgvContratos.AllowUserToDeleteRows = false;
                dgvContratos.ReadOnly = true;
                dgvContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvContratos.MultiSelect = false;
                dgvContratos.Height = 280;
                dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvContratos.Columns["ID_EMPLEADO"] != null)
                {
                    dgvContratos.Columns["ID_EMPLEADO"].HeaderText = "ID";
                    dgvContratos.Columns["ID_EMPLEADO"].Width = 50;
                    dgvContratos.Columns["ID_EMPLEADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (dgvContratos.Columns["NOMBRE_COMPLETO"] != null)
                    dgvContratos.Columns["NOMBRE_COMPLETO"].HeaderText = "Empleado";

                if (dgvContratos.Columns["TIPO_CONTRATO"] != null)
                {
                    dgvContratos.Columns["TIPO_CONTRATO"].HeaderText = "Tipo";
                    dgvContratos.Columns["TIPO_CONTRATO"].Width = 90;
                    dgvContratos.Columns["TIPO_CONTRATO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (dgvContratos.Columns["SALARIO_BASE"] != null)
                    dgvContratos.Columns["SALARIO_BASE"].HeaderText = "Salario Base";

                if (dgvContratos.Columns["FECHA_INICIO"] != null)
                    dgvContratos.Columns["FECHA_INICIO"].HeaderText = "Fecha Inicio";

                if (dgvContratos.Columns["FECHA_FIN"] != null)
                    dgvContratos.Columns["FECHA_FIN"].HeaderText = "Fecha Fin";
            }
        }

        private void CargarContratos(string filtro = "")
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
                        cmd.Parameters.AddWithValue("@FILTRO", filtro.Trim());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dgvContratos != null)
                            {
                                dgvContratos.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los contratos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar?.Text ?? string.Empty;
            CargarContratos(textoBusqueda);
        }

        private void CargarComboEmpleados()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_ListarEmpleadosSinContrato", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_EMPLEADO"] = 0;
                            fila["NOMBRE_COMPLETO"] = "-- Seleccione un Empleado sin Contrato --";
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
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboTipoContrato()
        {
            try
            {
                DataTable dtTipo = new DataTable();
                dtTipo.Columns.Add("Tipo", typeof(string));
                dtTipo.Rows.Add("-- Seleccione Tipo --");
                dtTipo.Rows.Add("Permanente");
                dtTipo.Rows.Add("Temporal");

                if (cbTipoContrato != null)
                {
                    cbTipoContrato.DataSource = dtTipo;
                    cbTipoContrato.DisplayMember = "Tipo";
                    cbTipoContrato.ValueMember = "Tipo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (cbEmpleados?.SelectedValue == null || Convert.ToInt32(cbEmpleados.SelectedValue) == 0 ||
                    cbTipoContrato?.SelectedValue == null || cbTipoContrato.Text.Contains("Seleccione"))
                {
                    MessageBox.Show("Por favor seleccione un empleado y el tipo de contrato.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idEmp = Convert.ToInt32(cbEmpleados.SelectedValue);
                string tipo = cbTipoContrato.Text.Trim();
                decimal salario = 0;
                decimal.TryParse(txtSalarioBase?.Text.Trim(), out salario);

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    string nombreSP = tipo.Equals("Permanente", StringComparison.OrdinalIgnoreCase)
                        ? "sp_ContratoPermanente_Insertar"
                        : "sp_ContratoTemporal_Insertar";

                    using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmp);
                        cmd.Parameters.AddWithValue("@SALARIO_BASE", salario);

                        if (tipo.Equals("Temporal", StringComparison.OrdinalIgnoreCase))
                        {
                            cmd.Parameters.AddWithValue("@FECHA_INICIO", dtpFechaInicio?.Value ?? DateTime.Now);
                            cmd.Parameters.AddWithValue("@FECHA_FIN", dtpFechaFin?.Value ?? DateTime.Now.AddMonths(6));
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Contrato registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarContratos();
                        CargarComboEmpleados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un contrato de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal salario = 0;
                decimal.TryParse(txtSalarioBase?.Text.Trim(), out salario);
                string tipo = cbTipoContrato?.Text.Trim() ?? "";
                bool esTemporal = tipo.Equals("Temporal", StringComparison.OrdinalIgnoreCase);

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Actualizar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleadoSeleccionado);
                        cmd.Parameters.AddWithValue("@SALARIO_BASE", salario);
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", dtpFechaInicio?.Value ?? DateTime.Now);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", dtpFechaFin?.Value ?? DateTime.Now);
                        cmd.Parameters.AddWithValue("@ES_TEMPORAL", esTemporal);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Contrato actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarContratos();
                        CargarComboEmpleados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un contrato para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este contrato y sus registros asociados?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = Conexion.GetConexion())
                    {
                        if (conexion == null) return;

                        using (SqlCommand cmd = new SqlCommand("sp_Contrato_Eliminar", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleadoSeleccionado);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Contrato eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarContratos();
                            CargarComboEmpleados();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvContratos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvContratos != null)
            {
                DataGridViewRow row = dgvContratos.Rows[e.RowIndex];
                idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);

                if (cbEmpleados != null)
                {
                    CargarComboEmpleados();
                    cbEmpleados.SelectedValue = idEmpleadoSeleccionado;
                }

                string tipo = row.Cells["TIPO_CONTRATO"].Value.ToString() ?? "";
                if (cbTipoContrato != null) cbTipoContrato.Text = tipo;

                if (txtSalarioBase != null) txtSalarioBase.Text = row.Cells["SALARIO_BASE"].Value.ToString();

                if (tipo.Equals("Temporal", StringComparison.OrdinalIgnoreCase))
                {
                    if (dtpFechaInicio != null && row.Cells["FECHA_INICIO"].Value != DBNull.Value)
                        dtpFechaInicio.Value = Convert.ToDateTime(row.Cells["FECHA_INICIO"].Value);

                    if (dtpFechaFin != null && row.Cells["FECHA_FIN"].Value != DBNull.Value)
                        dtpFechaFin.Value = Convert.ToDateTime(row.Cells["FECHA_FIN"].Value);
                }
            }
        }

        private void dgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            dgvContratos_CellClick(sender, e);
        }

        private void LimpiarCampos()
        {
            idEmpleadoSeleccionado = 0;
            CargarComboEmpleados();
            CargarComboTipoContrato();
            if (txtSalarioBase != null) txtSalarioBase.Clear();
            if (txtBuscar != null) txtBuscar.Clear();
            if (dtpFechaInicio != null) dtpFechaInicio.Value = DateTime.Now;
            if (dtpFechaFin != null) dtpFechaFin.Value = DateTime.Now;
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}