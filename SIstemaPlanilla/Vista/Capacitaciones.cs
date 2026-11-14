using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Controlador;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Capacitaciones : Form
    {
        private CapacitacionControlador controlador = new CapacitacionControlador();
        private int idCapacitacionSeleccionada = 0;
        public Capacitaciones()
        {
            InitializeComponent();
            Diseño();
        }



        private void Capacitaciones_Load(object? sender, EventArgs e)
        {
            CargarGrid();
        }

        // metodo para cargar los datos en el DataGridView
        private void CargarGrid()
        {
            try
            {
                dgvCapacitaciones.DataSource = controlador.ListarCapacitaciones();
                OcultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la tabla: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // metodo para ocultar columnas innecesarias en el DataGridView
        private void OcultarColumnas()
        {
            if (dgvCapacitaciones.Columns["ID_CAPACITACION"] != null)
                dgvCapacitaciones.Columns["ID_CAPACITACION"].Visible = false;
        }

        // metodo para limpiar los campos del formulario
        private void Limpiar()
        {
            idCapacitacionSeleccionada = 0;
            txtNombreCapacitacion.Clear();
            txtDescripcion.Clear();
            txtHoras.Clear();
            txtNombreCapacitacion.Focus();
        }

        // metodo para validar los campos del formulario
        private bool ValidarCampos(out int horas)
        {
            horas = 0;

            if (string.IsNullOrWhiteSpace(txtNombreCapacitacion.Text))
            {
                MessageBox.Show("El nombre de la capacitación es obligatorio.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtHoras.Text, out horas) || horas <= 0)
            {
                MessageBox.Show("Ingrese una cantidad de horas válida (mayor que cero).", "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // metodo para construir un objeto Capacitacion a partir de los datos del formulario
        private Capacitacion ConstruirCapacitacion(int horas)
        {
            return new Capacitacion
            {
                IdCapacitacion = idCapacitacionSeleccionada,
                NombreCapacitacion = txtNombreCapacitacion.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                HorasDuracion = horas
            };
        }

        // evento para guardar una nueva capacitación
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(out int horas)) return;

            Capacitacion capacitacion = ConstruirCapacitacion(horas);

            if (controlador.GuardarNuevaCapacitacion(capacitacion, out string mensaje))
            {
                MessageBox.Show("Capacitación creada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // evento para modificar una capacitación existente
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idCapacitacionSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una capacitación de la tabla haciendo clic sobre ella.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidarCampos(out int horas)) return;

            Capacitacion capacitacion = ConstruirCapacitacion(horas);

            if (controlador.ModificarCapacitacion(capacitacion, out string mensaje))
            {
                MessageBox.Show("Capacitación modificada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                CargarGrid();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // evento para eliminar una capacitación seleccionada
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCapacitacionSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una capacitación de la tabla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar esta capacitación?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.EliminarCapacitacion(idCapacitacionSeleccionada, out string mensaje))
                {
                    MessageBox.Show("Capacitación eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    CargarGrid();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // evento para buscar capacitaciones en tiempo real mientras se escribe en el TextBox de búsqueda
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(filtro))
                CargarGrid();
            else
            {
                dgvCapacitaciones.DataSource = controlador.BuscarCapacitaciones(filtro);
                OcultarColumnas();
            }
        }
        // evento para seleccionar una capacitación de la tabla y mostrar sus datos en los campos del formulario

        private void dgvCapacitaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvCapacitaciones.Rows[e.RowIndex];

            idCapacitacionSeleccionada = Convert.ToInt32(row.Cells["ID_CAPACITACION"].Value);
            txtNombreCapacitacion.Text = row.Cells["Capacitacion"].Value.ToString();
            txtDescripcion.Text = row.Cells["Descripcion"].Value == DBNull.Value
                ? string.Empty
                : row.Cells["Descripcion"].Value.ToString();
            txtHoras.Text = row.Cells["Horas"].Value.ToString();
        }

        // evento para limpiar los campos del formulario y recargar la tabla
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            CargarGrid();
        }

        // metodo para aplicar el diseño personalizado a los controles del formulario

        private void Diseño()
        {
            Color azulMuyOscuro = ColorTranslator.FromHtml("#0D1321");
            Color azulOscuro = ColorTranslator.FromHtml("#1D2D44");
            Color azulAcero = ColorTranslator.FromHtml("#3E5C76");
            Color azulClaro = ColorTranslator.FromHtml("#748CAB");
            Color cremaClaro = ColorTranslator.FromHtml("#F0EBD8");

            this.BackColor = cremaClaro;
            this.ForeColor = azulMuyOscuro;

            Button[] botones = { btnGuardar, btnModificar, btnEliminar, btnLimpiar };
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

            if (dgvCapacitaciones != null)
            {
                dgvCapacitaciones.BackgroundColor = cremaClaro;
                dgvCapacitaciones.BorderStyle = BorderStyle.None;
                dgvCapacitaciones.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvCapacitaciones.GridColor = azulClaro;
                dgvCapacitaciones.EnableHeadersVisualStyles = false;
                dgvCapacitaciones.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvCapacitaciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvCapacitaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvCapacitaciones.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvCapacitaciones.ColumnHeadersHeight = 35;
                dgvCapacitaciones.DefaultCellStyle.BackColor = Color.White;
                dgvCapacitaciones.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvCapacitaciones.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvCapacitaciones.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvCapacitaciones.RowTemplate.Height = 35;
                dgvCapacitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvCapacitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCapacitaciones.ReadOnly = true;
                dgvCapacitaciones.AllowUserToAddRows = false;
            }

            TextBox[] cajas = { txtNombreCapacitacion, txtDescripcion, txtHoras, txtBuscar };
            foreach (TextBox txt in cajas)
            {
                if (txt == null) continue;
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }
    }
}