using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Services
{
    public class TourService
    {
        private readonly AgenciaToursContext _context;

        public TourService(AgenciaToursContext context)
        {
            _context = context;
        }

        // ==========================================
        // OBTENER TODOS LOS TOURS
        // ==========================================
        public async Task<List<Tour>> ObtenerTodos()
        {
            return await _context.Tours
                .Include(t => t.Pais)
                .Include(t => t.Destino)
                .Include(t => t.Categoria)
                .Include(t => t.Guia)
                .Include(t => t.Transporte)
                .ToListAsync();
        }

        // ==========================================
        // OBTENER TOUR POR ID
        // ==========================================
        public async Task<Tour?> ObtenerPorId(int id)
        {
            return await _context.Tours
                .Include(t => t.Pais)
                .Include(t => t.Destino)
                .Include(t => t.Categoria)
                .Include(t => t.Guia)
                .Include(t => t.Transporte)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        // ==========================================
        // CREAR TOUR
        // ==========================================
        public async Task<Tour> Crear(Tour tour)
        {
            _context.Tours.Add(tour);

            await _context.SaveChangesAsync();

            return tour;
        }

        // ==========================================
        // ACTUALIZAR TOUR
        // ==========================================
        public async Task<bool> Actualizar(int id, Tour tour)
        {
            var tourExistente = await _context.Tours
                .FindAsync(id);

            if (tourExistente == null)
            {
                return false;
            }

            tourExistente.Nombre = tour.Nombre;

            tourExistente.PaisId = tour.PaisId;

            tourExistente.DestinoId = tour.DestinoId;

            tourExistente.Fecha = tour.Fecha;

            tourExistente.Hora = tour.Hora;

            tourExistente.Precio = tour.Precio;

            tourExistente.ITBIS = tour.ITBIS;

            tourExistente.DuracionDias = tour.DuracionDias;

            tourExistente.FechaHoraFin = tour.FechaHoraFin;

            tourExistente.Estado = tour.Estado;

            tourExistente.CategoriaId = tour.CategoriaId;

            tourExistente.GuiaId = tour.GuiaId;

            tourExistente.TransporteId = tour.TransporteId;

            await _context.SaveChangesAsync();

            return true;
        }

        // ==========================================
        // ELIMINAR TOUR
        // ==========================================
        public async Task<bool> Eliminar(int id)
        {
            var tour = await _context.Tours
                .FindAsync(id);

            if (tour == null)
            {
                return false;
            }

            _context.Tours.Remove(tour);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
