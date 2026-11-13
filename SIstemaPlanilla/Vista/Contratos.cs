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
    public partial class Contratos : Form
    {
        private int idEmpleadoSeleccionado = 0;
        private readonly ContratoControlador controlador = new ContratoControlador();

        public Contratos()
        {
            InitializeComponent();

            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
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
            CargarComboEmpleados();
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
                if (dgvContratos != null)
                {
                    dgvContratos.DataSource = controlador.ListarContratos(filtro);
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
                int empActual = (cbEmpleados != null && cbEmpleados.SelectedValue != null && cbEmpleados.SelectedValue != DBNull.Value)
                    ? Convert.ToInt32(cbEmpleados.SelectedValue)
                    : 0;

                DataTable dt = controlador.ListarEmpleadosSinContrato();

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

        private void cbTipoContrato_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ActualizarVisibilidadFechas();
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            /*if (cbEmpleados?.SelectedValue == null || Convert.ToInt32(cbEmpleados.SelectedValue) == 0 ||
                cbTipoContrato?.SelectedValue == null || string.IsNullOrWhiteSpace(cbTipoContrato.SelectedValue.ToString()))
            {
                MessageBox.Show("Por favor seleccione un empleado y el tipo de contrato.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }*/

            decimal.TryParse(txtSalarioBase?.Text.Trim(), out decimal salario);

            Contrato nuevo = new Contrato
            {
                IdEmpleado = Convert.ToInt32(cbEmpleados.SelectedValue),
                TipoContrato = cbTipoContrato.SelectedValue.ToString()!.Trim().ToUpper(),
                SalarioBase = salario,
                FechaInicio = dtpFechaInicio?.Value ?? DateTime.Now,
                FechaFin = dtpFechaFin?.Value ?? DateTime.Now.AddMonths(6)
            };

            if (controlador.GuardarNuevoContrato(nuevo, out string mensaje))
            {
                MessageBox.Show("Contrato registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarContratos();
                CargarComboEmpleados();
            }
            else
            {
                MessageBox.Show("Error al guardar el contrato: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void dgvContratos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvContratos != null)
            {
                DataGridViewRow row = dgvContratos.Rows[e.RowIndex];
                idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);

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

        private void dgvContratos_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            dgvContratos_CellClick(sender, e);
        }

        private void LimpiarCampos()
        {
            idEmpleadoSeleccionado = 0;
            CargarComboEmpleados();
            if (cbTipoContrato != null && cbTipoContrato.Items.Count > 0) cbTipoContrato.SelectedIndex = 0;
            if (txtSalarioBase != null) txtSalarioBase.Clear();
            if (txtBuscar != null) txtBuscar.Clear();
            if (dtpFechaInicio != null) dtpFechaInicio.Value = DateTime.Now;
            if (dtpFechaFin != null) dtpFechaFin.Value = DateTime.Now;
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }

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