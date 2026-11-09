using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstemaPlanilla.Controlador
{
    internal class RolControlador
    {
        public DataTable ListarRoles()
        {
            Rol objRol = new Rol();
            return objRol.ObtenerTodosLosRoles();
        }

        public List<int> ConsultarPermisosDeRol(int idRol)
        {
            Rol objRol = new Rol();
            return objRol.ObtenerPermisosAsignados(idRol);
        }

        public bool CrearNuevoRol(string nombreRol, List<int> opcionesSeleccionadas)
        {
            Rol nuevoRol = new Rol { NombreRol = nombreRol };
            return nuevoRol.GuardarRolConPermisos(opcionesSeleccionadas);
        }

        public bool ModificarRol(int idRol, string nombreRol, List<int> opcionesSeleccionadas)
        {
            Rol rolEditado = new Rol { IdRol = idRol, NombreRol = nombreRol };
            return rolEditado.ActualizarRolConPermisos(opcionesSeleccionadas);
        }
    }
}
