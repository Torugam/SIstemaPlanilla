using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Usuarios : Form
    {
        private DataGridView dgvUsuarios = null!;
        private ComboBox cmbRoles = null!;
        private Panel pnlSuperior = null!;

        public Usuarios()
        {
            InitializeComponent();
            InicializarComponentesDinamicos();
            this.Load += Usuarios_Load;
        }

        private void InicializarComponentesDinamicos()
        {
            pnlSuperior = new Panel();
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Height = 45;
            pnlSuperior.BackColor = Color.LightGray;

            Label lblRol = new Label();
            lblRol.Text = "Rol de usuario:";
            lblRol.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRol.Location = new Point(12, 13);
            lblRol.AutoSize = true;

            cmbRoles = new ComboBox();
            cmbRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoles.Location = new Point(110, 10);
            cmbRoles.Width = 200;

            pnlSuperior.Controls.Add(lblRol);
            pnlSuperior.Controls.Add(cmbRoles);

            dgvUsuarios = new DataGridView();
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = Color.WhiteSmoke;

            this.Controls.Add(dgvUsuarios);
            this.Controls.Add(pnlSuperior);

            dgvUsuarios.BringToFront();
        }

        private void Usuarios_Load(object? sender, EventArgs e)
        {
            CargarUsuarios();
            CargarRolesComboBox();
        }

        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = "SELECT * FROM Usuario";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dgvUsuarios != null)
                    {
                        dgvUsuarios.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarRolesComboBox()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = "SELECT * FROM Rol";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (cmbRoles != null && dt.Rows.Count > 0)
                    {
                        cmbRoles.DataSource = dt;
                        // Usa las posiciones de las columnas automáticamente para evitar fallos de nombres
                        cmbRoles.ValueMember = dt.Columns[0].ColumnName;     // ID de la primera columna
                        cmbRoles.DisplayMember = dt.Columns[1].ColumnName;   // Nombre de la segunda columna
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar roles en el combo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}