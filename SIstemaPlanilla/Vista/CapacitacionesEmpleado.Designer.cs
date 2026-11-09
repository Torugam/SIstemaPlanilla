namespace SIstemaPlanilla.Vista
{
    partial class CapacitacionesEmpleado
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CapacitacionesEmpleado));
            label1 = new Label();
            cmbEmpleado = new ComboBox();
            cmbCapacitacion = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            dtpFecha = new DateTimePicker();
            txtCalificacion = new TextBox();
            txtBuscar = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            dgvEmpleadoCapacitacion = new DataGridView();
            btnGuardar = new Button();
            btnActualizar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEmpleadoCapacitacion).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 25);
            label1.Name = "label1";
            label1.Size = new Size(376, 38);
            label1.TabIndex = 1;
            label1.Text = "CAPACITACION EMPLEADO";
            // 
            // cmbEmpleado
            // 
            cmbEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmpleado.FormattingEnabled = true;
            cmbEmpleado.Location = new Point(160, 178);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(208, 28);
            cmbEmpleado.TabIndex = 2;
            // 
            // cmbCapacitacion
            // 
            cmbCapacitacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCapacitacion.FormattingEnabled = true;
            cmbCapacitacion.Location = new Point(160, 231);
            cmbCapacitacion.Name = "cmbCapacitacion";
            cmbCapacitacion.Size = new Size(208, 28);
            cmbCapacitacion.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 178);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 4;
            label2.Text = "Empleado:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(52, 232);
            label3.Name = "label3";
            label3.Size = new Size(95, 20);
            label3.TabIndex = 5;
            label3.Text = "Capacitacion";
            // 
            // dtpFecha
            // 
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.Location = new Point(567, 176);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(250, 27);
            dtpFecha.TabIndex = 6;
            // 
            // txtCalificacion
            // 
            txtCalificacion.Location = new Point(567, 231);
            txtCalificacion.Name = "txtCalificacion";
            txtCalificacion.PlaceholderText = "0.0";
            txtCalificacion.Size = new Size(250, 27);
            txtCalificacion.TabIndex = 7;
            txtCalificacion.TextChanged += txtCalificacion_TextChanged;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(299, 104);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por el nombre de empleado o la capacitacion";
            txtBuscar.Size = new Size(444, 27);
            txtBuscar.TabIndex = 8;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(223, 111);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 9;
            label4.Text = "Buscar";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(450, 176);
            label5.Name = "label5";
            label5.Size = new Size(47, 20);
            label5.TabIndex = 10;
            label5.Text = "Fecha";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(453, 231);
            label6.Name = "label6";
            label6.Size = new Size(86, 20);
            label6.TabIndex = 11;
            label6.Text = "Calificacion";
            // 
            // dgvEmpleadoCapacitacion
            // 
            dgvEmpleadoCapacitacion.AllowUserToAddRows = false;
            dgvEmpleadoCapacitacion.AllowUserToDeleteRows = false;
            dgvEmpleadoCapacitacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmpleadoCapacitacion.BackgroundColor = Color.FromArgb(240, 235, 216);
            dgvEmpleadoCapacitacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmpleadoCapacitacion.Location = new Point(33, 346);
            dgvEmpleadoCapacitacion.MultiSelect = false;
            dgvEmpleadoCapacitacion.Name = "dgvEmpleadoCapacitacion";
            dgvEmpleadoCapacitacion.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvEmpleadoCapacitacion.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvEmpleadoCapacitacion.RowHeadersVisible = false;
            dgvEmpleadoCapacitacion.RowHeadersWidth = 51;
            dgvEmpleadoCapacitacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmpleadoCapacitacion.Size = new Size(897, 183);
            dgvEmpleadoCapacitacion.TabIndex = 12;
            dgvEmpleadoCapacitacion.CellClick += dgvEmpleadoCapacitacion_CellClick;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(959, 346);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(959, 395);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(94, 29);
            btnActualizar.TabIndex = 14;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(959, 442);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 15;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(889, 305);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 16;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // CapacitacionesEmpleado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 555);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnActualizar);
            Controls.Add(btnGuardar);
            Controls.Add(dgvEmpleadoCapacitacion);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtBuscar);
            Controls.Add(txtCalificacion);
            Controls.Add(dtpFecha);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(cmbCapacitacion);
            Controls.Add(cmbEmpleado);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CapacitacionesEmpleado";
            Text = "CapacitacionesEmpleado";
            Activated += CapacitacionesEmpleado_Activated;
            Load += CapacitacionesEmpleado_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEmpleadoCapacitacion).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbEmpleado;
        private ComboBox cmbCapacitacion;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpFecha;
        private TextBox txtCalificacion;
        private TextBox txtBuscar;
        private Label label4;
        private Label label5;
        private Label label6;
        private DataGridView dgvEmpleadoCapacitacion;
        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnLimpiar;
    }
}