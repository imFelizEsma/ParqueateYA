namespace Sistema_Gestion_Tickets.DTOs
{
    public class EstacionamientoDTO
    {
        public int IDEstacionamiento { get; set; }
        public string TipoVehiculo { get; set; }
        public int CapacidadTotal { get; set; }
        public int Ocupados { get; set; }
        public int Disponibles => CapacidadTotal - Ocupados;
    }
}
