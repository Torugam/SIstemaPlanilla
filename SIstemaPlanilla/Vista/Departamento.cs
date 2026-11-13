using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Departamentos : Form
    {
        private int idDepartamentoSeleccionado = 0;

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

        private void CargarDepartamentos()
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

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dgvDepartamentos != null)
                            {
                                dgvDepartamentos.DataSource = dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los departamentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarComboEncargados()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    string query = "SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM Empleado";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreDepto?.Text))
                {
                    MessageBox.Show("El nombre del departamento es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreDepto?.Focus();
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cbEncargado?.SelectedValue == null || cbEncargado.SelectedValue == DBNull.Value)
                        {
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", Convert.ToInt32(cbEncargado.SelectedValue));
                        }

                        cmd.Parameters.AddWithValue("@NOMBRE_DEPARTAMENTO", txtNombreDepto.Text.Trim());
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(txtDescripcion?.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Departamento registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarDepartamentos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idDepartamentoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un departamento de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtNombreDepto?.Text))
                {
                    MessageBox.Show("El nombre del departamento es obligatorio.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombreDepto?.Focus();
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Actualizar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", idDepartamentoSeleccionado);

                        if (cbEncargado?.SelectedValue == null || cbEncargado.SelectedValue == DBNull.Value)
                        {
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", Convert.ToInt32(cbEncargado.SelectedValue));
                        }

                        cmd.Parameters.AddWithValue("@NOMBRE_DEPARTAMENTO", txtNombreDepto.Text.Trim());
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(txtDescripcion?.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Departamento actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimpiarCampos();
                        CargarDepartamentos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idDepartamentoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un departamento para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este departamento?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = Conexion.GetConexion())
                    {
                        if (conexion == null) return;

                        using (SqlCommand cmd = new SqlCommand("sp_Departamento_Eliminar", conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", idDepartamentoSeleccionado);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Departamento eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LimpiarCampos();
                            CargarDepartamentos();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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

        private void LimpiarCampos()
        {
            idDepartamentoSeleccionado = 0;
            if (txtNombreDepto != null) txtNombreDepto.Clear();
            if (txtDescripcion != null) txtDescripcion.Clear();
            if (cbEncargado != null && cbEncargado.Items.Count > 0) cbEncargado.SelectedIndex = 0;
            if (txtNombreDepto != null) txtNombreDepto.Focus();
            if(txtBuscar != null) txtBuscar.Clear();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }

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
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null)
                    {
                        MessageBox.Show("Fallo al conectar con la base de datos.", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataView dv = dt.DefaultView;
                            dv.RowFilter = $"NOMBRE_DEPARTAMENTO LIKE '%{nombreBuscado.Replace("'", "''")}%'";
                            DataTable dtFiltrado = dv.ToTable();

                            if (dgvDepartamentos != null)
                            {
                                dgvDepartamentos.DataSource = dtFiltrado;
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}