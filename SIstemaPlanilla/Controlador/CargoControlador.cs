using SistemaPlanilla.Modelo;
using SIstemaPlanilla.Modelo;
using System;
using System.Data;

namespace SIstemaPlanilla.Controlador
{
    internal class CargoControlador
    {
        public DataTable ListarCargos()
        {
            Cargo objCargo = new Cargo();
            return objCargo.ObtenerTodosLosCargos();
        }

        public DataTable BuscarCargos(string filtro)
        {
            Cargo objCargo = new Cargo();
            return objCargo.BuscarCargos(filtro);
        }

        public DataTable ListarCargosCombo()
        {
            Cargo objCargo = new Cargo();
            return objCargo.ObtenerCargosParaCombo();
        }

        public bool GuardarNuevoCargo(Cargo cargo, out string mensaje)
        {
            return cargo.Insertar(out mensaje);
        }

        public bool ModificarCargo(Cargo cargo, out string mensaje)
        {
            return cargo.Actualizar(out mensaje);
        }

        public bool EliminarCargo(int idCargo, out string mensaje)
        {
            Cargo objCargo = new Cargo { IdCargo = idCargo };
            return objCargo.EliminarCargo(out mensaje);
        }
    }
}