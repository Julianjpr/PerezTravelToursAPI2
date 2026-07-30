using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuiasTuristicosController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

    public GuiasTuristicosController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/GuiasTuristicos
        // OBTENER TODOS LOS GUÍAS CON SUS TOURS
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetGuias()
        {
            var guias = await _context.GuiasTuristicos
                .AsNoTracking()
                .Select(g => new
                {
                    g.Id,
                    g.Nombre,
                    g.Apellido,
                    g.Telefono,
                    g.Correo,
                    g.Especialidad,

                    Tours = g.Tours.Select(t => new
                    {
                        t.Id,
                        t.Nombre,
                        t.Fecha,
                        t.Hora,
                        t.Precio,
                        t.ITBIS,
                        t.DuracionDias,
                        t.FechaHoraFin,
                        t.Estado,

                        Pais = t.Pais == null
                            ? null
                            : new
                            {
                                t.Pais.Id,
                                t.Pais.Nombre
                            },

                        Destino = t.Destino == null
                            ? null
                            : new
                            {
                                t.Destino.Id,
                                t.Destino.Nombre,
                                t.Destino.Descripcion
                            },

                        Categoria = t.Categoria == null
                            ? null
                            : new
                            {
                                t.Categoria.Id,
                                t.Categoria.Nombre,
                                t.Categoria.Descripcion
                            },

                        Transporte = t.Transporte == null
                            ? null
                            : new
                            {
                                t.Transporte.Id,
                                t.Transporte.Tipo,
                                t.Transporte.Descripcion,
                                t.Transporte.Capacidad
                            }
                    })
                })
                .ToListAsync();

            return Ok(guias);
        }

        // =========================================================
        // GET: api/GuiasTuristicos/5
        // OBTENER UN GUÍA CON SUS TOURS
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuia(int id)
        {
            var guia = await _context.GuiasTuristicos
                .AsNoTracking()
                .Where(g => g.Id == id)
                .Select(g => new
                {
                    g.Id,
                    g.Nombre,
                    g.Apellido,
                    g.Telefono,
                    g.Correo,
                    g.Especialidad,

                    Tours = g.Tours.Select(t => new
                    {
                        t.Id,
                        t.Nombre,
                        t.Fecha,
                        t.Hora,
                        t.Precio,
                        t.ITBIS,
                        t.DuracionDias,
                        t.FechaHoraFin,
                        t.Estado,

                        Pais = t.Pais == null
                            ? null
                            : new
                            {
                                t.Pais.Id,
                                t.Pais.Nombre
                            },

                        Destino = t.Destino == null
                            ? null
                            : new
                            {
                                t.Destino.Id,
                                t.Destino.Nombre,
                                t.Destino.Descripcion
                            },

                        Categoria = t.Categoria == null
                            ? null
                            : new
                            {
                                t.Categoria.Id,
                                t.Categoria.Nombre,
                                t.Categoria.Descripcion
                            },

                        Transporte = t.Transporte == null
                            ? null
                            : new
                            {
                                t.Transporte.Id,
                                t.Transporte.Tipo,
                                t.Transporte.Descripcion,
                                t.Transporte.Capacidad
                            }
                    })
                })
                .FirstOrDefaultAsync();

            if (guia == null)
            {
                return NotFound(new
                {
                    mensaje = "El guía turístico no existe."
                });
            }

            return Ok(guia);
        }

        // =========================================================
        // POST: api/GuiasTuristicos
        // CREAR GUÍA TURÍSTICO
        // =========================================================
        [HttpPost]
        public async Task<ActionResult> PostGuia(
            Models.GuiaTuristico guia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = await _context.GuiasTuristicos
                .AnyAsync(g =>
                    g.Correo.ToLower() ==
                    guia.Correo.ToLower());

            if (existe)
            {
                return Conflict(new
                {
                    mensaje =
                        "Ya existe un guía registrado con ese correo."
                });
            }

            _context.GuiasTuristicos.Add(guia);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGuia),
                new { id = guia.Id },
                guia
            );
        }

        // =========================================================
        // PUT: api/GuiasTuristicos/5
        // ACTUALIZAR GUÍA TURÍSTICO
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuia(
            int id,
            Models.GuiaTuristico guia)
        {
            if (id != guia.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID no coincide."
                });
            }

            var guiaExistente =
                await _context.GuiasTuristicos
                    .FindAsync(id);

            if (guiaExistente == null)
            {
                return NotFound(new
                {
                    mensaje =
                        "El guía turístico no existe."
                });
            }

            guiaExistente.Nombre =
                guia.Nombre;

            guiaExistente.Apellido =
                guia.Apellido;

            guiaExistente.Telefono =
                guia.Telefono;

            guiaExistente.Correo =
                guia.Correo;

            guiaExistente.Especialidad =
                guia.Especialidad;

            await _context.SaveChangesAsync();

            return Ok(guiaExistente);
        }

        // =========================================================
        // DELETE: api/GuiasTuristicos/5
        // ELIMINAR GUÍA TURÍSTICO
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuia(int id)
        {
            var guia =
                await _context.GuiasTuristicos
                    .FindAsync(id);

            if (guia == null)
            {
                return NotFound(new
                {
                    mensaje =
                        "El guía turístico no existe."
                });
            }

            var tieneTours =
                await _context.Tours
                    .AnyAsync(t =>
                        t.GuiaId == id);

            if (tieneTours)
            {
                return Conflict(new
                {
                    mensaje =
                        "No se puede eliminar el guía porque tiene tours asociados."
                });
            }

            _context.GuiasTuristicos.Remove(guia);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
