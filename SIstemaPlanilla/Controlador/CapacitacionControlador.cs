using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Modelo;
using System;
using System.Data;

namespace SIstemaPlanilla.Controlador
{
    internal class CapacitacionControlador
    {
        public DataTable ListarCapacitaciones()
        {
            Capacitacion obj = new Capacitacion();
            return obj.ObtenerTodasLasCapacitaciones();
        }

        public DataTable BuscarCapacitaciones(string filtro)
        {
            Capacitacion obj = new Capacitacion();
            return obj.BuscarCapacitaciones(filtro);
        }

        public bool GuardarNuevaCapacitacion(Capacitacion capacitacion, out string mensaje)
        {
            return capacitacion.Insertar(out mensaje);
        }

        public bool ModificarCapacitacion(Capacitacion capacitacion, out string mensaje)
        {
            return capacitacion.Actualizar(out mensaje);
        }

        public bool EliminarCapacitacion(int idCapacitacion, out string mensaje)
        {
            Capacitacion obj = new Capacitacion { IdCapacitacion = idCapacitacion };
            return obj.Eliminar(out mensaje);
        }
    }
}