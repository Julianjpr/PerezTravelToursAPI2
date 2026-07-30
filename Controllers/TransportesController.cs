using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportesController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

        public TransportesController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Transportes
        // Obtener todos los transportes con sus tours relacionados
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetTransportes()
        {
            var transportes = await _context.Transportes
                .AsNoTracking()
                .Select(t => new
                {
                    id = t.Id,
                    tipo = t.Tipo,
                    descripcion = t.Descripcion,
                    capacidad = t.Capacidad,

                    tours = t.Tours.Select(tour => new
                    {
                        id = tour.Id,
                        nombre = tour.Nombre,
                        paisId = tour.PaisId,
                        destinoId = tour.DestinoId,
                        fecha = tour.Fecha,
                        hora = tour.Hora,
                        precio = tour.Precio,
                        itbis = tour.ITBIS,
                        duracionDias = tour.DuracionDias,
                        fechaHoraFin = tour.FechaHoraFin,
                        estado = tour.Estado,
                        categoriaId = tour.CategoriaId,
                        guiaId = tour.GuiaId,
                        transporteId = tour.TransporteId
                    }).ToList()
                })
                .ToListAsync();

            return Ok(transportes);
        }

        // =========================================================
        // GET: api/Transportes/5
        // Obtener un transporte por ID con sus tours relacionados
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransporte(int id)
        {
            var transporte = await _context.Transportes
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    id = t.Id,
                    tipo = t.Tipo,
                    descripcion = t.Descripcion,
                    capacidad = t.Capacidad,

                    tours = t.Tours.Select(tour => new
                    {
                        id = tour.Id,
                        nombre = tour.Nombre,
                        paisId = tour.PaisId,
                        destinoId = tour.DestinoId,
                        fecha = tour.Fecha,
                        hora = tour.Hora,
                        precio = tour.Precio,
                        itbis = tour.ITBIS,
                        duracionDias = tour.DuracionDias,
                        fechaHoraFin = tour.FechaHoraFin,
                        estado = tour.Estado,
                        categoriaId = tour.CategoriaId,
                        guiaId = tour.GuiaId,
                        transporteId = tour.TransporteId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (transporte == null)
            {
                return NotFound(new
                {
                    mensaje = "El transporte no existe."
                });
            }

            return Ok(transporte);
        }

        // =========================================================
        // POST: api/Transportes
        // Crear un nuevo transporte
        // =========================================================
        [HttpPost]
        public async Task<IActionResult> PostTransporte(Models.Transporte transporte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Transportes.Add(transporte);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTransporte),
                new { id = transporte.Id },
                new
                {
                    id = transporte.Id,
                    tipo = transporte.Tipo,
                    descripcion = transporte.Descripcion,
                    capacidad = transporte.Capacidad
                }
            );
        }

        // =========================================================
        // PUT: api/Transportes/5
        // Actualizar un transporte
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransporte(
            int id,
            Models.Transporte transporte)
        {
            if (id != transporte.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID de la URL no coincide con el ID del transporte."
                });
            }

            var transporteExistente = await _context.Transportes
                .FindAsync(id);

            if (transporteExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "El transporte no existe."
                });
            }

            transporteExistente.Tipo = transporte.Tipo;
            transporteExistente.Descripcion = transporte.Descripcion;
            transporteExistente.Capacidad = transporte.Capacidad;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Transporte actualizado correctamente.",
                transporte = new
                {
                    id = transporteExistente.Id,
                    tipo = transporteExistente.Tipo,
                    descripcion = transporteExistente.Descripcion,
                    capacidad = transporteExistente.Capacidad
                }
            });
        }

        // =========================================================
        // DELETE: api/Transportes/5
        // Eliminar un transporte
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransporte(int id)
        {
            var transporte = await _context.Transportes
                .FindAsync(id);

            if (transporte == null)
            {
                return NotFound(new
                {
                    mensaje = "El transporte no existe."
                });
            }

            var tieneTours = await _context.Tours
                .AnyAsync(t => t.TransporteId == id);

            if (tieneTours)
            {
                return Conflict(new
                {
                    mensaje = "No se puede eliminar el transporte porque tiene tours asociados."
                });
            }

            _context.Transportes.Remove(transporte);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}