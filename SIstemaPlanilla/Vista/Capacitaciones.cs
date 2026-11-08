using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Capacitaciones : Form
    {
        private string cadenaConexion = "Server=.; Database=BD_PLANILLA; Integrated Security=true;";

        public Capacitaciones()
        {
            InitializeComponent();
        }

        private void Capacitaciones_Load(object sender, EventArgs e)
        {
            CargarCapacitaciones();
        }

        // 1. Listar Capacitaciones en el DataGridView
        private void CargarCapacitaciones()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Capacitacion_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dgvCapacitaciones.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las capacitaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Botón Guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtIdCapacitacion.Text) || string.IsNullOrWhiteSpace(txtNombreCapacitacion.Text) || string.IsNullOrWhiteSpace(txtHoras.Text))
                {
                    MessageBox.Show("El ID, el Nombre y las Horas de duración son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Capacitacion_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_CAPACITACION", Convert.ToInt32(txtIdCapacitacion.Text.Trim()));
                        cmd.Parameters.AddWithValue("@NOMBRE_CAPACITACION", txtNombreCapacitacion.Text.Trim());
                        cmd.Parameters.AddWithValue("@DESCRIPCION", string.IsNullOrWhiteSpace(txtDescripcion.Text) ? (object)DBNull.Value : txtDescripcion.Text.Trim());
                        cmd.Parameters.AddWithValue("@HORAS_DURACION", Convert.ToInt32(txtHoras.Text.Trim()));

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                        conexion.Close();

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

        private void LimpiarCampos()
        {
            txtIdCapacitacion.Clear();
            txtNombreCapacitacion.Clear();
            txtDescripcion.Clear();
            txtHoras.Clear();
            txtIdCapacitacion.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}