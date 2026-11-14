using SIstemaPlanilla.Modelo;
using SIstemaPlanilla.Vista;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstemaPlanilla.Controlador
{
    internal class CapacitacionEmpleadoControlador
    {
        //// Implementación de métodos para manejar la lógica de negocio relacionada con CapacitacionEmpleado
        public DataTable ListarRegistros()
        {
            CapacitacionEmpleado obj = new CapacitacionEmpleado();
            return obj.ObtenerTodos();
        }

        public DataTable BuscarRegistros(string filtro)
        {
            CapacitacionEmpleado obj = new CapacitacionEmpleado();
            return obj.Buscar(filtro);
        }

        public DataTable ListarEmpleadosCombo()
        {
            CapacitacionEmpleado obj = new CapacitacionEmpleado();
            return obj.ObtenerEmpleadosParaCombo();
        }

        public DataTable ListarCapacitacionesCombo()
        {
            CapacitacionEmpleado obj = new CapacitacionEmpleado();
            return obj.ObtenerCapacitacionesParaCombo();
        }

        public bool GuardarNuevoRegistro(CapacitacionEmpleado registro, out string mensaje)
        {
            return registro.Insertar(out mensaje);
        }

        public bool ModificarRegistro(CapacitacionEmpleado registro, out string mensaje)
        {
            return registro.Actualizar(out mensaje);
        }

        public bool EliminarRegistro(int idEmpleado, int idCapacitacion, DateTime fecha, out string mensaje)
        {
            CapacitacionEmpleado obj = new CapacitacionEmpleado
            {
                IdEmpleado = idEmpleado,
                IdCapacitacion = idCapacitacion,
                FechaOriginal = fecha
            };
            return obj.Eliminar(out mensaje);
        }
    }
}
