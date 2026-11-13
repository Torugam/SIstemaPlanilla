using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Controlador;
using SIstemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    //Comentario de prueba para ver si se sube al repositorio. Ignorenlo
    public partial class Empleado : Form
    {
        private int idEmpleadoSeleccionado = 0;
        private Button btnReactivar = null!;
        private readonly EmpleadoControlador controlador = new EmpleadoControlador();

        public Empleado()
        {
            InitializeComponent();

            bool enModoDiseno = this.DesignMode || (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

            if (!enModoDiseno)
            {
                CrearBotonReactivarPorCodigo();
            }

            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
            if (btnModificar != null) btnModificar.Click += btnModificar_Click;
            if (btnEliminar != null) btnEliminar.Click += btnEliminar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;
            if (btnBuscar != null) btnBuscar.Click += btnBuscar_Click;
            if (dgvEmpleados != null)
            {
                dgvEmpleados.CellClick += dgvEmpleados_CellClick;
                dgvEmpleados.CellContentClick += dgvEmpleados_CellContentClick;
            }
            Diseño();
            this.Load += Empleado_Load;
            this.Activated += Empleados_Activated;
        }

        private void Diseño()
        {
            Color azulMuyOscuro = ColorTranslator.FromHtml("#0D1321");
            Color azulOscuro = ColorTranslator.FromHtml("#1D2D44");
            Color azulAcero = ColorTranslator.FromHtml("#3E5C76");
            Color azulClaro = ColorTranslator.FromHtml("#748CAB");
            Color cremaClaro = ColorTranslator.FromHtml("#F0EBD8");

            this.BackColor = cremaClaro;
            this.ForeColor = azulMuyOscuro;

            Button[] botones = { btnGuardar, btnModificar, btnEliminar, btnReactivar, btnLimpiar, btnBuscar };
            foreach (Button btn in botones)
            {
                if (btn == null) continue;

                btn.BackColor = azulAcero;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                btn.FlatAppearance.MouseOverBackColor = azulOscuro;
            }

            if (dgvEmpleados != null)
            {
                dgvEmpleados.BackgroundColor = cremaClaro;
                dgvEmpleados.BorderStyle = BorderStyle.None;
                dgvEmpleados.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvEmpleados.GridColor = azulClaro;
                dgvEmpleados.EnableHeadersVisualStyles = false;

                dgvEmpleados.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvEmpleados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvEmpleados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvEmpleados.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvEmpleados.ColumnHeadersHeight = 35;

                dgvEmpleados.DefaultCellStyle.BackColor = Color.White;
                dgvEmpleados.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvEmpleados.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                dgvEmpleados.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvEmpleados.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvEmpleados.RowTemplate.Height = 35;
            }

            TextBox[] cajasDeTexto = { txtNombre, txtDui, txtDireccion, txtTelefono, txtBuscar };
            foreach (TextBox txt in cajasDeTexto)
            {
                if (txt == null) continue;
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        private void CrearBotonReactivarPorCodigo()
        {
            btnReactivar = new Button();
            btnReactivar.Text = "Reactivar";
            btnReactivar.Size = btnEliminar != null ? btnEliminar.Size : new Size(75, 25);

            if (btnEliminar != null && btnLimpiar != null)
            {
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

            if (cbCargos != null)
            {
                cbCargos.DropDown += (s, ev) => CargarComboCargos();
            }
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
                dgvEmpleados.Height = 280;
                dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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
                if (dgvEmpleados != null)
                {
                    dgvEmpleados.DataSource = controlador.ListarEmpleados(filtro);
                    if (dgvEmpleados.Columns["ID_CARGO"] != null)
                        dgvEmpleados.Columns["ID_CARGO"].Visible = false;
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
                int cargoActual = (cbCargos != null && cbCargos.SelectedValue != null && cbCargos.SelectedValue != DBNull.Value)
                    ? Convert.ToInt32(cbCargos.SelectedValue)
                    : 0;

                DataTable dt = controlador.ListarCargosCombo();

                DataRow fila = dt.NewRow();
                fila["ID_CARGO"] = 0;
                fila["NOMBRE_CARGO"] = "-- Seleccione un Cargo --";
                dt.Rows.InsertAt(fila, 0);

                if (cbCargos != null)
                {
                    cbCargos.DataSource = dt;
                    cbCargos.DisplayMember = "NOMBRE_CARGO";
                    cbCargos.ValueMember = "ID_CARGO";
                    cbCargos.SelectedValue = cargoActual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cargos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre?.Text) || string.IsNullOrWhiteSpace(txtDui?.Text) || cbCargos?.SelectedValue == null || Convert.ToInt32(cbCargos.SelectedValue) == 0)
            {
                MessageBox.Show("Por favor complete los campos obligatorios y seleccione un cargo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SIstemaPlanilla.Modelo.Empleado nuevo = new SIstemaPlanilla.Modelo.Empleado
            {
                NombreCompleto = txtNombre.Text.Trim(),
                Dui = txtDui.Text.Trim(),
                Direccion = txtDireccion?.Text.Trim() ?? "",
                Telefono = txtTelefono?.Text.Trim() ?? "",
                IdCargo = Convert.ToInt32(cbCargos.SelectedValue)
            };

            if (controlador.GuardarNuevoEmpleado(nuevo, out string mensaje))
            {
                MessageBox.Show("Empleado registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarEmpleados();
            }
            else
            {
                MessageBox.Show("Error al guardar el empleado: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            SIstemaPlanilla.Modelo.Empleado actualizado = new SIstemaPlanilla.Modelo.Empleado
            {
                IdEmpleado = idEmpleadoSeleccionado,
                NombreCompleto = txtNombre?.Text.Trim() ?? "",
                Dui = txtDui?.Text.Trim() ?? "",
                Direccion = txtDireccion?.Text.Trim() ?? "",
                Telefono = txtTelefono?.Text.Trim() ?? "",
                IdCargo = Convert.ToInt32(cbCargos?.SelectedValue ?? 0)
            };

            if (controlador.ModificarEmpleado(actualizado, out string mensaje))
            {
                MessageBox.Show("Empleado actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarEmpleados();
            }
            else
            {
                MessageBox.Show("Error al actualizar el empleado: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (controlador.CambiarEstadoEmpleado(idEmpleadoSeleccionado, "INACTIVO", out string mensaje))
                {
                    MessageBox.Show("Empleado dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarEmpleados();
                }
                else
                {
                    MessageBox.Show("Error al dar de baja el empleado: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (controlador.CambiarEstadoEmpleado(idEmpleadoSeleccionado, "ACTIVO", out string mensaje))
                {
                    MessageBox.Show("Empleado reactivado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarEmpleados();
                }
                else
                {
                    MessageBox.Show("Error al reactivar el empleado: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Empleados_Activated(object? sender, EventArgs e)
        {
            CargarComboCargos();
        }

        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            string nombreBuscado = txtBuscar?.Text.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nombreBuscado))
            {
                MessageBox.Show("Por favor ingrese un nombre para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarEmpleados();
                txtBuscar?.Focus();
                return;
            }

            try
            {
                DataTable dt = controlador.ListarEmpleados(nombreBuscado);

                if (dgvEmpleados != null)
                {
                    dgvEmpleados.DataSource = dt;
                    if (dgvEmpleados.Columns["ID_CARGO"] != null)
                        dgvEmpleados.Columns["ID_CARGO"].Visible = false;
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];

                    idEmpleadoSeleccionado = Convert.ToInt32(fila["ID_EMPLEADO"]);

                    if (txtNombre != null) txtNombre.Text = fila["NOMBRE_COMPLETO"]?.ToString() ?? "";
                    if (txtDui != null) txtDui.Text = fila["DUI"]?.ToString() ?? "";
                    if (txtDireccion != null) txtDireccion.Text = fila["DIRECCION"]?.ToString() ?? "";
                    if (txtTelefono != null) txtTelefono.Text = fila["TELEFONO"]?.ToString() ?? "";

                    if (cbCargos != null && fila["ID_CARGO"] != DBNull.Value)
                    {
                        cbCargos.SelectedValue = Convert.ToInt32(fila["ID_CARGO"]);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún empleado con ese nombre.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}