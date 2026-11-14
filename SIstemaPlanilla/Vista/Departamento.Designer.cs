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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Departamentos));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            label1.Location = new Point(54, 53);
            label1.Name = "label1";
            label1.Size = new Size(255, 38);
            label1.TabIndex = 0;
            label1.Text = "DEPARTAMENTOS";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(100, 183);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(163, 20);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre departamento";
            // 
            // txtNombreDepto
            // 
            txtNombreDepto.Location = new Point(281, 183);
            txtNombreDepto.Name = "txtNombreDepto";
            txtNombreDepto.PlaceholderText = "Nombre";
            txtNombreDepto.Size = new Size(220, 27);
            txtNombreDepto.TabIndex = 4;
            // 
            // lblEncargado
            // 
            lblEncargado.AutoSize = true;
            lblEncargado.Location = new Point(574, 183);
            lblEncargado.Name = "lblEncargado";
            lblEncargado.Size = new Size(83, 20);
            lblEncargado.TabIndex = 5;
            lblEncargado.Text = "Encargado:";
            // 
            // cbEncargado
            // 
            cbEncargado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEncargado.FormattingEnabled = true;
            cbEncargado.Location = new Point(683, 180);
            cbEncargado.Name = "cbEncargado";
            cbEncargado.Size = new Size(220, 28);
            cbEncargado.TabIndex = 6;
            // 
            // lblDesc
            // 
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(100, 230);
            lblDesc.Name = "lblDesc";
            lblDesc.Size = new Size(90, 20);
            lblDesc.TabIndex = 7;
            lblDesc.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(211, 227);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripción";
            txtDescripcion.Size = new Size(303, 62);
            txtDescripcion.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(1023, 313);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(101, 32);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(932, 272);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvDepartamentos
            // 
            dgvDepartamentos.AllowUserToAddRows = false;
            dgvDepartamentos.AllowUserToDeleteRows = false;
            dgvDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDepartamentos.BackgroundColor = Color.FromArgb(240, 235, 216);
            dgvDepartamentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDepartamentos.Location = new Point(47, 313);
            dgvDepartamentos.MultiSelect = false;
            dgvDepartamentos.Name = "dgvDepartamentos";
            dgvDepartamentos.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDepartamentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDepartamentos.RowHeadersVisible = false;
            dgvDepartamentos.RowHeadersWidth = 51;
            dgvDepartamentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartamentos.Size = new Size(946, 265);
            dgvDepartamentos.TabIndex = 11;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(1023, 401);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(101, 32);
            btnEliminar.TabIndex = 12;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(1023, 361);
            btnModificar.Margin = new Padding(3, 4, 3, 4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(101, 32);
            btnModificar.TabIndex = 13;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(326, 111);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(52, 20);
            lblBuscar.TabIndex = 21;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(374, 107);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre del departamento";
            txtBuscar.Size = new Size(372, 27);
            txtBuscar.TabIndex = 20;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(763, 105);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(86, 31);
            btnBuscar.TabIndex = 19;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // Departamentos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1167, 771);
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