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
            Diseño();
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

            btnEliminar.Text = "Desactivar";
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

            string estadoActual = dgvUsuarios.CurrentRow.Cells["Estado"].Value.ToString();

            string nuevoEstado = (estadoActual == "ACTIVO") ? "INACTIVO" : "ACTIVO";
            string accion = (estadoActual == "ACTIVO") ? "desactivar" : "activar";

            if (MessageBox.Show($"¿Está seguro de {accion} este acceso al sistema?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.CambiarEstadoUsuario(idUsuarioSeleccionado, nuevoEstado))
                {
                    MessageBox.Show($"Usuario {accion}do correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                btnEliminar.Text = (row.Cells["Estado"].Value.ToString() == "ACTIVO") ? "Desactivar" : "Activar";
            }
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

            if (dgvUsuarios != null)
            {
                dgvUsuarios.BackgroundColor = cremaClaro;
                dgvUsuarios.BorderStyle = BorderStyle.None;
                dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvUsuarios.GridColor = azulClaro;

                dgvUsuarios.EnableHeadersVisualStyles = false;

                dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvUsuarios.ColumnHeadersHeight = 35;

                dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
                dgvUsuarios.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                dgvUsuarios.RowTemplate.Height = 30;

                dgvUsuarios.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvUsuarios.RowTemplate.Height = 35;
            }

            TextBox[] cajasDeTexto = { txtUsuario, txtContrasenia, txtBuscar };
            foreach (TextBox txt in cajasDeTexto)
            {
                if (txt == null) continue;

                txt.Enter += (s, e) => { txt.BackColor = Color.White; };

                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        private void Usuarios_Activated(object sender, EventArgs e)
        {
            CargarCombos();
        }
    }
}