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

        public bool CambiarEstadoUsuario(int idSeleccionado, string nuevoEstado)
        {
            Usuario obj = new Usuario { IdUsuario = idSeleccionado };
            return obj.CambiarEstado(nuevoEstado);
        }

        public bool IniciarSesion(string username, string password, out string mensaje)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                mensaje = "Por favor ingrese su usuario y contraseña.";
                return false;
            }

            string passwordHash = HelperSeguridad.CalcularSHA256(password);

            Usuario objUsuario = new Usuario();
            return objUsuario.ValidarLogin(username, passwordHash, out mensaje);
        }
    }
}
