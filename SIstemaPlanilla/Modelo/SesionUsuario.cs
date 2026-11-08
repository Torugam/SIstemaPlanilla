namespace SistemaPlanilla.Modelo
{
    public static class SesionUsuario
    {
        public static int IdUsuario { get; set; }
        public static int IdRol { get; set; }
        public static string Username { get; set; } = string.Empty;
        public static string NombreRol { get; set; } = string.Empty;
        public static string NombreEmpleado { get; set; } = string.Empty;

        // Método para resetear la sesión al cerrar sesión
        public static void LimpiarSesion()
        {
            IdUsuario = 0;
            IdRol = 0;
            Username = string.Empty;
            NombreRol = string.Empty;
            NombreEmpleado = string.Empty;
        }
    }
}