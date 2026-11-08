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
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.txtIdCargo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombreCargo = new System.Windows.Forms.TextBox();
            this.lblSuperior = new System.Windows.Forms.Label();
            this.cbCargoSuperior = new System.Windows.Forms.ComboBox();
            this.lblMin = new System.Windows.Forms.Label();
            this.txtSalarioMin = new System.Windows.Forms.TextBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.txtSalarioMax = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvCargos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCargos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "CARGOS";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 70);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(69, 20);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "ID Cargo:";
            // 
            // txtIdCargo
            // 
            this.txtIdCargo.Location = new System.Drawing.Point(140, 67);
            this.txtIdCargo.Name = "txtIdCargo";
            this.txtIdCargo.Size = new System.Drawing.Size(150, 27);
            this.txtIdCargo.TabIndex = 2;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 110);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(110, 20);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Nombre Cargo:";
            // 
            // txtNombreCargo
            // 
            this.txtNombreCargo.Location = new System.Drawing.Point(140, 107);
            this.txtNombreCargo.Name = "txtNombreCargo";
            this.txtNombreCargo.Size = new System.Drawing.Size(200, 27);
            this.txtNombreCargo.TabIndex = 4;
            // 
            // lblSuperior
            // 
            this.lblSuperior.AutoSize = true;
            this.lblSuperior.Location = new System.Drawing.Point(20, 150);
            this.lblSuperior.Name = "lblSuperior";
            this.lblSuperior.Size = new System.Drawing.Size(109, 20);
            this.lblSuperior.TabIndex = 5;
            this.lblSuperior.Text = "Cargo Superior:";
            // 
            // cbCargoSuperior
            // 
            this.cbCargoSuperior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCargoSuperior.FormattingEnabled = true;
            this.cbCargoSuperior.Location = new System.Drawing.Point(140, 147);
            this.cbCargoSuperior.Name = "cbCargoSuperior";
            this.cbCargoSuperior.Size = new System.Drawing.Size(200, 28);
            this.cbCargoSuperior.TabIndex = 6;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(370, 70);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(104, 20);
            this.lblMin.TabIndex = 7;
            this.lblMin.Text = "Salario Mínimo:";
            // 
            // txtSalarioMin
            // 
            this.txtSalarioMin.Location = new System.Drawing.Point(480, 67);
            this.txtSalarioMin.Name = "txtSalarioMin";
            this.txtSalarioMin.Size = new System.Drawing.Size(150, 27);
            this.txtSalarioMin.TabIndex = 8;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(370, 110);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(107, 20);
            this.lblMax.TabIndex = 9;
            this.lblMax.Text = "Salario Máximo:";
            // 
            // txtSalarioMax
            // 
            this.txtSalarioMax.Location = new System.Drawing.Point(480, 107);
            this.txtSalarioMax.Name = "txtSalarioMax";
            this.txtSalarioMax.Size = new System.Drawing.Size(150, 27);
            this.txtSalarioMax.TabIndex = 10;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(430, 145);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(90, 30);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(540, 145);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(90, 30);
            this.btnLimpiar.TabIndex = 12;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // dgvCargos
            // 
            this.dgvCargos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCargos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCargos.Location = new System.Drawing.Point(20, 200);
            this.dgvCargos.Name = "dgvCargos";
            this.dgvCargos.RowHeadersWidth = 51;
            this.dgvCargos.Size = new System.Drawing.Size(750, 230);
            this.dgvCargos.TabIndex = 13;
            // 
            // Cargos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvCargos);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtSalarioMax);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.txtSalarioMin);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.cbCargoSuperior);
            this.Controls.Add(this.lblSuperior);
            this.Controls.Add(this.txtNombreCargo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtIdCargo);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Cargos";
            this.Text = "Cargos";
            this.Load += new System.EventHandler(this.Cargos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCargos)).EndInit();
            this.ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtIdCargo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombreCargo;
        private System.Windows.Forms.Label lblSuperior;
        private System.Windows.Forms.ComboBox cbCargoSuperior;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox txtSalarioMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.TextBox txtSalarioMax;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvCargos;
    }
}