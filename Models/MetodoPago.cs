using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class MetodoPago
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
