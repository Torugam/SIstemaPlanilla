using System;
using SIstemaPlanilla.Controlador;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Planillas : Form // <-- Ajusta a 'planilla' o 'Planillas' si tu archivo .Designer.cs usa ese nombre
    {
        private PlanilleroControlador controlador = new PlanilleroControlador();
        private int idPlanillaActual = 0;
        private int idEmpleadoSeleccionado = 0;

        private ComboBox cmbQuincena, cmbMes;
        private NumericUpDown nudAnio;
        private TextBox txtBuscar, txtHorasExtra, txtBonificaciones;
        private Label lblEmpleadoSel, lblTotalPlanilla;
        private DataGridView dgvPlanilla;
        private Button btnGenerar, btnConsultar, btnAplicarExtras, btnBoleta;

        public Planillas()
        {
            InitializeComponent();
            ConstruirInterfaz();
            Diseño();
        }

        private void Planillas_Load(object sender, EventArgs e)
        {
            ConsultarPlanilla();
        }

        private void ConstruirInterfaz()
        {
            this.Controls.Clear();
            this.Size = new Size(960, 580);

            Label lblTitulo = new Label { Text = "PLANILLAS", Font = new Font("Segoe UI", 16F, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true };

            // Fila 1: Selección de Periodo y Búsqueda Dinámica
            Label lblQuincena = new Label { Text = "Quincena:", Font = new Font("Segoe UI", 9.5F), Location = new Point(30, 68), AutoSize = true };
            cmbQuincena = new ComboBox { Location = new Point(108, 65), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbQuincena.Items.AddRange(new object[] { "1 - Primera Quincena", "2 - Segunda Quincena" });
            cmbQuincena.SelectedIndex = DateTime.Now.Day <= 15 ? 0 : 1;

            Label lblMes = new Label { Text = "Mes:", Font = new Font("Segoe UI", 9.5F), Location = new Point(300, 68), AutoSize = true };
            cmbMes = new ComboBox { Location = new Point(342, 65), Width = 115, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9.5F) };
            cmbMes.Items.AddRange(new object[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" });
            cmbMes.SelectedIndex = DateTime.Now.Month - 1;

            Label lblAnio = new Label { Text = "Año:", Font = new Font("Segoe UI", 9.5F), Location = new Point(470, 68), AutoSize = true };
            nudAnio = new NumericUpDown { Location = new Point(512, 65), Width = 72, Minimum = 2020, Maximum = 2100, Value = DateTime.Now.Year, Font = new Font("Segoe UI", 9.5F) };

            Label lblBuscar = new Label { Text = "Buscar:", Font = new Font("Segoe UI", 9.5F), Location = new Point(596, 68), AutoSize = true };
            txtBuscar = new TextBox { Location = new Point(656, 65), Width = 164, Font = new Font("Segoe UI", 9.5F) };
            txtBuscar.TextChanged += (s, e) => ConsultarPlanilla(txtBuscar.Text.Trim());

            // Fila 2: Horas extra y Bonificaciones para el empleado seleccionado
            lblEmpleadoSel = new Label
            {
                Text = "Empleado: (Seleccione una fila)",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(30, 112),
                AutoSize = true,
                AutoEllipsis = true,
                MaximumSize = new Size(320, 25)
            };

            Label lblHE = new Label { Text = "Horas Extra:", Font = new Font("Segoe UI", 9.5F), Location = new Point(360, 110), AutoSize = true };
            txtHorasExtra = new TextBox { Location = new Point(455, 107), Width = 55, Text = "0", Font = new Font("Segoe UI", 9.5F) };

            Label lblBono = new Label { Text = "Bonificación ($):", Font = new Font("Segoe UI", 9.5F), Location = new Point(525, 110), AutoSize = true };
            txtBonificaciones = new TextBox { Location = new Point(645, 107), Width = 75, Text = "0.00", Font = new Font("Segoe UI", 9.5F) };

            btnAplicarExtras = new Button { Text = "Aplicar Extras", Location = new Point(725, 104), Size = new Size(125, 34) };
            btnAplicarExtras.Click += BtnAplicarExtras_Click;

            // Tabla principal
            dgvPlanilla = new DataGridView
            {
                Location = new Point(30, 155),
                Size = new Size(790, 355),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvPlanilla.CellClick += DgvPlanilla_CellClick;
            dgvPlanilla.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) MostrarBoletaIndividual(); };

            // Botones laterales
            btnGenerar = new Button { Text = "Generar Planilla", Location = new Point(832, 155), Size = new Size(118, 42), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnGenerar.Click += BtnGenerar_Click;

            btnConsultar = new Button { Text = "Consultar", Location = new Point(832, 207), Size = new Size(118, 38), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnConsultar.Click += (s, e) => ConsultarPlanilla(txtBuscar.Text.Trim());

            btnBoleta = new Button { Text = "Ver Boleta", Location = new Point(832, 255), Size = new Size(118, 38), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnBoleta.Click += (s, e) => MostrarBoletaIndividual();

            // Totalizador inferior
            lblTotalPlanilla = new Label
            {
                Text = "Total Neto a Pagar: $0.00",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(30, 520),
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            this.Controls.AddRange(new Control[] {
                lblTitulo, lblQuincena, cmbQuincena, lblMes, cmbMes, lblAnio, nudAnio, lblBuscar, txtBuscar,
                lblEmpleadoSel, lblHE, txtHorasExtra, lblBono, txtBonificaciones, btnAplicarExtras,
                dgvPlanilla, btnGenerar, btnConsultar, btnBoleta, lblTotalPlanilla
            });
        }

        private int ObtenerQuincena() => cmbQuincena.SelectedIndex + 1;
        private int ObtenerMes() => cmbMes.SelectedIndex + 1;
        private int ObtenerAnio() => (int)nudAnio.Value;

        private void BtnGenerar_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show($"¿Confirma generar la planilla de la Quincena {ObtenerQuincena()} de {cmbMes.Text} {ObtenerAnio()}?\nSe calcularán retenciones de ley y cuotas de préstamos activos.",
                "Confirmar Generación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int idPlanilla = controlador.GenerarPlanillaQuincenal(ObtenerQuincena(), ObtenerMes(), ObtenerAnio());
                MessageBox.Show($"Planilla #{idPlanilla} procesada exitosamente.", "Planilla Generada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarPlanilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar la planilla: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConsultarPlanilla(string filtro = "")
        {
            try
            {
                DataTable dt = controlador.ConsultarPlanillaPorPeriodo(ObtenerQuincena(), ObtenerMes(), ObtenerAnio(), filtro);
                dgvPlanilla.DataSource = dt;

                if (dgvPlanilla.Columns["ID_PLANILLA"] != null) dgvPlanilla.Columns["ID_PLANILLA"].Visible = false;
                if (dgvPlanilla.Columns["ID_EMPLEADO"] != null) dgvPlanilla.Columns["ID_EMPLEADO"].Visible = false;

                decimal totalNeto = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Salario Neto"] != DBNull.Value)
                        totalNeto += Convert.ToDecimal(row["Salario Neto"]);
                }
                lblTotalPlanilla.Text = $"Empleados en planilla: {dt.Rows.Count}   |   Total Neto a Pagar: ${totalNeto:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar planilla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPlanilla_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPlanilla.Rows[e.RowIndex];

            idPlanillaActual = Convert.ToInt32(row.Cells["ID_PLANILLA"].Value);
            idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);
            lblEmpleadoSel.Text = $"Empleado: {row.Cells["Empleado"].Value}";
            txtHorasExtra.Text = row.Cells["Horas Extra"].Value?.ToString() ?? "0";
            txtBonificaciones.Text = row.Cells["Bonificaciones"].Value?.ToString() ?? "0.00";
        }

        private void BtnAplicarExtras_Click(object? sender, EventArgs e)
        {
            if (idPlanillaActual == 0 || idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado dentro de la planilla generada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtHorasExtra.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal he) || he < 0 ||
                !decimal.TryParse(txtBonificaciones.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal bono) || bono < 0)
            {
                MessageBox.Show("Ingrese valores numéricos válidos (0 o mayor) para horas extra y bonificaciones.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (controlador.ActualizarHorasExtraBonos(idPlanillaActual, idEmpleadoSeleccionado, he, bono))
                {
                    MessageBox.Show("Horas extra, bonificaciones y retenciones recalculadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ConsultarPlanilla(txtBuscar.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recalcular: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarBoletaIndividual()
        {
            if (idPlanillaActual == 0 || idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado de la tabla para ver su boleta de pago individual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = controlador.ConsultarBoletaIndividual(idPlanillaActual, idEmpleadoSeleccionado);
                if (dt.Rows.Count == 0) return;

                DataRow r = dt.Rows[0];

                Form boleta = new Form
                {
                    Text = $"Boleta de Pago Individual - {r["NOMBRE_COMPLETO"]}",
                    Size = new Size(460, 520),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    BackColor = Color.White
                };

                Panel header = new Panel { Dock = DockStyle.Top, Height = 65, BackColor = ColorTranslator.FromHtml("#1D2D44") };
                Label lblEmpresa = new Label
                {
                    Text = "CORPORACIÓN TEXTIL SALVADOREÑA, S.A.\nBOLETA DE PAGO QUINCENAL",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                    Location = new Point(20, 12),
                    AutoSize = true
                };
                header.Controls.Add(lblEmpresa);

                string detalle =
                    $"Planilla N°:       #{r["ID_PLANILLA"]}   |   Periodo: Quincena {r["PERIODO_QUINCENAL"]} ({r["MES"]}/{r["ANIO"]})\n" +
                    $"Fecha Emisión:     {Convert.ToDateTime(r["FECHA_PROCESAMIENTO"]):dd/MM/yyyy HH:mm}\n" +
                    $"------------------------------------------------------------------\n" +
                    $"Empleado:          {r["NOMBRE_COMPLETO"]}\n" +
                    $"DUI:               {r["DUI"]}\n" +
                    $"Cargo:             {r["NOMBRE_CARGO"]}\n" +
                    $"Salario Base:      ${Convert.ToDecimal(r["SALARIO_BASE"]):N2}   |   Días Trabajados: {r["DIAS_TRABAJADOS"]}\n" +
                    $"==================================================================\n" +
                    $"INGRESOS:\n" +
                    $"  Salario Devengado:                  ${Convert.ToDecimal(r["SALARIO_DEVENGADO"]):N2}\n" +
                    $"  Horas Extra ({r["HORAS_EXTRA"]} hrs):          ${Convert.ToDecimal(r["MONTO_HORAS_EXTRA"]):N2}\n" +
                    $"  Bonificaciones:                     ${Convert.ToDecimal(r["BONIFICACIONES"]):N2}\n" +
                    $"  TOTAL INGRESOS:                     ${Convert.ToDecimal(r["TOTAL_INGRESOS"]):N2}\n" +
                    $"------------------------------------------------------------------\n" +
                    $"DEDUCCIONES:\n" +
                    $"  Cotización ISSS:                   -${Convert.ToDecimal(r["DESCUENTO_ISSS"]):N2}\n" +
                    $"  Cotización AFP:                    -${Convert.ToDecimal(r["DESCUENTO_AFP"]):N2}\n" +
                    $"  Retención ISR (Renta):             -${Convert.ToDecimal(r["DESCUENTO_RENTA"]):N2}\n" +
                    $"  Cuota Préstamo Interno:            -${Convert.ToDecimal(r["DESCUENTO_PRESTAMO"]):N2}\n" +
                    $"  TOTAL DEDUCCIONES:                 -${Convert.ToDecimal(r["TOTAL_DESCUENTOS"]):N2}\n" +
                    $"==================================================================\n" +
                    $"SALARIO NETO A RECIBIR:               ${Convert.ToDecimal(r["SALARIO_NETO"]):N2}";

                Label lblDetalle = new Label
                {
                    Text = detalle,
                    Font = new Font("Consolas", 9.5F, FontStyle.Regular),
                    ForeColor = ColorTranslator.FromHtml("#0D1321"),
                    Location = new Point(20, 80),
                    AutoSize = true
                };

                Button btnCerrar = new Button
                {
                    Text = "Cerrar Boleta",
                    Size = new Size(120, 34),
                    Location = new Point(165, 430),
                    BackColor = ColorTranslator.FromHtml("#3E5C76"),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnCerrar.FlatAppearance.BorderSize = 0;
                btnCerrar.Click += (s, e) => boleta.Close();

                boleta.Controls.AddRange(new Control[] { header, lblDetalle, btnCerrar });
                boleta.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar boleta individual: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            foreach (Button btn in new[] { btnGenerar, btnConsultar, btnAplicarExtras, btnBoleta })
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

            dgvPlanilla.BackgroundColor = cremaClaro;
            dgvPlanilla.BorderStyle = BorderStyle.None;
            dgvPlanilla.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvPlanilla.GridColor = azulClaro;
            dgvPlanilla.EnableHeadersVisualStyles = false;
            dgvPlanilla.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
            dgvPlanilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPlanilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvPlanilla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvPlanilla.ColumnHeadersHeight = 45;
            dgvPlanilla.DefaultCellStyle.BackColor = Color.White;
            dgvPlanilla.DefaultCellStyle.ForeColor = azulMuyOscuro;
            dgvPlanilla.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvPlanilla.DefaultCellStyle.SelectionBackColor = azulClaro;
            dgvPlanilla.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvPlanilla.RowTemplate.Height = 35;

            foreach (TextBox txt in new[] { txtBuscar, txtHorasExtra, txtBonificaciones })
            {
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }
    }
}
