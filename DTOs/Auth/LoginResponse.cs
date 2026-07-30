namespace PerezTravelToursAPI.DTOs.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiracion { get; set; }

        public string Correo { get; set; } = string.Empty;
    }
}