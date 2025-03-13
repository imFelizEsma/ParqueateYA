namespace Sistema_Gestion_Tickets.DTOs
{
    public class TicketDTO
    {
        public string Codigo { get; set; }
        public string TipoVehiculo { get; set; }
        public DateTime HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public decimal TotalPago { get; set; }
    }
}
