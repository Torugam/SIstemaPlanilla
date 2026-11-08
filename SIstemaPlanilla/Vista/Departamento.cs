using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Departamentos : Form
    {
        // Ajusta tu cadena de conexión según tu entorno local
        private string cadenaConexion = "Server=.; Database=BD_PLANILLA; Integrated Security=true;";

        public Departamentos()
        {
            InitializeComponent();
        }

        private void Departamentos_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
            CargarComboEncargados();
        }

        // 1. Listar Departamentos en el DataGridView
        private void CargarDepartamentos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvDepartamentos.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los departamentos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Llenar el ComboBox con los empleados disponibles para jefatura
        private void CargarComboEncargados()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
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

                            cbEncargado.DataSource = dt;
                            cbEncargado.DisplayMember = "NOMBRE_COMPLETO";
                            cbEncargado.ValueMember = "ID_EMPLEADO";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIdDepto.Text) || string.IsNullOrWhiteSpace(txtNombreDepto.Text))
                {
                    MessageBox.Show("El ID del Departamento y el Nombre son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Departamento_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_DEPARTAMENTO", Convert.ToInt32(txtIdDepto.Text.Trim()));

                        if (cbEncargado.SelectedValue == null || cbEncargado.SelectedValue == DBNull.Value)
                        {
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ID_EMPLEADO", Convert.ToInt32(cbEncargado.SelectedValue));
                        }

                        cmd.Parameters.AddWithValue("@NOMBRE_DEPARTAMENTO", txtNombreDepto.Text.Trim());
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();

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

        private void LimpiarCampos()
        {
            txtIdDepto.Clear();
            txtNombreDepto.Clear();
            txtDescripcion.Clear();
            if (cbEncargado.Items.Count > 0) cbEncargado.SelectedIndex = 0;
            txtIdDepto.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}