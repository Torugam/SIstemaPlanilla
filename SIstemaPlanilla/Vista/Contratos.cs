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
    public partial class Contratos : Form
    {
        private int idEmpleadoSeleccionado = 0;
        private readonly ContratoControlador controlador = new ContratoControlador();

        public Contratos()
        {
            InitializeComponent();

            if (btnModificar != null) btnModificar.Click += btnModificar_Click;
            if (btnEliminar != null) btnEliminar.Click += btnEliminar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;
            if (btnBuscar != null) btnBuscar.Click += btnBuscar_Click;

            if (dgvContratos != null)
            {
                dgvContratos.CellClick += dgvContratos_CellClick;
                dgvContratos.CellContentClick += dgvContratos_CellContentClick;
            }

            Diseño();

            this.Load += Contratos_Load;
            this.Activated += Contratos_Activated;
        }

        // Método para aplicar el diseño personalizado a los controles del formulario
        private void Diseño()
        {
            Color azulMuyOscuro = ColorTranslator.FromHtml("#0D1321");
            Color azulOscuro = ColorTranslator.FromHtml("#1D2D44");
            Color azulAcero = ColorTranslator.FromHtml("#3E5C76");
            Color azulClaro = ColorTranslator.FromHtml("#748CAB");
            Color cremaClaro = ColorTranslator.FromHtml("#F0EBD8");

            this.BackColor = cremaClaro;
            this.ForeColor = azulMuyOscuro;

            Button[] botones = { btnGuardar, btnModificar, btnEliminar, btnLimpiar, btnBuscar };
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

            if (dgvContratos != null)
            {
                dgvContratos.BackgroundColor = cremaClaro;
                dgvContratos.BorderStyle = BorderStyle.None;
                dgvContratos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvContratos.GridColor = azulClaro;
                dgvContratos.EnableHeadersVisualStyles = false;

                dgvContratos.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvContratos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvContratos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvContratos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvContratos.ColumnHeadersHeight = 35;

                dgvContratos.DefaultCellStyle.BackColor = Color.White;
                dgvContratos.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvContratos.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                dgvContratos.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvContratos.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvContratos.RowTemplate.Height = 35;
            }

            TextBox[] cajasDeTexto = { txtSalarioBase, txtBuscar };
            foreach (TextBox txt in cajasDeTexto)
            {
                if (txt == null) continue;
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        private void Contratos_Load(object? sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarContratos();
            CargarComboEmpleados();
            CargarComboTipoContrato();
            LimpiarCampos();
        }

        private void Contratos_Activated(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0) CargarComboEmpleados();
        }

        // Configuración del DataGridView para mostrar los contratos
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

        // Carga de contratos en el DataGridView con un filtro opcional

        private void CargarContratos(string filtro = "")
        {
            try
            {
                if (dgvContratos != null)
                {
                    dgvContratos.DataSource = controlador.ListarContratos(filtro);
                    OcultarColumnasId();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los contratos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carga de empleados en el ComboBox, incluyendo al empleado actualmente seleccionado si no está en la lista
        private void CargarComboEmpleados()
        {
            try
            {
                int empActual = 0;
                string nombreActual = "";

                if (cbEmpleados != null && cbEmpleados.SelectedValue != null && cbEmpleados.SelectedValue != DBNull.Value)
                {
                    if (int.TryParse(cbEmpleados.SelectedValue.ToString(), out int id))
                    {
                        empActual = id;
                        nombreActual = cbEmpleados.Text;
                    }
                }

                DataTable dt = controlador.ListarEmpleadosSinContrato();

                if (empActual > 0 && !dt.AsEnumerable().Any(r => Convert.ToInt32(r["ID_EMPLEADO"]) == empActual))
                {
                    DataRow actual = dt.NewRow();
                    actual["ID_EMPLEADO"] = empActual;
                    actual["NOMBRE_COMPLETO"] = nombreActual;
                    dt.Rows.Add(actual);
                }

                DataRow fila = dt.NewRow();
                fila["ID_EMPLEADO"] = 0;
                fila["NOMBRE_COMPLETO"] = "-- Seleccione un Empleado --";
                dt.Rows.InsertAt(fila, 0);

                if (cbEmpleados != null)
                {
                    cbEmpleados.DataSource = dt;
                    cbEmpleados.DisplayMember = "NOMBRE_COMPLETO";
                    cbEmpleados.ValueMember = "ID_EMPLEADO";
                    cbEmpleados.SelectedValue = empActual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Carga de tipos de contrato en el ComboBox

        private void CargarComboTipoContrato()
        {
            try
            {
                DataTable dtTipo = new DataTable();
                dtTipo.Columns.Add("Valor", typeof(string));
                dtTipo.Columns.Add("Texto", typeof(string));

                dtTipo.Rows.Add("", "-- Seleccione Tipo --");
                dtTipo.Rows.Add("PERMANENTE", "Permanente");
                dtTipo.Rows.Add("TEMPORAL", "Temporal");

                if (cbTipoContrato != null)
                {
                    cbTipoContrato.SelectedIndexChanged -= cbTipoContrato_SelectedIndexChanged;

                    cbTipoContrato.DataSource = dtTipo;
                    cbTipoContrato.DisplayMember = "Texto";
                    cbTipoContrato.ValueMember = "Valor";
                    cbTipoContrato.SelectedIndex = 0;

                    cbTipoContrato.SelectedIndexChanged += cbTipoContrato_SelectedIndexChanged;
                }

                ActualizarVisibilidadFechas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Actualiza la visibilidad de los controles de fecha según el tipo de contrato seleccionado
        private void cbTipoContrato_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarVisibilidadFechas();
        }

        // Maneja el evento de clic en el botón Guardar para registrar un nuevo contrato
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            decimal.TryParse(txtSalarioBase?.Text.Trim(), out decimal salario);

            Contrato nuevo = new Contrato
            {
                IdEmpleado = cbEmpleados?.SelectedValue != null ? Convert.ToInt32(cbEmpleados.SelectedValue) : 0,
                TipoContrato = cbTipoContrato?.SelectedValue?.ToString()?.Trim().ToUpper() ?? "",
                SalarioBase = salario,
                FechaInicio = dtpFechaInicio?.Value ?? DateTime.Now,
                FechaFin = dtpFechaFin?.Value ?? DateTime.Now
            };

            if (controlador.GuardarNuevoContrato(nuevo, out string mensaje))
            {
                MessageBox.Show("Contrato registrado exitosamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarContratos();
            }
            else
            {
                MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Maneja el evento de clic en el botón Modificar para actualizar un contrato existente
        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un contrato de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(txtSalarioBase?.Text.Trim(), out decimal salario);

            Contrato actualizado = new Contrato
            {
                IdEmpleado = idEmpleadoSeleccionado,
                TipoContrato = cbTipoContrato?.SelectedValue?.ToString()?.ToUpper() ?? "",
                SalarioBase = salario,
                FechaInicio = dtpFechaInicio?.Value ?? DateTime.Now,
                FechaFin = dtpFechaFin?.Value ?? DateTime.Now
            };

            if (controlador.ModificarContrato(actualizado, out string mensaje))
            {
                MessageBox.Show("Contrato actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarContratos();
                CargarComboEmpleados();
            }
            else
            {
                MessageBox.Show("Error al modificar el contrato: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Actualiza la visibilidad de los controles de fecha según el tipo de contrato seleccionado

        private void ActualizarVisibilidadFechas()
        {
            if (cbTipoContrato == null) return;

            string tipo = cbTipoContrato.SelectedValue?.ToString()?.ToUpper().Trim() ?? "";

            if (dtpFechaInicio != null)
            {
                dtpFechaInicio.Enabled = true;
            }

            if (tipo == "PERMANENTE" || tipo == "")
            {
                if (dtpFechaFin != null)
                {
                    dtpFechaFin.Enabled = false;
                    dtpFechaFin.Value = DateTime.Now;
                }
            }
            else if (tipo == "TEMPORAL")
            {
                if (dtpFechaFin != null)
                {
                    dtpFechaFin.Enabled = true;
                }
            }
        }

        // Maneja el evento de clic en el botón Eliminar para eliminar un contrato seleccionado
        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un contrato para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este contrato y sus registros asociados?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.EliminarContrato(idEmpleadoSeleccionado, out string mensaje))
                {
                    MessageBox.Show("Contrato eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarContratos();
                    CargarComboEmpleados();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el contrato: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Maneja el evento de clic en una celda del DataGridView para seleccionar un contrato y mostrar sus detalles en los controles correspondientes
        private void dgvContratos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvContratos != null)
            {
                DataGridViewRow row = dgvContratos.Rows[e.RowIndex];
                idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);
                string nombre = row.Cells["NOMBRE_COMPLETO"].Value?.ToString() ?? "";
                SeleccionarEmpleadoEnCombo(idEmpleadoSeleccionado, nombre);

                if (cbEmpleados != null)
                {
                    cbEmpleados.SelectedValue = idEmpleadoSeleccionado;
                }

                string tipo = row.Cells["TIPO_CONTRATO"].Value.ToString() ?? "";
                if (cbTipoContrato != null)
                {
                    cbTipoContrato.SelectedValue = tipo.ToUpper().Trim();
                }

                ActualizarVisibilidadFechas();

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

        // Selecciona un empleado en el ComboBox, agregándolo si no está presente en la lista
        private void SeleccionarEmpleadoEnCombo(int id, string nombre)
        {
            if (cbEmpleados == null) return;

            DataTable dt = controlador.ListarEmpleadosSinContrato();

            if (!dt.AsEnumerable().Any(r => Convert.ToInt32(r["ID_EMPLEADO"]) == id))
            {
                DataRow actual = dt.NewRow();
                actual["ID_EMPLEADO"] = id;
                actual["NOMBRE_COMPLETO"] = nombre;
                dt.Rows.Add(actual);
            }

            DataRow vacio = dt.NewRow();
            vacio["ID_EMPLEADO"] = 0;
            vacio["NOMBRE_COMPLETO"] = "-- Seleccione un Empleado --";
            dt.Rows.InsertAt(vacio, 0);

            cbEmpleados.DataSource = dt;
            cbEmpleados.DisplayMember = "NOMBRE_COMPLETO";
            cbEmpleados.ValueMember = "ID_EMPLEADO";
            cbEmpleados.SelectedValue = id;
        }

        // Maneja el evento de clic en el contenido de una celda del DataGridView para seleccionar un contrato y mostrar sus detalles en los controles correspondientes
        private void dgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            dgvContratos_CellClick(sender, e);
        }

        // Limpia los campos del formulario y reinicia la selección de empleado y tipo de contrato
        private void LimpiarCampos()
        {
            idEmpleadoSeleccionado = 0;

            if (cbEmpleados != null) cbEmpleados.SelectedIndex = -1; 
            CargarComboEmpleados();

            if (cbTipoContrato != null && cbTipoContrato.Items.Count > 0) cbTipoContrato.SelectedIndex = 0;
            if (txtSalarioBase != null) txtSalarioBase.Clear();
            if (txtBuscar != null) txtBuscar.Clear();
            if (dtpFechaInicio != null) dtpFechaInicio.Value = DateTime.Now;
            if (dtpFechaFin != null) dtpFechaFin.Value = DateTime.Now;
        }

        // Maneja el evento de clic en el botón Limpiar para restablecer los campos del formulario
        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
        // Oculta las columnas de ID en el DataGridView para mejorar la presentación

        private void OcultarColumnasId()
        {
            if (dgvContratos == null) return;

            foreach (DataGridViewColumn c in dgvContratos.Columns)
            {
                if (c.Name.Equals("ID_EMPLEADO", StringComparison.OrdinalIgnoreCase))
                    c.Visible = false;
            }
        }

        // Maneja el evento de clic en el botón Buscar para filtrar los contratos por nombre de empleado
        private void btnBuscar_Click(object? sender, EventArgs e)
        {
            string nombreBuscado = txtBuscar?.Text.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nombreBuscado))
            {
                MessageBox.Show("Por favor ingrese el nombre del empleado para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarContratos();
                txtBuscar?.Focus();
                return;
            }

            try
            {
                DataTable dt = controlador.ListarContratos(nombreBuscado);

                if (dgvContratos != null)
                {
                    dgvContratos.DataSource = dt;
                    OcultarColumnasId();
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];

                    idEmpleadoSeleccionado = Convert.ToInt32(fila["ID_EMPLEADO"]);
                    string nombreEmp = fila["NOMBRE_COMPLETO"]?.ToString() ?? "";

                    if (cbEmpleados != null)
                    {
                        if (cbEmpleados.DataSource is DataTable dtEmp)
                        {
                            bool existe = dtEmp.AsEnumerable().Any(r => Convert.ToInt32(r["ID_EMPLEADO"]) == idEmpleadoSeleccionado);
                            if (!existe)
                            {
                                DataRow nuevaFila = dtEmp.NewRow();
                                nuevaFila["ID_EMPLEADO"] = idEmpleadoSeleccionado;
                                nuevaFila["NOMBRE_COMPLETO"] = nombreEmp;
                                dtEmp.Rows.Add(nuevaFila);
                            }
                        }
                        cbEmpleados.SelectedValue = idEmpleadoSeleccionado;
                    }

                    string tipo = fila["TIPO_CONTRATO"]?.ToString() ?? "";
                    if (cbTipoContrato != null)
                    {
                        cbTipoContrato.SelectedValue = tipo.ToUpper().Trim();
                    }

                    ActualizarVisibilidadFechas();

                    if (txtSalarioBase != null)
                        txtSalarioBase.Text = fila["SALARIO_BASE"]?.ToString() ?? "";

                    if (fila["FECHA_INICIO"] != DBNull.Value && dtpFechaInicio != null)
                        dtpFechaInicio.Value = Convert.ToDateTime(fila["FECHA_INICIO"]);

                    if (tipo.Equals("Temporal", StringComparison.OrdinalIgnoreCase) && fila["FECHA_FIN"] != DBNull.Value && dtpFechaFin != null)
                        dtpFechaFin.Value = Convert.ToDateTime(fila["FECHA_FIN"]);
                }
                else
                {
                    MessageBox.Show("No se encontró ningún contrato para ese empleado.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarContratos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}