namespace SIstemaPlanilla.Vista
{
    partial class RolesPermisos
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RolesPermisos));
            label1 = new Label();
            txtNombreRol = new TextBox();
            clbOpciones = new CheckedListBox();
            dgvRoles = new DataGridView();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            label2 = new Label();
            label3 = new Label();
            txtBuscar = new TextBox();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(41, 32);
            label1.Name = "label1";
            label1.Size = new Size(273, 38);
            label1.TabIndex = 1;
            label1.Text = "ROLES Y PERMISOS";
            // 
            // txtNombreRol
            // 
            txtNombreRol.Location = new Point(170, 162);
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.PlaceholderText = "Rol";
            txtNombreRol.Size = new Size(218, 27);
            txtNombreRol.TabIndex = 2;
            // 
            // clbOpciones
            // 
            clbOpciones.FormattingEnabled = true;
            clbOpciones.Location = new Point(522, 162);
            clbOpciones.Name = "clbOpciones";
            clbOpciones.Size = new Size(278, 114);
            clbOpciones.TabIndex = 3;
            // 
            // dgvRoles
            // 
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(116, 140, 171);
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvRoles.DefaultCellStyle = dataGridViewCellStyle3;
            dgvRoles.Location = new Point(41, 305);
            dgvRoles.MultiSelect = false;
            dgvRoles.Name = "dgvRoles";
            dgvRoles.ReadOnly = true;
            dgvRoles.RowHeadersVisible = false;
            dgvRoles.RowHeadersWidth = 51;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.Size = new Size(887, 224);
            dgvRoles.TabIndex = 4;
            dgvRoles.CellClick += dgvRoles_CellClick;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(955, 305);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 15;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Image = (Image)resources.GetObject("btnLimpiar.Image");
            btnLimpiar.Location = new Point(877, 253);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(41, 35);
            btnLimpiar.TabIndex = 16;
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(116, 169);
            label2.Name = "label2";
            label2.Size = new Size(31, 20);
            label2.TabIndex = 17;
            label2.Text = "Rol";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(439, 162);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 18;
            label3.Text = "Permisos";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(268, 95);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Busca...";
            txtBuscar.Size = new Size(705, 27);
            txtBuscar.TabIndex = 19;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(176, 100);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 20;
            label4.Text = "Buscar";
            // 
            // RolesPermisos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 555);
            Controls.Add(label4);
            Controls.Add(txtBuscar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(clbOpciones);
            Controls.Add(txtNombreRol);
            Controls.Add(label1);
            Controls.Add(dgvRoles);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RolesPermisos";
            Text = "RolesPermisos";
            Load += RolesPermisos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtNombreRol;
        private CheckedListBox clbOpciones;
        private Button btnGuardar;
        private Button btnLimpiar;
        private DataGridView dgvRoles;
        private Label label2;
        private Label label3;
        private TextBox txtBuscar;
        private Label label4;
    }
}