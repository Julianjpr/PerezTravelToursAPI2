using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinosController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

        public DestinosController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Destinos
        // Obtener todos los destinos con su país y tours
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetDestinos()
        {
            var destinos = await _context.Destinos
                .AsNoTracking()
                .Select(d => new
                {
                    id = d.Id,
                    nombre = d.Nombre,
                    descripcion = d.Descripcion,
                    paisId = d.PaisId,

                    pais = d.Pais == null
                        ? null
                        : new
                        {
                            id = d.Pais.Id,
                            nombre = d.Pais.Nombre
                        },

                    tours = d.Tours.Select(t => new
                    {
                        id = t.Id,
                        nombre = t.Nombre,
                        fecha = t.Fecha,
                        hora = t.Hora,
                        precio = t.Precio,
                        itbis = t.ITBIS,
                        duracionDias = t.DuracionDias,
                        fechaHoraFin = t.FechaHoraFin,
                        estado = t.Estado,

                        categoriaId = t.CategoriaId,
                        guiaId = t.GuiaId,
                        transporteId = t.TransporteId,
                        paisId = t.PaisId,
                        destinoId = t.DestinoId
                    }).ToList()
                })
                .ToListAsync();

            return Ok(destinos);
        }

        // =========================================================
        // GET: api/Destinos/5
        // Obtener un destino con su país y tours
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDestino(int id)
        {
            var destino = await _context.Destinos
                .AsNoTracking()
                .Where(d => d.Id == id)
                .Select(d => new
                {
                    id = d.Id,
                    nombre = d.Nombre,
                    descripcion = d.Descripcion,
                    paisId = d.PaisId,

                    pais = d.Pais == null
                        ? null
                        : new
                        {
                            id = d.Pais.Id,
                            nombre = d.Pais.Nombre
                        },

                    tours = d.Tours.Select(t => new
                    {
                        id = t.Id,
                        nombre = t.Nombre,
                        fecha = t.Fecha,
                        hora = t.Hora,
                        precio = t.Precio,
                        itbis = t.ITBIS,
                        duracionDias = t.DuracionDias,
                        fechaHoraFin = t.FechaHoraFin,
                        estado = t.Estado,

                        categoriaId = t.CategoriaId,
                        guiaId = t.GuiaId,
                        transporteId = t.TransporteId,
                        paisId = t.PaisId,
                        destinoId = t.DestinoId
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (destino == null)
            {
                return NotFound(new
                {
                    mensaje = "El destino no existe."
                });
            }

            return Ok(destino);
        }

        // =========================================================
        // POST: api/Destinos
        // Crear un nuevo destino
        // =========================================================
        [HttpPost]
        public async Task<IActionResult> PostDestino(Destino destino)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paisExiste = await _context.Paises
                .AnyAsync(p => p.Id == destino.PaisId);

            if (!paisExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El país indicado no existe."
                });
            }

            _context.Destinos.Add(destino);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDestino),
                new { id = destino.Id },
                destino
            );
        }

        // =========================================================
        // PUT: api/Destinos/5
        // Actualizar destino
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestino(
            int id,
            Destino destino)
        {
            if (id != destino.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID de la URL no coincide con el ID del destino."
                });
            }

            var destinoExistente = await _context.Destinos
                .FindAsync(id);

            if (destinoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "El destino no existe."
                });
            }

            var paisExiste = await _context.Paises
                .AnyAsync(p => p.Id == destino.PaisId);

            if (!paisExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El país indicado no existe."
                });
            }

            destinoExistente.Nombre = destino.Nombre;
            destinoExistente.Descripcion = destino.Descripcion;
            destinoExistente.PaisId = destino.PaisId;

            await _context.SaveChangesAsync();

            return Ok(destinoExistente);
        }

        // =========================================================
        // DELETE: api/Destinos/5
        // Eliminar destino
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestino(int id)
        {
            var destino = await _context.Destinos
                .FindAsync(id);

            if (destino == null)
            {
                return NotFound(new
                {
                    mensaje = "El destino no existe."
                });
            }

            var tieneTours = await _context.Tours
                .AnyAsync(t => t.DestinoId == id);

            if (tieneTours)
            {
                return Conflict(new
                {
                    mensaje = "No se puede eliminar el destino porque tiene tours asociados."
                });
            }

            _context.Destinos.Remove(destino);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}