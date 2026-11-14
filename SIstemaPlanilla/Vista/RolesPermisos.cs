using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Controlador;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class RolesPermisos : Form
    {
        private RolControlador controlador = new RolControlador();
        private int idRolSeleccionado = 0;

        public RolesPermisos()
        {
            InitializeComponent();
            Diseño();
        }


        private void RolesPermisos_Load(object? sender, EventArgs e)
        {
            clbOpciones.DataSource = Enum.GetValues(typeof(OpcionesSistema));
            CargarGridRoles();
        }

        // Carga los roles en el DataGridView
        private void CargarGridRoles()
        {
            try
            {
                dgvRoles.DataSource = controlador.ListarRoles();
                if (dgvRoles.Columns["ID_ROL"] != null) dgvRoles.Columns["ID_ROL"].Visible = false;

                dgvRoles.RowHeadersVisible = false;
                dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRoles.MultiSelect = false;
                dgvRoles.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tabla: " + ex.Message);
            }
        }

        // Limpia los campos del formulario
        private void Limpiar()
        {
            idRolSeleccionado = 0;
            txtNombreRol.Clear();
            btnGuardar.Text = "Guardar";
            for (int i = 0; i < clbOpciones.Items.Count; i++) { clbOpciones.SetItemChecked(i, false); }
            txtNombreRol.Focus();
        }

        // Evento que se dispara al hacer clic en una celda del DataGridView
        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRoles.Rows[e.RowIndex];

                idRolSeleccionado = Convert.ToInt32(row.Cells["ID_ROL"].Value);
                txtNombreRol.Text = row.Cells["Rol"].Value.ToString();

                for (int i = 0; i < clbOpciones.Items.Count; i++) { clbOpciones.SetItemChecked(i, false); }

                List<int> permisosDelRol = controlador.ConsultarPermisosDeRol(idRolSeleccionado);

                for (int i = 0; i < clbOpciones.Items.Count; i++)
                {
                    int idOpcionLista = (int)clbOpciones.Items[i];
                    if (permisosDelRol.Contains(idOpcionLista))
                    {
                        clbOpciones.SetItemChecked(i, true);
                    }
                }

                btnGuardar.Text = "Modificar";
            }
        }

        // evento que guarada o modifica un rol según corresponda
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreRol.Text))
            {
                MessageBox.Show("Ingrese un nombre para el Rol.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (clbOpciones.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe asignarle al menos un permiso.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> permisosSeleccionados = new List<int>();
            foreach (var itemChequeado in clbOpciones.CheckedItems)
            {
                permisosSeleccionados.Add((int)itemChequeado);
            }

            string nombre = txtNombreRol.Text.Trim();

            if (idRolSeleccionado == 0) 
            {
                if (controlador.CrearNuevoRol(nombre, permisosSeleccionados))
                {
                    MessageBox.Show("Rol creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGridRoles();
                }
            }
            else
            {
                if (controlador.ModificarRol(idRolSeleccionado, nombre, permisosSeleccionados))
                {
                    MessageBox.Show("Rol modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGridRoles();
                }
            }
        }

        // evento que limpia los campos del formulario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        // Método para aplicar el diseño a los controles del formulario

        private void Diseño()
        {
            Color azulMuyOscuro = ColorTranslator.FromHtml("#0D1321");
            Color azulOscuro = ColorTranslator.FromHtml("#1D2D44");
            Color azulAcero = ColorTranslator.FromHtml("#3E5C76");
            Color azulClaro = ColorTranslator.FromHtml("#748CAB");
            Color cremaClaro = ColorTranslator.FromHtml("#F0EBD8");

            this.BackColor = cremaClaro;
            this.ForeColor = azulMuyOscuro;

            Button[] botones = { btnGuardar, btnLimpiar };
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

            if (dgvRoles != null)
            {
                dgvRoles.BackgroundColor = cremaClaro;
                dgvRoles.BorderStyle = BorderStyle.None;
                dgvRoles.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvRoles.GridColor = azulClaro;

                dgvRoles.EnableHeadersVisualStyles = false;

                dgvRoles.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvRoles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvRoles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvRoles.ColumnHeadersHeight = 35;

                dgvRoles.DefaultCellStyle.BackColor = Color.White;
                dgvRoles.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvRoles.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                dgvRoles.RowTemplate.Height = 30;

                dgvRoles.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvRoles.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvRoles.RowTemplate.Height = 35;
            }

            TextBox[] cajasDeTexto = { txtNombreRol, txtBuscar };
            foreach (TextBox txt in cajasDeTexto)
            {
                if (txt == null) continue;

                txt.Enter += (s, e) => { txt.BackColor = Color.White; };

                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        // Evento que se dispara al cambiar el texto en el cuadro de búsqueda
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(filtro))
                CargarGridRoles();   
            else
                dgvRoles.DataSource = controlador.BuscarRoles(filtro);
        }
    }
}