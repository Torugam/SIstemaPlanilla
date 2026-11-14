using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Controlador;
using SIstemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    
    public partial class Departamentos : Form
    {
        private int idDepartamentoSeleccionado = 0;
        private readonly DepartamentoControlador controlador = new DepartamentoControlador();

        public Departamentos()
        {
            InitializeComponent();
            Diseño();

            if (dgvDepartamentos != null)
            {
                dgvDepartamentos.CellClick -= dgvDepartamentos_CellClick;
                dgvDepartamentos.CellClick += dgvDepartamentos_CellClick;
            }

            this.Load -= Departamentos_Load;
            this.Load += Departamentos_Load;
        }

        // Método para aplicar el diseño personalizado a los controles del formulario
        private void Diseño()
        {
            Color azulMuyOscuro = ColorTranslator.FromHtml("#0D1321");
            Color azulOscuro = ColorTranslator.FromHtml("#1D2D44");
            Color azulAcero = ColorTranslator.FromHtml("#3E5C76");
            Color azulClaro = ColorTranslator.FromHtml("#748CAB");
            Color cremaClaro = ColorTranslator.FromHtml("#F0EBD8");

            this.BackColor = cremaClaro;
            this.ForeColor = azulMuyOscuro;

            Button[] botones = { btnGuardar, btnModificar, btnEliminar, btnLimpiar, btnBuscar };
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

            if (dgvDepartamentos != null)
            {
                dgvDepartamentos.BackgroundColor = cremaClaro;
                dgvDepartamentos.BorderStyle = BorderStyle.None;
                dgvDepartamentos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvDepartamentos.GridColor = azulClaro;
                dgvDepartamentos.EnableHeadersVisualStyles = false;

                dgvDepartamentos.ColumnHeadersDefaultCellStyle.BackColor = azulOscuro;
                dgvDepartamentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvDepartamentos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvDepartamentos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvDepartamentos.ColumnHeadersHeight = 35;

                dgvDepartamentos.DefaultCellStyle.BackColor = Color.White;
                dgvDepartamentos.DefaultCellStyle.ForeColor = azulMuyOscuro;
                dgvDepartamentos.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                dgvDepartamentos.DefaultCellStyle.SelectionBackColor = azulClaro;
                dgvDepartamentos.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvDepartamentos.RowTemplate.Height = 35;
            }

            TextBox[] cajasDeTexto = { txtNombreDepto, txtDescripcion };
            foreach (TextBox txt in cajasDeTexto)
            {
                if (txt == null) continue;
                txt.Enter += (s, e) => { txt.BackColor = Color.White; };
                txt.Leave += (s, e) => { txt.BackColor = ColorTranslator.FromHtml("#F9F9F9"); };
            }
        }

        private void Departamentos_Load(object? sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDepartamentos();
            CargarComboEncargados();
            LimpiarCampos();
        }

        // Configura las propiedades del DataGridView para mejorar la experiencia del usuario
        private void ConfigurarDataGridView()
        {
            if (dgvDepartamentos != null)
            {
                dgvDepartamentos.AllowUserToAddRows = false;
                dgvDepartamentos.AllowUserToDeleteRows = false;
                dgvDepartamentos.ReadOnly = true;
                dgvDepartamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvDepartamentos.MultiSelect = false;
                dgvDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        // Carga los departamentos desde la base de datos y los muestra en el DataGridView
        private void CargarDepartamentos()
        {
            try
            {
                if (dgvDepartamentos != null)
                {
                    dgvDepartamentos.DataSource = controlador.ListarDepartamentos();
                    OcultarColumnasId();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los departamentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Oculta las columnas de ID en el DataGridView para mejorar la presentación
        private void OcultarColumnasId()
        {
            if (dgvDepartamentos == null) return;

            if (dgvDepartamentos.Columns["ID_EMPLEADO"] != null)
                dgvDepartamentos.Columns["ID_EMPLEADO"].Visible = false;

            if (dgvDepartamentos.Columns["ID_DEPARTAMENTO"] != null)
                dgvDepartamentos.Columns["ID_DEPARTAMENTO"].Visible = false;
        }

        // Carga los encargados desde la base de datos y los muestra en el ComboBox
        private void CargarComboEncargados()
        {
            try
            {
                DataTable dt = controlador.ListarEncargadosCombo();

                DataRow fila = dt.NewRow();
                fila["ID_EMPLEADO"] = DBNull.Value;
                fila["NOMBRE_COMPLETO"] = "-- Sin Encargado / Vacante --";
                dt.Rows.InsertAt(fila, 0);

                if (cbEncargado != null)
                {
                    cbEncargado.DataSource = dt;
                    cbEncargado.DisplayMember = "NOMBRE_COMPLETO";
                    cbEncargado.ValueMember = "ID_EMPLEADO";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón Guardar para registrar un nuevo departamento
        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreDepto?.Text))
            {
                MessageBox.Show("El nombre del departamento es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreDepto?.Focus();
                return;
            }

            Departamento nuevo = new Departamento
            {
                IdEmpleado = (cbEncargado?.SelectedValue == null || cbEncargado.SelectedValue == DBNull.Value)
                             ? null
                             : Convert.ToInt32(cbEncargado.SelectedValue),
                NombreDepartamento = txtNombreDepto.Text.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcion?.Text) ? null : txtDescripcion.Text.Trim()
            };

            if (controlador.GuardarNuevoDepartamento(nuevo, out string mensaje))
            {
                MessageBox.Show("Departamento registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDepartamentos();
            }
            else
            {
                MessageBox.Show("Error al guardar el departamento: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón Modificar para actualizar un departamento existente
        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idDepartamentoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un departamento de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreDepto?.Text))
            {
                MessageBox.Show("El nombre del departamento es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreDepto?.Focus();
                return;
            }

            Departamento actualizado = new Departamento
            {
                IdDepartamento = idDepartamentoSeleccionado,
                IdEmpleado = (cbEncargado?.SelectedValue == null || cbEncargado.SelectedValue == DBNull.Value)
                             ? null
                             : Convert.ToInt32(cbEncargado.SelectedValue),
                NombreDepartamento = txtNombreDepto.Text.Trim(),
                Descripcion = string.IsNullOrWhiteSpace(txtDescripcion?.Text) ? null : txtDescripcion.Text.Trim()
            };

            if (controlador.ModificarDepartamento(actualizado, out string mensaje))
            {
                MessageBox.Show("Departamento actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarDepartamentos();
            }
            else
            {
                MessageBox.Show("Error al modificar el departamento: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón Eliminar para borrar un departamento seleccionado
        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idDepartamentoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un departamento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este departamento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (controlador.EliminarDepartamento(idDepartamentoSeleccionado, out string mensaje))
                {
                    MessageBox.Show("Departamento eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDepartamentos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el departamento: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Evento del DataGridView para seleccionar un departamento y mostrar sus detalles en los campos de texto
        private void dgvDepartamentos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDepartamentos != null)
            {
                DataGridViewRow row = dgvDepartamentos.Rows[e.RowIndex];

                if (row.Cells["ID_DEPARTAMENTO"].Value != null)
                {
                    idDepartamentoSeleccionado = Convert.ToInt32(row.Cells["ID_DEPARTAMENTO"].Value);
                }

                if (txtNombreDepto != null && row.Cells["NOMBRE_DEPARTAMENTO"].Value != DBNull.Value)
                    txtNombreDepto.Text = row.Cells["NOMBRE_DEPARTAMENTO"].Value.ToString();

                if (txtDescripcion != null)
                    txtDescripcion.Text = (row.Cells["DESCRIPCION"].Value != DBNull.Value) ? row.Cells["DESCRIPCION"].Value.ToString() : "";

                if (cbEncargado != null)
                {
                    if (row.Cells["ID_EMPLEADO"].Value != DBNull.Value && row.Cells["ID_EMPLEADO"].Value != null)
                    {
                        cbEncargado.SelectedValue = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);
                    }
                    else
                    {
                        cbEncargado.SelectedValue = DBNull.Value;
                    }
                }
            }
        }

        // Limpia los campos de entrada y restablece el estado del formulario
        private void LimpiarCampos()
        {
            idDepartamentoSeleccionado = 0;
            if (txtNombreDepto != null) txtNombreDepto.Clear();
            if (txtDescripcion != null) txtDescripcion.Clear();
            if (cbEncargado != null && cbEncargado.Items.Count > 0) cbEncargado.SelectedIndex = 0;
            if (txtNombreDepto != null) txtNombreDepto.Focus();
            if (txtBuscar != null) txtBuscar.Clear();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // Evento del botón Buscar para filtrar los departamentos por nombre
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nombreBuscado = txtBuscar?.Text.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(nombreBuscado) || nombreBuscado == "Buscar por nombre del departamento")
            {
                MessageBox.Show("Por favor ingrese el nombre del departamento para buscar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CargarDepartamentos();
                txtBuscar?.Focus();
                return;
            }

            try
            {
                DataTable dtFiltrado = controlador.BuscarDepartamentos(nombreBuscado);

                if (dgvDepartamentos != null)
                {
                    dgvDepartamentos.DataSource = dtFiltrado;
                    OcultarColumnasId();
                }

                if (dtFiltrado.Rows.Count > 0)
                {
                    DataRow fila = dtFiltrado.Rows[0];

                    idDepartamentoSeleccionado = Convert.ToInt32(fila["ID_DEPARTAMENTO"]);

                    if (txtNombreDepto != null && fila["NOMBRE_DEPARTAMENTO"] != DBNull.Value)
                        txtNombreDepto.Text = fila["NOMBRE_DEPARTAMENTO"].ToString();

                    if (txtDescripcion != null)
                        txtDescripcion.Text = (fila["DESCRIPCION"] != DBNull.Value) ? fila["DESCRIPCION"].ToString() : "";

                    if (cbEncargado != null)
                    {
                        if (fila["ID_EMPLEADO"] != DBNull.Value && fila["ID_EMPLEADO"] != null)
                        {
                            cbEncargado.SelectedValue = Convert.ToInt32(fila["ID_EMPLEADO"]);
                        }
                        else
                        {
                            cbEncargado.SelectedValue = DBNull.Value;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún departamento con ese nombre.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarDepartamentos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}