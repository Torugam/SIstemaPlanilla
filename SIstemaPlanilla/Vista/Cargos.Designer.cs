namespace SIstemaPlanilla.Vista
{
    partial class Cargos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cargos));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            lblNombre = new Label();
            txtNombreCargo = new TextBox();
            lblSuperior = new Label();
            cbCargoSuperior = new ComboBox();
            lblMin = new Label();
            txtSalarioMin = new TextBox();
            lblMax = new Label();
            txtSalarioMax = new TextBox();
            btnGuardar = new Button();
            btnModificar = new Button();
            btnEliminar = new Button();
            btnLimpiar = new Button();
            dgvCargos = new DataGridView();
            txtBuscar = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCargos).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(20, 15);
            label1.Name = "label1";
            label1.Size = new Size(129, 38);
            label1.TabIndex = 0;
            label1.Text = "CARGOS";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(100, 165);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(111, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre Cargo:";
            // 
            // txtNombreCargo
            // 
            txtNombreCargo.Location = new Point(231, 158);
            txtNombreCargo.Name = "txtNombreCargo";
            txtNombreCargo.PlaceholderText = "Nombre";
            txtNombreCargo.Size = new Size(200, 27);
            txtNombreCargo.TabIndex = 2;
            // 
            // lblSuperior
            // 
            lblSuperior.AutoSize = true;
            lblSuperior.Location = new Point(99, 216);
            lblSuperior.Name = "lblSuperior";
            lblSuperior.Size = new Size(112, 20);
            lblSuperior.TabIndex = 3;
            lblSuperior.Text = "Cargo Superior:";
            // 
            // cbCargoSuperior
            // 
            cbCargoSuperior.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCargoSuperior.FormattingEnabled = true;
            cbCargoSuperior.Location = new Point(231, 213);
            cbCargoSuperior.Name = "cbCargoSuperior";
            cbCargoSuperior.Size = new Size(200, 28);
            cbCargoSuperior.TabIndex = 4;
            // 
            // lblMin
            // 
            lblMin.AutoSize = true;
            lblMin.Location = new Point(558, 161);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(113, 20);
            lblMin.TabIndex = 5;
            lblMin.Text = "Salario Mínimo:";
            // 
            // txtSalarioMin
            // 
            txtSalarioMin.Location = new Point(710, 161);
            txtSalarioMin.Name = "txtSalarioMin";
            txtSalarioMin.Size = new Size(150, 27);
            txtSalarioMin.TabIndex = 6;
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Location = new Point(555, 213);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(116, 20);
            lblMax.TabIndex = 7;
            lblMax.Text = "Salario Máximo:";
            // 
            // txtSalarioMax
            // 
            txtSalarioMax.Location = new Point(710, 209);
            txtSalarioMax.Name = "txtSalarioMax";
            txtSalarioMax.Size = new Size(150, 27);
            txtSalarioMax.TabIndex = 8;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(983, 285);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(90, 30);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click_1;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(983, 331);
            btnModificar.Name = "btnModificar";
            btnModificar.Size = new Size(90, 30);
            btnModificar.TabIndex = 10;
            btnModificar.Text = "Modificar";
            btnModificar.UseVisualStyleBackColor = true;
            btnModificar.Click += btnModificar_Click_1;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(983, 376);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(90, 30);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click_1;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(924, 244);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click_1;
            // 
            // dgvCargos
            // 
            dgvCargos.AllowUserToAddRows = false;
            dgvCargos.AllowUserToDeleteRows = false;
            dgvCargos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCargos.BackgroundColor = Color.FromArgb(240, 235, 216);
            dgvCargos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCargos.Location = new Point(29, 285);
            dgvCargos.MultiSelect = false;
            dgvCargos.Name = "dgvCargos";
            dgvCargos.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvCargos.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvCargos.RowHeadersVisible = false;
            dgvCargos.RowHeadersWidth = 51;
            dgvCargos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCargos.Size = new Size(936, 230);
            dgvCargos.TabIndex = 13;
            dgvCargos.CellClick += dgvCargos_CellClick;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(364, 69);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por el nombre cargo o el cargo superior";
            txtBuscar.Size = new Size(431, 27);
            txtBuscar.TabIndex = 14;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(289, 72);
            label2.Name = "label2";
            label2.Size = new Size(52, 20);
            label2.TabIndex = 15;
            label2.Text = "Buscar";
            // 
            // Cargos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 555);
            Controls.Add(label2);
            Controls.Add(txtBuscar);
            Controls.Add(dgvCargos);
            Controls.Add(btnLimpiar);
            Controls.Add(btnEliminar);
            Controls.Add(btnModificar);
            Controls.Add(btnGuardar);
            Controls.Add(txtSalarioMax);
            Controls.Add(lblMax);
            Controls.Add(txtSalarioMin);
            Controls.Add(lblMin);
            Controls.Add(cbCargoSuperior);
            Controls.Add(lblSuperior);
            Controls.Add(txtNombreCargo);
            Controls.Add(lblNombre);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Cargos";
            Text = "Cargos";
            Load += Cargos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCargos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombreCargo;
        private System.Windows.Forms.Label lblSuperior;
        private System.Windows.Forms.ComboBox cbCargoSuperior;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox txtSalarioMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.TextBox txtSalarioMax;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvCargos;
        private TextBox txtBuscar;
        private Label label2;
    }
}