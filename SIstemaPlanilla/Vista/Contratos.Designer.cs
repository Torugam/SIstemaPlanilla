namespace SIstemaPlanilla.Vista
{
    partial class Contratos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contratos));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            lblEmpleado = new Label();
            cbEmpleados = new ComboBox();
            lblInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFin = new Label();
            dtpFechaFin = new DateTimePicker();
            lblSalario = new Label();
            txtSalarioBase = new TextBox();
            lblTipo = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            dgvContratos = new DataGridView();
            cbTipoContrato = new ComboBox();
            btnModificar = new Button();
            btnEliminar = new Button();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(62, 60);
            label1.Name = "label1";
            label1.Size = new Size(181, 38);
            label1.TabIndex = 0;
            label1.Text = "CONTRATOS";
            // 
            // lblEmpleado
            // 
            lblEmpleado.AutoSize = true;
            lblEmpleado.Location = new Point(201, 160);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(80, 20);
            lblEmpleado.TabIndex = 1;
            lblEmpleado.Text = "Empleado:";
            // 
            // cbEmpleados
            // 
            cbEmpleados.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEmpleados.FormattingEnabled = true;
            cbEmpleados.Location = new Point(311, 157);
            cbEmpleados.Name = "cbEmpleados";
            cbEmpleados.Size = new Size(220, 28);
            cbEmpleados.TabIndex = 2;
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(201, 200);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(90, 20);
            lblInicio.TabIndex = 3;
            lblInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(311, 197);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(150, 27);
            dtpFechaInicio.TabIndex = 4;
            // 
            // lblFin
            // 
            lblFin.AutoSize = true;
            lblFin.Location = new Point(201, 240);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(73, 20);
            lblFin.TabIndex = 5;
            lblFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Checked = false;
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(311, 237);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(180, 27);
            dtpFechaFin.TabIndex = 6;
            // 
            // lblSalario
            // 
            lblSalario.AutoSize = true;
            lblSalario.Location = new Point(581, 160);
            lblSalario.Name = "lblSalario";
            lblSalario.Size = new Size(93, 20);
            lblSalario.TabIndex = 7;
            lblSalario.Text = "Salario Base:";
            // 
            // txtSalarioBase
            // 
            txtSalarioBase.Location = new Point(701, 157);
            txtSalarioBase.Name = "txtSalarioBase";
            txtSalarioBase.PlaceholderText = "0.00";
            txtSalarioBase.Size = new Size(150, 27);
            txtSalarioBase.TabIndex = 8;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(581, 200);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(104, 20);
            lblTipo.TabIndex = 9;
            lblTipo.Text = "Tipo Contrato:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(993, 318);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(90, 32);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(928, 277);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvContratos
            // 
            dgvContratos.AllowUserToAddRows = false;
            dgvContratos.AllowUserToDeleteRows = false;
            dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContratos.BackgroundColor = Color.FromArgb(240, 235, 216);
            dgvContratos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContratos.Location = new Point(62, 318);
            dgvContratos.MultiSelect = false;
            dgvContratos.Name = "dgvContratos";
            dgvContratos.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvContratos.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvContratos.RowHeadersVisible = false;
            dgvContratos.RowHeadersWidth = 51;
            dgvContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContratos.Size = new Size(919, 249);
            dgvContratos.TabIndex = 13;
            // 
            // cbTipoContrato
            // 
            cbTipoContrato.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoContrato.FormattingEnabled = true;
            cbTipoContrato.Location = new Point(699, 196);
            cbTipoContrato.Margin = new Padding(3, 4, 3, 4);
            cbTipoContrato.Name = "cbTipoContrato";
            cbTipoContrato.Size = new Size(180, 28);
            cbTipoContrato.TabIndex = 14;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(997, 376);
            btnModificar.Margin = new Padding(3, 4, 3, 4);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(86, 31);
            btnModificar.TabIndex = 15;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(997, 428);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(86, 31);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(315, 111);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(52, 20);
            lblBuscar.TabIndex = 21;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(363, 107);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre del empleado";
            txtBuscar.Size = new Size(372, 27);
            txtBuscar.TabIndex = 20;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(753, 105);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(86, 31);
            btnBuscar.TabIndex = 19;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // Contratos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 737);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnBuscar);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(cbTipoContrato);
            Controls.Add(dgvContratos);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(lblTipo);
            Controls.Add(txtSalarioBase);
            Controls.Add(lblSalario);
            Controls.Add(dtpFechaFin);
            Controls.Add(lblFin);
            Controls.Add(dtpFechaInicio);
            Controls.Add(lblInicio);
            Controls.Add(cbEmpleados);
            Controls.Add(lblEmpleado);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Contratos";
            Text = "Contratos";
            Load += Contratos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvContratos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.ComboBox cbEmpleados;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label lblSalario;
        private System.Windows.Forms.TextBox txtSalarioBase;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvContratos;
        private ComboBox cbTipoContrato;
        private Button btnModificar;
        private Button btnEliminar;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
    }
}