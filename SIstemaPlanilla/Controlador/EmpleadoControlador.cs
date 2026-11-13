using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIstemaPlanilla.Modelo;

namespace SIstemaPlanilla.Controlador
{
    internal class EmpleadoControlador
    {
        public DataTable ListarEmpleados(string filtro = "")
        {
            Empleado obj = new Empleado();
            return obj.ObtenerEmpleados(filtro);
        }

        public DataTable ListarCargosCombo()
        {
            Empleado obj = new Empleado();
            return obj.ObtenerCargosParaCombo();
        }

        public bool GuardarNuevoEmpleado(Empleado empleado, out string mensaje)
        {
            return empleado.Insertar(out mensaje);
        }

        public bool ModificarEmpleado(Empleado empleado, out string mensaje)
        {
            return empleado.Actualizar(out mensaje);
        }

        public bool CambiarEstadoEmpleado(int idEmpleado, string nuevoEstado, out string mensaje)
        {
            Empleado obj = new Empleado { IdEmpleado = idEmpleado, Estado = nuevoEstado };
            return obj.CambiarEstado(out mensaje);
        }
    }
}
