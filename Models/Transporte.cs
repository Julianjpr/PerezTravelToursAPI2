using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Transporte
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El tipo de transporte es obligatorio.")]
        [StringLength(100)]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Range(1, 1000, ErrorMessage = "La capacidad debe ser mayor que cero.")]
        public int Capacidad { get; set; }

        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}