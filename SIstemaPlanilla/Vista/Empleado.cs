using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Empleado : Form
    {
        private int idEmpleadoSeleccionado = 0;
        private TextBox txtBuscar = null!;
        private Button btnReactivar = null!;

        public Empleado()
        {
            InitializeComponent();

            // Evita errores de diseño gráficos en Visual Studio (IUIService / DesignMode)
            bool enModoDiseno = this.DesignMode || (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

            if (!enModoDiseno)
            {
                // Crear y posicionar el buscador arriba a la par del título "EMPLEADOS"
                CrearBuscadorPorCodigo();

                // Crear y posicionar el botón Reactivar ordenadamente entre Eliminar y Limpiar
                CrearBotonReactivarPorCodigo();
            }

            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
            if (btnModificar != null) btnModificar.Click += btnModificar_Click;
            if (btnEliminar != null) btnEliminar.Click += btnEliminar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;
            if (dgvEmpleados != null)
            {
                dgvEmpleados.CellClick += dgvEmpleados_CellClick;
                dgvEmpleados.CellContentClick += dgvEmpleados_CellContentClick;
            }

            this.Load += Empleado_Load;
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

        private void CrearBotonReactivarPorCodigo()
        {
            btnReactivar = new Button();
            btnReactivar.Text = "Reactivar";
            btnReactivar.Size = btnEliminar != null ? btnEliminar.Size : new Size(75, 25);

            if (btnEliminar != null && btnLimpiar != null)
            {
                // Colocamos el botón Reactivar a la derecha de Eliminar, y movemos Limpiar un poco más a la derecha
                btnReactivar.Location = new Point(btnEliminar.Location.X + btnEliminar.Width + 10, btnEliminar.Location.Y);
                btnLimpiar.Location = new Point(btnReactivar.Location.X + btnReactivar.Width + 10, btnLimpiar.Location.Y);
            }
            else
            {
                btnReactivar.Location = new Point(500, 150);
            }

            btnReactivar.Click += btnReactivar_Click;
            this.Controls.Add(btnReactivar);
            btnReactivar.BringToFront();
        }

        private void Empleado_Load(object? sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarEmpleados();
            CargarComboCargos();
            LimpiarCampos();
        }

        private void ConfigurarDataGridView()
        {
            if (dgvEmpleados != null)
            {
                dgvEmpleados.AllowUserToAddRows = false;
                dgvEmpleados.AllowUserToDeleteRows = false;
                dgvEmpleados.ReadOnly = true;
                dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEmpleados.MultiSelect = false;

                // Ampliamos la altura de la tabla para que muestre más filas antes de que aparezca el scroll.
                dgvEmpleados.Height = 280;

                // Modo de llenado para las columnas
                dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Anchos fijos y encabezados limpios
                if (dgvEmpleados.Columns["ID_EMPLEADO"] != null)
                {
                    dgvEmpleados.Columns["ID_EMPLEADO"].HeaderText = "ID";
                    dgvEmpleados.Columns["ID_EMPLEADO"].Width = 50;
                    dgvEmpleados.Columns["ID_EMPLEADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (dgvEmpleados.Columns["NOMBRE_COMPLETO"] != null)
                    dgvEmpleados.Columns["NOMBRE_COMPLETO"].HeaderText = "Nombre Completo";

                if (dgvEmpleados.Columns["DUI"] != null)
                    dgvEmpleados.Columns["DUI"].HeaderText = "DUI";

                if (dgvEmpleados.Columns["DIRECCION"] != null)
                    dgvEmpleados.Columns["DIRECCION"].HeaderText = "Dirección";

                if (dgvEmpleados.Columns["TELEFONO"] != null)
                    dgvEmpleados.Columns["TELEFONO"].HeaderText = "Teléfono";

                if (dgvEmpleados.Columns["ESTADO"] != null)
                {
                    dgvEmpleados.Columns["ESTADO"].HeaderText = "Estado";
                    dgvEmpleados.Columns["ESTADO"].Width = 80;
                    dgvEmpleados.Columns["ESTADO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (dgvEmpleados.Columns["NOMBRE_CARGO"] != null)
                    dgvEmpleados.Columns["NOMBRE_CARGO"].HeaderText = "Cargo";
            }
        }

        private void CargarEmpleados(string filtro = "")
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

                    using (SqlCommand cmd = new SqlCommand("sp_Empleado_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FILTRO", filtro.Trim());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dgvEmpleados != null)
                            {
                                dgvEmpleados.DataSource = dt;
                                if (dgvEmpleados.Columns["ID_CARGO"] != null)
                                    dgvEmpleados.Columns["ID_CARGO"].Visible = false;
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

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar?.Text ?? string.Empty;
            CargarEmpleados(textoBusqueda);
        }

        private void CargarComboCargos()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Cargo_ListarCombo", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_CARGO"] = 0;
                            fila["NOMBRE_CARGO"] = "-- Seleccione un Cargo --";
                            dt.Rows.InsertAt(fila, 0);

                            if (cbCargos != null)
                            {
                                cbCargos.DataSource = dt;
                                cbCargos.DisplayMember = "NOMBRE_CARGO";
                                cbCargos.ValueMember = "ID_CARGO";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cargos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre?.Text) || string.IsNullOrWhiteSpace(txtDui?.Text) || cbCargos?.SelectedValue == null || Convert.ToInt32(cbCargos.SelectedValue) == 0)
                {
                    MessageBox.Show("Por favor complete los campos obligatorios y seleccione un cargo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Empleado_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@DUI", txtDui.Text.Trim());
                        cmd.Parameters.AddWithValue("@DIRECCION", txtDireccion?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@TELEFONO", txtTelefono?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@ID_CARGO", Convert.ToInt32(cbCargos.SelectedValue));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Empleado registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarEmpleados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Empleado_Actualizar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleadoSeleccionado);
                        cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@DUI", txtDui.Text.Trim());
                        cmd.Parameters.AddWithValue("@DIRECCION", txtDireccion?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@TELEFONO", txtTelefono?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@ID_CARGO", Convert.ToInt32(cbCargos?.SelectedValue ?? 0));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Empleado actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarEmpleados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado para dar de baja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de dar de baja a este empleado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

                        using (SqlCommand cmd = new SqlCommand("sp_Empleado_CambiarEstado", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleadoSeleccionado);
                            cmd.Parameters.AddWithValue("@ESTADO", "INACTIVO");

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Empleado dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarEmpleados();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al dar de baja el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReactivar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado inactivo de la lista para reactivar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de reactivar a este empleado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

                        using (SqlCommand cmd = new SqlCommand("sp_Empleado_CambiarEstado", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleadoSeleccionado);
                            cmd.Parameters.AddWithValue("@ESTADO", "ACTIVO");

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Empleado reactivado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarEmpleados();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al reactivar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEmpleados_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEmpleados != null)
            {
                DataGridViewRow row = dgvEmpleados.Rows[e.RowIndex];
                idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);

                if (txtNombre != null) txtNombre.Text = row.Cells["NOMBRE_COMPLETO"].Value.ToString();
                if (txtDui != null) txtDui.Text = row.Cells["DUI"].Value.ToString();
                if (txtDireccion != null) txtDireccion.Text = row.Cells["DIRECCION"].Value.ToString();
                if (txtTelefono != null) txtTelefono.Text = row.Cells["TELEFONO"].Value.ToString();

                if (cbCargos != null && row.Cells["ID_CARGO"].Value != DBNull.Value)
                {
                    cbCargos.SelectedValue = Convert.ToInt32(row.Cells["ID_CARGO"].Value);
                }
            }
        }

        private void dgvEmpleados_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            dgvEmpleados_CellClick(sender, e);
        }

        private void LimpiarCampos()
        {
            idEmpleadoSeleccionado = 0;
            if (txtNombre != null) txtNombre.Clear();
            if (txtDui != null) txtDui.Clear();
            if (txtDireccion != null) txtDireccion.Clear();
            if (txtTelefono != null) txtTelefono.Clear();
            if (cbCargos != null && cbCargos.Items.Count > 0) cbCargos.SelectedIndex = 0;
            if (txtBuscar != null) txtBuscar.Clear();
            if (txtNombre != null) txtNombre.Focus();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}