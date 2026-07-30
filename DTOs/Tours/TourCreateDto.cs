using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.DTOs.Tours
{
    public class TourCreateDto
    {
        [Required(ErrorMessage = "El nombre del tour es obligatorio.")]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un país.")]
        [Range(1, int.MaxValue)]
        public int PaisId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un destino.")]
        [Range(1, int.MaxValue)]
        public int DestinoId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria.")]
        public TimeSpan Hora { get; set; }

        [Range(0.01, double.MaxValue,
            ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ITBIS { get; set; }

        [Range(1, 365,
            ErrorMessage = "La duración debe estar entre 1 y 365 días.")]
        public int DuracionDias { get; set; }

        public DateTime FechaHoraFin { get; set; }

        public string Estado { get; set; } = "Vigente";

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoriaId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int GuiaId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TransporteId { get; set; }
    }
}