using Sistema_Gestion_Tickets.DTOs;
using Sistema_Gestion_Tickets.Models;
using Sistema_Gestion_Tickets.Repositories.Interfaces;
using Sistema_Gestion_Tickets.Services.Interfaces;
using Sistema_Gestion_Tickets.Utils;

namespace Sistema_Gestion_Tickets.Services.Implementations
{
    public class VehicleService:IVehicle
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEstacionamientoRepository _estacionamientoRepository;

        public VehicleService(
            ITicketRepository ticketRepository,
            IEstacionamientoRepository estacionamientoRepository)
        {
            _ticketRepository = ticketRepository;
            _estacionamientoRepository = estacionamientoRepository;
        }

        public async Task<TicketDTO> GenerarTicket(TicketViewModel vehiculo)
        {
            if (!Enum.TryParse<TipoVehiculo>(vehiculo.TipoVehiculo, true, out var tipoVehiculo))
                throw new Exception("Tipo de vehículo no válido.");

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(tipoVehiculo);

            if (estacionamiento == null)
                throw new Exception("Tipo de vehículo no soportado.");

            if (estacionamiento.Ocupados >= estacionamiento.CapacidadTotal)
                throw new Exception("No hay disponibilidad para este tipo de vehículo.");

            var codigo = stringGenerator.GenerarCodigoUnico();

            var ticket = new Ticket
            {
                Codigo = codigo,
                TipoVehiculo = tipoVehiculo,
                HoraIngreso = DateTime.Now
            };

            await _ticketRepository.Crear(ticket);

            estacionamiento.Ocupados += 1;
            await _estacionamientoRepository.Actualizar(estacionamiento);

            return new TicketDTO
            {
                Codigo = ticket.Codigo,
                TipoVehiculo = ticket.TipoVehiculo.ToString(),
                HoraIngreso = ticket.HoraIngreso
            };
        }

        public async Task<SalidaResponseDTO> RegistrarSalida(string codigoTicket)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigoTicket);

            if (ticket == null)
                throw new Exception("Ticket no encontrado.");

            if (ticket.HoraSalida != null)
                throw new Exception("La salida ya ha sido registrada para este ticket.");

            ticket.HoraSalida = DateTime.Now;
            ///bien ata aqui

            await _ticketRepository.Actualizar(ticket);

            var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(ticket.TipoVehiculo);

            if (estacionamiento == null)
                throw new Exception("Estacionamiento no encontrado para este tipo de vehículo.");

            if (estacionamiento.Ocupados <= 0)
                throw new Exception("No hay vehículos ocupando espacios para este tipo.");

            estacionamiento.Ocupados -= 1;
            await _estacionamientoRepository.Actualizar(estacionamiento);

            var response = new SalidaResponseDTO
            {
                Message = "Salida registrada exitosamente.",
                TotalPago = 0
            };

            return response;
        }


        public async Task<IEnumerable<TicketDTO>> ObtenerTodosLosTickets()
        {
            var tickets = await _ticketRepository.ObtenerTodos();

            return tickets.Select(t => new TicketDTO
            {
                Codigo = t.Codigo,
                TipoVehiculo = t.TipoVehiculo.ToString(),
                HoraIngreso = t.HoraIngreso,
                HoraSalida = t.HoraSalida,
                TotalPago = t.TotalPago
            });
        }

        public async Task<IEnumerable<TicketDTO>> ObtenerTicketsActivos()
        {
            var tickets = await _ticketRepository.ObtenerActivos();

            return tickets.Select(t => new TicketDTO
            {
                Codigo = t.Codigo,
                TipoVehiculo = t.TipoVehiculo.ToString(),
                HoraIngreso = t.HoraIngreso
            });
        }

        public async Task<TicketDTO> ObtenerTicketPorCodigo(string codigo)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigo);

            if (ticket == null)
                return null;

            return new TicketDTO
            {
                Codigo = ticket.Codigo,
                TipoVehiculo = ticket.TipoVehiculo.ToString(),
                HoraIngreso = ticket.HoraIngreso,
                HoraSalida = ticket.HoraSalida,
                TotalPago = ticket.TotalPago
            };
        }

        public async Task ActualizarTicket(TicketDTO ticketDTO)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(ticketDTO.Codigo);

            if (ticket == null)
                throw new Exception("Ticket no encontrado.");

            ticket.TipoVehiculo = (TipoVehiculo)Enum.Parse(typeof(TipoVehiculo), ticketDTO.TipoVehiculo);
            ticket.HoraIngreso = ticketDTO.HoraIngreso;
            ticket.HoraSalida = ticketDTO.HoraSalida;
            ticket.TotalPago = ticketDTO.TotalPago;

            await _ticketRepository.Actualizar(ticket);
        }

        public async Task<bool> EliminarTicket(string codigo)
        {
            var ticket = await _ticketRepository.ObtenerPorCodigo(codigo);

            if (ticket != null)
            {
                var estacionamiento = await _estacionamientoRepository.ObtenerPorTipoVehiculo(ticket.TipoVehiculo);

                if (estacionamiento != null && estacionamiento.Ocupados > 0)
                {
                    estacionamiento.Ocupados -= 1;
                    await _estacionamientoRepository.Actualizar(estacionamiento);
                }

                await _ticketRepository.Eliminar(ticket);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
