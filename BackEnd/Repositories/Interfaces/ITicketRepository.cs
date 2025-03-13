using Sistema_Gestion_Tickets.Models;

namespace Sistema_Gestion_Tickets.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> ObtenerPorCodigo(string codigo);
        Task<IEnumerable<Ticket>> ObtenerActivos();
        Task<IEnumerable<Ticket>> ObtenerTodos();
        Task Crear(Ticket ticket);
        Task Actualizar(Ticket ticket);
        Task Eliminar(Ticket ticket);
    }
}
