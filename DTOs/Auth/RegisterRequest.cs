namespace PerezTravelToursAPI.DTOs.Auth
{
    public class RegisterRequest
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public int RolId { get; set; }
    }
}