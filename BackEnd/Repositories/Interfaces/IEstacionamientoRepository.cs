using Sistema_Gestion_Tickets.Models;

namespace Sistema_Gestion_Tickets.Repositories.Interfaces
{
    public interface IEstacionamientoRepository
    {
        Task<IEnumerable<Estacionamiento>> ObtenerTodos();
        Task<Estacionamiento> ObtenerPorTipoVehiculo(TipoVehiculo tipoVehiculo);
        Task Actualizar(Estacionamiento estacionamiento);
        Task Crear(Estacionamiento estacionamiento);

        Task LiberarEspacio(string tipoVehiculo);
    }
}
