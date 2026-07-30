using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

    public PaisesController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Paises
        // Obtener todos los países con sus destinos y tours
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetPaises()
        {
            var paises = await _context.Paises
                .AsNoTracking()
                .Select(p => new
                {
                    id = p.Id,
                    nombre = p.Nombre,

                    // =================================================
                    // DESTINOS DEL PAÍS
                    // =================================================
                    destinos = p.Destinos
                        .Select(d => new
                        {
                            id = d.Id,
                            nombre = d.Nombre,
                            descripcion = d.Descripcion,
                            paisId = d.PaisId,

                            // =========================================
                            // TOURS DEL DESTINO
                            // =========================================
                            tours = d.Tours
                                .Select(t => new
                                {
                                    id = t.Id,
                                    nombre = t.Nombre,
                                    paisId = t.PaisId,
                                    destinoId = t.DestinoId,
                                    fecha = t.Fecha,
                                    hora = t.Hora,
                                    precio = t.Precio,
                                    itbis = t.ITBIS,
                                    duracionDias = t.DuracionDias,
                                    fechaHoraFin = t.FechaHoraFin,
                                    estado = t.Estado,

                                    categoria = t.Categoria == null
                                        ? null
                                        : new
                                        {
                                            id = t.Categoria.Id,
                                            nombre = t.Categoria.Nombre,
                                            descripcion = t.Categoria.Descripcion
                                        },

                                    guia = t.Guia == null
                                        ? null
                                        : new
                                        {
                                            id = t.Guia.Id,
                                            nombre = t.Guia.Nombre,
                                            apellido = t.Guia.Apellido,
                                            telefono = t.Guia.Telefono,
                                            correo = t.Guia.Correo,
                                            especialidad = t.Guia.Especialidad
                                        },

                                    transporte = t.Transporte == null
                                        ? null
                                        : new
                                        {
                                            id = t.Transporte.Id,
                                            tipo = t.Transporte.Tipo,
                                            descripcion = t.Transporte.Descripcion,
                                            capacidad = t.Transporte.Capacidad
                                        }
                                })
                                .ToList()
                        })
                        .ToList(),

                    // =================================================
                    // TOURS DIRECTAMENTE ASOCIADOS AL PAÍS
                    // =================================================
                    tours = p.Tours
                        .Select(t => new
                        {
                            id = t.Id,
                            nombre = t.Nombre,
                            paisId = t.PaisId,
                            destinoId = t.DestinoId,
                            fecha = t.Fecha,
                            hora = t.Hora,
                            precio = t.Precio,
                            itbis = t.ITBIS,
                            duracionDias = t.DuracionDias,
                            fechaHoraFin = t.FechaHoraFin,
                            estado = t.Estado,

                            destino = t.Destino == null
                                ? null
                                : new
                                {
                                    id = t.Destino.Id,
                                    nombre = t.Destino.Nombre,
                                    descripcion = t.Destino.Descripcion
                                },

                            categoria = t.Categoria == null
                                ? null
                                : new
                                {
                                    id = t.Categoria.Id,
                                    nombre = t.Categoria.Nombre,
                                    descripcion = t.Categoria.Descripcion
                                },

                            guia = t.Guia == null
                                ? null
                                : new
                                {
                                    id = t.Guia.Id,
                                    nombre = t.Guia.Nombre,
                                    apellido = t.Guia.Apellido,
                                    telefono = t.Guia.Telefono,
                                    correo = t.Guia.Correo,
                                    especialidad = t.Guia.Especialidad
                                },

                            transporte = t.Transporte == null
                                ? null
                                : new
                                {
                                    id = t.Transporte.Id,
                                    tipo = t.Transporte.Tipo,
                                    descripcion = t.Transporte.Descripcion,
                                    capacidad = t.Transporte.Capacidad
                                }
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(paises);
        }

        // =========================================================
        // GET: api/Paises/5
        // Obtener un país específico con sus relaciones
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPais(int id)
        {
            var pais = await _context.Paises
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new
                {
                    id = p.Id,
                    nombre = p.Nombre,

                    destinos = p.Destinos
                        .Select(d => new
                        {
                            id = d.Id,
                            nombre = d.Nombre,
                            descripcion = d.Descripcion,
                            paisId = d.PaisId,

                            tours = d.Tours
                                .Select(t => new
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

                                    categoria = t.Categoria == null
                                        ? null
                                        : new
                                        {
                                            id = t.Categoria.Id,
                                            nombre = t.Categoria.Nombre,
                                            descripcion = t.Categoria.Descripcion
                                        },

                                    guia = t.Guia == null
                                        ? null
                                        : new
                                        {
                                            id = t.Guia.Id,
                                            nombre = t.Guia.Nombre,
                                            apellido = t.Guia.Apellido,
                                            telefono = t.Guia.Telefono,
                                            correo = t.Guia.Correo,
                                            especialidad = t.Guia.Especialidad
                                        },

                                    transporte = t.Transporte == null
                                        ? null
                                        : new
                                        {
                                            id = t.Transporte.Id,
                                            tipo = t.Transporte.Tipo,
                                            descripcion = t.Transporte.Descripcion,
                                            capacidad = t.Transporte.Capacidad
                                        }
                                })
                                .ToList()
                        })
                        .ToList(),

                    tours = p.Tours
                        .Select(t => new
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

                            destino = t.Destino == null
                                ? null
                                : new
                                {
                                    id = t.Destino.Id,
                                    nombre = t.Destino.Nombre,
                                    descripcion = t.Destino.Descripcion
                                },

                            categoria = t.Categoria == null
                                ? null
                                : new
                                {
                                    id = t.Categoria.Id,
                                    nombre = t.Categoria.Nombre,
                                    descripcion = t.Categoria.Descripcion
                                },

                            guia = t.Guia == null
                                ? null
                                : new
                                {
                                    id = t.Guia.Id,
                                    nombre = t.Guia.Nombre,
                                    apellido = t.Guia.Apellido,
                                    telefono = t.Guia.Telefono,
                                    correo = t.Guia.Correo,
                                    especialidad = t.Guia.Especialidad
                                },

                            transporte = t.Transporte == null
                                ? null
                                : new
                                {
                                    id = t.Transporte.Id,
                                    tipo = t.Transporte.Tipo,
                                    descripcion = t.Transporte.Descripcion,
                                    capacidad = t.Transporte.Capacidad
                                }
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (pais == null)
            {
                return NotFound(new
                {
                    mensaje = "El país no existe."
                });
            }

            return Ok(pais);
        }

        // =========================================================
        // POST: api/Paises
        // Crear un país
        // =========================================================
        [HttpPost]
        public async Task<IActionResult> PostPais(Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = await _context.Paises
                .AnyAsync(p => p.Nombre.ToLower() == pais.Nombre.ToLower());

            if (existe)
            {
                return Conflict(new
                {
                    mensaje = "El país ya existe."
                });
            }

            _context.Paises.Add(pais);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPais),
                new { id = pais.Id },
                pais
            );
        }

        // =========================================================
        // PUT: api/Paises/5
        // Actualizar país
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(
            int id,
            Pais pais)
        {
            if (id != pais.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID de la URL no coincide con el ID del país."
                });
            }

            var paisExistente = await _context.Paises
                .FindAsync(id);

            if (paisExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "El país no existe."
                });
            }

            paisExistente.Nombre = pais.Nombre;

            await _context.SaveChangesAsync();

            return Ok(paisExistente);
        }

        // =========================================================
        // DELETE: api/Paises/5
        // Eliminar país
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePais(int id)
        {
            var pais = await _context.Paises
                .FindAsync(id);

            if (pais == null)
            {
                return NotFound(new
                {
                    mensaje = "El país no existe."
                });
            }

            var tieneDestinos = await _context.Destinos
                .AnyAsync(d => d.PaisId == id);

            if (tieneDestinos)
            {
                return Conflict(new
                {
                    mensaje = "No se puede eliminar el país porque tiene destinos asociados."
                });
            }

            var tieneTours = await _context.Tours
                .AnyAsync(t => t.PaisId == id);

            if (tieneTours)
            {
                return Conflict(new
                {
                    mensaje = "No se puede eliminar el país porque tiene tours asociados."
                });
            }

            _context.Paises.Remove(pais);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
