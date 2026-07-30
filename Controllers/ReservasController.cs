
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

        public ReservasController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Reservas
        // Obtener todas las reservas con sus relaciones
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetReservas()
        {
            var reservas = await _context.Reservas
                .AsNoTracking()
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

                    // CLIENTE
                    cliente = r.Cliente == null ? null : new
                    {
                        id = r.Cliente.Id,
                        nombre = r.Cliente.Nombre,
                        apellido = r.Cliente.Apellido,
                        correo = r.Cliente.Correo,
                        telefono = r.Cliente.Telefono
                    },

                    // TOUR
                    tour = r.Tour == null ? null : new
                    {
                        id = r.Tour.Id,
                        nombre = r.Tour.Nombre,

                        pais = r.Tour.Pais == null ? null : new
                        {
                            id = r.Tour.Pais.Id,
                            nombre = r.Tour.Pais.Nombre
                        },

                        destino = r.Tour.Destino == null ? null : new
                        {
                            id = r.Tour.Destino.Id,
                            nombre = r.Tour.Destino.Nombre,
                            descripcion = r.Tour.Destino.Descripcion
                        },

                        categoria = r.Tour.Categoria == null ? null : new
                        {
                            id = r.Tour.Categoria.Id,
                            nombre = r.Tour.Categoria.Nombre,
                            descripcion = r.Tour.Categoria.Descripcion
                        },

                        guia = r.Tour.Guia == null ? null : new
                        {
                            id = r.Tour.Guia.Id,
                            nombre = r.Tour.Guia.Nombre,
                            apellido = r.Tour.Guia.Apellido,
                            telefono = r.Tour.Guia.Telefono,
                            correo = r.Tour.Guia.Correo,
                            especialidad = r.Tour.Guia.Especialidad
                        },

                        transporte = r.Tour.Transporte == null ? null : new
                        {
                            id = r.Tour.Transporte.Id,
                            tipo = r.Tour.Transporte.Tipo,
                            descripcion = r.Tour.Transporte.Descripcion,
                            capacidad = r.Tour.Transporte.Capacidad
                        },

                        fecha = r.Tour.Fecha,
                        hora = r.Tour.Hora,
                        precio = r.Tour.Precio,
                        itbis = r.Tour.ITBIS,
                        duracionDias = r.Tour.DuracionDias,
                        fechaHoraFin = r.Tour.FechaHoraFin,
                        estado = r.Tour.Estado
                    },

                    // MÉTODO DE PAGO
                    metodoPago = r.MetodoPago == null ? null : new
                    {
                        id = r.MetodoPago.Id,
                        nombre = r.MetodoPago.Nombre,
                        descripcion = r.MetodoPago.Descripcion
                    }
                })
                .ToListAsync();

            return Ok(reservas);
        }


        // =========================================================
        // GET: api/Reservas/5
        // Obtener una reserva por ID con sus relaciones
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .AsNoTracking()
                .Where(r => r.Id == id)
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

                    cliente = r.Cliente == null ? null : new
                    {
                        id = r.Cliente.Id,
                        nombre = r.Cliente.Nombre,
                        apellido = r.Cliente.Apellido,
                        correo = r.Cliente.Correo,
                        telefono = r.Cliente.Telefono
                    },

                    tour = r.Tour == null ? null : new
                    {
                        id = r.Tour.Id,
                        nombre = r.Tour.Nombre,

                        pais = r.Tour.Pais == null ? null : new
                        {
                            id = r.Tour.Pais.Id,
                            nombre = r.Tour.Pais.Nombre
                        },

                        destino = r.Tour.Destino == null ? null : new
                        {
                            id = r.Tour.Destino.Id,
                            nombre = r.Tour.Destino.Nombre,
                            descripcion = r.Tour.Destino.Descripcion
                        },

                        categoria = r.Tour.Categoria == null ? null : new
                        {
                            id = r.Tour.Categoria.Id,
                            nombre = r.Tour.Categoria.Nombre,
                            descripcion = r.Tour.Categoria.Descripcion
                        },

                        guia = r.Tour.Guia == null ? null : new
                        {
                            id = r.Tour.Guia.Id,
                            nombre = r.Tour.Guia.Nombre,
                            apellido = r.Tour.Guia.Apellido,
                            telefono = r.Tour.Guia.Telefono,
                            correo = r.Tour.Guia.Correo,
                            especialidad = r.Tour.Guia.Especialidad
                        },

                        transporte = r.Tour.Transporte == null ? null : new
                        {
                            id = r.Tour.Transporte.Id,
                            tipo = r.Tour.Transporte.Tipo,
                            descripcion = r.Tour.Transporte.Descripcion,
                            capacidad = r.Tour.Transporte.Capacidad
                        },

                        fecha = r.Tour.Fecha,
                        hora = r.Tour.Hora,
                        precio = r.Tour.Precio,
                        itbis = r.Tour.ITBIS,
                        duracionDias = r.Tour.DuracionDias,
                        fechaHoraFin = r.Tour.FechaHoraFin,
                        estado = r.Tour.Estado
                    },

                    metodoPago = r.MetodoPago == null ? null : new
                    {
                        id = r.MetodoPago.Id,
                        nombre = r.MetodoPago.Nombre,
                        descripcion = r.MetodoPago.Descripcion
                    }
                })
                .FirstOrDefaultAsync();

            if (reserva == null)
            {
                return NotFound(new
                {
                    mensaje = "La reserva no existe."
                });
            }

            return Ok(reserva);
        }


        // =========================================================
        // POST: api/Reservas
        // Crear una nueva reserva
        // =========================================================
        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == reserva.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El cliente indicado no existe."
                });
            }

            var tourExiste = await _context.Tours
                .AnyAsync(t => t.Id == reserva.TourId);

            if (!tourExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El tour indicado no existe."
                });
            }

            var metodoPagoExiste = await _context.MetodosPago
                .AnyAsync(m => m.Id == reserva.MetodoPagoId);

            if (!metodoPagoExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El método de pago indicado no existe."
                });
            }

            reserva.FechaReserva = DateTime.Now;

            _context.Reservas.Add(reserva);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetReserva),
                new { id = reserva.Id },
                reserva
            );
        }


        // =========================================================
        // PUT: api/Reservas/5
        // Actualizar una reserva
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(
            int id,
            Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID de la reserva no coincide."
                });
            }

            var reservaExistente = await _context.Reservas
                .FindAsync(id);

            if (reservaExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "La reserva no existe."
                });
            }

            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == reserva.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El cliente indicado no existe."
                });
            }

            var tourExiste = await _context.Tours
                .AnyAsync(t => t.Id == reserva.TourId);

            if (!tourExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El tour indicado no existe."
                });
            }

            var metodoPagoExiste = await _context.MetodosPago
                .AnyAsync(m => m.Id == reserva.MetodoPagoId);

            if (!metodoPagoExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El método de pago indicado no existe."
                });
            }

            reservaExistente.ClienteId = reserva.ClienteId;
            reservaExistente.TourId = reserva.TourId;
            reservaExistente.CantidadPersonas = reserva.CantidadPersonas;
            reservaExistente.Total = reserva.Total;
            reservaExistente.Estado = reserva.Estado;
            reservaExistente.MetodoPagoId = reserva.MetodoPagoId;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // =========================================================
        // DELETE: api/Reservas/5
        // Eliminar una reserva
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas
                .FindAsync(id);

            if (reserva == null)
            {
                return NotFound(new
                {
                    mensaje = "La reserva no existe."
                });
            }

            _context.Reservas.Remove(reserva);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}