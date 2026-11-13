using SIstemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstemaPlanilla.Controlador
{
    //Comentario de prueba para ver si se sube al repositorio. Ignorenlo
    internal class ContratoControlador
    {
        public DataTable ListarContratos(string filtro = "")
        {
            Contrato obj = new Contrato();
            return obj.ObtenerContratos(filtro);
        }

        public DataTable ListarEmpleadosSinContrato()
        {
            Contrato obj = new Contrato();
            return obj.ObtenerEmpleadosSinContrato();
        }

        public bool GuardarNuevoContrato(Contrato contrato, out string mensaje)
        {
            return contrato.Insertar(out mensaje);
        }

        public bool ModificarContrato(Contrato contrato, out string mensaje)
        {
            return contrato.Actualizar(out mensaje);
        }

        public bool EliminarContrato(int idEmpleado, out string mensaje)
        {
            Contrato obj = new Contrato { IdEmpleado = idEmpleado };
            return obj.Eliminar(out mensaje);
        }
    }
}
