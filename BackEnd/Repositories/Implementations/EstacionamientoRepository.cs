using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Tickets.Data;
using Sistema_Gestion_Tickets.Models;
using Sistema_Gestion_Tickets.Repositories.Interfaces;

namespace Sistema_Gestion_Tickets.Repositories.Implementations
{
    public class EstacionamientoRepository: IEstacionamientoRepository
    {
        private readonly SAEContext _context;

        public EstacionamientoRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estacionamiento>> ObtenerTodos()
        {
            return await _context.Estacionamiento.ToListAsync();
        }

        public async Task<Estacionamiento> ObtenerPorTipoVehiculo(TipoVehiculo tipoVehiculo)
        {
            return await _context.Estacionamiento
                .FirstOrDefaultAsync(e => e.TipoVehiculo == tipoVehiculo);
        }

        public async Task Actualizar(Estacionamiento estacionamiento)
        {
            _context.Estacionamiento.Update(estacionamiento);
            await _context.SaveChangesAsync();
        }

        public async Task Crear(Estacionamiento estacionamiento)
        {
            _context.Estacionamiento.Add(estacionamiento);
            await _context.SaveChangesAsync();
        }

        public async Task LiberarEspacio(string tipoVehiculo)
        {
            Console.WriteLine($"Espacio liberado para el tipo de vehículo: {tipoVehiculo}");

            await Task.CompletedTask;
        }
    }
}
