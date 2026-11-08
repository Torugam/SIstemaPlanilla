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
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Es mejor especificar las columnas para no mostrar el PASSWORD_HASH en la pantalla por seguridad
                    // Además hacemos un JOIN con la tabla ROL para ver el nombre del rol en lugar de solo un número.
                    string query = @"SELECT U.ID_USUARIO AS [ID], 
                                            U.USERNAME AS [Usuario], 
                                            R.NOMBRE_ROL AS [Rol], 
                                            U.ESTADO AS [Estado], 
                                            U.INTENTOS_FALLIDOS AS [Intentos Fallidos]
                                     FROM USUARIO U
                                     INNER JOIN ROL R ON U.ID_ROL = R.ID_ROL";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dgvUsuarios != null)
                        {
                            dgvUsuarios.DataSource = dt;
                        }
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
            try { 
            
                using (SqlConnection con = Conexion.GetConexion())
                {
                    if (con == null) return; // Si no hay conexión, sale silenciosamente

                    string query = "SELECT ID_ROL, NOMBRE_ROL FROM ROL";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (cmbRoles != null && dt.Rows.Count > 0)
                        {
                            cmbRoles.DataSource = dt;
                            // Asignamos explícitamente las columnas por su nombre en la base de datos
                            cmbRoles.ValueMember = "ID_ROL";
                            cmbRoles.DisplayMember = "NOMBRE_ROL";
                        }
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