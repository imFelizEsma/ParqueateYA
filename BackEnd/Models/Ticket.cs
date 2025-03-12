using System.ComponentModel.DataAnnotations;

namespace Sistema_Gestion_Tickets.Models
{
    public class Ticket
    {
        [Key]
        public string Codigo { get; set; }
        [Required]
        public TipoVehiculo TipoVehiculo { get; set; }
        public DateTime HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public decimal TotalPago { get; set; }
    }
}
