using SIstemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstemaPlanilla.Controlador
{
    //Comentario de prueba para ver si se sube al repositorio. Ignorenlo.
    internal class DepartamentoControlador
    {
        public DataTable ListarDepartamentos()
        {
            Departamento obj = new Departamento();
            return obj.ObtenerTodosLosDepartamentos();
        }

        public DataTable BuscarDepartamentos(string filtro)
        {
            Departamento obj = new Departamento();
            return obj.BuscarDepartamentos(filtro);
        }

        public DataTable ListarEncargadosCombo()
        {
            Departamento obj = new Departamento();
            return obj.ObtenerEncargadosCombo();
        }

        public bool GuardarNuevoDepartamento(Departamento depto, out string mensaje)
        {
            return depto.Insertar(out mensaje);
        }

        public bool ModificarDepartamento(Departamento depto, out string mensaje)
        {
            return depto.Actualizar(out mensaje);
        }

        public bool EliminarDepartamento(int idDepartamento, out string mensaje)
        {
            Departamento obj = new Departamento { IdDepartamento = idDepartamento };
            return obj.Eliminar(out mensaje);
        }
    }
}
