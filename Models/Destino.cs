using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Destino
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del destino es obligatorio.")]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un país.")]
        public int PaisId { get; set; }

        public Pais? Pais { get; set; }

        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
