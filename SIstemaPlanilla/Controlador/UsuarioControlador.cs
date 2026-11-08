using SIstemaPlanilla.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIstemaPlanilla.Controlador
{
    internal class UsuarioControlador
    {


        public DataTable ListarUsuarios()
        {
            Usuario obj = new Usuario();
            return obj.Leer();
        }

        public DataTable BuscarUsuarios(string filtro)
        {
            Usuario obj = new Usuario();
            return obj.Buscar(filtro);
        }

       
        public bool GuardarNuevoUsuario(Usuario nuevoUsuario)
        {
            
            return nuevoUsuario.Insertar();
        }

        
        public bool ModificarUsuario(Usuario usuarioEditado, bool cambiaPassword)
        {
            return usuarioEditado.Actualizar(cambiaPassword);
        }

        public bool DesactivarUsuario(int idSeleccionado)
        {
            Usuario obj = new Usuario { IdUsuario = idSeleccionado };
            return obj.Desactivar();
        }
    }
}
