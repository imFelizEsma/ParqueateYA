using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Tickets.Data;
using Sistema_Gestion_Tickets.Models;
using Sistema_Gestion_Tickets.Repositories.Interfaces;

namespace Sistema_Gestion_Tickets.Repositories.Implementations
{
    public class AdministradorRepository: IAdministradorRepository
    {
        private readonly SAEContext _context;

        public AdministradorRepository(SAEContext context)
        {
            _context = context;
        }

        public async Task<Administrador> AutenticarAdministrador(string nombreUsuario, string contrasena)
        {
            return await _context.Administrador.FirstOrDefaultAsync(a => a.NombreUsuario == nombreUsuario && a.Contrasena == contrasena);
        }
    }
}
