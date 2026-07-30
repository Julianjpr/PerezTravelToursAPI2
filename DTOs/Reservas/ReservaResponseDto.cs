namespace PerezTravelToursAPI.DTOs.Reservas
{
    public class ReservaResponseDto
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public int TourId { get; set; }

        public int MetodoPagoId { get; set; }

        public DateTime FechaReserva { get; set; }

        public int CantidadPersonas { get; set; }

        public decimal Total { get; set; }
    }
}
