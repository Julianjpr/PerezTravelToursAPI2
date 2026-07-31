using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerezTravelToursAPI.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int RolId { get; set; }
    }
}