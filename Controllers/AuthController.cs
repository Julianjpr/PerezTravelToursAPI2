using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.DTOs.Auth;
using PerezTravelToursAPI.Services;

namespace PerezTravelToursAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AgenciaToursContext _context;
        private readonly JwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthController(
            AgenciaToursContext context,
            JwtService jwtService,
            IConfiguration configuration)
        {
            _context = context;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        // ==========================================
        // LOGIN PÚBLICO
        // POST: api/Auth/login
        // ==========================================
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del usuario."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Correo) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    mensaje = "El correo y la contraseña son obligatorios."
                });
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.Correo == request.Correo);

            if (usuario == null)
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }

            if (!usuario.Activo)
            {
                return Unauthorized(new
                {
                    mensaje = "El usuario se encuentra inactivo."
                });
            }

            if (!BCrypt.Net.BCrypt.Verify(
                    request.Password,
                    usuario.PasswordHash))
            {
                return Unauthorized(new
                {
                    mensaje = "Correo o contraseña incorrectos."
                });
            }

            var token = _jwtService.GenerarToken(usuario);

            var duracion = _configuration
                .GetValue<int>("Jwt:DurationInMinutes");

            return Ok(new LoginResponse
            {
                Token = token,

                Expiracion = DateTime.UtcNow
                    .AddMinutes(duracion),

                Correo = usuario.Correo
            });
        }


        // ==========================================
        // REGISTRO PÚBLICO
        // POST: api/Auth/register
        // ==========================================
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterRequest request)
        {
            // ==========================================
            // VALIDAR REQUEST
            // ==========================================

            if (request == null)
            {
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del usuario."
                });
            }

            // ==========================================
            // VALIDAR CAMPOS
            // ==========================================

            if (string.IsNullOrWhiteSpace(request.Nombre) ||
                string.IsNullOrWhiteSpace(request.Apellido) ||
                string.IsNullOrWhiteSpace(request.Correo) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    mensaje = "Nombre, apellido, correo y contraseña son obligatorios."
                });
            }

            // ==========================================
            // VERIFICAR CORREO EXISTENTE
            // ==========================================

            var usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo == request.Correo);

            if (usuarioExiste)
            {
                return Conflict(new
                {
                    mensaje = "Ya existe un usuario registrado con ese correo."
                });
            }

            // ==========================================
            // CREAR USUARIO
            // ==========================================

            var usuario = new Models.Usuario
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Correo = request.Correo,

                // La contraseña se convierte en HASH
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                    request.Password),

                Telefono = request.Telefono,

                Activo = true,

                FechaRegistro = DateTime.UtcNow,

                RolId = request.RolId
            };

            // ==========================================
            // GUARDAR USUARIO
            // ==========================================

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            // ==========================================
            // RESPUESTA
            // ==========================================

            return Ok(new
            {
                mensaje = "Usuario registrado correctamente.",

                usuario.Id,

                usuario.Nombre,

                usuario.Apellido,

                usuario.Correo,

                usuario.RolId
            });
        }
    }
}