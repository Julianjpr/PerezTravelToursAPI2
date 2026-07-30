using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.DTOs.Reservas
{
    public class ReservaCreateDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ClienteId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TourId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MetodoPagoId { get; set; }

        [Required]
        public DateTime FechaReserva { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CantidadPersonas { get; set; }
    }
}
