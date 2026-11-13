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
            label1.Location = new Point(64, 39);
            label1.Name = "label1";
            label1.Size = new Size(143, 30);
            label1.TabIndex = 0;
            label1.Text = "EMPLEADOS";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(165, 135);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(110, 15);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre Completo:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(287, 133);
            txtNombre.Margin = new Padding(3, 2, 3, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(219, 23);
            txtNombre.TabIndex = 2;
            // 
            // lblDui
            // 
            lblDui.AutoSize = true;
            lblDui.Location = new Point(165, 165);
            lblDui.Name = "lblDui";
            lblDui.Size = new Size(29, 15);
            lblDui.TabIndex = 3;
            lblDui.Text = "DUI:";
            // 
            // txtDui
            // 
            txtDui.Location = new Point(287, 163);
            txtDui.Margin = new Padding(3, 2, 3, 2);
            txtDui.Name = "txtDui";
            txtDui.Size = new Size(219, 23);
            txtDui.TabIndex = 4;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(532, 135);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(60, 15);
            lblDireccion.TabIndex = 5;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(611, 133);
            txtDireccion.Margin = new Padding(3, 2, 3, 2);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(202, 23);
            txtDireccion.TabIndex = 6;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(532, 165);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(56, 15);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(611, 163);
            txtTelefono.Margin = new Padding(3, 2, 3, 2);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(202, 23);
            txtTelefono.TabIndex = 8;
            // 
            // lblCargo
            // 
            lblCargo.AutoSize = true;
            lblCargo.Location = new Point(165, 195);
            lblCargo.Name = "lblCargo";
            lblCargo.Size = new Size(42, 15);
            lblCargo.TabIndex = 9;
            lblCargo.Text = "Cargo:";
            // 
            // cbCargos
            // 
            cbCargos.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCargos.FormattingEnabled = true;
            cbCargos.Location = new Point(287, 193);
            cbCargos.Margin = new Padding(3, 2, 3, 2);
            cbCargos.Name = "cbCargos";
            cbCargos.Size = new Size(219, 23);
            cbCargos.TabIndex = 10;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(532, 193);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(70, 24);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(611, 193);
            btnModificar.Margin = new Padding(3, 2, 3, 2);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(70, 24);
            btnModificar.TabIndex = 12;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(689, 193);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(70, 24);
            btnEliminar.TabIndex = 13;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(768, 193);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(66, 24);
            btnLimpiar.TabIndex = 14;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // dgvEmpleados
            // 
            dgvEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmpleados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmpleados.Location = new Point(108, 250);
            dgvEmpleados.Margin = new Padding(3, 2, 3, 2);
            dgvEmpleados.Name = "dgvEmpleados";
            dgvEmpleados.RowHeadersWidth = 51;
            dgvEmpleados.Size = new Size(824, 216);
            dgvEmpleados.TabIndex = 15;
            dgvEmpleados.CellContentClick += dgvEmpleados_CellContentClick;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(652, 70);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 16;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(311, 71);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(326, 23);
            txtBuscar.TabIndex = 17;
            txtBuscar.Text = "Buscar por nombre del empleado";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(269, 74);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(42, 15);
            lblBuscar.TabIndex = 18;
            lblBuscar.Text = "Buscar";
            // 
            // Empleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(967, 488);
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
            Margin = new Padding(3, 2, 3, 2);
            Name = "Empleado";
            Text = "Empleado";
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