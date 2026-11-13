using System.Globalization;
using SIstemaPlanilla.Controlador;

namespace SIstemaPlanilla.Vista
{
    public partial class Parametros : Form 
    {
        private PlanilleroControlador controlador = new PlanilleroControlador();
        private int idParametroSeleccionado = 0;

        private TextBox txtBuscar, txtNombreLey, txtPorcentajeMonto, txtTechoMaximo, txtTramoDesde, txtTramoHasta;
        private DataGridView dgvParametros;
        private Button btnActualizar, btnLimpiar;

        public Parametros()
        {
            InitializeComponent();
            ConstruirInterfaz();
            Diseño();
        }

        private void Parametros_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void ConstruirInterfaz()
        {
            this.Controls.Clear();
            this.Size = new Size(920, 560);

            Label lblTitulo = new Label { Text = "PARÁMETROS DE LEY", Font = new Font("Segoe UI", 16F, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true };
            Label lblBuscar = new Label { Text = "Buscar", Font = new Font("Segoe UI", 9.5F), Location = new Point(140, 68), AutoSize = true };
            txtBuscar = new TextBox { Location = new Point(200, 65), Width = 480, Font = new Font("Segoe UI", 9.5F) };
            txtBuscar.TextChanged += (s, e) => CargarGrid(txtBuscar.Text.Trim());

            
            Label lblLey = new Label { Text = "Parámetro / Ley", Font = new Font("Segoe UI", 9.5F), Location = new Point(50, 115), AutoSize = true };
            txtNombreLey = new TextBox { Location = new Point(175, 112), Width = 250, ReadOnly = true, Font = new Font("Segoe UI", 9.5F) };

            Label lblPct = new Label { Text = "Porcentaje (Dec.)", Font = new Font("Segoe UI", 9.5F), Location = new Point(450, 115), AutoSize = true };
            txtPorcentajeMonto = new TextBox { Location = new Point(580, 112), Width = 120, Font = new Font("Segoe UI", 9.5F) };

            Label lblTecho = new Label { Text = "Techo / Cuota Fija", Font = new Font("Segoe UI", 9.5F), Location = new Point(50, 155), AutoSize = true };
            txtTechoMaximo = new TextBox { Location = new Point(190, 152), Width = 105, Font = new Font("Segoe UI", 9.5F) };

            Label lblDesde = new Label { Text = "Tramo Desde ($)", Font = new Font("Segoe UI", 9.5F), Location = new Point(315, 155), AutoSize = true };
            txtTramoDesde = new TextBox { Location = new Point(445, 152), Width = 105, Font = new Font("Segoe UI", 9.5F) };

            Label lblHasta = new Label { Text = "Tramo Hasta ($)", Font = new Font("Segoe UI", 9.5F), Location = new Point(570, 155), AutoSize = true };
            txtTramoHasta = new TextBox { Location = new Point(695, 152), Width = 105, Font = new Font("Segoe UI", 9.5F) };

            dgvParametros = new DataGridView
            {
                Location = new Point(50, 210),
                Size = new Size(735, 300),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvParametros.CellClick += DgvParametros_CellClick;

            btnActualizar = new Button { Text = "Actualizar", Location = new Point(800, 210), Size = new Size(100, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnActualizar.Click += BtnActualizar_Click;

            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(800, 255), Size = new Size(100, 34), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            btnLimpiar.Click += (s, e) => { Limpiar(); CargarGrid(); };

            this.Controls.AddRange(new Control[] {
                lblTitulo, lblBuscar, txtBuscar, lblLey, txtNombreLey, lblPct, txtPorcentajeMonto, 
                lblTecho, txtTechoMaximo, lblDesde, txtTramoDesde, lblHasta, txtTramoHasta,
                dgvParametros, btnActualizar, btnLimpiar
            });
        }

        private void CargarGrid(string filtro = "")
        {
            try
            {
                dgvParametros.DataSource = controlador.ListarParametrosLey(filtro);

                if (dgvParametros.Columns["ID_PARAMETRO"] != null)
                    dgvParametros.Columns["ID_PARAMETRO"].Visible = false;

                if (dgvParametros.Columns["FECHA_FIN_VIGENCIA"] != null)
                    dgvParametros.Columns["FECHA_FIN_VIGENCIA"].Visible = false;

                if (dgvParametros.Columns["NOMBRE_LEY"] != null)
                {
                    dgvParametros.Columns["NOMBRE_LEY"].HeaderText = "Parámetro / Ley";
                    dgvParametros.Columns["NOMBRE_LEY"].FillWeight = 190;
                }
                if (dgvParametros.Columns["PORCENTAJE_O_MONTO"] != null)
                {
                    dgvParametros.Columns["PORCENTAJE_O_MONTO"].HeaderText = "Tasa / %";
                    dgvParametros.Columns["PORCENTAJE_O_MONTO"].FillWeight = 80;
                }
                if (dgvParametros.Columns["TECHO_MAXIMO"] != null)
                {
                    dgvParametros.Columns["TECHO_MAXIMO"].HeaderText = "Techo / Cuota Fija";
                    dgvParametros.Columns["TECHO_MAXIMO"].FillWeight = 115;
                }
                if (dgvParametros.Columns["TRAMO_DESDE"] != null)
                {
                    dgvParametros.Columns["TRAMO_DESDE"].HeaderText = "Desde ($)";
                    dgvParametros.Columns["TRAMO_DESDE"].FillWeight = 85;
                }
                if (dgvParametros.Columns["TRAMO_HASTA"] != null)
                {
                    dgvParametros.Columns["TRAMO_HASTA"].HeaderText = "Hasta ($)";
                    dgvParametros.Columns["TRAMO_HASTA"].FillWeight = 90;
                }
                if (dgvParametros.Columns["FECHA_INICIO_VIGENCIA"] != null)
                {
                    dgvParametros.Columns["FECHA_INICIO_VIGENCIA"].HeaderText = "Vigente Desde";
                    dgvParametros.Columns["FECHA_INICIO_VIGENCIA"].FillWeight = 100;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar parámetros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnActualizar_Click(object? sender, EventArgs e)
        {
            if (idParametroSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un parámetro de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryParseDecimal(txtPorcentajeMonto.Text, out decimal porcentaje))
            {
                MessageBox.Show("Ingrese un porcentaje o monto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"¿Está seguro de modificar el parámetro legal '{txtNombreLey.Text}'?\nEste cambio aplicará a las próximas planillas generadas.",
                "Confirmar Reforma Legal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            decimal? techo = string.IsNullOrWhiteSpace(txtTechoMaximo.Text) ? null : (TryParseDecimal(txtTechoMaximo.Text, out decimal t) ? t : null);
            decimal? desde = string.IsNullOrWhiteSpace(txtTramoDesde.Text) ? null : (TryParseDecimal(txtTramoDesde.Text, out decimal d) ? d : null);
            decimal? hasta = string.IsNullOrWhiteSpace(txtTramoHasta.Text) ? null : (TryParseDecimal(txtTramoHasta.Text, out decimal h) ? h : null);

            try
            {
                if (controlador.ActualizarParametroLey(idParametroSeleccionado, porcentaje, techo, desde, hasta))
                {
                    MessageBox.Show("Parámetro de ley actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DgvParametros_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvParametros.Rows[e.RowIndex];

            idParametroSeleccionado = Convert.ToInt32(row.Cells["ID_PARAMETRO"].Value);
            txtNombreLey.Text = row.Cells["NOMBRE_LEY"].Value?.ToString();
            txtPorcentajeMonto.Text = row.Cells["PORCENTAJE_O_MONTO"].Value?.ToString();
            txtTechoMaximo.Text = row.Cells["TECHO_MAXIMO"].Value == DBNull.Value ? "" : row.Cells["TECHO_MAXIMO"].Value.ToString();
            txtTramoDesde.Text = row.Cells["TRAMO_DESDE"].Value == DBNull.Value ? "" : row.Cells["TRAMO_DESDE"].Value.ToString();
            txtTramoHasta.Text = row.Cells["TRAMO_HASTA"].Value == DBNull.Value ? "" : row.Cells["TRAMO_HASTA"].Value.ToString();
        }


        private void Limpiar()
        {
            idParametroSeleccionado = 0;
            txtNombreLey.Clear();
            txtPorcentajeMonto.Clear();
            txtTechoMaximo.Clear();
            txtTramoDesde.Clear();
            txtTramoHasta.Clear();
        }

        private bool TryParseDecimal(string texto, out decimal valor)
        {
            return decimal.TryParse(texto.Trim().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out valor);
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

            foreach (Button btn in new[] { btnActualizar, btnLimpiar })
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

            dgvParametros.BackgroundColor = cremaClaro;
            dgvParametros.BorderStyle = BorderStyle.None;
            dgvParametros.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvParametros.GridColor = azulClaro;
            dgvParametros.EnableHeadersVisualStyles = false;
            dgvParametros.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
            dgvParametros.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvParametros.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvParametros.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvParametros.ColumnHeadersHeight = 35;
            dgvParametros.DefaultCellStyle.BackColor = Color.White;
            dgvParametros.DefaultCellStyle.ForeColor = azulMuyOscuro;
            dgvParametros.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgvParametros.DefaultCellStyle.SelectionBackColor = azulClaro;
            dgvParametros.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvParametros.RowTemplate.Height = 35;

            foreach (TextBox txt in new[] { txtBuscar, txtPorcentajeMonto, txtTechoMaximo, txtTramoDesde, txtTramoHasta })
            {
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }
    }
}