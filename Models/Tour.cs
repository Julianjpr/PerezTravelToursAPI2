using System.ComponentModel.DataAnnotations;

namespace PerezTravelToursAPI.Models
{
    public class Tour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del tour es obligatorio.")]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un país.")]
        public int PaisId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un destino.")]
        public int DestinoId { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La hora es obligatoria.")]
        public TimeSpan Hora { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        public decimal ITBIS { get; set; }

        [Range(1, 365, ErrorMessage = "La duración debe estar entre 1 y 365 días.")]
        public int DuracionDias { get; set; }

        public DateTime FechaHoraFin { get; set; }

        public string Estado { get; set; } = "Vigente";

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int GuiaId { get; set; }

        [Required]
        public int TransporteId { get; set; }

        public Pais? Pais { get; set; }

        public Destino? Destino { get; set; }

        public Categoria? Categoria { get; set; }

        public GuiaTuristico? Guia { get; set; }

        public Transporte? Transporte { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
