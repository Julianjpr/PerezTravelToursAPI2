using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
