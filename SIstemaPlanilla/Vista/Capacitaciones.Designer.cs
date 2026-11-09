namespace SIstemaPlanilla.Vista
{
    partial class Capacitaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Capacitaciones));
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            txtHoras = new TextBox();
            lblHoras = new Label();
            txtDescripcion = new TextBox();
            lblDescripcion = new Label();
            txtNombreCapacitacion = new TextBox();
            lblNombre = new Label();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            dgvCapacitaciones = new DataGridView();
            label1 = new Label();
            txtBuscar = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCapacitaciones).BeginInit();
            SuspendLayout();
            // 
            // txtHoras
            // 
            txtHoras.Location = new Point(714, 166);
            txtHoras.Margin = new Padding(4, 5, 4, 5);
            txtHoras.Name = "txtHoras";
            txtHoras.Size = new Size(132, 27);
            txtHoras.TabIndex = 5;
            // 
            // lblHoras
            // 
            lblHoras.AutoSize = true;
            lblHoras.Location = new Point(591, 173);
            lblHoras.Margin = new Padding(4, 0, 4, 0);
            lblHoras.Name = "lblHoras";
            lblHoras.Size = new Size(115, 20);
            lblHoras.TabIndex = 4;
            lblHoras.Text = "Horas Duración:";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(191, 220);
            txtDescripcion.Margin = new Padding(4, 5, 4, 5);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripcion";
            txtDescripcion.Size = new Size(372, 75);
            txtDescripcion.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(77, 220);
            lblDescripcion.Margin = new Padding(4, 0, 4, 0);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(90, 20);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Descripción:";
            // 
            // txtNombreCapacitacion
            // 
            txtNombreCapacitacion.Location = new Point(191, 173);
            txtNombreCapacitacion.Margin = new Padding(4, 5, 4, 5);
            txtNombreCapacitacion.Name = "txtNombreCapacitacion";
            txtNombreCapacitacion.PlaceholderText = "Nombre";
            txtNombreCapacitacion.Size = new Size(372, 27);
            txtNombreCapacitacion.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(77, 173);
            lblNombre.Margin = new Padding(4, 0, 4, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(67, 20);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(962, 340);
            btnGuardar.Margin = new Padding(4, 5, 4, 5);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(98, 32);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(962, 382);
            btnModificar.Margin = new Padding(4, 5, 4, 5);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(98, 38);
            btnModificar.TabIndex = 2;
            btnModificar.Text = "Actualizar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(962, 430);
            btnEliminar.Margin = new Padding(4, 5, 4, 5);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(98, 35);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(900, 271);
            btnLimpiar.Margin = new Padding(4, 5, 4, 5);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // dgvCapacitaciones
            // 
            dgvCapacitaciones.AllowUserToAddRows = false;
            dgvCapacitaciones.AllowUserToDeleteRows = false;
            dgvCapacitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCapacitaciones.BackgroundColor = Color.FromArgb(240, 235, 216);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvCapacitaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCapacitaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCapacitaciones.Location = new Point(27, 316);
            dgvCapacitaciones.Margin = new Padding(4, 5, 4, 5);
            dgvCapacitaciones.MultiSelect = false;
            dgvCapacitaciones.Name = "dgvCapacitaciones";
            dgvCapacitaciones.ReadOnly = true;
            dgvCapacitaciones.RowHeadersVisible = false;
            dgvCapacitaciones.RowHeadersWidth = 51;
            dgvCapacitaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCapacitaciones.Size = new Size(914, 210);
            dgvCapacitaciones.TabIndex = 5;
            dgvCapacitaciones.CellClick += dgvCapacitaciones_CellClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 21);
            label1.Name = "label1";
            label1.Size = new Size(218, 38);
            label1.TabIndex = 6;
            label1.Text = "CAPACITACIÓN";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(308, 87);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por el nombre o descripción";
            txtBuscar.Size = new Size(496, 27);
            txtBuscar.TabIndex = 7;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(232, 87);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 8;
            label2.Text = "Buscar";
            // 
            // Capacitaciones
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 555);
            Controls.Add(label2);
            Controls.Add(txtBuscar);
            Controls.Add(label1);
            Controls.Add(lblNombre);
            Controls.Add(lblDescripcion);
            Controls.Add(lblHoras);
            Controls.Add(txtHoras);
            Controls.Add(dgvCapacitaciones);
            Controls.Add(btnLimpiar);
            Controls.Add(txtDescripcion);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(txtNombreCapacitacion);
            Controls.Add(btnGuardar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Name = "Capacitaciones";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Capacitaciones";
            Load += Capacitaciones_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCapacitaciones).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtHoras;
        private System.Windows.Forms.Label lblHoras;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtNombreCapacitacion;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvCapacitaciones;
        private Label label1;
        private TextBox txtBuscar;
        private Label label2;
    }
}