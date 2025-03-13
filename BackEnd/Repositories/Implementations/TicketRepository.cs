using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Tickets.Data;
using Sistema_Gestion_Tickets.Models;
using Sistema_Gestion_Tickets.Repositories.Interfaces;

namespace Sistema_Gestion_Tickets.Repositories.Implementations
{
    public class TicketRepository: ITicketRepository
    {
        private readonly SAEContext _context;

        public TicketRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task<Ticket> ObtenerPorCodigo(string codigo)
        {
            return await _context.Ticket.FirstOrDefaultAsync(t => t.Codigo == codigo);
        }

        public async Task<IEnumerable<Ticket>> ObtenerActivos()
        {
            return await _context.Ticket.Where(t => t.HoraSalida == null).ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> ObtenerTodos()
        {
            return await _context.Ticket.ToListAsync();
        }

        public async Task Crear(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Ticket ticket)
        {
            _context.Ticket.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(Ticket ticket)
        {
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
