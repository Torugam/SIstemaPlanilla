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
            label1.Location = new Point(54, 45);
            label1.Name = "label1";
            label1.Size = new Size(142, 30);
            label1.TabIndex = 0;
            label1.Text = "CONTRATOS";
            // 
            // lblEmpleado
            // 
            lblEmpleado.AutoSize = true;
            lblEmpleado.Location = new Point(176, 120);
            lblEmpleado.Name = "lblEmpleado";
            lblEmpleado.Size = new Size(63, 15);
            lblEmpleado.TabIndex = 1;
            lblEmpleado.Text = "Empleado:";
            // 
            // cbEmpleados
            // 
            cbEmpleados.DropDownStyle = ComboBoxStyle.DropDownList;
            cbEmpleados.FormattingEnabled = true;
            cbEmpleados.Location = new Point(272, 118);
            cbEmpleados.Margin = new Padding(3, 2, 3, 2);
            cbEmpleados.Name = "cbEmpleados";
            cbEmpleados.Size = new Size(193, 23);
            cbEmpleados.TabIndex = 2;
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(176, 150);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(73, 15);
            lblInicio.TabIndex = 3;
            lblInicio.Text = "Fecha Inicio:";
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(272, 148);
            dtpFechaInicio.Margin = new Padding(3, 2, 3, 2);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(132, 23);
            dtpFechaInicio.TabIndex = 4;
            // 
            // lblFin
            // 
            lblFin.AutoSize = true;
            lblFin.Location = new Point(176, 180);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(60, 15);
            lblFin.TabIndex = 5;
            lblFin.Text = "Fecha Fin:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Checked = false;
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(272, 178);
            dtpFechaFin.Margin = new Padding(3, 2, 3, 2);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(158, 23);
            dtpFechaFin.TabIndex = 6;
            // 
            // lblSalario
            // 
            lblSalario.AutoSize = true;
            lblSalario.Location = new Point(508, 120);
            lblSalario.Name = "lblSalario";
            lblSalario.Size = new Size(72, 15);
            lblSalario.TabIndex = 7;
            lblSalario.Text = "Salario Base:";
            // 
            // txtSalarioBase
            // 
            txtSalarioBase.Location = new Point(613, 118);
            txtSalarioBase.Margin = new Padding(3, 2, 3, 2);
            txtSalarioBase.Name = "txtSalarioBase";
            txtSalarioBase.Size = new Size(132, 23);
            txtSalarioBase.TabIndex = 8;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(508, 150);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(84, 15);
            lblTipo.TabIndex = 9;
            lblTipo.Text = "Tipo Contrato:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(484, 180);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(79, 24);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(735, 180);
            btnLimpiar.Margin = new Padding(3, 2, 3, 2);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(79, 24);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvContratos
            // 
            dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContratos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContratos.Location = new Point(120, 241);
            dgvContratos.Margin = new Padding(3, 2, 3, 2);
            dgvContratos.Name = "dgvContratos";
            dgvContratos.RowHeadersWidth = 51;
            dgvContratos.Size = new Size(791, 204);
            dgvContratos.TabIndex = 13;
            // 
            // cbTipoContrato
            // 
            cbTipoContrato.FormattingEnabled = true;
            cbTipoContrato.Location = new Point(612, 147);
            cbTipoContrato.Name = "cbTipoContrato";
            cbTipoContrato.Size = new Size(158, 23);
            cbTipoContrato.TabIndex = 14;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(569, 180);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(75, 23);
            btnModificar.TabIndex = 15;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(654, 181);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 16;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(276, 83);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(42, 15);
            lblBuscar.TabIndex = 21;
            lblBuscar.Text = "Buscar";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(318, 80);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(326, 23);
            txtBuscar.TabIndex = 20;
            txtBuscar.Text = "Buscar por nombre del empleado";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(659, 79);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(75, 23);
            btnBuscar.TabIndex = 19;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // Contratos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 553);
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
            Margin = new Padding(3, 2, 3, 2);
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