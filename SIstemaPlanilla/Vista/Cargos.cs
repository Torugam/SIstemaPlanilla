using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Cargos : Form
    {
        private int idCargoSeleccionado = 0;
        private TextBox txtBuscar = null!;

        public Cargos()
        {
            InitializeComponent();

            // Configuración visual limpia (sin bordes de ventana molestos)
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.ControlBox = true;
            this.ShowIcon = false;

            bool enModoDiseno = this.DesignMode || (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

            if (!enModoDiseno)
            {
                CrearBuscadorPorCodigo();
            }

            // Suscripción a eventos de botones
            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
            if (btnModificar != null) btnModificar.Click += btnModificar_Click;
            if (btnEliminar != null) btnEliminar.Click += btnEliminar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;
            if (dgvCargos != null) dgvCargos.CellClick += dgvCargos_CellClick;

            this.Load += Cargos_Load;
        }

        private void CrearBuscadorPorCodigo()
        {
            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                AutoSize = true,
                Font = new Font(this.Font.FontFamily, 10, FontStyle.Bold),
                Location = new Point(170, 24)
            };

            txtBuscar = new TextBox
            {
                Width = 200,
                Location = new Point(230, 21)
            };
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            this.Controls.Add(lblBuscar);
            this.Controls.Add(txtBuscar);

            lblBuscar.BringToFront();
            txtBuscar.BringToFront();
        }

        private void Cargos_Load(object? sender, EventArgs e)
        {
            CargarCargos();
            CargarComboCargoSuperior();
            LimpiarCampos();
        }

        private void CargarCargos(string filtro = "")
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

                    // Usando Procedimiento Almacenado para listar
                    using (SqlCommand cmd = new SqlCommand("sp_Cargo_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FILTRO", filtro.Trim());

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            if (dgvCargos != null)
                            {
                                dgvCargos.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cargos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar?.Text ?? string.Empty;
            CargarCargos(textoBusqueda);
        }

        private void CargarComboCargoSuperior()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    // Usando el Procedimiento Almacenado del combo
                    using (SqlCommand cmd = new SqlCommand("sp_Cargo_ListarCombo", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_CARGO"] = DBNull.Value;
                            fila["NOMBRE_CARGO"] = "-- Ninguno (Cargo Principal) --";
                            dt.Rows.InsertAt(fila, 0);

                            if (cbCargoSuperior != null)
                            {
                                cbCargoSuperior.DataSource = dt;
                                cbCargoSuperior.DisplayMember = "NOMBRE_CARGO";
                                cbCargoSuperior.ValueMember = "ID_CARGO";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los cargos superiores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCargo?.Text))
                {
                    MessageBox.Show("El Nombre del Cargo es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Cargo_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cbCargoSuperior?.SelectedValue == null || cbCargoSuperior.SelectedValue == DBNull.Value)
                        {
                            cmd.Parameters.AddWithValue("@CAR_ID_CARGO", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CAR_ID_CARGO", Convert.ToInt32(cbCargoSuperior.SelectedValue));
                        }

                        cmd.Parameters.AddWithValue("@NOMBRE_CARGO", txtNombreCargo.Text.Trim());
                        cmd.Parameters.AddWithValue("@SALARIO_MIN_REF", string.IsNullOrWhiteSpace(txtSalarioMin?.Text) ? 0 : Convert.ToDecimal(txtSalarioMin.Text.Trim()));
                        cmd.Parameters.AddWithValue("@SALARIO_MAX_REF", string.IsNullOrWhiteSpace(txtSalarioMax?.Text) ? 0 : Convert.ToDecimal(txtSalarioMax.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cargo registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarCargos();
                        CargarComboCargoSuperior();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el cargo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idCargoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cargo de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreCargo?.Text))
                {
                    MessageBox.Show("El Nombre del Cargo es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Cargo_Actualizar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CARGO", idCargoSeleccionado);

                        if (cbCargoSuperior?.SelectedValue == null || cbCargoSuperior.SelectedValue == DBNull.Value)
                        {
                            cmd.Parameters.AddWithValue("@CAR_ID_CARGO", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CAR_ID_CARGO", Convert.ToInt32(cbCargoSuperior.SelectedValue));
                        }

                        cmd.Parameters.AddWithValue("@NOMBRE_CARGO", txtNombreCargo.Text.Trim());
                        cmd.Parameters.AddWithValue("@SALARIO_MIN_REF", string.IsNullOrWhiteSpace(txtSalarioMin?.Text) ? 0 : Convert.ToDecimal(txtSalarioMin.Text.Trim()));
                        cmd.Parameters.AddWithValue("@SALARIO_MAX_REF", string.IsNullOrWhiteSpace(txtSalarioMax?.Text) ? 0 : Convert.ToDecimal(txtSalarioMax.Text.Trim()));

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cargo actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarCargos();
                        CargarComboCargoSuperior();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el cargo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idCargoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cargo de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este cargo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = Conexion.GetConexion())
                    {
                        if (conexion == null) return;

                        using (SqlCommand cmd = new SqlCommand("sp_Cargo_Eliminar", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_CARGO", idCargoSeleccionado);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Cargo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LimpiarCampos();
                            CargarCargos();
                            CargarComboCargoSuperior();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el cargo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCargos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCargos != null)
            {
                DataGridViewRow row = dgvCargos.Rows[e.RowIndex];
                idCargoSeleccionado = Convert.ToInt32(row.Cells["ID_CARGO"].Value);

                if (txtNombreCargo != null) txtNombreCargo.Text = row.Cells["NOMBRE_CARGO"].Value.ToString();
                if (txtSalarioMin != null) txtSalarioMin.Text = row.Cells["SALARIO_MIN_REF"].Value.ToString();
                if (txtSalarioMax != null) txtSalarioMax.Text = row.Cells["SALARIO_MAX_REF"].Value.ToString();

                if (row.Cells["CAR_ID_CARGO"].Value != DBNull.Value)
                {
                    if (cbCargoSuperior != null) cbCargoSuperior.SelectedValue = row.Cells["CAR_ID_CARGO"].Value;
                }
                else
                {
                    if (cbCargoSuperior != null && cbCargoSuperior.Items.Count > 0) cbCargoSuperior.SelectedIndex = 0;
                }
            }
        }

        private void LimpiarCampos()
        {
            idCargoSeleccionado = 0;
            if (txtNombreCargo != null) txtNombreCargo.Clear();
            if (txtSalarioMin != null) txtSalarioMin.Clear();
            if (txtSalarioMax != null) txtSalarioMax.Clear();
            if (txtBuscar != null) txtBuscar.Clear();
            if (cbCargoSuperior != null && cbCargoSuperior.Items.Count > 0) cbCargoSuperior.SelectedIndex = 0;
            if (txtNombreCargo != null) txtNombreCargo.Focus();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}