
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

        public ToursController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Tours
        // Obtener todos los tours con sus relaciones
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            var tours = await _context.Tours
                .AsNoTracking()
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
                    categoriaId = t.CategoriaId,
                    guiaId = t.GuiaId,
                    transporteId = t.TransporteId,

                    // PAÍS
                    pais = t.Pais == null
                        ? null
                        : new
                        {
                            id = t.Pais.Id,
                            nombre = t.Pais.Nombre
                        },

                    // DESTINO
                    destino = t.Destino == null
                        ? null
                        : new
                        {
                            id = t.Destino.Id,
                            nombre = t.Destino.Nombre,
                            descripcion = t.Destino.Descripcion,
                            paisId = t.Destino.PaisId
                        },

                    // CATEGORÍA
                    categoria = t.Categoria == null
                        ? null
                        : new
                        {
                            id = t.Categoria.Id,
                            nombre = t.Categoria.Nombre,
                            descripcion = t.Categoria.Descripcion
                        },

                    // GUÍA
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

                    // TRANSPORTE
                    transporte = t.Transporte == null
                        ? null
                        : new
                        {
                            id = t.Transporte.Id,
                            tipo = t.Transporte.Tipo,
                            descripcion = t.Transporte.Descripcion,
                            capacidad = t.Transporte.Capacidad
                        },

                    // RESERVAS
                    reservas = t.Reservas
                        .Select(r => new
                        {
                            id = r.Id,
                            clienteId = r.ClienteId,
                            tourId = r.TourId,
                            fechaReserva = r.FechaReserva,
                            cantidadPersonas = r.CantidadPersonas,
                            total = r.Total,
                            estado = r.Estado,
                            metodoPagoId = r.MetodoPagoId,

                            cliente = r.Cliente == null
                                ? null
                                : new
                                {
                                    id = r.Cliente.Id,
                                    nombre = r.Cliente.Nombre,
                                    apellido = r.Cliente.Apellido,
                                    correo = r.Cliente.Correo,
                                    telefono = r.Cliente.Telefono
                                },

                            metodoPago = r.MetodoPago == null
                                ? null
                                : new
                                {
                                    id = r.MetodoPago.Id,
                                    nombre = r.MetodoPago.Nombre,
                                    descripcion = r.MetodoPago.Descripcion
                                }
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(tours);
        }


        // =========================================================
        // GET: api/Tours/5
        // Obtener un tour por ID con sus relaciones
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTour(int id)
        {
            var tour = await _context.Tours
                .AsNoTracking()
                .Where(t => t.Id == id)
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
                    categoriaId = t.CategoriaId,
                    guiaId = t.GuiaId,
                    transporteId = t.TransporteId,

                    pais = t.Pais == null
                        ? null
                        : new
                        {
                            id = t.Pais.Id,
                            nombre = t.Pais.Nombre
                        },

                    destino = t.Destino == null
                        ? null
                        : new
                        {
                            id = t.Destino.Id,
                            nombre = t.Destino.Nombre,
                            descripcion = t.Destino.Descripcion,
                            paisId = t.Destino.PaisId
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
                        },

                    reservas = t.Reservas
                        .Select(r => new
                        {
                            id = r.Id,
                            clienteId = r.ClienteId,
                            tourId = r.TourId,
                            fechaReserva = r.FechaReserva,
                            cantidadPersonas = r.CantidadPersonas,
                            total = r.Total,
                            estado = r.Estado,
                            metodoPagoId = r.MetodoPagoId,

                            cliente = r.Cliente == null
                                ? null
                                : new
                                {
                                    id = r.Cliente.Id,
                                    nombre = r.Cliente.Nombre,
                                    apellido = r.Cliente.Apellido,
                                    correo = r.Cliente.Correo,
                                    telefono = r.Cliente.Telefono
                                },

                            metodoPago = r.MetodoPago == null
                                ? null
                                : new
                                {
                                    id = r.MetodoPago.Id,
                                    nombre = r.MetodoPago.Nombre,
                                    descripcion = r.MetodoPago.Descripcion
                                }
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                return NotFound(new
                {
                    mensaje = "El tour no existe."
                });
            }

            return Ok(tour);
        }


        // =========================================================
        // POST: api/Tours
        // Crear un nuevo tour
        // =========================================================
        [HttpPost]
        public async Task<ActionResult<Tour>> PostTour(Tour tour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paisExiste = await _context.Paises
                .AnyAsync(p => p.Id == tour.PaisId);

            if (!paisExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El país indicado no existe."
                });
            }

            var destinoExiste = await _context.Destinos
                .AnyAsync(d => d.Id == tour.DestinoId);

            if (!destinoExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El destino indicado no existe."
                });
            }

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == tour.CategoriaId);

            if (!categoriaExiste)
            {
                return BadRequest(new
                {
                    mensaje = "La categoría indicada no existe."
                });
            }

            var guiaExiste = await _context.GuiasTuristicos
                .AnyAsync(g => g.Id == tour.GuiaId);

            if (!guiaExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El guía turístico indicado no existe."
                });
            }

            var transporteExiste = await _context.Transportes
                .AnyAsync(t => t.Id == tour.TransporteId);

            if (!transporteExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El transporte indicado no existe."
                });
            }

            _context.Tours.Add(tour);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTour),
                new { id = tour.Id },
                tour
            );
        }


        // =========================================================
        // PUT: api/Tours/5
        // Actualizar un tour
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTour(
            int id,
            Tour tour)
        {
            if (id != tour.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID del tour no coincide."
                });
            }

            var tourExistente = await _context.Tours
                .FindAsync(id);

            if (tourExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "El tour no existe."
                });
            }

            var paisExiste = await _context.Paises
                .AnyAsync(p => p.Id == tour.PaisId);

            if (!paisExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El país indicado no existe."
                });
            }

            var destinoExiste = await _context.Destinos
                .AnyAsync(d => d.Id == tour.DestinoId);

            if (!destinoExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El destino indicado no existe."
                });
            }

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == tour.CategoriaId);

            if (!categoriaExiste)
            {
                return BadRequest(new
                {
                    mensaje = "La categoría indicada no existe."
                });
            }

            var guiaExiste = await _context.GuiasTuristicos
                .AnyAsync(g => g.Id == tour.GuiaId);

            if (!guiaExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El guía turístico indicado no existe."
                });
            }

            var transporteExiste = await _context.Transportes
                .AnyAsync(t => t.Id == tour.TransporteId);

            if (!transporteExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El transporte indicado no existe."
                });
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

            return NoContent();
        }


        // =========================================================
        // DELETE: api/Tours/5
        // Eliminar un tour
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            var tour = await _context.Tours
                .FindAsync(id);

            if (tour == null)
            {
                return NotFound(new
                {
                    mensaje = "El tour no existe."
                });
            }

            var tieneReservas = await _context.Reservas
                .AnyAsync(r => r.TourId == id);

            if (tieneReservas)
            {
                return Conflict(new
                {
                    mensaje = "No se puede eliminar el tour porque tiene reservas asociadas."
                });
            }

            _context.Tours.Remove(tour);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}