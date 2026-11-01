using SIstemaPlanilla.Vista;

namespace SIstemaPlanilla
{
    public partial class Form1 : Form
    {
        Empleado empleado;
        Departamento departamento;
        Contratos contratos;
        Cargos cargos;
        Capacitaciones capacitaciones;
        Planillas planillas;
        Prestamos prestamos;
        Parametros parametros;
        Usuarios usuarios;
        RolesPermisos rolesPermisos;

        bool menuExpandidoAdministracion = false;
        bool menuExpandidoGestion = false;
        bool sliderExpandido = true;
        bool menuExpandido = false;

        public Form1()
        {
            InitializeComponent();
        }


        private void menuTransicion_Tick(object sender, EventArgs e)
        {
            if (menuExpandido == false)
            {
                menuContainer.Height += 5;
                if (menuContainer.Height >= 307)
                {
                    menuTransicion.Stop();
                    menuExpandido = true;
                }

            }
            else
            {
                menuContainer.Height -= 5;
                if (menuContainer.Height <= 48)
                {
                    menuTransicion.Stop();
                    menuExpandido = false;
                }
            }
        }

        private void sliderTransicion_Tick(object sender, EventArgs e)
        {
            if (sliderExpandido)
            {
                sliderbar.Width -= 5;
                if (sliderbar.Width <= 60)
                {
                    sliderExpandido = false;
                    sliderTransicion.Stop();

                    btnMenuRecurso.Width = sliderbar.Width;
                    btnMenuGestion.Width = sliderbar.Width;
                    btnAdministracion.Width = sliderbar.Width;
                }
            }
            else
            {
                sliderbar.Width += 5;
                if (sliderbar.Width >= 245)
                {
                    sliderExpandido = true;
                    sliderTransicion.Stop();

                    btnMenuRecurso.Width = sliderbar.Width;
                    btnMenuGestion.Width = sliderbar.Width;
                    btnAdministracion.Width = sliderbar.Width;
                }
            }

        }

        private void pMenu_Click(object sender, EventArgs e)
        {
            sliderTransicion.Start();
        }

        private void btnMenuRecurso_Click(object sender, EventArgs e)
        {
            menuTransicion.Start();
        }

        private void btnMenuGestion_Click(object sender, EventArgs e)
        {
            menuGestion.Start();
        }

        private void menuGestion_Tick(object sender, EventArgs e)
        {
            if (menuExpandidoGestion == false)
            {
                menuPlanilla.Height += 5;
                if (menuPlanilla.Height >= 206)
                {
                    menuGestion.Stop();
                    menuExpandidoGestion = true;
                }

            }
            else
            {
                menuPlanilla.Height -= 5;
                if (menuPlanilla.Height <= 48)
                {
                    menuGestion.Stop();
                    menuExpandidoGestion = false;
                }
            }

        }

        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            menuUsuario.Start();
        }


        private void menuUsuario_Tick(object sender, EventArgs e)
        {
            if (menuExpandidoAdministracion == false)
            {
                menuAdministracion.Height += 5;
                if (menuAdministracion.Height >= 155)
                {
                    menuUsuario.Stop();
                    menuExpandidoAdministracion = true;
                }

            }
            else
            {
                menuAdministracion.Height -= 5;
                if (menuAdministracion.Height <= 48)
                {
                    menuUsuario.Stop();
                    menuExpandidoAdministracion = false;
                }
            }
        }

        private void menuAdministracion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDepartamento_Click(object sender, EventArgs e)
        {
            if (departamento == null)
            {
                departamento = new Departamento();
                departamento.FormClosed += Departamento_FormClosed;
                departamento.MdiParent = this;
                departamento.Dock = DockStyle.Fill;
                departamento.Show();
            }
            else
            {
                departamento.Activate();
            }
        }

        private void Departamento_FormClosed(object sender, FormClosedEventArgs e)
        {
            departamento = null;

        }

        private void btnCargo_Click(object sender, EventArgs e)
        {
            if (cargos == null)
            {
                cargos = new Cargos();
                cargos.FormClosed += Cargo_FormClosed;
                cargos.MdiParent = this;
                cargos.Dock = DockStyle.Fill;
                cargos.Show();
            }
            else
            {
                cargos.Activate();
            }
        }

