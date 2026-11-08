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
    public partial class Usuarios : Form
    {
        private UsuarioControlador controlador = new UsuarioControlador();

        private int idUsuarioSeleccionado = 0;

        public Usuarios()
        {
            InitializeComponent();
        }



        private void Usuarios_Load(object? sender, EventArgs e)
        {
            CargarCombos();
            CargarGrid();
        }

        private void CargarCombos()
        {
            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con == null) return;


                using (SqlDataAdapter daRol = new SqlDataAdapter("SELECT ID_ROL, NOMBRE_ROL FROM ROL", con))
                {
                    DataTable dtRol = new DataTable();
                    daRol.Fill(dtRol);

                    // Fila de "Seleccione..."
                    dtRol.Rows.InsertAt(dtRol.NewRow(), 0);
                    dtRol.Rows[0]["ID_ROL"] = 0;
                    dtRol.Rows[0]["NOMBRE_ROL"] = "-- Seleccione Rol --";

                    cmbRoles.DataSource = dtRol;
                    cmbRoles.DisplayMember = "NOMBRE_ROL";
                    cmbRoles.ValueMember = "ID_ROL";
                }

                using (SqlDataAdapter daEmp = new SqlDataAdapter("SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM EMPLEADO WHERE ESTADO = 'ACTIVO'", con))
                {
                    DataTable dtEmp = new DataTable();
                    daEmp.Fill(dtEmp);

                    // Fila de "Seleccione..."
                    dtEmp.Rows.InsertAt(dtEmp.NewRow(), 0);
                    dtEmp.Rows[0]["ID_EMPLEADO"] = 0;
                    dtEmp.Rows[0]["NOMBRE_COMPLETO"] = "-- Seleccione Empleado --";

                    cmbEmpleado.DataSource = dtEmp;
                    cmbEmpleado.DisplayMember = "NOMBRE_COMPLETO";
                    cmbEmpleado.ValueMember = "ID_EMPLEADO";
                }
            }
        }

        private void CargarGrid()
        {
            try
            {
                dgvUsuarios.DataSource = controlador.ListarUsuarios();

                if (dgvUsuarios.Columns["ID_USUARIO"] != null) dgvUsuarios.Columns["ID_USUARIO"].Visible = false;
                if (dgvUsuarios.Columns["ID_ROL"] != null) dgvUsuarios.Columns["ID_ROL"].Visible = false;
                if (dgvUsuarios.Columns["ID_EMPLEADO"] != null) dgvUsuarios.Columns["ID_EMPLEADO"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Limpiar()
        {
            idUsuarioSeleccionado = 0;
            txtUsuario.Clear();
            txtContrasenia.Clear();
            if (cmbRoles.Items.Count > 0) cmbRoles.SelectedIndex = 0;
            if (cmbEmpleado.Items.Count > 0) cmbEmpleado.SelectedIndex = 0;
            txtUsuario.Focus();
        }

        private bool ValidarCampos(bool esNuevo)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || Convert.ToInt32(cmbRoles.SelectedValue) == 0)
            {
                MessageBox.Show("El nombre de usuario y el rol son obligatorios.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (esNuevo && string.IsNullOrWhiteSpace(txtContrasenia.Text))
            {
                MessageBox.Show("Debe asignar una contraseña al momento de crear un nuevo usuario.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(true)) return;

            Usuario nuevoUsuario = new Usuario
            {
                Username = txtUsuario.Text.Trim(),
                PasswordHash = HelperSeguridad.CalcularSHA256(txtContrasenia.Text.Trim()),
                IdRol = Convert.ToInt32(cmbRoles.SelectedValue),
                IdEmpleado = Convert.ToInt32(cmbEmpleado.SelectedValue)
            };

            if (controlador.GuardarNuevoUsuario(nuevoUsuario))
            {
                MessageBox.Show("Usuario creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
            else
            {
                MessageBox.Show("No se pudo guardar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla haciendo clic sobre él.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos(false)) return;


            bool cambiaPassword = !string.IsNullOrWhiteSpace(txtContrasenia.Text);

            Usuario usuarioEditado = new Usuario
            {
                IdUsuario = idUsuarioSeleccionado,
                Username = txtUsuario.Text.Trim(),
                IdRol = Convert.ToInt32(cmbRoles.SelectedValue),
                IdEmpleado = Convert.ToInt32(cmbEmpleado.SelectedValue)
            };

            if (cambiaPassword)
            {
                usuarioEditado.PasswordHash = HelperSeguridad.CalcularSHA256(txtContrasenia.Text.Trim());
            }

            if (controlador.ModificarUsuario(usuarioEditado, cambiaPassword))
            {
                MessageBox.Show("Usuario modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de desactivar este acceso al sistema?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.DesactivarUsuario(idUsuarioSeleccionado))
                {
                    MessageBox.Show("Usuario desactivado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGrid();
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
            {
                CargarGrid();
            }
            else
            {

                dgvUsuarios.DataSource = controlador.BuscarUsuarios(filtro);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];

                idUsuarioSeleccionado = Convert.ToInt32(row.Cells["ID_USUARIO"].Value);

                txtUsuario.Text = row.Cells["Usuario"].Value.ToString();
                txtContrasenia.Clear();

                if (row.Cells["ID_ROL"].Value != DBNull.Value)
                    cmbRoles.SelectedValue = Convert.ToInt32(row.Cells["ID_ROL"].Value);

                if (row.Cells["ID_EMPLEADO"].Value != DBNull.Value)
                    cmbEmpleado.SelectedValue = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);
                else
                    cmbEmpleado.SelectedIndex = 0;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}