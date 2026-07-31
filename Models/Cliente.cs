using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        public string Telefono { get; set; } = string.Empty;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}