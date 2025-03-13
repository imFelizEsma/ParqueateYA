using Sistema_Gestion_Tickets.DTOs;

namespace Sistema_Gestion_Tickets.Services.Interfaces
{
    public interface IVehicle
    {
        Task<TicketDTO> GenerarTicket(TicketViewModel vehiculo);
        Task<SalidaResponseDTO> RegistrarSalida(string codigoTicket);
        Task<IEnumerable<TicketDTO>> ObtenerTicketsActivos();
        Task<TicketDTO> ObtenerTicketPorCodigo(string codigo);
        Task ActualizarTicket(TicketDTO ticket);
        Task<bool> EliminarTicket(string codigo);
    }
}
