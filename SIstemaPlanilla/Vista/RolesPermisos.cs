using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class RolesPermisos : Form
    {
        private DataGridView dgvRoles = null!;

        public RolesPermisos()
        {
            InitializeComponent();
            InicializarComponentesDinamicos();
            this.Load += RolesPermisos_Load;
        }

        private void InicializarComponentesDinamicos()
        {
            dgvRoles = new DataGridView();
            dgvRoles.Dock = DockStyle.Fill;
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.ReadOnly = true;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.BackgroundColor = Color.WhiteSmoke;

            this.Controls.Add(dgvRoles);
            dgvRoles.BringToFront();
        }

        private void RolesPermisos_Load(object? sender, EventArgs e)
        {
            CargarCatalogoRoles();
        }

        private void CargarCatalogoRoles()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.CadenaConexion))
                {
                    // Consulta a BD_PLANILLA
                    string query = @"SELECT IdRol, NombreRol AS [Nombre del Rol], 
                                     CASE 
                                         WHEN IdRol = 1 THEN 'Administrador - Acceso total al sistema'
                                         WHEN IdRol = 2 THEN 'Planillero - Acceso a Gestión de Planilla y Recursos Humanos'
                                         WHEN IdRol = 3 THEN 'Recursos Humanos - Acceso a Empleados y Contratos'
                                         ELSE 'Sin descripción'
                                     END AS [Permisos Asignados]
                                     FROM Roles";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dgvRoles != null)
                    {
                        dgvRoles.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tabla de roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}