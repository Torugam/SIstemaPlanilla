namespace SIstemaPlanilla.Vista
{
    partial class Usuarios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuarios));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtUsuario = new TextBox();
            txtContrasenia = new TextBox();
            txtBuscar = new TextBox();
            cmbRoles = new ComboBox();
            cmbEmpleado = new ComboBox();
            btnActualizar = new Button();
            btnLimpiar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            dgvUsuarios = new DataGridView();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(569, 187);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 0;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(569, 245);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 1;
            label2.Text = "Contraseña";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 237);
            label3.Name = "label3";
            label3.Size = new Size(31, 20);
            label3.TabIndex = 2;
            label3.Text = "Rol";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(66, 187);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 3;
            label4.Text = "Empleado";
            // 
            // txtUsuario
            // 
            txtUsuario.BackColor = Color.White;
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 10F);
            txtUsuario.ForeColor = Color.FromArgb(13, 19, 33);
            txtUsuario.Location = new Point(668, 179);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "  usuario";
            txtUsuario.Size = new Size(308, 30);
            txtUsuario.TabIndex = 4;
            // 
            // txtContrasenia
            // 
            txtContrasenia.BackColor = Color.White;
            txtContrasenia.BorderStyle = BorderStyle.FixedSingle;
            txtContrasenia.Font = new Font("Segoe UI", 10F);
            txtContrasenia.ForeColor = Color.FromArgb(13, 19, 33);
            txtContrasenia.Location = new Point(668, 241);
            txtContrasenia.Name = "txtContrasenia";
            txtContrasenia.PasswordChar = '*';
            txtContrasenia.Size = new Size(308, 30);
            txtContrasenia.TabIndex = 5;
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.White;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.ForeColor = Color.FromArgb(13, 19, 33);
            txtBuscar.Location = new Point(224, 87);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "  Buscar por usuario, rol o empleado";
            txtBuscar.Size = new Size(699, 30);
            txtBuscar.TabIndex = 6;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // cmbRoles
            // 
            cmbRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoles.FormattingEnabled = true;
            cmbRoles.Location = new Point(163, 229);
            cmbRoles.Name = "cmbRoles";
            cmbRoles.Size = new Size(302, 28);
            cmbRoles.TabIndex = 7;
            // 
            // cmbEmpleado
            // 
            cmbEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmpleado.FormattingEnabled = true;
            cmbEmpleado.Location = new Point(163, 179);
            cmbEmpleado.Name = "cmbEmpleado";
            cmbEmpleado.Size = new Size(302, 28);
            cmbEmpleado.TabIndex = 8;
            // 
            // btnActualizar
            // 
            btnActualizar.Location = new Point(976, 382);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(94, 29);
            btnActualizar.TabIndex = 9;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = true;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.BackColor = SystemColors.AppWorkspace;
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(915, 304);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 10;
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(976, 417);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Desactivar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(976, 347);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 12;
            btnGuardar.Text = "Agregar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = Color.FromArgb(240, 235, 216);
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.Location = new Point(35, 345);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(921, 198);
            dgvUsuarios.TabIndex = 13;
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(35, 20);
            label5.Name = "label5";
            label5.Size = new Size(156, 38);
            label5.TabIndex = 14;
            label5.Text = "USUARIOS";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(163, 91);
            label6.Name = "label6";
            label6.Size = new Size(52, 20);
            label6.TabIndex = 15;
            label6.Text = "Buscar";
            // 
            // Usuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 555);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(dgvUsuarios);
            Controls.Add(btnGuardar);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnActualizar);
            Controls.Add(cmbEmpleado);
            Controls.Add(cmbRoles);
            Controls.Add(txtBuscar);
            Controls.Add(txtContrasenia);
            Controls.Add(txtUsuario);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Usuarios";
            Text = "Usuarios";
            Activated += Usuarios_Activated;
            Load += Usuarios_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtUsuario;
        private TextBox txtContrasenia;
        private TextBox txtBuscar;
        private ComboBox cmbRoles;
        private ComboBox cmbEmpleado;
        private Button btnActualizar;
        private Button btnLimpiar;
        private Button btnEliminar;
        private Button btnGuardar;
        private DataGridView dgvUsuarios;
        private Label label5;
        private Label label6;
    }
}