using SistemaPlanilla.Modelo;
using SistemaPlanilla.Vista;
using SIstemaPlanilla.Vista;
using System;
using System.Drawing;
using System.Windows.Forms;
using SIstemaPlanilla.Vista;

namespace SIstemaPlanilla
{
    public partial class Form1 : Form
    {
        // Instancias de los formularios
        Empleado empleado = null!;
        Departamentos departamento = null!;
        Contratos contratos = null!;
        Cargos cargos = null!;
        Capacitaciones capacitaciones = null!;
        Planillas planillas = null!;
        Prestamos prestamos = null!;
        Parametros parametros = null!;
        Usuarios usuarios = null!;
        RolesPermisos rolesPermisos = null!;

        CapacitacionesEmpleado capacitacionEmpleado = null!;


        // Variables para controlar el estado de los menús
        bool menuExpandidoAdministracion = false;
        bool menuExpandidoGestion = false;
        bool sliderExpandido = true;
        bool menuExpandido = false;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }



        private void Form1_Load(object? sender, EventArgs e)
        {
            lbNombreUsuario.Text = "Usuario: " + SesionUsuario.Username;

            ActualizarHora();
            timerReloj.Interval = 1000;
            timerReloj.Start();
            AplicarPermisosMenu();
        }

        // Método para actualizar la hora en el label lbHoras

        private void ActualizarHora()
        {
            lbHoras.Text = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss tt");
        }

        // Método para aplicar los permisos de los menús según el rol del usuario
        private void AplicarPermisosMenu()
        {
            if (btnAdministracion != null) btnAdministracion.Visible = false;
            if (btnMenuGestion != null) btnMenuGestion.Visible = false;
            if (btnMenuRecurso != null) btnMenuRecurso.Visible = false;
            if (menuAdministracion != null) menuAdministracion.Visible = false;
            if (menuContainer != null) menuContainer.Visible = false;
            if (menuPlanilla != null) menuPlanilla.Visible = false;

            // Obtener los permisos del usuario según su rol
            List<int> permisos = ModuloMenu.ObtenerPermisos(SesionUsuario.IdRol);


            if (permisos.Contains((int)OpcionesSistema.Administracion))
            {
                if (btnAdministracion != null) btnAdministracion.Visible = true;
                if (menuAdministracion != null) menuAdministracion.Visible = true;
            }

            if (permisos.Contains((int)OpcionesSistema.RecursosHumanos))
            {
                if (btnMenuRecurso != null) btnMenuRecurso.Visible = true;
                if (menuContainer != null) menuContainer.Visible = true;
            }

            if (permisos.Contains((int)OpcionesSistema.GestionPlanilla))
            {
                if (btnMenuGestion != null) btnMenuGestion.Visible = true;
                if (menuPlanilla != null) menuPlanilla.Visible = true;
            }
        }


        // Método para manejar la transición de expansión y contracción del menú
        private void menuTransicion_Tick(object sender, EventArgs e)
        {
            if (menuExpandido == false)
            {
                menuContainer.Height += 5;
                if (menuContainer.Height >= 373)
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

        // Método para manejar la transición de expansión y contracción del slider
        private void sliderTransicion_Tick(object sender, EventArgs e)
        {
            if (sliderExpandido)
            {
                sliderbar.Width -= 5;
                if (sliderbar.Width <= 60)
                {
                    sliderExpandido = false;
                    sliderTransicion.Stop();

                    if (btnMenuRecurso != null) btnMenuRecurso.Width = sliderbar.Width;
                    if (btnMenuGestion != null) btnMenuGestion.Width = sliderbar.Width;
                    if (btnAdministracion != null) btnAdministracion.Width = sliderbar.Width;
                    if (btnSalir != null) btnSalir.Width = sliderbar.Width;
                }
            }
            else
            {
                sliderbar.Width += 5;
                if (sliderbar.Width >= 245)
                {
                    sliderExpandido = true;
                    sliderTransicion.Stop();

                    if (btnMenuRecurso != null) btnMenuRecurso.Width = sliderbar.Width;
                    if (btnMenuGestion != null) btnMenuGestion.Width = sliderbar.Width;
                    if (btnAdministracion != null) btnAdministracion.Width = sliderbar.Width;
                    if (btnSalir != null) btnSalir.Width = sliderbar.Width;
                }
            }
        }

        // Evento para manejar el clic en el botón del menú
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

        // Evento para manejar la transición de expansión y contracción del menú de gestión
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

        // Evento para manejar la transición de expansión y contracción del menú de administración
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

        // evento para abrir el formulario de Departamentos
        private void btnDepartamento_Click(object sender, EventArgs e)
        {
            if (departamento == null)
            {
                departamento = new Departamentos();
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

        // evento para cerrar el formulario de Departamentos
        private void Departamento_FormClosed(object? sender, FormClosedEventArgs e)
        {
            departamento = null!;
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

        private void Cargo_FormClosed(object? sender, FormClosedEventArgs e)
        {
            cargos = null!;
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

        private void Capacitaciones_FormClosed(object? sender, FormClosedEventArgs e)
        {
            capacitaciones = null!;
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

        private void Contratos_FormClosed(object? sender, FormClosedEventArgs e)
        {
            contratos = null!;
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

        private void Empleado_FormClosed(object? sender, FormClosedEventArgs e)
        {
            empleado = null!;
        }

        public bool SalirDelPrograma { get; private set; } = false;
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir de la aplicación?", "Salir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SalirDelPrograma = true;
                this.Close();
            }
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button17_Click(object sender, EventArgs e)
        {
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

        private void Prestamos_FormClosed(object? sender, FormClosedEventArgs e)
        {
            prestamos = null!;
        }

        private void button19_Click(object sender, EventArgs e)
        {
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

        private void Parametros_FormClosed(object? sender, FormClosedEventArgs e)
        {
            parametros = null!;
        }

        private void button15_Click(object sender, EventArgs e)
        {
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

        private void Planillas_FormClosed(object? sender, FormClosedEventArgs e)
        {
            planillas = null!;
        }

        private void button11_Click(object sender, EventArgs e)
        {
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

        private void Usuarios_FormClosed(object? sender, FormClosedEventArgs e)
        {
            usuarios = null!;
        }

        private void button12_Click(object sender, EventArgs e)
        {
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

        private void RolesPermisos_FormClosed(object? sender, FormClosedEventArgs e)
        {
            rolesPermisos = null!;
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
            "¿Está seguro de que desea cerrar la sesión actual?",
            "Cerrar Sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
    );

            if (respuesta == DialogResult.Yes)
            {
                SesionUsuario.LimpiarSesion();


                this.Close();
            }
        }

        private void btnCapEmpleeado_Click(object sender, EventArgs e)
        {
            if (capacitacionEmpleado == null)
            {
                capacitacionEmpleado = new CapacitacionesEmpleado();
                capacitacionEmpleado.FormClosed += CapacitacionesEmpleado_FormClosed;
                capacitacionEmpleado.MdiParent = this;
                capacitacionEmpleado.Dock = DockStyle.Fill;
                capacitacionEmpleado.Show();
            }
            else
            {
                capacitacionEmpleado.Activate();
            }
        }

        private void CapacitacionesEmpleado_FormClosed(object? sender, FormClosedEventArgs e)
        {
            capacitacionEmpleado = null!;
        }

        private void timerReloj_Tick(object sender, EventArgs e)
        {
            ActualizarHora();
        }
    }
}