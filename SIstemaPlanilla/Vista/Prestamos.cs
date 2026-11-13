using SIstemaPlanilla.Controlador;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Prestamos : Form
    {
        private PlanilleroControlador controlador = new PlanilleroControlador();
        private int idPrestamoSeleccionado = 0;

        private TextBox txtBuscar, txtMontoTotal, txtCuotaQuincenal;
        private NumericUpDown nudPlazoQuincenas;
        private ComboBox cmbEmpleado;
        private DataGridView dgvPrestamos;
        private Button btnGuardar, btnHistorial, btnLimpiar;

        public Prestamos()
        {
            InitializeComponent();
            ConstruirInterfaz();
            Diseño();
        }

        private void Prestamos_Load(object sender, EventArgs e)
        {
            CargarComboEmpleados();
            CargarGrid();
        }

        private void ConstruirInterfaz()
        {
            this.Controls.Clear();
            this.Size = new Size(920, 560);

            Label lblTitulo = new Label { Text = "PRÉSTAMOS", Font = new Font("Segoe UI", 16F, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true };
            Label lblBuscar = new Label { Text = "Buscar", Font = new Font("Segoe UI", 9.5F), Location = new Point(140, 68), AutoSize = true };
            txtBuscar = new TextBox { Location = new Point(200, 65), Width = 480, Font = new Font("Segoe UI", 9.5F) };
            txtBuscar.TextChanged += (s, e) => CargarGrid(txtBuscar.Text.Trim());

            // Fila 1: Empleado y Monto Total
            Label lblEmp = new Label { Text = "Empleado", Font = new Font("Segoe UI", 9.5F), Location = new Point(50, 115), AutoSize = true };
            cmbEmpleado = new ComboBox { Location = new Point(145, 112), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };

            Label lblMonto = new Label { Text = "Monto Total ($)", Font = new Font("Segoe UI", 9.5F), Location = new Point(475, 115), AutoSize = true };
            txtMontoTotal = new TextBox { Location = new Point(605, 112), Width = 140, Font = new Font("Segoe UI", 9.5F) };
            txtMontoTotal.TextChanged += (s, e) => CalcularCuotaPorPlazo();

            // Fila 2: Plazo en quincenas y Cuota Quincenal calculada
            Label lblPlazo = new Label { Text = "Plazo (Quincenas)", Font = new Font("Segoe UI", 9.5F), Location = new Point(50, 155), AutoSize = true };
            nudPlazoQuincenas = new NumericUpDown { Location = new Point(195, 152), Width = 90, Minimum = 1, Maximum = 120, Value = 4, Font = new Font("Segoe UI", 9.5F) };
            nudPlazoQuincenas.ValueChanged += (s, e) => CalcularCuotaPorPlazo();

            Label lblCuota = new Label { Text = "Cuota Quincenal ($)", Font = new Font("Segoe UI", 9.5F), Location = new Point(320, 155), AutoSize = true };
            txtCuotaQuincenal = new TextBox { Location = new Point(475, 152), Width = 140, Font = new Font("Segoe UI", 9.5F) };

            dgvPrestamos = new DataGridView
            {
                Location = new Point(50, 205),
                Size = new Size(710, 305),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvPrestamos.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    idPrestamoSeleccionado = Convert.ToInt32(dgvPrestamos.Rows[e.RowIndex].Cells["ID_PRESTAMO"].Value);
            };
            dgvPrestamos.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) MostrarHistorialPagos(); };

            btnGuardar = new Button { Text = "Otorgar", Location = new Point(780, 205), Size = new Size(105, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnGuardar.Click += BtnGuardar_Click;

            btnHistorial = new Button { Text = "Ver Pagos", Location = new Point(780, 250), Size = new Size(105, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnHistorial.Click += (s, e) => MostrarHistorialPagos();

            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(780, 295), Size = new Size(105, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnLimpiar.Click += (s, e) => { Limpiar(); CargarGrid(); };

            this.Controls.AddRange(new Control[] {
                lblTitulo, lblBuscar, txtBuscar, lblEmp, cmbEmpleado, lblMonto, txtMontoTotal,
                lblPlazo, nudPlazoQuincenas, lblCuota, txtCuotaQuincenal,
                dgvPrestamos, btnGuardar, btnHistorial, btnLimpiar
            });
        }

        private void CargarComboEmpleados()
        {
            try
            {
                DataTable dt = controlador.ListarEmpleadosActivosCombo();
                DataRow row = dt.NewRow();
                row["ID_EMPLEADO"] = 0;
                row["EMPLEADO_DISPLAY"] = "-- Seleccione Empleado --";
                dt.Rows.InsertAt(row, 0);

                cmbEmpleado.DataSource = dt;
                cmbEmpleado.DisplayMember = "EMPLEADO_DISPLAY";
                cmbEmpleado.ValueMember = "ID_EMPLEADO";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                dgvPrestamos.DataSource = controlador.ListarPrestamos(filtro);
                if (dgvPrestamos.Columns["ID_PRESTAMO"] != null) dgvPrestamos.Columns["ID_PRESTAMO"].Visible = false;
                if (dgvPrestamos.Columns["ID_EMPLEADO"] != null) dgvPrestamos.Columns["ID_EMPLEADO"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar préstamos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularCuotaPorPlazo()
        {
            if (decimal.TryParse(txtMontoTotal.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto) && monto > 0)
            {
                decimal cuota = Math.Round(monto / nudPlazoQuincenas.Value, 2);
                txtCuotaQuincenal.Text = cuota.ToString("0.00", CultureInfo.InvariantCulture);
            }
            else
            {
                txtCuotaQuincenal.Clear();
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            int idEmpleado = cmbEmpleado.SelectedValue != null ? Convert.ToInt32(cmbEmpleado.SelectedValue) : 0;
            if (idEmpleado == 0)
            {
                MessageBox.Show("Seleccione un empleado activo.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMontoTotal.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto) || monto <= 0 ||
                !decimal.TryParse(txtCuotaQuincenal.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cuota) || cuota <= 0)
            {
                MessageBox.Show("Ingrese un monto total y cuota quincenal mayores a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cuota > monto)
            {
                MessageBox.Show("La cuota quincenal no puede ser mayor al monto total del préstamo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idGenerado = controlador.RegistrarPrestamo(idEmpleado, monto, cuota);
                MessageBox.Show($"Préstamo #{idGenerado} otorgado correctamente ({nudPlazoQuincenas.Value} quincenas de ${cuota:N2}).\nSe descontará automáticamente en la próxima planilla.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar préstamo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarHistorialPagos()
        {
            if (idPrestamoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un préstamo de la tabla para ver sus abonos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable historial = controlador.ConsultarHistorialPagosPrestamo(idPrestamoSeleccionado);
                Form popup = new Form
                {
                    Text = $"Historial de Abonos - Préstamo #{idPrestamoSeleccionado}",
                    Size = new Size(680, 350),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = ColorTranslator.FromHtml("#F0EBD8")
                };

                DataGridView dgvHist = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = historial,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    BackgroundColor = ColorTranslator.FromHtml("#F0EBD8"),
                    BorderStyle = BorderStyle.None
                };

                popup.Controls.Add(dgvHist);
                popup.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar historial: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            idPrestamoSeleccionado = 0;
            if (cmbEmpleado.Items.Count > 0) cmbEmpleado.SelectedIndex = 0;
            txtMontoTotal.Clear();
            nudPlazoQuincenas.Value = 4;
            txtCuotaQuincenal.Clear();
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

            foreach (Button btn in new[] { btnGuardar, btnHistorial, btnLimpiar })
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

            dgvPrestamos.BackgroundColor = cremaClaro;
            dgvPrestamos.BorderStyle = BorderStyle.None;
            dgvPrestamos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPrestamos.GridColor = azulClaro;
            dgvPrestamos.EnableHeadersVisualStyles = false;
            dgvPrestamos.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
            dgvPrestamos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPrestamos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPrestamos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPrestamos.ColumnHeadersHeight = 50;
            dgvPrestamos.DefaultCellStyle.BackColor = Color.White;
            dgvPrestamos.DefaultCellStyle.ForeColor = azulMuyOscuro;
            dgvPrestamos.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvPrestamos.DefaultCellStyle.SelectionBackColor = azulClaro;
            dgvPrestamos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPrestamos.RowTemplate.Height = 35;

            foreach (TextBox txt in new[] { txtBuscar, txtMontoTotal, txtCuotaQuincenal })
            {
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }
    }
}