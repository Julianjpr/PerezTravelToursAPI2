using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AgenciaToursContext _context;

        public ClientesController(AgenciaToursContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET: api/Clientes
        // Obtener todos los clientes con sus reservas
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _context.Clientes
                .AsNoTracking()
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    apellido = c.Apellido,
                    correo = c.Correo,
                    telefono = c.Telefono,

                    reservas = c.Reservas
                        .Select(r => new
                        {
                            id = r.Id,
                            clienteId = r.ClienteId,
                            tourId = r.TourId,
                            fechaReserva = r.FechaReserva,
                            cantidadPersonas = r.CantidadPersonas,
                            total = r.Total,
                            estado = r.Estado,
                            metodoPagoId = r.MetodoPagoId
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(clientes);
        }


        // =========================================================
        // GET: api/Clientes/5
        // Obtener un cliente con sus reservas
        // =========================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    apellido = c.Apellido,
                    correo = c.Correo,
                    telefono = c.Telefono,

                    reservas = c.Reservas
                        .Select(r => new
                        {
                            id = r.Id,
                            clienteId = r.ClienteId,
                            tourId = r.TourId,
                            fechaReserva = r.FechaReserva,
                            cantidadPersonas = r.CantidadPersonas,
                            total = r.Total,
                            estado = r.Estado,
                            metodoPagoId = r.MetodoPagoId
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensaje = "El cliente no existe."
                });
            }

            return Ok(cliente);
        }


        // =========================================================
        // POST: api/Clientes
        // Crear un nuevo cliente
        // =========================================================
        [HttpPost]
        public async Task<IActionResult> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = await _context.Clientes
                .AnyAsync(c => c.Correo.ToLower() == cliente.Correo.ToLower());

            if (existe)
            {
                return Conflict(new
                {
                    mensaje = "Ya existe un cliente registrado con ese correo."
                });
            }

            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCliente),
                new { id = cliente.Id },
                cliente
            );
        }


        // =========================================================
        // PUT: api/Clientes/5
        // Actualizar cliente
        // =========================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(
            int id,
            Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest(new
                {
                    mensaje = "El ID del cliente no coincide."
                });
            }

            var clienteExistente = await _context.Clientes
                .FindAsync(id);

            if (clienteExistente == null)
            {
                return NotFound(new
                {
                    mensaje = "El cliente no existe."
                });
            }

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Correo = cliente.Correo;
            clienteExistente.Telefono = cliente.Telefono;

            await _context.SaveChangesAsync();

            return Ok(clienteExistente);
        }


        // =========================================================
        // DELETE: api/Clientes/5
        // Eliminar cliente
        // =========================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes
                .FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new
                {
                    mensaje = "El cliente no existe."
                });
            }

            var tieneReservas = await _context.Reservas
                .AnyAsync(r => r.ClienteId == id);

            if (tieneReservas)
            {
                return Conflict(new
                {
                    mensaje = "No se puede eliminar el cliente porque tiene reservas asociadas."
                });
            }

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}