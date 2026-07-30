namespace PerezTravelToursAPI.DTOs.Tours
{
    public class TourResponseDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int PaisId { get; set; }

        public int DestinoId { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public decimal Precio { get; set; }

        public decimal ITBIS { get; set; }

        public int DuracionDias { get; set; }

        public DateTime FechaHoraFin { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int CategoriaId { get; set; }

        public int GuiaId { get; set; }

        public int TransporteId { get; set; }
    }
}