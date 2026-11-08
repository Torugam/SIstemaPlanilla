using SistemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SIstemaPlanilla.Vista
{
    public partial class Contratos : Form
    {
        private int idContratoSeleccionado = 0;

        public Contratos()
        {
            InitializeComponent();

            bool enModoDiseno = this.DesignMode || (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

            if (!enModoDiseno)
            {
                // Código inicial si se requiere
            }

            this.Load += Contratos_Load;
        }

        private void Contratos_Load(object? sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarComboEmpleados();
            CargarComboTipoContrato();
            CargarContratos();
        }

        // ---------- Búsqueda de controles ----------

        // Recorre todos los controles del formulario (incluso los que están dentro de paneles)
        private IEnumerable<Control> TodosLosControles(Control padre)
        {
            foreach (Control hijo in padre.Controls)
            {
                yield return hijo;
                foreach (Control nieto in TodosLosControles(hijo))
                    yield return nieto;
            }
        }

        // ComboBox de empleados
        private ComboBox? ObtenerComboEmpleado()
        {
            foreach (Control c in TodosLosControles(this))
            {
                if (c is ComboBox cb && (c.Name.Contains("Empleado", StringComparison.OrdinalIgnoreCase) || c.Name.Contains("comboBox1", StringComparison.OrdinalIgnoreCase)))
                    return cb;
            }
            return null;
        }

        // ComboBox de tipo de contrato.
        // Si en el formulario ese campo es un TextBox, lo reemplaza automáticamente por un ComboBox.
        private ComboBox? ObtenerComboTipoContrato()
        {
            ComboBox? comboEmpleado = ObtenerComboEmpleado();

            // 1) Si ya existe un ComboBox (que no sea el de empleados), se usa ese
            foreach (Control c in TodosLosControles(this))
            {
                if (c is ComboBox cb && cb != comboEmpleado)
                    return cb;
            }

            // 2) Si no existe, buscamos el TextBox de "Tipo Contrato"
            Control? campo = null;

            // 2a) Por nombre del control
            foreach (Control c in TodosLosControles(this))
            {
                if (c is TextBoxBase && (c.Name.Contains("Tipo", StringComparison.OrdinalIgnoreCase) || c.Name.Contains("Contrato", StringComparison.OrdinalIgnoreCase)))
                {
                    campo = c;
                    break;
                }
            }

            // 2b) Por cercanía al label "Tipo Contrato"
            if (campo == null)
            {
                Label? etiqueta = null;
                foreach (Control c in TodosLosControles(this))
                {
                    if (c is Label lb && lb.Text.Contains("Tipo", StringComparison.OrdinalIgnoreCase) && lb.Text.Contains("Contrato", StringComparison.OrdinalIgnoreCase))
                    {
                        etiqueta = lb;
                        break;
                    }
                }

                if (etiqueta != null)
                {
                    int mejorDistancia = int.MaxValue;
                    foreach (Control c in TodosLosControles(this))
                    {
                        if (c is TextBoxBase && c.Parent == etiqueta.Parent && Math.Abs(c.Top - etiqueta.Top) < 30 && c.Left >= etiqueta.Left)
                        {
                            int distancia = c.Left - etiqueta.Left;
                            if (distancia < mejorDistancia)
                            {
                                mejorDistancia = distancia;
                                campo = c;
                            }
                        }
                    }
                }
            }

            if (campo == null || campo.Parent == null) return null;

            // 3) Reemplazamos el TextBox por un ComboBox en la misma posición y tamaño
            ComboBox nuevo = new ComboBox();
            nuevo.Name = "cbTipoContrato";
            nuevo.Font = campo.Font;
            nuevo.Location = campo.Location;
            nuevo.Width = campo.Width;
            nuevo.Anchor = campo.Anchor;
            nuevo.TabIndex = campo.TabIndex;

            Control padreCampo = campo.Parent;
            int indice = padreCampo.Controls.GetChildIndex(campo);
            padreCampo.Controls.Remove(campo);
            padreCampo.Controls.Add(nuevo);
            padreCampo.Controls.SetChildIndex(nuevo, indice);
            campo.Dispose();

            return nuevo;
        }

        private void ConfigurarDataGridView()
        {
            DataGridView? tabla = null;
            foreach (Control c in this.Controls)
            {
                if (c is DataGridView dgv)
                {
                    tabla = dgv;
                    break;
                }
            }

            if (tabla != null)
            {
                tabla.AllowUserToAddRows = false;
                tabla.AllowUserToDeleteRows = false;
                tabla.ReadOnly = true;
                tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                tabla.MultiSelect = false;
                tabla.Height = 280;
                tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (tabla.Columns["ID_CONTRATO"] != null)
                {
                    tabla.Columns["ID_CONTRATO"].HeaderText = "ID";
                    tabla.Columns["ID_CONTRATO"].Width = 50;
                    tabla.Columns["ID_CONTRATO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (tabla.Columns["NOMBRE_COMPLETO"] != null)
                    tabla.Columns["NOMBRE_COMPLETO"].HeaderText = "Nombre Completo";

                if (tabla.Columns["SALARIO_BASE"] != null)
                    tabla.Columns["SALARIO_BASE"].HeaderText = "Salario Base";

                if (tabla.Columns["TIPO_CONTRATO"] != null)
                    tabla.Columns["TIPO_CONTRATO"].HeaderText = "Tipo de Contrato";

                if (tabla.Columns["FECHA_INICIO_CONTRATO"] != null)
                    tabla.Columns["FECHA_INICIO_CONTRATO"].HeaderText = "Fecha Inicio";

                if (tabla.Columns["FECHA_FIN_CONTRATO"] != null)
                    tabla.Columns["FECHA_FIN_CONTRATO"].HeaderText = "Fecha Fin";
            }
        }

        // Empleado: buscador por nombre (escribes y sugiere)
        private void CargarComboEmpleados()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    string query = "SELECT ID_EMPLEADO, NOMBRE_COMPLETO FROM EMPLEADO WHERE ESTADO = 'ACTIVO' ORDER BY NOMBRE_COMPLETO ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            ComboBox? cb = ObtenerComboEmpleado();
                            if (cb != null)
                            {
                                cb.Visible = true;
                                cb.AutoCompleteMode = AutoCompleteMode.None;

                                cb.DataSource = dt;
                                cb.DisplayMember = "NOMBRE_COMPLETO";
                                cb.ValueMember = "ID_EMPLEADO";

                                // Buscador
                                cb.DropDownStyle = ComboBoxStyle.DropDown;
                                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                                cb.SelectedIndex = -1;
                                cb.Text = string.Empty;
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

        // Tipo de contrato: solo 2 opciones -> Permanente y Temporal (no se puede escribir)
        private void CargarComboTipoContrato()
        {
            try
            {
                DataTable dtTipo = new DataTable();
                dtTipo.Columns.Add("Tipo", typeof(string));

                dtTipo.Rows.Add("Permanente");
                dtTipo.Rows.Add("Temporal");

                ComboBox? cb = ObtenerComboTipoContrato();
                if (cb != null)
                {
                    cb.Visible = true;
                    cb.DataSource = dtTipo;
                    cb.DisplayMember = "Tipo";
                    cb.ValueMember = "Tipo";
                    cb.DropDownStyle = ComboBoxStyle.DropDownList; // no permite escribir
                    cb.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tipos de contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarContratos()
        {
            try
            {
                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Listar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (Control c in this.Controls)
                            {
                                if (c is DataGridView tabla)
                                {
                                    tabla.DataSource = dt;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los contratos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                int idEmpleadoSeleccionado = 0;
                string tipoContratoSeleccionado = string.Empty;
                decimal salarioBase = 0;
                DateTime fechaInicio = DateTime.Now;
                DateTime fechaFin = DateTime.Now;

                // Recorremos los controles del formulario para capturar los valores de forma segura
                foreach (Control c in this.Controls)
                {
                    if (c is ComboBox cb)
                    {
                        if (cb.Name.Contains("Empleado", StringComparison.OrdinalIgnoreCase) && cb.SelectedValue != null)
                        {
                            int.TryParse(cb.SelectedValue.ToString(), out idEmpleadoSeleccionado);
                        }
                        else if ((cb.Name.Contains("Contrato", StringComparison.OrdinalIgnoreCase) || cb.Name.Contains("Tipo", StringComparison.OrdinalIgnoreCase)) && cb.SelectedItem != null)
                        {
                            tipoContratoSeleccionado = cb.Text;
                        }
                    }
                    else if (c is TextBox tb)
                    {
                        if (tb.Name.Contains("Salario", StringComparison.OrdinalIgnoreCase))
                        {
                            decimal.TryParse(tb.Text.Trim(), out salarioBase);
                        }
                    }
                    else if (c is DateTimePicker dtp)
                    {
                        if (dtp.Name.Contains("Inicio", StringComparison.OrdinalIgnoreCase))
                        {
                            fechaInicio = dtp.Value;
                        }
                        else if (dtp.Name.Contains("Fin", StringComparison.OrdinalIgnoreCase))
                        {
                            fechaFin = dtp.Value;
                        }
                    }
                }

                if (idEmpleadoSeleccionado == 0)
                {
                    MessageBox.Show("Por favor seleccione un empleado de la lista.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(tipoContratoSeleccionado) || tipoContratoSeleccionado.Contains("Seleccione"))
                {
                    MessageBox.Show("Por favor seleccione el tipo de contrato.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conexion = Conexion.GetConexion())
                {
                    if (conexion == null) return;

                    using (SqlCommand cmd = new SqlCommand("sp_Contrato_Insertar", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Enviamos todos los parámetros requeridos por tu procedimiento almacenado
                        cmd.Parameters.AddWithValue("@ID_EMPLEADO", idEmpleadoSeleccionado);
                        cmd.Parameters.AddWithValue("@TIPO_CONTRATO", tipoContratoSeleccionado);
                        cmd.Parameters.AddWithValue("@SALARIO_BASE", salarioBase);
                        cmd.Parameters.AddWithValue("@FECHA_INICIO", fechaInicio);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", tipoContratoSeleccionado.Equals("Permanente", StringComparison.OrdinalIgnoreCase) ? (object)DBNull.Value : fechaFin);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Contrato registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarContratos();
                        LimpiarCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            idContratoSeleccionado = 0;
            CargarComboEmpleados();
            CargarComboTipoContrato();
            foreach (Control c in TodosLosControles(this))
            {
                if (c is TextBox tb) tb.Clear();
            }
        }
    }
}
