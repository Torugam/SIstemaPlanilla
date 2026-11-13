namespace SIstemaPlanilla.Vista
{
    partial class Departamentos
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
            txtNombreDepto = new TextBox();
            lblEncargado = new Label();
            cbEncargado = new ComboBox();
            lblDesc = new Label();
            txtDescripcion = new TextBox();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            dgvDepartamentos = new DataGridView();
            btnEliminar = new Button();
            btnModificar = new Button();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(47, 40);
            label1.Name = "label1";
            label1.Size = new Size(200, 30);
            label1.TabIndex = 0;
            label1.Text = "DEPARTAMENTOS";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(169, 137);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(89, 15);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre Depto:";
            // 
            // txtNombreDepto
            // 
            txtNombreDepto.Location = new Point(282, 135);
            txtNombreDepto.Margin = new Padding(3, 2, 3, 2);
            txtNombreDepto.Name = "txtNombreDepto";
            txtNombreDepto.Size = new Size(193, 23);
            txtNombreDepto.TabIndex = 4;
            // 
            // lblEncargado
            // 
            lblEncargado.AutoSize = true;
            lblEncargado.Location = new Point(502, 137);
            lblEncargado.Name = "lblEncargado";
            lblEncargado.Size = new Size(66, 15);
            lblEncargado.TabIndex = 5;
            lblEncargado.Text = "Encargado:";
            // 
            // cbEncargado
            // 
            cbEncargado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEncargado.FormattingEnabled = true;
            cbEncargado.Location = new Point(598, 135);
            cbEncargado.Margin = new Padding(3, 2, 3, 2);
            cbEncargado.Name = "cbEncargado";
            cbEncargado.Size = new Size(193, 23);
            cbEncargado.TabIndex = 6;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(186, 172);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(72, 15);
            lblDesc.TabIndex = 7;
            lblDesc.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(282, 170);
            txtDescripcion.Margin = new Padding(3, 2, 3, 2);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(193, 23);
            txtDescripcion.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(492, 184);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(88, 24);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(774, 184);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(88, 24);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvDepartamentos
            // 
            dgvDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDepartamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDepartamentos.Location = new Point(110, 235);
            dgvDepartamentos.Margin = new Padding(3, 2, 3, 2);
            dgvDepartamentos.Name = "dgvDepartamentos";
            dgvDepartamentos.RowHeadersWidth = 51;
            dgvDepartamentos.Size = new Size(816, 239);
            dgvDepartamentos.TabIndex = 11;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(680, 184);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(88, 24);
            btnEliminar.TabIndex = 12;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(586, 184);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(88, 24);
            btnModificar.TabIndex = 13;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(285, 83);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(42, 15);
            lblBuscar.TabIndex = 21;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(327, 80);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(326, 23);
            txtBuscar.TabIndex = 20;
            txtBuscar.Text = "Buscar por nombre del departamento";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(668, 79);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 19;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // Departamentos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 578);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnModificar);
            Controls.Add(btnEliminar);
            Controls.Add(dgvDepartamentos);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(txtDescripcion);
            Controls.Add(lblDesc);
            Controls.Add(cbEncargado);
            Controls.Add(lblEncargado);
            Controls.Add(txtNombreDepto);
            Controls.Add(lblNombre);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Departamentos";
            Text = "Departamentos";
            Load += Departamentos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDepartamentos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombreDepto;
        private System.Windows.Forms.Label lblEncargado;
        private System.Windows.Forms.ComboBox cbEncargado;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvDepartamentos;
        private Button btnEliminar;
        private Button btnModificar;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
    }
}