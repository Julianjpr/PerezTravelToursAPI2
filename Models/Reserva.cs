using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int TourId { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.Now;

        [Range(1, 100, ErrorMessage = "La cantidad de personas debe ser mayor que cero.")]
        public int CantidadPersonas { get; set; }

        public decimal Total { get; set; }

        public string Estado { get; set; } = "Pendiente";

        [Required]
        public int MetodoPagoId { get; set; }

        public Cliente? Cliente { get; set; }

        public Tour? Tour { get; set; }

        public MetodoPago? MetodoPago { get; set; }
    }
}
