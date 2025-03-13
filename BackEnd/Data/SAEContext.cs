using Microsoft.EntityFrameworkCore;
using Sistema_Gestion_Tickets.Models;

namespace Sistema_Gestion_Tickets.Data
{
    public class SAEContext : DbContext
    {
        public SAEContext(DbContextOptions<SAEContext> options) : base(options) { }

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Estacionamiento> Estacionamiento { get; set; }

    }
}
