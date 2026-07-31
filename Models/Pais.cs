using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Pais
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del país es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Destino> Destinos { get; set; } = new List<Destino>();

        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
