using SistemaPlanilla.Modelo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Empleado : Form
    {
        private int idEmpleadoSeleccionado = 0;

        public Empleado()
        {
            InitializeComponent();

            if (btnGuardar != null) btnGuardar.Click += btnGuardar_Click;
            if (btnModificar != null) btnModificar.Click += btnModificar_Click;
            if (btnEliminar != null) btnEliminar.Click += btnEliminar_Click;
            if (btnLimpiar != null) btnLimpiar.Click += btnLimpiar_Click;
            if (dgvEmpleados != null) dgvEmpleados.CellClick += dgvEmpleados_CellClick;

            this.Load += Empleado_Load;
        }

        private void Empleado_Load(object? sender, EventArgs e)
        {
            CargarEmpleados();
            CargarComboCargos();
            LimpiarCampos();
        }

        private void CargarEmpleados()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = @"SELECT e.ID_EMPLEADO, e.NOMBRE_COMPLETO, e.DUI, e.DIRECCION, 
                                            e.TELEFONO, e.ESTADO, c.NOMBRE_CARGO, e.ID_CARGO 
                                     FROM EMPLEADO e
                                     INNER JOIN CARGO c ON e.ID_CARGO = c.ID_CARGO";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dgvEmpleados != null)
                            {
                                dgvEmpleados.DataSource = dt;
                                if (dgvEmpleados.Columns["ID_CARGO"] != null)
                                    dgvEmpleados.Columns["ID_CARGO"].Visible = false;
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

        private void CargarComboCargos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = "SELECT ID_CARGO, NOMBRE_CARGO FROM CARGO";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            DataRow fila = dt.NewRow();
                            fila["ID_CARGO"] = 0;
                            fila["NOMBRE_CARGO"] = "-- Seleccione un Cargo --";
                            dt.Rows.InsertAt(fila, 0);

                            if (cbCargos != null)
                            {
                                cbCargos.DataSource = dt;
                                cbCargos.DisplayMember = "NOMBRE_CARGO";
                                cbCargos.ValueMember = "ID_CARGO";
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

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre?.Text) || string.IsNullOrWhiteSpace(txtDui?.Text) || cbCargos?.SelectedValue == null || Convert.ToInt32(cbCargos.SelectedValue) == 0)
                {
                    MessageBox.Show("Por favor complete los campos obligatorios y seleccione un cargo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = "INSERT INTO EMPLEADO (NOMBRE_COMPLETO, DUI, DIRECCION, TELEFONO, ESTADO, ID_CARGO, SALARIO_BASE) VALUES (@NOMBRE, @DUI, @DIR, @TEL, 'ACTIVO', @CARGO, 0.00)";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@NOMBRE", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@DUI", txtDui.Text.Trim());
                        cmd.Parameters.AddWithValue("@DIR", txtDireccion?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@TEL", txtTelefono?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@CARGO", Convert.ToInt32(cbCargos.SelectedValue));

                        conexion.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Empleado registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarEmpleados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado de la tabla para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.CadenaConexion))
                {
                    string query = "UPDATE EMPLEADO SET NOMBRE_COMPLETO = @NOMBRE, DUI = @DUI, DIRECCION = @DIR, TELEFONO = @TEL, ID_CARGO = @CARGO WHERE ID_EMPLEADO = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID", idEmpleadoSeleccionado);
                        cmd.Parameters.AddWithValue("@NOMBRE", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@DUI", txtDui.Text.Trim());
                        cmd.Parameters.AddWithValue("@DIR", txtDireccion?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@TEL", txtTelefono?.Text.Trim() ?? "");
                        cmd.Parameters.AddWithValue("@CARGO", Convert.ToInt32(cbCargos?.SelectedValue ?? 0));

                        conexion.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Empleado actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarEmpleados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            if (idEmpleadoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un empleado para dar de baja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de dar de baja a este empleado?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conexion = new SqlConnection(Conexion.CadenaConexion))
                    {
                        string query = "UPDATE EMPLEADO SET ESTADO = 'INACTIVO' WHERE ID_EMPLEADO = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conexion))
                        {
                            cmd.Parameters.AddWithValue("@ID", idEmpleadoSeleccionado);
                            conexion.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Empleado dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarCampos();
                            CargarEmpleados();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEmpleados_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvEmpleados != null)
            {
                DataGridViewRow row = dgvEmpleados.Rows[e.RowIndex];
                idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["ID_EMPLEADO"].Value);

                if (txtNombre != null) txtNombre.Text = row.Cells["NOMBRE_COMPLETO"].Value.ToString();
                if (txtDui != null) txtDui.Text = row.Cells["DUI"].Value.ToString();
                if (txtDireccion != null) txtDireccion.Text = row.Cells["DIRECCION"].Value.ToString();
                if (txtTelefono != null) txtTelefono.Text = row.Cells["TELEFONO"].Value.ToString();

                if (cbCargos != null && row.Cells["ID_CARGO"].Value != DBNull.Value)
                {
                    cbCargos.SelectedValue = Convert.ToInt32(row.Cells["ID_CARGO"].Value);
                }
            }
        }

        private void LimpiarCampos()
        {
            idEmpleadoSeleccionado = 0;
            if (txtNombre != null) txtNombre.Clear();
            if (txtDui != null) txtDui.Clear();
            if (txtDireccion != null) txtDireccion.Clear();
            if (txtTelefono != null) txtTelefono.Clear();
            if (cbCargos != null && cbCargos.Items.Count > 0) cbCargos.SelectedIndex = 0;
            if (txtNombre != null) txtNombre.Focus();
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}