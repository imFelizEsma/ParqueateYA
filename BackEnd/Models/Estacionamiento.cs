using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Gestion_Tickets.Models
{
    public class Estacionamiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDEstacionamiento { get; set; }
        [Required]
        public TipoVehiculo TipoVehiculo { get; set; }
        [Required]
        public int CapacidadTotal { get; set; }
        [Required]
        public int Ocupados { get; set; }
        [NotMapped]
        public int Disponibles => CapacidadTotal - Ocupados;
    }
}
