namespace PerezTravelToursAPI.DTOs.Clientes
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Cedula { get; set; } = string.Empty;

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Direccion { get; set; }
    }
}
