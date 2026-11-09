using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Controlador;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Cargos : Form
    {
        private CargoControlador controlador = new CargoControlador();
        private int idCargoSeleccionado = 0;

        public Cargos()
        {
            InitializeComponent();
            Diseño();

        }



        private void Cargos_Load(object? sender, EventArgs e)
        {
            CargarCombo();
            CargarGrid();
        }

        private void CargarCombo()
        {
            DataTable dt = controlador.ListarCargosCombo();

            DataRow fila = dt.NewRow();
            fila["ID_CARGO"] = 0;
            fila["NOMBRE_CARGO"] = "-- Sin cargo superior --";
            dt.Rows.InsertAt(fila, 0);

            cbCargoSuperior.DataSource = dt;
            cbCargoSuperior.DisplayMember = "NOMBRE_CARGO";
            cbCargoSuperior.ValueMember = "ID_CARGO";
            cbCargoSuperior.SelectedIndex = 0;
        }

        private void CargarGrid()
        {
            try
            {
                dgvCargos.DataSource = controlador.ListarCargos();
                OcultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OcultarColumnas()
        {
            if (dgvCargos.Columns["ID_CARGO"] != null) dgvCargos.Columns["ID_CARGO"].Visible = false;
            if (dgvCargos.Columns["CAR_ID_CARGO"] != null) dgvCargos.Columns["CAR_ID_CARGO"].Visible = false;

            if (dgvCargos.Columns["SalarioMinimo"] != null) dgvCargos.Columns["SalarioMinimo"].DefaultCellStyle.Format = "C2";
            if (dgvCargos.Columns["SalarioMaximo"] != null) dgvCargos.Columns["SalarioMaximo"].DefaultCellStyle.Format = "C2";
        }




        private void Limpiar()
        {
            idCargoSeleccionado = 0;
            txtNombreCargo.Clear();
            txtSalarioMin.Clear();
            txtSalarioMax.Clear();
            if (cbCargoSuperior.Items.Count > 0) cbCargoSuperior.SelectedIndex = 0;
            txtNombreCargo.Focus();
        }


        private bool ValidarCampos(out decimal salMin, out decimal salMax)
        {
            salMin = 0;
            salMax = 0;

            if (string.IsNullOrWhiteSpace(txtNombreCargo.Text))
            {
                MessageBox.Show("El nombre del cargo es obligatorio.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtSalarioMin.Text, out salMin) || salMin < 0)
            {
                MessageBox.Show("Ingrese un salario mínimo válido.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(txtSalarioMax.Text, out salMax) || salMax < 0)
            {
                MessageBox.Show("Ingrese un salario máximo válido.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (salMin > salMax)
            {
                MessageBox.Show("El salario mínimo no puede ser mayor que el máximo.", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private Cargo ConstruirCargo(decimal salMin, decimal salMax)
        {
            int idSuperior = Convert.ToInt32(cbCargoSuperior.SelectedValue);

            return new Cargo
            {
                IdCargo = idCargoSeleccionado,
                IdCargoSuperior = idSuperior == 0 ? (int?)null : idSuperior,
                NombreCargo = txtNombreCargo.Text.Trim(),
                SalarioMin = salMin,
                SalarioMax = salMax
            };
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

            Button[] botones = { btnGuardar, btnModificar, btnEliminar, btnLimpiar };
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

            if (dgvCargos != null)
            {
                dgvCargos.BackgroundColor = cremaClaro;
                dgvCargos.BorderStyle = BorderStyle.None;
                dgvCargos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvCargos.GridColor = azulClaro;
                dgvCargos.EnableHeadersVisualStyles = false;
                dgvCargos.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvCargos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvCargos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvCargos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvCargos.ColumnHeadersHeight = 35;
                dgvCargos.DefaultCellStyle.BackColor = Color.White;
                dgvCargos.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvCargos.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvCargos.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvCargos.RowTemplate.Height = 35;
                dgvCargos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCargos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCargos.ReadOnly = true;
                dgvCargos.AllowUserToAddRows = false;
            }

            TextBox[] cajas = { txtNombreCargo, txtSalarioMin, txtSalarioMax, txtBuscar };
            foreach (TextBox txt in cajas)
            {
                if (txt == null) continue;
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos(out decimal salMin, out decimal salMax)) return;

            Cargo cargo = ConstruirCargo(salMin, salMax);

            if (controlador.GuardarNuevoCargo(cargo, out string mensaje))
            {
                MessageBox.Show("Cargo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarCombo();
                CargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (idCargoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cargo de la tabla haciendo clic sobre él.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos(out decimal salMin, out decimal salMax)) return;

            Cargo cargo = ConstruirCargo(salMin, salMax);

            if (controlador.ModificarCargo(cargo, out string mensaje))
            {
                MessageBox.Show("Cargo modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarCombo();
                CargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (idCargoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cargo de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este cargo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.EliminarCargo(idCargoSeleccionado, out string mensaje))
                {
                    MessageBox.Show("Cargo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarCombo();
                    CargarGrid();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {

            Limpiar();
            CargarGrid();
        }

        private void dgvCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvCargos.Rows[e.RowIndex];

            idCargoSeleccionado = Convert.ToInt32(row.Cells["ID_CARGO"].Value);
            txtNombreCargo.Text = row.Cells["Cargo"].Value.ToString();
            txtSalarioMin.Text = Convert.ToDecimal(row.Cells["SalarioMinimo"].Value).ToString("0.00");
            txtSalarioMax.Text = Convert.ToDecimal(row.Cells["SalarioMaximo"].Value).ToString("0.00");

            if (row.Cells["CAR_ID_CARGO"].Value != DBNull.Value)
            {
                cbCargoSuperior.SelectedValue = Convert.ToInt32(row.Cells["CAR_ID_CARGO"].Value);
            }

            else
            {
                cbCargoSuperior.SelectedIndex = 0;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
                CargarGrid();
            else
            {
                dgvCargos.DataSource = controlador.BuscarCargos(filtro);
                OcultarColumnas();
            }
        }
    }
}