        private void Cargo_FormClosed(object sender, FormClosedEventArgs e)
        {
            cargos = null;

        }

        private void btnCapacitacion_Click(object sender, EventArgs e)
        {
            if (capacitaciones == null)
            {
                capacitaciones = new Capacitaciones();
                capacitaciones.FormClosed += Capacitaciones_FormClosed;
                capacitaciones.MdiParent = this;
                capacitaciones.Dock = DockStyle.Fill;
                capacitaciones.Show();
            }
            else
            {
                capacitaciones.Activate();
            }
        }

        private void Capacitaciones_FormClosed(object sender, FormClosedEventArgs e)
        {
            capacitaciones = null;

        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            if (contratos == null)
            {
                contratos = new Contratos();
                contratos.FormClosed += Contratos_FormClosed;
                contratos.MdiParent = this;
                contratos.Dock = DockStyle.Fill;
                contratos.Show();
            }
            else
            {
                contratos.Activate();
            }
        }

        private void Contratos_FormClosed(object sender, FormClosedEventArgs e)
        {
            contratos = null;

        }

        private void btnEmple_Click(object sender, EventArgs e)
        {
            if (empleado == null)
            {
                empleado = new Empleado();
                empleado.FormClosed += Empleado_FormClosed;
                empleado.MdiParent = this;
                empleado.Dock = DockStyle.Fill;
                empleado.Show();
            }
            else
            {
                empleado.Activate();
            }
        }

        private void Empleado_FormClosed(object sender, FormClosedEventArgs e)
        {
            empleado = null;

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //prestamos
            if (prestamos == null)
            {
                prestamos = new Prestamos();
                prestamos.FormClosed += Prestamos_FormClosed;
                prestamos.MdiParent = this;
                prestamos.Dock = DockStyle.Fill;
                prestamos.Show();
            }
            else
            {
                prestamos.Activate();
            }
        }

        private void Prestamos_FormClosed(object sender, FormClosedEventArgs e)
        {
            prestamos = null;

        }

        private void button19_Click(object sender, EventArgs e)
        {
            //ley
            if (parametros == null)
            {
                parametros = new Parametros();
                parametros.FormClosed += Parametros_FormClosed;
                parametros.MdiParent = this;
                parametros.Dock = DockStyle.Fill;
                parametros.Show();
            }
            else
            {
                parametros.Activate();
            }
        }

        private void Parametros_FormClosed(object sender, FormClosedEventArgs e)
        {
            parametros = null;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            //planillas
            if (planillas == null)
            {
                planillas = new Planillas();
                planillas.FormClosed += Planillas_FormClosed;
                planillas.MdiParent = this;
                planillas.Dock = DockStyle.Fill;
                planillas.Show();
            }
            else
            {
                planillas.Activate();
            }
        }

        private void Planillas_FormClosed(object sender, FormClosedEventArgs e)
        {
            planillas = null;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //usuario
            if (usuarios == null)
            {
                usuarios = new Usuarios();
                usuarios.FormClosed += Usuarios_FormClosed;
                usuarios.MdiParent = this;
                usuarios.Dock = DockStyle.Fill;
                usuarios.Show();
            }
            else
            {
                usuarios.Activate();
            }
        }

        private void Usuarios_FormClosed(object sender, FormClosedEventArgs e)
        {
            usuarios = null;

        }
        private void button12_Click(object sender, EventArgs e)
        {
            //roles
            if (rolesPermisos == null)
            {
                rolesPermisos = new RolesPermisos();
                rolesPermisos.FormClosed += RolesPermisos_FormClosed;
                rolesPermisos.MdiParent = this;
                rolesPermisos.Dock = DockStyle.Fill;
                rolesPermisos.Show();
            }
            else
            {
                rolesPermisos.Activate();
            }
            
        }

        private void RolesPermisos_FormClosed(object sender, FormClosedEventArgs e)
        {
            rolesPermisos = null;

        }
    }
}
