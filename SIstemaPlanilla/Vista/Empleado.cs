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
        private DataGridView dgvEmpleados = null!;
        private TextBox txtNombre = null!;
        private TextBox txtDui = null!;
        private TextBox txtNit = null!;
        private TextBox txtIsss = null!;
        private TextBox txtTelefono = null!;
        private TextBox txtDireccion = null!;
        private TextBox txtSalario = null!;
        private ComboBox cmbCargo = null!;
        private ComboBox cmbAfp = null!;
        private DateTimePicker dtpFechaNac = null!;
        private Button btnGuardar = null!;
        private Panel pnlFormulario = null!;

        public Empleado()
        {
            InitializeComponent();
            InicializarInterfazEmpleado();
            this.Load += Empleado_Load;
        }

        private void InicializarInterfazEmpleado()
        {
            this.Text = "Gestión de Empleados - Recursos Humanos";
            this.Size = new Size(1000, 600);

            // 1. Panel izquierdo para el formulario de registro
            pnlFormulario = new Panel();
            pnlFormulario.Dock = DockStyle.Left;
            pnlFormulario.Width = 320;
            pnlFormulario.BackColor = Color.WhiteSmoke;
            pnlFormulario.Padding = new Padding(15);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Registrar Empleado";
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.Location = new Point(15, 10);
            lblTitulo.AutoSize = true;

            // Nombre Completo
            Label lblNombre = new Label();
            lblNombre.Text = "Nombre Completo:";
            lblNombre.Location = new Point(15, 45);
            lblNombre.AutoSize = true;
            txtNombre = new TextBox { Location = new Point(15, 65), Width = 280 };

            // DUI
            Label lblDui = new Label();
            lblDui.Text = "DUI:";
            lblDui.Location = new Point(15, 95);
            lblDui.AutoSize = true;
            txtDui = new TextBox { Location = new Point(15, 115), Width = 280 };

            // NIT
            Label lblNit = new Label();
            lblNit.Text = "NIT:";
            lblNit.Location = new Point(15, 145);
            lblNit.AutoSize = true;
            txtNit = new TextBox { Location = new Point(15, 165), Width = 280 };

            // ISSS
            Label lblIsss = new Label();
            lblIsss.Text = "ISSS:";
            lblIsss.Location = new Point(15, 195);
            lblIsss.AutoSize = true;
            txtIsss = new TextBox { Location = new Point(15, 215), Width = 280 };

            // Teléfono
            Label lblTel = new Label();
            lblTel.Text = "Teléfono:";
            lblTel.Location = new Point(15, 245);
            lblTel.AutoSize = true;
            txtTelefono = new TextBox { Location = new Point(15, 265), Width = 280 };

            // Salario Base
            Label lblSalario = new Label();
            lblSalario.Text = "Salario Base ($):";
            lblSalario.Location = new Point(15, 295);
            lblSalario.AutoSize = true;
            txtSalario = new TextBox { Location = new Point(15, 315), Width = 280 };

            // AFP Cotiza
            Label lblAfp = new Label();
            lblAfp.Text = "AFP:";
            lblAfp.Location = new Point(15, 345);
            lblAfp.AutoSize = true;
            cmbAfp = new ComboBox();
            cmbAfp.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAfp.Location = new Point(15, 365);
            cmbAfp.Width = 280;
            cmbAfp.Items.AddRange(new string[] { "AFP Crecer", "AFP Confia" });
            cmbAfp.SelectedIndex = 0;

            // Cargo (ID Cargo)
            Label lblCargo = new Label();
            lblCargo.Text = "Cargo (ID):";
            lblCargo.Location = new Point(15, 395);
            lblCargo.AutoSize = true;
            cmbCargo = new ComboBox();
            cmbCargo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCargo.Location = new Point(15, 415);
            cmbCargo.Width = 280;

            // Botón Guardar
            btnGuardar = new Button();
            btnGuardar.Text = "💾 Guardar Empleado";
            btnGuardar.BackColor = Color.SeaGreen;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Location = new Point(15, 460);
            btnGuardar.Size = new Size(280, 35);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Click += BtnGuardar_Click;

            // Agregar controles al panel
            pnlFormulario.Controls.Add(lblTitulo);
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblDui);
            pnlFormulario.Controls.Add(txtDui);
            pnlFormulario.Controls.Add(lblNit);
            pnlFormulario.Controls.Add(txtNit);
            pnlFormulario.Controls.Add(lblIsss);
            pnlFormulario.Controls.Add(txtIsss);
            pnlFormulario.Controls.Add(lblTel);
            pnlFormulario.Controls.Add(txtTelefono);
            pnlFormulario.Controls.Add(lblSalario);
            pnlFormulario.Controls.Add(txtSalario);
            pnlFormulario.Controls.Add(lblAfp);
            pnlFormulario.Controls.Add(cmbAfp);
            pnlFormulario.Controls.Add(lblCargo);
            pnlFormulario.Controls.Add(cmbCargo);
            pnlFormulario.Controls.Add(btnGuardar);

            // 2. DataGridView a la derecha para listar
            dgvEmpleados = new DataGridView();
            dgvEmpleados.Dock = DockStyle.Fill;
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.AllowUserToDeleteRows = false;
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmpleados.BackgroundColor = Color.WhiteSmoke;

            this.Controls.Add(dgvEmpleados);
            this.Controls.Add(pnlFormulario);
            dgvEmpleados.BringToFront();
        }

        private void Empleado_Load(object? sender, EventArgs e)
        {
            CargarEmpleados();
            CargarCargosComboBox();
        }

        private void CargarEmpleados()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_Empleado_Listar", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvEmpleados.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCargosComboBox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = "SELECT * FROM Cargo"; // O como se llame tu tabla de cargos
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cmbCargo.DataSource = dt;
                        cmbCargo.ValueMember = dt.Columns[0].ColumnName;   // ID del cargo
                        cmbCargo.DisplayMember = dt.Columns[1].ColumnName; // Nombre del cargo
                    }
                }
            }
            catch
            {
                // Si la tabla se llama diferente, dejamos un valor por defecto o evitamos interrupciones
                cmbCargo.Items.Add("1");
                cmbCargo.SelectedIndex = 0;
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDui.Text) || string.IsNullOrWhiteSpace(txtSalario.Text))
            {
                MessageBox.Show("Por favor, complete los campos obligatorios (Nombre, DUI y Salario).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.CadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand("sp_Empleado_Insertar", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID_CARGO", cmbCargo.SelectedValue ?? 1);
                    cmd.Parameters.AddWithValue("@NOMBRE_COMPLETO", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@DUI", txtDui.Text.Trim());
                    cmd.Parameters.AddWithValue("@NIT", txtNit.Text.Trim());
                    cmd.Parameters.AddWithValue("@ISSS", txtIsss.Text.Trim());
                    cmd.Parameters.AddWithValue("@DIRECCION", "San Salvador"); // Valor por defecto o agrega un textbox si deseas
                    cmd.Parameters.AddWithValue("@TELEFONO", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@AFP_COTIZA", cmbAfp.SelectedItem?.ToString() ?? "AFP Crecer");
                    cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", DateTime.Now.AddYears(-25)); // Valor estándar o usa DateTimePicker
                    cmd.Parameters.AddWithValue("@SALARIO_BASE", Convert.ToDecimal(txtSalario.Text.Trim()));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Empleado registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos y recargar tabla
                    txtNombre.Clear();
                    txtDui.Clear();
                    txtNit.Clear();
                    txtIsss.Clear();
                    txtTelefono.Clear();
                    txtSalario.Clear();
                    CargarEmpleados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}