using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

    public MetodosPagoController(AgenciaToursContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET: api/MetodosPago
        // OBTENER TODOS LOS MÉTODOS DE PAGO CON SUS RESERVAS
        // Y TODAS LAS RELACIONES
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> GetMetodosPago()
        {
            var metodos = await _context.MetodosPago
                .AsNoTracking()
                .Select(m => new
                {
                    id = m.Id,
                    nombre = m.Nombre,
                    descripcion = m.Descripcion,

                    reservas = m.Reservas.Select(r => new
                    {
                        id = r.Id,
                        clienteId = r.ClienteId,
                        tourId = r.TourId,
                        fechaReserva = r.FechaReserva,
                        cantidadPersonas = r.CantidadPersonas,
                        total = r.Total,
                        estado = r.Estado,
                        metodoPagoId = r.MetodoPagoId,

                        // ==================================================
                        // CLIENTE
                        // ==================================================
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

                        // ==================================================
                        // TOUR
                        // ==================================================
                        tour = r.Tour == null
                            ? null
                            : new
                            {
                                id = r.Tour.Id,
                                nombre = r.Tour.Nombre,
                                paisId = r.Tour.PaisId,
                                destinoId = r.Tour.DestinoId,
                                fecha = r.Tour.Fecha,
                                hora = r.Tour.Hora,
                                precio = r.Tour.Precio,
                                duracionDias = r.Tour.DuracionDias,
                                fechaHoraFin = r.Tour.FechaHoraFin,
                                estado = r.Tour.Estado,
                                categoriaId = r.Tour.CategoriaId,
                                guiaId = r.Tour.GuiaId,
                                transporteId = r.Tour.TransporteId,

                                // ==================================================
                                // PAÍS
                                // ==================================================
                                pais = r.Tour.Pais == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Pais.Id,
                                        nombre = r.Tour.Pais.Nombre
                                    },

                                // ==================================================
                                // DESTINO
                                // ==================================================
                                destino = r.Tour.Destino == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Destino.Id,
                                        nombre = r.Tour.Destino.Nombre,
                                        descripcion = r.Tour.Destino.Descripcion,
                                        paisId = r.Tour.Destino.PaisId
                                    },

                                // ==================================================
                                // CATEGORÍA
                                // ==================================================
                                categoria = r.Tour.Categoria == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Categoria.Id,
                                        nombre = r.Tour.Categoria.Nombre,
                                        descripcion = r.Tour.Categoria.Descripcion
                                    },

                                // ==================================================
                                // GUÍA
                                // ==================================================
                                guia = r.Tour.Guia == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Guia.Id,
                                        nombre = r.Tour.Guia.Nombre,
                                        apellido = r.Tour.Guia.Apellido,
                                        telefono = r.Tour.Guia.Telefono,
                                        correo = r.Tour.Guia.Correo,
                                        especialidad = r.Tour.Guia.Especialidad
                                    },

                                // ==================================================
                                // TRANSPORTE
                                // ==================================================
                                transporte = r.Tour.Transporte == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Transporte.Id,
                                        tipo = r.Tour.Transporte.Tipo,
                                        descripcion = r.Tour.Transporte.Descripcion,
                                        capacidad = r.Tour.Transporte.Capacidad
                                    }
                            }
                    })
                })
                .ToListAsync();

            return Ok(metodos);
        }


        // ============================================================
        // GET: api/MetodosPago/{id}
        // OBTENER UN MÉTODO DE PAGO POR ID
        // CON TODAS SUS RELACIONES
        // ============================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMetodoPago(int id)
        {
            var metodo = await _context.MetodosPago
                .AsNoTracking()
                .Where(m => m.Id == id)
                .Select(m => new
                {
                    id = m.Id,
                    nombre = m.Nombre,
                    descripcion = m.Descripcion,

                    reservas = m.Reservas.Select(r => new
                    {
                        id = r.Id,
                        clienteId = r.ClienteId,
                        tourId = r.TourId,
                        fechaReserva = r.FechaReserva,
                        cantidadPersonas = r.CantidadPersonas,
                        total = r.Total,
                        estado = r.Estado,
                        metodoPagoId = r.MetodoPagoId,

                        // ==================================================
                        // CLIENTE
                        // ==================================================
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

                        // ==================================================
                        // TOUR
                        // ==================================================
                        tour = r.Tour == null
                            ? null
                            : new
                            {
                                id = r.Tour.Id,
                                nombre = r.Tour.Nombre,
                                paisId = r.Tour.PaisId,
                                destinoId = r.Tour.DestinoId,
                                fecha = r.Tour.Fecha,
                                hora = r.Tour.Hora,
                                precio = r.Tour.Precio,
                                duracionDias = r.Tour.DuracionDias,
                                fechaHoraFin = r.Tour.FechaHoraFin,
                                estado = r.Tour.Estado,
                                categoriaId = r.Tour.CategoriaId,
                                guiaId = r.Tour.GuiaId,
                                transporteId = r.Tour.TransporteId,

                                // ==================================================
                                // PAÍS
                                // ==================================================
                                pais = r.Tour.Pais == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Pais.Id,
                                        nombre = r.Tour.Pais.Nombre
                                    },

                                // ==================================================
                                // DESTINO
                                // ==================================================
                                destino = r.Tour.Destino == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Destino.Id,
                                        nombre = r.Tour.Destino.Nombre,
                                        descripcion = r.Tour.Destino.Descripcion,
                                        paisId = r.Tour.Destino.PaisId
                                    },

                                // ==================================================
                                // CATEGORÍA
                                // ==================================================
                                categoria = r.Tour.Categoria == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Categoria.Id,
                                        nombre = r.Tour.Categoria.Nombre,
                                        descripcion = r.Tour.Categoria.Descripcion
                                    },

                                // ==================================================
                                // GUÍA
                                // ==================================================
                                guia = r.Tour.Guia == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Guia.Id,
                                        nombre = r.Tour.Guia.Nombre,
                                        apellido = r.Tour.Guia.Apellido,
                                        telefono = r.Tour.Guia.Telefono,
                                        correo = r.Tour.Guia.Correo,
                                        especialidad = r.Tour.Guia.Especialidad
                                    },

                                // ==================================================
                                // TRANSPORTE
                                // ==================================================
                                transporte = r.Tour.Transporte == null
                                    ? null
                                    : new
                                    {
                                        id = r.Tour.Transporte.Id,
                                        tipo = r.Tour.Transporte.Tipo,
                                        descripcion = r.Tour.Transporte.Descripcion,
                                        capacidad = r.Tour.Transporte.Capacidad
                                    }
                            }
                    })
                })
                .FirstOrDefaultAsync();

            if (metodo == null)
            {
                return NotFound(new
                {
                    mensaje = "El método de pago no existe."
                });
            }

            return Ok(metodo);
        }


        // ============================================================
        // POST: api/MetodosPago
        // CREAR MÉTODO DE PAGO
        // ============================================================
        [HttpPost]
        public async Task<ActionResult<MetodoPago>> PostMetodoPago(
            [FromBody] MetodoPago metodoPago)
        {
            if (metodoPago == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del método de pago."
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MetodosPago.Add(metodoPago);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMetodoPago),
                new { id = metodoPago.Id },
                metodoPago
            );
        }


        // ============================================================
        // PUT: api/MetodosPago/{id}
        // ACTUALIZAR MÉTODO DE PAGO
        // ============================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetodoPago(
            int id,
            [FromBody] MetodoPago metodoPago)
        {
            if (metodoPago == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del método de pago."
                });
            }

            if (id != metodoPago.Id)
            {
                return BadRequest(new
                {
                    mensaje =
                        "El ID de la URL no coincide con el ID del método de pago."
                });
            }

            var metodoExistente =
                await _context.MetodosPago.FindAsync(id);

            if (metodoExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "El método de pago no existe."
                });
            }

            metodoExistente.Nombre = metodoPago.Nombre;
            metodoExistente.Descripcion = metodoPago.Descripcion;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje =
                    "Método de pago actualizado correctamente.",
                id = metodoExistente.Id,
                nombre = metodoExistente.Nombre,
                descripcion = metodoExistente.Descripcion
            });
        }


        // ============================================================
        // DELETE: api/MetodosPago/{id}
        // ELIMINAR MÉTODO DE PAGO
        // ============================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetodoPago(int id)
        {
            var metodo =
                await _context.MetodosPago.FindAsync(id);

            if (metodo == null)
            {
                return NotFound(new
                {
                    mensaje = "El método de pago no existe."
                });
            }

            var tieneReservas =
                await _context.Reservas
                    .AnyAsync(r => r.MetodoPagoId == id);

            if (tieneReservas)
            {
                return Conflict(new
                {
                    mensaje =
                        "No se puede eliminar el método de pago porque tiene reservas asociadas."
                });
            }

            _context.MetodosPago.Remove(metodo);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensaje =
                    "Método de pago eliminado correctamente."
            });
        }
    }

}

