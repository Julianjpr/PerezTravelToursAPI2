using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.DTOs.Clientes
{
    public class ClienteCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [StringLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
        [StringLength(20)]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(250)]
        public string? Direccion { get; set; }
    }
}
