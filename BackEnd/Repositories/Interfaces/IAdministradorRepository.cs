using Sistema_Gestion_Tickets.Models;

namespace Sistema_Gestion_Tickets.Repositories.Interfaces
{
    public interface IAdministradorRepository
    {
        Task<Administrador> AutenticarAdministrador(string nombreUsuario, string contrasena);
    }
}
