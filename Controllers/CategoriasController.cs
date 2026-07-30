using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

    public CategoriasController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Categorias
        // OBTENER TODAS LAS CATEGORÍAS CON SUS TOURS
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Descripcion,

                    Tours = c.Tours.Select(t => new
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

                        Guia = t.Guia == null
                            ? null
                            : new
                            {
                                t.Guia.Id,
                                t.Guia.Nombre,
                                t.Guia.Apellido,
                                t.Guia.Telefono,
                                t.Guia.Correo,
                                t.Guia.Especialidad
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

            return Ok(categorias);
        }

        // =========================================================
        // GET: api/Categorias/5
        // OBTENER UNA CATEGORÍA CON SUS TOURS
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            var categoria = await _context.Categorias
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Descripcion,

                    Tours = c.Tours.Select(t => new
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

                        Guia = t.Guia == null
                            ? null
                            : new
                            {
                                t.Guia.Id,
                                t.Guia.Nombre,
                                t.Guia.Apellido,
                                t.Guia.Telefono,
                                t.Guia.Correo,
                                t.Guia.Especialidad
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

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje = "La categoría no existe."
                });
            }

            return Ok(categoria);
        }

        // =========================================================
        // POST: api/Categorias
        // CREAR CATEGORÍA
        // =========================================================
        [HttpPost]
        public async Task<ActionResult> PostCategoria(
            Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = await _context.Categorias
                .AnyAsync(c =>
                    c.Nombre.ToLower() ==
                    categoria.Nombre.ToLower());

            if (existe)
            {
                return Conflict(new
                {
                    mensaje =
                        "Ya existe una categoría con ese nombre."
                });
            }

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategoria),
                new { id = categoria.Id },
                categoria
            );
        }

        // =========================================================
        // PUT: api/Categorias/5
        // ACTUALIZAR CATEGORÍA
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(
            int id,
            Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest(new
                {
                    mensaje =
                        "El ID de la URL no coincide con el ID de la categoría."
                });
            }

            var categoriaExistente =
                await _context.Categorias
                    .FindAsync(id);

            if (categoriaExistente == null)
            {
                return NotFound(new
                {
                    mensaje =
                        "La categoría no existe."
                });
            }

            var existeNombre = await _context.Categorias
                .AnyAsync(c =>
                    c.Id != id &&
                    c.Nombre.ToLower() ==
                    categoria.Nombre.ToLower());

            if (existeNombre)
            {
                return Conflict(new
                {
                    mensaje =
                        "Ya existe otra categoría con ese nombre."
                });
            }

            categoriaExistente.Nombre =
                categoria.Nombre;

            categoriaExistente.Descripcion =
                categoria.Descripcion;

            await _context.SaveChangesAsync();

            return Ok(categoriaExistente);
        }

        // =========================================================
        // DELETE: api/Categorias/5
        // ELIMINAR CATEGORÍA
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria =
                await _context.Categorias
                    .FindAsync(id);

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje =
                        "La categoría no existe."
                });
            }

            var tieneTours =
                await _context.Tours
                    .AnyAsync(t =>
                        t.CategoriaId == id);

            if (tieneTours)
            {
                return Conflict(new
                {
                    mensaje =
                        "No se puede eliminar la categoría porque tiene tours asociados."
                });
            }

            _context.Categorias.Remove(categoria);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

