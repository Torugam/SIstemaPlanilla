using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Capacitaciones : Form
    {
        private int idCapacitacionSeleccionada = 0;
        private TextBox txtBuscar = null!;

        public Capacitaciones()
        {
            InitializeComponent();

            // Configuración del formulario (sin bordes de ventana y limpio)
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ControlBox = true;
            this.ShowIcon = false;

            // Evita errores de diseño gráficos en Visual Studio
            bool enModoDiseno = this.DesignMode || (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

            if (!enModoDiseno)
            {
                CrearTituloYBuscadorPorCodigo();
            }

            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
            if (btnModificar != null) btnModificar.Click += btnModificar_Click;
            if (btnEliminar != null) btnEliminar.Click += btnEliminar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;
            if (dgvCapacitaciones != null) dgvCapacitaciones.CellClick += dgvCapacitaciones_CellClick;

            this.Load += Capacitaciones_Load;
        }

        private void CrearTituloYBuscadorPorCodigo()
        {
            // 1. Título grande estilo "EMPLEADOS"
            Label lblTitulo = new Label
            {
                Text = "CAPACITACIONES",
                AutoSize = true,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(20, 18)
            };

            // 2. Etiqueta y Caja de Texto para el buscador a la par del título
            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Font = new Font(this.Font, FontStyle.Bold),
                Location = new Point(230, 22)
            };

            txtBuscar = new TextBox
            {
                Width = 220,
                Location = new Point(285, 19)
            };
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtBuscar);

            lblTitulo.BringToFront();
            lblBuscar.BringToFront();
            txtBuscar.BringToFront();
        }

        private void Capacitaciones_Load(object? sender, EventArgs e)
        {
            // Ajustamos la posición del GroupBox de datos un poco más abajo para dejar espacio al título y buscador
            if (gbDatos != null)
            {
                gbDatos.Location = new Point(20, 55);
            }

            // Ajustamos la posición de la tabla y los botones acorde al nuevo espacio
            ConfigurarDataGridView();
            CargarCapacitaciones();
            LimpiarCampos();
        }

        private void ConfigurarDataGridView()
        {
            if (dgvCapacitaciones != null)
            {
                dgvCapacitaciones.AllowUserToAddRows = false;
                dgvCapacitaciones.AllowUserToDeleteRows = false;
                dgvCapacitaciones.ReadOnly = true;
                dgvCapacitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCapacitaciones.MultiSelect = false;

                // Posicionamos la tabla debajo del buscador
                dgvCapacitaciones.Location = new Point(20, 250);
                dgvCapacitaciones.Height = 190;
                dgvCapacitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvCapacitaciones.Columns["ID_CAPACITACION"] != null)
                {
                    dgvCapacitaciones.Columns["ID_CAPACITACION"].HeaderText = "ID";
                    dgvCapacitaciones.Columns["ID_CAPACITACION"].Width = 60;
                    dgvCapacitaciones.Columns["ID_CAPACITACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (dgvCapacitaciones.Columns["NOMBRE_CAPACITACION"] != null)
                    dgvCapacitaciones.Columns["NOMBRE_CAPACITACION"].HeaderText = "Capacitación";

                if (dgvCapacitaciones.Columns["DESCRIPCION"] != null)
                    dgvCapacitaciones.Columns["DESCRIPCION"].HeaderText = "Descripción";

                if (dgvCapacitaciones.Columns["HORAS_DURACION"] != null)
                {
                    dgvCapacitaciones.Columns["HORAS_DURACION"].HeaderText = "Horas";
                    dgvCapacitaciones.Columns["HORAS_DURACION"].Width = 80;
                    dgvCapacitaciones.Columns["HORAS_DURACION"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }
            }
        }

        private void CargarCapacitaciones(string filtro = "")
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Capacitacion_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (!string.IsNullOrWhiteSpace(filtro))
                            {
                                DataView dv = dt.DefaultView;
                                dv.RowFilter = $"NOMBRE_CAPACITACION LIKE '%{filtro.Replace("'", "''")}%' OR DESCRIPCION LIKE '%{filtro.Replace("'", "''")}%'";
                                dt = dv.ToTable();
                            }

                            if (dgvCapacitaciones != null)
                            {
                                dgvCapacitaciones.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las capacitaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar?.Text ?? string.Empty;
            CargarCapacitaciones(textoBusqueda);
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                // Ya no validamos ID porque es autoincrementable por IDENTITY en la base de datos
                if (string.IsNullOrWhiteSpace(txtNombreCapacitacion?.Text) || string.IsNullOrWhiteSpace(txtHoras?.Text))
                {
                    MessageBox.Show("El Nombre y las Horas de duración son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Capacitacion_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Se envían únicamente los campos necesarios (sin ID)
                        cmd.Parameters.AddWithValue("@NOMBRE_CAPACITACION", txtNombreCapacitacion.Text.Trim());
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(txtDescripcion?.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@HORAS_DURACION", Convert.ToInt32(txtHoras.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Capacitación registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarCapacitaciones();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la capacitación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idCapacitacionSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una capacitación de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Capacitacion_Actualizar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", idCapacitacionSeleccionada);
                        cmd.Parameters.AddWithValue("@NOMBRE_CAPACITACION", txtNombreCapacitacion.Text.Trim());
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(txtDescripcion?.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@HORAS_DURACION", Convert.ToInt32(txtHoras.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Capacitación actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarCapacitaciones();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la capacitación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idCapacitacionSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una capacitación para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar esta capacitación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = Conexion.GetConexion())
                    {
                        if (conexion == null)
                        {
                            MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        using (SqlCommand cmd = new SqlCommand("sp_Capacitacion_Eliminar", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_CAPACITACION", idCapacitacionSeleccionada);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Capacitación eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarCapacitaciones();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la capacitación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCapacitaciones_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCapacitaciones != null)
            {
                DataGridViewRow row = dgvCapacitaciones.Rows[e.RowIndex];
                idCapacitacionSeleccionada = Convert.ToInt32(row.Cells["ID_CAPACITACION"].Value);

                if (txtNombreCapacitacion != null) txtNombreCapacitacion.Text = row.Cells["NOMBRE_CAPACITACION"].Value.ToString();
                if (txtDescripcion != null) txtDescripcion.Text = row.Cells["DESCRIPCION"].Value != DBNull.Value ? row.Cells["DESCRIPCION"].Value.ToString() : string.Empty;
                if (txtHoras != null) txtHoras.Text = row.Cells["HORAS_DURACION"].Value.ToString();
            }
        }

        private void LimpiarCampos()
        {
            idCapacitacionSeleccionada = 0;
            if (txtNombreCapacitacion != null) txtNombreCapacitacion.Clear();
            if (txtDescripcion != null) txtDescripcion.Clear();
            if (txtHoras != null) txtHoras.Clear();
            if (txtBuscar != null) txtBuscar.Clear();
            if (txtNombreCapacitacion != null) txtNombreCapacitacion.Focus();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}