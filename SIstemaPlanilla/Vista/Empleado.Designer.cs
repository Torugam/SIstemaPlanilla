namespace SIstemaPlanilla.Vista
{
    partial class Empleado
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Empleado));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblDui = new Label();
            txtDui = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblCargo = new Label();
            cbCargos = new ComboBox();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            dgvEmpleados = new DataGridView();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            lblBuscar = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleados).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(73, 52);
            label1.Name = "label1";
            label1.Size = new Size(183, 38);
            label1.TabIndex = 0;
            label1.Text = "EMPLEADOS";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(57, 177);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(137, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre Completo:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(219, 177);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Nombre";
            txtNombre.Size = new Size(250, 27);
            txtNombre.TabIndex = 2;
            // 
            // lblDui
            // 
            lblDui.AutoSize = true;
            lblDui.Location = new Point(57, 224);
            lblDui.Name = "lblDui";
            lblDui.Size = new Size(37, 20);
            lblDui.TabIndex = 3;
            lblDui.Text = "DUI:";
            // 
            // txtDui
            // 
            txtDui.Location = new Point(219, 217);
            txtDui.Name = "txtDui";
            txtDui.PlaceholderText = "0000000-0";
            txtDui.Size = new Size(250, 27);
            txtDui.TabIndex = 4;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(579, 180);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(75, 20);
            lblDireccion.TabIndex = 5;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(682, 177);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.PlaceholderText = "Dirección";
            txtDireccion.Size = new Size(230, 27);
            txtDireccion.TabIndex = 6;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(579, 224);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(70, 20);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(682, 221);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "0000-0000";
            txtTelefono.Size = new Size(230, 27);
            txtTelefono.TabIndex = 8;
            // 
            // lblCargo
            // 
            lblCargo.AutoSize = true;
            lblCargo.Location = new Point(57, 263);
            lblCargo.Name = "lblCargo";
            lblCargo.Size = new Size(52, 20);
            lblCargo.TabIndex = 9;
            lblCargo.Text = "Cargo:";
            // 
            // cbCargos
            // 
            cbCargos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCargos.FormattingEnabled = true;
            cbCargos.Location = new Point(219, 260);
            cbCargos.Name = "cbCargos";
            cbCargos.Size = new Size(250, 28);
            cbCargos.TabIndex = 10;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(568, 276);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(86, 32);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(670, 276);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(86, 32);
            btnModificar.TabIndex = 12;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(771, 276);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(92, 32);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Desactivar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(1021, 276);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // dgvEmpleados
            // 
            dgvEmpleados.AllowUserToAddRows = false;
            dgvEmpleados.AllowUserToDeleteRows = false;
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmpleados.BackgroundColor = Color.FromArgb(240, 235, 216);
            dgvEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvEmpleados.DefaultCellStyle = dataGridViewCellStyle1;
            dgvEmpleados.Location = new Point(31, 331);
            dgvEmpleados.MultiSelect = false;
            dgvEmpleados.Name = "dgvEmpleados";
            dgvEmpleados.ReadOnly = true;
            dgvEmpleados.RowHeadersVisible = false;
            dgvEmpleados.RowHeadersWidth = 51;
            dgvEmpleados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleados.Size = new Size(1031, 194);
            dgvEmpleados.TabIndex = 15;
            dgvEmpleados.CellContentClick += dgvEmpleados_CellContentClick;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(745, 93);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(86, 31);
            btnBuscar.TabIndex = 16;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(355, 95);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre del empleado";
            txtBuscar.Size = new Size(372, 27);
            txtBuscar.TabIndex = 17;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(307, 99);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(52, 20);
            lblBuscar.TabIndex = 18;
            lblBuscar.Text = "Buscar";
            // 
            // Empleado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1105, 651);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(dgvEmpleados);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnGuardar);
            Controls.Add(cbCargos);
            Controls.Add(lblCargo);
            Controls.Add(txtTelefono);
            Controls.Add(lblTelefono);
            Controls.Add(txtDireccion);
            Controls.Add(lblDireccion);
            Controls.Add(txtDui);
            Controls.Add(lblDui);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Empleado";
            Text = "Empleado";
            Activated += Empleado_Activated;
            Load += Empleado_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEmpleados).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDui;
        private System.Windows.Forms.TextBox txtDui;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCargo;
        private System.Windows.Forms.ComboBox cbCargos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private Button btnBuscar;
        private TextBox txtBuscar;
        private Label lblBuscar;
    }
}