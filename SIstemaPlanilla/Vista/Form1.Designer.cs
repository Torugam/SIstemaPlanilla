namespace SIstemaPlanilla
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            btnMini = new Button();
            btnSalir = new Button();
            label1 = new Label();
            pMenu = new PictureBox();
            sliderbar = new FlowLayoutPanel();
            menuContainer = new FlowLayoutPanel();
            panel7 = new Panel();
            btnMenuRecurso = new Button();
            panel9 = new Panel();
            btnDepartamento = new Button();
            panel3 = new Panel();
            btnCargo = new Button();
            panel5 = new Panel();
            btnCapacitacion = new Button();
            button10 = new Button();
            panel4 = new Panel();
            btnContrato = new Button();
            button7 = new Button();
            panel2 = new Panel();
            btnEmple = new Button();
            menuPlanilla = new FlowLayoutPanel();
            panel10 = new Panel();
            btnMenuGestion = new Button();
            panel12 = new Panel();
            button17 = new Button();
            panel13 = new Panel();
            button19 = new Button();
            panel11 = new Panel();
            button15 = new Button();
            menuAdministracion = new FlowLayoutPanel();
            panel8 = new Panel();
            btnAdministracion = new Button();
            panel14 = new Panel();
            button11 = new Button();
            panel15 = new Panel();
            button12 = new Button();
            menuTransicion = new System.Windows.Forms.Timer(components);
            sliderTransicion = new System.Windows.Forms.Timer(components);
            menuGestion = new System.Windows.Forms.Timer(components);
            menuUsuario = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pMenu).BeginInit();
            sliderbar.SuspendLayout();
            menuContainer.SuspendLayout();
            panel7.SuspendLayout();
            panel9.SuspendLayout();
            panel3.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            menuPlanilla.SuspendLayout();
            panel10.SuspendLayout();
            panel12.SuspendLayout();
            panel13.SuspendLayout();
            panel11.SuspendLayout();
            menuAdministracion.SuspendLayout();
            panel8.SuspendLayout();
            panel14.SuspendLayout();
            panel15.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(btnMini);
            panel1.Controls.Add(btnSalir);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pMenu);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1392, 39);
            panel1.TabIndex = 1;
            // 
            // btnMini
            // 
            btnMini.Image = (Image)resources.GetObject("btnMini.Image");
            btnMini.Location = new Point(1292, 4);
            btnMini.Name = "btnMini";
            btnMini.Size = new Size(41, 33);
            btnMini.TabIndex = 6;
            btnMini.UseVisualStyleBackColor = true;
            btnMini.Click += btnMini_Click;
            // 
            // btnSalir
            // 
            btnSalir.Image = (Image)resources.GetObject("btnSalir.Image");
            btnSalir.Location = new Point(1339, 3);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(41, 33);
            btnSalir.TabIndex = 4;
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(64, 8);
            label1.Name = "label1";
            label1.Size = new Size(135, 23);
            label1.TabIndex = 3;
            label1.Text = "Sistema Planillas";
            // 
            // pMenu
            // 
            pMenu.Image = (Image)resources.GetObject("pMenu.Image");
            pMenu.Location = new Point(12, 10);
            pMenu.Name = "pMenu";
            pMenu.Size = new Size(30, 23);
            pMenu.SizeMode = PictureBoxSizeMode.CenterImage;
            pMenu.TabIndex = 2;
            pMenu.TabStop = false;
            pMenu.Click += pMenu_Click;
            // 
            // sliderbar
            // 
            sliderbar.BackColor = Color.FromArgb(29, 48, 64);
            sliderbar.Controls.Add(menuContainer);
            sliderbar.Controls.Add(menuPlanilla);
            sliderbar.Controls.Add(menuAdministracion);
            sliderbar.Dock = DockStyle.Left;
            sliderbar.FlowDirection = FlowDirection.TopDown;
            sliderbar.Location = new Point(0, 39);
            sliderbar.Name = "sliderbar";
            sliderbar.Size = new Size(245, 632);
            sliderbar.TabIndex = 2;
            sliderbar.WrapContents = false;
            // 
            // menuContainer
            // 
            menuContainer.BackColor = Color.FromArgb(29, 48, 64);
            menuContainer.Controls.Add(panel7);
            menuContainer.Controls.Add(panel9);
            menuContainer.Controls.Add(panel3);
            menuContainer.Controls.Add(panel5);
            menuContainer.Controls.Add(panel4);
            menuContainer.Controls.Add(panel2);
            menuContainer.Location = new Point(3, 3);
            menuContainer.Name = "menuContainer";
            menuContainer.Size = new Size(248, 48);
            menuContainer.TabIndex = 7;
            // 
            // panel7
            // 
            panel7.Controls.Add(btnMenuRecurso);
            panel7.Location = new Point(0, 0);
            panel7.Margin = new Padding(0);
            panel7.Name = "panel7";
            panel7.Size = new Size(248, 49);
            panel7.TabIndex = 8;
            // 
            // btnMenuRecurso
            // 
            btnMenuRecurso.BackColor = Color.FromArgb(29, 48, 64);
            btnMenuRecurso.FlatAppearance.BorderSize = 0;
            btnMenuRecurso.FlatStyle = FlatStyle.Flat;
            btnMenuRecurso.ForeColor = Color.White;
            btnMenuRecurso.Image = (Image)resources.GetObject("btnMenuRecurso.Image");
            btnMenuRecurso.ImageAlign = ContentAlignment.MiddleLeft;
            btnMenuRecurso.Location = new Point(0, 0);
            btnMenuRecurso.Name = "btnMenuRecurso";
            btnMenuRecurso.Padding = new Padding(10, 0, 0, 0);
            btnMenuRecurso.Size = new Size(248, 55);
            btnMenuRecurso.TabIndex = 6;
            btnMenuRecurso.Text = "           Recursos Humanos";
            btnMenuRecurso.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuRecurso.UseVisualStyleBackColor = false;
            btnMenuRecurso.Click += btnMenuRecurso_Click;
            // 
            // panel9
            // 
            panel9.Controls.Add(btnDepartamento);
            panel9.Location = new Point(0, 51);
            panel9.Margin = new Padding(0, 2, 0, 0);
            panel9.Name = "panel9";
            panel9.Size = new Size(248, 49);
            panel9.TabIndex = 10;
            // 
            // btnDepartamento
            // 
            btnDepartamento.BackColor = Color.FromArgb(16, 24, 32);
            btnDepartamento.FlatAppearance.BorderSize = 0;
            btnDepartamento.FlatStyle = FlatStyle.Flat;
            btnDepartamento.ForeColor = Color.White;
            btnDepartamento.Image = (Image)resources.GetObject("btnDepartamento.Image");
            btnDepartamento.ImageAlign = ContentAlignment.MiddleLeft;
            btnDepartamento.Location = new Point(0, 0);
            btnDepartamento.Name = "btnDepartamento";
            btnDepartamento.Padding = new Padding(10, 0, 0, 0);
            btnDepartamento.Size = new Size(248, 55);
            btnDepartamento.TabIndex = 4;
            btnDepartamento.Text = "           Departamentos";
            btnDepartamento.TextAlign = ContentAlignment.MiddleLeft;
            btnDepartamento.UseVisualStyleBackColor = false;
            btnDepartamento.Click += btnDepartamento_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnCargo);
            panel3.Location = new Point(0, 102);
            panel3.Margin = new Padding(0, 2, 0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(248, 49);
            panel3.TabIndex = 12;
            // 
            // btnCargo
            // 
            btnCargo.BackColor = Color.FromArgb(16, 24, 32);
            btnCargo.FlatAppearance.BorderSize = 0;
            btnCargo.FlatStyle = FlatStyle.Flat;
            btnCargo.ForeColor = Color.White;
            btnCargo.Image = (Image)resources.GetObject("btnCargo.Image");
            btnCargo.ImageAlign = ContentAlignment.MiddleLeft;
            btnCargo.Location = new Point(0, 0);
            btnCargo.Name = "btnCargo";
            btnCargo.Padding = new Padding(10, 0, 0, 0);
            btnCargo.Size = new Size(248, 55);
            btnCargo.TabIndex = 4;
            btnCargo.Text = "           Cargos";
            btnCargo.TextAlign = ContentAlignment.MiddleLeft;
            btnCargo.UseVisualStyleBackColor = false;
            btnCargo.Click += btnCargo_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(btnCapacitacion);
            panel5.Controls.Add(button10);
            panel5.Location = new Point(0, 153);
            panel5.Margin = new Padding(0, 2, 0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(248, 49);
            panel5.TabIndex = 13;
            // 
            // btnCapacitacion
            // 
            btnCapacitacion.BackColor = Color.FromArgb(16, 24, 32);
            btnCapacitacion.FlatAppearance.BorderSize = 0;
            btnCapacitacion.FlatStyle = FlatStyle.Flat;
            btnCapacitacion.ForeColor = Color.White;
            btnCapacitacion.Image = (Image)resources.GetObject("btnCapacitacion.Image");
            btnCapacitacion.ImageAlign = ContentAlignment.MiddleLeft;
            btnCapacitacion.Location = new Point(0, -3);
            btnCapacitacion.Name = "btnCapacitacion";
            btnCapacitacion.Padding = new Padding(10, 0, 0, 0);
            btnCapacitacion.Size = new Size(248, 55);
            btnCapacitacion.TabIndex = 5;
            btnCapacitacion.Text = "           Capacitaciones";
            btnCapacitacion.TextAlign = ContentAlignment.MiddleLeft;
            btnCapacitacion.UseVisualStyleBackColor = false;
            btnCapacitacion.Click += btnCapacitacion_Click;
            // 
            // button10
            // 
            button10.BackColor = Color.FromArgb(29, 48, 64);
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatStyle = FlatStyle.Flat;
            button10.ForeColor = Color.White;
            button10.Image = (Image)resources.GetObject("button10.Image");
            button10.ImageAlign = ContentAlignment.MiddleLeft;
            button10.Location = new Point(0, 0);
            button10.Name = "button10";
            button10.Padding = new Padding(10, 0, 0, 0);
            button10.Size = new Size(248, 55);
            button10.TabIndex = 4;
            button10.Text = "           Departamento";
            button10.TextAlign = ContentAlignment.MiddleLeft;
            button10.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            panel4.Controls.Add(btnContrato);
            panel4.Controls.Add(button7);
            panel4.Location = new Point(0, 204);
            panel4.Margin = new Padding(0, 2, 0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(248, 49);
            panel4.TabIndex = 15;
            // 
            // btnContrato
            // 
            btnContrato.BackColor = Color.FromArgb(16, 24, 32);
            btnContrato.FlatAppearance.BorderSize = 0;
            btnContrato.FlatStyle = FlatStyle.Flat;
            btnContrato.ForeColor = Color.White;
            btnContrato.Image = (Image)resources.GetObject("btnContrato.Image");
            btnContrato.ImageAlign = ContentAlignment.MiddleLeft;
            btnContrato.Location = new Point(0, -3);
            btnContrato.Name = "btnContrato";
            btnContrato.Padding = new Padding(10, 0, 0, 0);
            btnContrato.Size = new Size(248, 55);
            btnContrato.TabIndex = 5;
            btnContrato.Text = "           Contratos";
            btnContrato.TextAlign = ContentAlignment.MiddleLeft;
            btnContrato.UseVisualStyleBackColor = false;
            btnContrato.Click += btnContrato_Click;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(29, 48, 64);
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.ForeColor = Color.White;
            button7.Image = (Image)resources.GetObject("button7.Image");
            button7.ImageAlign = ContentAlignment.MiddleLeft;
            button7.Location = new Point(0, 0);
            button7.Name = "button7";
            button7.Padding = new Padding(10, 0, 0, 0);
            button7.Size = new Size(248, 55);
            button7.TabIndex = 4;
            button7.Text = "           Departamento";
            button7.TextAlign = ContentAlignment.MiddleLeft;
            button7.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnEmple);
            panel2.Location = new Point(0, 255);
            panel2.Margin = new Padding(0, 2, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(248, 49);
            panel2.TabIndex = 20;
            // 
            // btnEmple
            // 
            btnEmple.BackColor = Color.FromArgb(16, 24, 32);
            btnEmple.FlatAppearance.BorderSize = 0;
            btnEmple.FlatStyle = FlatStyle.Flat;
            btnEmple.ForeColor = Color.White;
            btnEmple.Image = (Image)resources.GetObject("btnEmple.Image");
            btnEmple.ImageAlign = ContentAlignment.MiddleLeft;
            btnEmple.Location = new Point(0, 0);
            btnEmple.Name = "btnEmple";
            btnEmple.Padding = new Padding(10, 0, 0, 0);
            btnEmple.Size = new Size(248, 55);
            btnEmple.TabIndex = 5;
            btnEmple.Text = "           Empleados";
            btnEmple.TextAlign = ContentAlignment.MiddleLeft;
            btnEmple.UseVisualStyleBackColor = false;
            btnEmple.Click += btnEmple_Click;
            // 
            // menuPlanilla
            // 
            menuPlanilla.BackColor = Color.FromArgb(29, 48, 64);
            menuPlanilla.Controls.Add(panel10);
            menuPlanilla.Controls.Add(panel12);
            menuPlanilla.Controls.Add(panel13);
            menuPlanilla.Controls.Add(panel11);
            menuPlanilla.Location = new Point(3, 57);
            menuPlanilla.Name = "menuPlanilla";
            menuPlanilla.Size = new Size(248, 48);
            menuPlanilla.TabIndex = 8;
            // 
            // panel10
            // 
            panel10.Controls.Add(btnMenuGestion);
            panel10.Location = new Point(0, 0);
            panel10.Margin = new Padding(0);
            panel10.Name = "panel10";
            panel10.Size = new Size(248, 49);
            panel10.TabIndex = 15;
            // 
            // btnMenuGestion
            // 
            btnMenuGestion.BackColor = Color.FromArgb(29, 48, 64);
            btnMenuGestion.FlatAppearance.BorderSize = 0;
            btnMenuGestion.FlatStyle = FlatStyle.Flat;
            btnMenuGestion.ForeColor = Color.White;
            btnMenuGestion.Image = (Image)resources.GetObject("btnMenuGestion.Image");
            btnMenuGestion.ImageAlign = ContentAlignment.MiddleLeft;
            btnMenuGestion.Location = new Point(0, 0);
            btnMenuGestion.Name = "btnMenuGestion";
            btnMenuGestion.Padding = new Padding(10, 0, 0, 0);
            btnMenuGestion.Size = new Size(248, 48);
            btnMenuGestion.TabIndex = 5;
            btnMenuGestion.Text = "           Gestión planilla";
            btnMenuGestion.TextAlign = ContentAlignment.MiddleLeft;
            btnMenuGestion.UseVisualStyleBackColor = false;
            btnMenuGestion.Click += btnMenuGestion_Click;
            // 
            // panel12
            // 
            panel12.Controls.Add(button17);
            panel12.Location = new Point(0, 51);
            panel12.Margin = new Padding(0, 2, 0, 0);
            panel12.Name = "panel12";
            panel12.Size = new Size(248, 49);
            panel12.TabIndex = 17;
            // 
            // button17
            // 
            button17.BackColor = Color.FromArgb(16, 24, 32);
            button17.FlatAppearance.BorderSize = 0;
            button17.FlatStyle = FlatStyle.Flat;
            button17.ForeColor = Color.White;
            button17.Image = (Image)resources.GetObject("button17.Image");
            button17.ImageAlign = ContentAlignment.MiddleLeft;
            button17.Location = new Point(0, -6);
            button17.Name = "button17";
            button17.Padding = new Padding(10, 0, 0, 0);
            button17.Size = new Size(248, 55);
            button17.TabIndex = 5;
            button17.Text = "           Préstamos";
            button17.TextAlign = ContentAlignment.MiddleLeft;
            button17.UseVisualStyleBackColor = false;
            button17.Click += button17_Click;
            // 
            // panel13
            // 
            panel13.Controls.Add(button19);
            panel13.Location = new Point(0, 102);
            panel13.Margin = new Padding(0, 2, 0, 0);
            panel13.Name = "panel13";
            panel13.Size = new Size(248, 49);
            panel13.TabIndex = 18;
            // 
            // button19
            // 
            button19.BackColor = Color.FromArgb(16, 24, 32);
            button19.FlatAppearance.BorderSize = 0;
            button19.FlatStyle = FlatStyle.Flat;
            button19.ForeColor = Color.White;
            button19.Image = (Image)resources.GetObject("button19.Image");
            button19.ImageAlign = ContentAlignment.MiddleLeft;
            button19.Location = new Point(0, -6);
            button19.Name = "button19";
            button19.Padding = new Padding(10, 0, 0, 0);
            button19.Size = new Size(248, 55);
            button19.TabIndex = 5;
            button19.Text = "           Parámetro de ley";
            button19.TextAlign = ContentAlignment.MiddleLeft;
            button19.UseVisualStyleBackColor = false;
            button19.Click += button19_Click;
            // 
            // panel11
            // 
            panel11.Controls.Add(button15);
            panel11.Location = new Point(0, 153);
            panel11.Margin = new Padding(0, 2, 0, 0);
            panel11.Name = "panel11";
            panel11.Size = new Size(248, 49);
            panel11.TabIndex = 16;
            // 
            // button15
            // 
            button15.BackColor = Color.FromArgb(16, 24, 32);
            button15.FlatAppearance.BorderSize = 0;
            button15.FlatStyle = FlatStyle.Flat;
            button15.ForeColor = Color.White;
            button15.Image = (Image)resources.GetObject("button15.Image");
            button15.ImageAlign = ContentAlignment.MiddleLeft;
            button15.Location = new Point(0, 0);
            button15.Name = "button15";
            button15.Padding = new Padding(10, 0, 0, 0);
            button15.Size = new Size(248, 55);
            button15.TabIndex = 5;
            button15.Text = "           Planillas";
            button15.TextAlign = ContentAlignment.MiddleLeft;
            button15.UseVisualStyleBackColor = false;
            button15.Click += button15_Click;
            // 
            // menuAdministracion
            // 
            menuAdministracion.BackColor = Color.FromArgb(29, 48, 64);
            menuAdministracion.Controls.Add(panel8);
            menuAdministracion.Controls.Add(panel14);
            menuAdministracion.Controls.Add(panel15);
            menuAdministracion.FlowDirection = FlowDirection.TopDown;
            menuAdministracion.Location = new Point(3, 111);
            menuAdministracion.Name = "menuAdministracion";
            menuAdministracion.Size = new Size(248, 48);
            menuAdministracion.TabIndex = 9;
            menuAdministracion.Paint += menuAdministracion_Paint;
            // 
            // panel8
            // 
            panel8.Controls.Add(btnAdministracion);
            panel8.Location = new Point(0, 0);
            panel8.Margin = new Padding(0);
            panel8.Name = "panel8";
            panel8.Size = new Size(248, 49);
            panel8.TabIndex = 17;
            // 
            // btnAdministracion
            // 
            btnAdministracion.BackColor = Color.FromArgb(29, 48, 64);
            btnAdministracion.FlatAppearance.BorderSize = 0;
            btnAdministracion.FlatStyle = FlatStyle.Flat;
            btnAdministracion.ForeColor = Color.White;
            btnAdministracion.Image = (Image)resources.GetObject("btnAdministracion.Image");
            btnAdministracion.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdministracion.Location = new Point(0, -3);
            btnAdministracion.Name = "btnAdministracion";
            btnAdministracion.Padding = new Padding(10, 0, 0, 0);
            btnAdministracion.Size = new Size(248, 48);
            btnAdministracion.TabIndex = 5;
            btnAdministracion.Text = "           Administracion";
            btnAdministracion.TextAlign = ContentAlignment.MiddleLeft;
            btnAdministracion.UseVisualStyleBackColor = false;
            btnAdministracion.Click += btnAdministracion_Click;
            // 
            // panel14
            // 
            panel14.Controls.Add(button11);
            panel14.Location = new Point(248, 2);
            panel14.Margin = new Padding(0, 2, 0, 0);
            panel14.Name = "panel14";
            panel14.Size = new Size(248, 49);
            panel14.TabIndex = 18;
            // 
            // button11
            // 
            button11.BackColor = Color.FromArgb(16, 24, 32);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatStyle = FlatStyle.Flat;
            button11.ForeColor = Color.White;
            button11.Image = (Image)resources.GetObject("button11.Image");
            button11.ImageAlign = ContentAlignment.MiddleLeft;
            button11.Location = new Point(0, 0);
            button11.Name = "button11";
            button11.Padding = new Padding(10, 0, 0, 0);
            button11.Size = new Size(248, 55);
            button11.TabIndex = 5;
            button11.Text = "           Usuarios";
            button11.TextAlign = ContentAlignment.MiddleLeft;
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
            // 
            // panel15
            // 
            panel15.Controls.Add(button12);
            panel15.Location = new Point(496, 2);
            panel15.Margin = new Padding(0, 2, 0, 0);
            panel15.Name = "panel15";
            panel15.Size = new Size(248, 49);
            panel15.TabIndex = 19;
            // 
            // button12
            // 
            button12.BackColor = Color.FromArgb(16, 24, 32);
            button12.FlatAppearance.BorderSize = 0;
            button12.FlatStyle = FlatStyle.Flat;
            button12.ForeColor = Color.White;
            button12.Image = (Image)resources.GetObject("button12.Image");
            button12.ImageAlign = ContentAlignment.MiddleLeft;
            button12.Location = new Point(0, 0);
            button12.Name = "button12";
            button12.Padding = new Padding(10, 0, 0, 0);
            button12.Size = new Size(248, 55);
            button12.TabIndex = 5;
            button12.Text = "           Roles y permisos";
            button12.TextAlign = ContentAlignment.MiddleLeft;
            button12.UseVisualStyleBackColor = false;
            button12.Click += button12_Click;
            // 
            // menuTransicion
            // 
            menuTransicion.Interval = 10;
            menuTransicion.Tick += menuTransicion_Tick;
            // 
            // sliderTransicion
            // 
            sliderTransicion.Interval = 10;
            sliderTransicion.Tick += sliderTransicion_Tick;
            // 
            // menuGestion
            // 
            menuGestion.Interval = 10;
            menuGestion.Tick += menuGestion_Tick;
            // 
            // menuUsuario
            // 
            menuUsuario.Interval = 10;
            menuUsuario.Tick += menuUsuario_Tick;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1392, 671);
            Controls.Add(sliderbar);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            IsMdiContainer = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pMenu).EndInit();
            sliderbar.ResumeLayout(false);
            menuContainer.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            menuPlanilla.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel12.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel11.ResumeLayout(false);
            menuAdministracion.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel14.ResumeLayout(false);
            panel15.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pMenu;
        private Label label1;
        private FlowLayoutPanel sliderbar;
        private FlowLayoutPanel menuContainer;
        private System.Windows.Forms.Timer menuTransicion;
        private System.Windows.Forms.Timer sliderTransicion;
        private Panel panel7;
        private Panel panel6;
        private Button button5;
        private Button button4;
        private Panel panel9;
        private Button btnDepartamento;
        private Button button6;
        private Button btnEmple;
        private Button button2;
        private Panel panel3;
        private Button button3;
        private Button btnCargo;
        private Panel panel5;
        private Button btnCapacitacion;
        private Button button10;
        private FlowLayoutPanel menuPlanilla;
        private Panel panel10;
        private Button btnMenuGestion;
        private Panel panel11;
        private Button button15;
        private Panel panel12;
        private Button button17;
        private Panel panel13;
        private Button button19;
        private System.Windows.Forms.Timer menuGestion;
        private Button btnMenuRecurso;
        private FlowLayoutPanel menuAdministracion;
        private Panel panel8;
        private Button btnAdministracion;
        private Panel panel14;
        private Button button11;
        private Panel panel15;
        private Button button12;
        private System.Windows.Forms.Timer menuUsuario;
        private Panel panel4;
        private Button btnContrato;
        private Button button7;
        private Panel panel2;
        private Button button8;
        private Button btnSalir;
        private Button btnMini;
    }
}
