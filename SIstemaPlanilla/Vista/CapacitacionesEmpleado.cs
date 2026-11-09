using SIstemaPlanilla.Controlador;
using SIstemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class CapacitacionesEmpleado : Form
    {
        private CapacitacionEmpleadoControlador controlador = new CapacitacionEmpleadoControlador();

        private bool haySeleccion = false;
        private int idEmpleadoSel = 0;
        private int idCapacitacionSel = 0;
        private DateTime fechaOriginalSel;
        public CapacitacionesEmpleado()
        {
            InitializeComponent();
            Diseño();
        }

        private void CapacitacionesEmpleado_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarGrid();
        }

        private void CapacitacionesEmpleado_Activated(object sender, EventArgs e)
        {
            if (!haySeleccion) CargarCombos();
        }

        private void CargarCombos()
        {
            int empActual = cmbEmpleado.SelectedValue != null ? Convert.ToInt32(cmbEmpleado.SelectedValue) : 0;
            int capActual = cmbCapacitacion.SelectedValue != null ? Convert.ToInt32(cmbCapacitacion.SelectedValue) : 0;

            DataTable dtEmp = controlador.ListarEmpleadosCombo();
            DataRow filaEmp = dtEmp.NewRow();
            filaEmp["ID_EMPLEADO"] = 0;
            filaEmp["NOMBRE_COMPLETO"] = "-- Seleccione Empleado --";
            dtEmp.Rows.InsertAt(filaEmp, 0);

            cmbEmpleado.DataSource = dtEmp;
            cmbEmpleado.DisplayMember = "NOMBRE_COMPLETO";
            cmbEmpleado.ValueMember = "ID_EMPLEADO";
            cmbEmpleado.SelectedValue = empActual;

            DataTable dtCap = controlador.ListarCapacitacionesCombo();
            DataRow filaCap = dtCap.NewRow();
            filaCap["ID_CAPACITACION"] = 0;
            filaCap["NOMBRE_CAPACITACION"] = "-- Seleccione Capacitación --";
            dtCap.Rows.InsertAt(filaCap, 0);

            cmbCapacitacion.DataSource = dtCap;
            cmbCapacitacion.DisplayMember = "NOMBRE_CAPACITACION";
            cmbCapacitacion.ValueMember = "ID_CAPACITACION";
            cmbCapacitacion.SelectedValue = capActual;
        }

        private void CargarGrid()
        {
            try
            {
                dgvEmpleadoCapacitacion.DataSource = controlador.ListarRegistros();
                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnas()
        {
            if (dgvEmpleadoCapacitacion.Columns["ID_EMPLEADO"] != null) dgvEmpleadoCapacitacion.Columns["ID_EMPLEADO"].Visible = false;
            if (dgvEmpleadoCapacitacion.Columns["ID_CAPACITACION"] != null) dgvEmpleadoCapacitacion.Columns["ID_CAPACITACION"].Visible = false;
            if (dgvEmpleadoCapacitacion.Columns["FechaRealizacion"] != null)
                dgvEmpleadoCapacitacion.Columns["FechaRealizacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void Limpiar()
        {
            haySeleccion = false;
            idEmpleadoSel = 0;
            idCapacitacionSel = 0;

            cmbEmpleado.Enabled = true;
            cmbCapacitacion.Enabled = true;

            if (cmbEmpleado.Items.Count > 0) cmbEmpleado.SelectedIndex = 0;
            if (cmbCapacitacion.Items.Count > 0) cmbCapacitacion.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Today;
            txtCalificacion.Clear();
            cmbEmpleado.Focus();
        }

        private bool ValidarCampos(bool esNuevo, out decimal? calificacion)
        {
            calificacion = null;

            if (esNuevo)
            {
                if (Convert.ToInt32(cmbEmpleado.SelectedValue) == 0)
                {
                    MessageBox.Show("Seleccione un empleado.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (Convert.ToInt32(cmbCapacitacion.SelectedValue) == 0)
                {
                    MessageBox.Show("Seleccione una capacitación.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtCalificacion.Text))
            {
                if (!decimal.TryParse(txtCalificacion.Text, out decimal cal) || cal < 0 || cal > 100)
                {
                    MessageBox.Show("La calificación debe ser un número entre 0 y 100.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                calificacion = cal;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (haySeleccion)
            {
                MessageBox.Show("Hay un registro seleccionado. Presione \"Refrescar\" para crear uno nuevo o \"Actualizar\" para modificarlo.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos(true, out decimal? calificacion)) return;

            CapacitacionEmpleado registro = new CapacitacionEmpleado
            {
                IdEmpleado = Convert.ToInt32(cmbEmpleado.SelectedValue),
                IdCapacitacion = Convert.ToInt32(cmbCapacitacion.SelectedValue),
                FechaRealizacion = dtpFecha.Value.Date,
                Calificacion = calificacion
            };

            if (controlador.GuardarNuevoRegistro(registro, out string mensaje))
            {
                MessageBox.Show("Registro creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!haySeleccion)
            {
                MessageBox.Show("Seleccione un registro de la tabla haciendo clic sobre él.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos(false, out decimal? calificacion)) return;

            CapacitacionEmpleado registro = new CapacitacionEmpleado
            {
                IdEmpleado = idEmpleadoSel,
                IdCapacitacion = idCapacitacionSel,
                FechaOriginal = fechaOriginalSel,
                FechaRealizacion = dtpFecha.Value.Date,
                Calificacion = calificacion
            };

            if (controlador.ModificarRegistro(registro, out string mensaje))
            {
                MessageBox.Show("Registro modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!haySeleccion)
            {
                MessageBox.Show("Seleccione un registro de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.EliminarRegistro(idEmpleadoSel, idCapacitacionSel, fechaOriginalSel, out string mensaje))
                {
                    MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGrid();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
                CargarGrid();
            else
            {
                dgvEmpleadoCapacitacion.DataSource = controlador.BuscarRegistros(filtro);
                ConfigurarColumnas();
            }
        }

        private void dgvEmpleadoCapacitacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvEmpleadoCapacitacion.Rows[e.RowIndex];

            idEmpleadoSel = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);
            idCapacitacionSel = Convert.ToInt32(row.Cells["ID_CAPACITACION"].Value);
            fechaOriginalSel = Convert.ToDateTime(row.Cells["FechaRealizacion"].Value);
            haySeleccion = true;

            cmbEmpleado.SelectedValue = idEmpleadoSel;
            cmbCapacitacion.SelectedValue = idCapacitacionSel;
            dtpFecha.Value = fechaOriginalSel;

            txtCalificacion.Text = row.Cells["Calificacion"].Value == DBNull.Value
                ? string.Empty
                : Convert.ToDecimal(row.Cells["Calificacion"].Value).ToString("0.00");

            cmbEmpleado.Enabled = false;
            cmbCapacitacion.Enabled = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarGrid();
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

            Button[] botones = { btnGuardar, btnActualizar, btnEliminar, btnLimpiar };
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

            if (dgvEmpleadoCapacitacion != null)
            {
                dgvEmpleadoCapacitacion.BackgroundColor = cremaClaro;
                dgvEmpleadoCapacitacion.BorderStyle = BorderStyle.None;
                dgvEmpleadoCapacitacion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvEmpleadoCapacitacion.GridColor = azulClaro;
                dgvEmpleadoCapacitacion.EnableHeadersVisualStyles = false;
                dgvEmpleadoCapacitacion.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvEmpleadoCapacitacion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvEmpleadoCapacitacion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvEmpleadoCapacitacion.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvEmpleadoCapacitacion.ColumnHeadersHeight = 35;
                dgvEmpleadoCapacitacion.DefaultCellStyle.BackColor = Color.White;
                dgvEmpleadoCapacitacion.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvEmpleadoCapacitacion.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvEmpleadoCapacitacion.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvEmpleadoCapacitacion.RowTemplate.Height = 35;
                dgvEmpleadoCapacitacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvEmpleadoCapacitacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEmpleadoCapacitacion.ReadOnly = true;
                dgvEmpleadoCapacitacion.AllowUserToAddRows = false;
            }

            TextBox[] cajas = { txtCalificacion, txtBuscar };
            foreach (TextBox txt in cajas)
            {
                if (txt == null) continue;
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        private void txtCalificacion_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
