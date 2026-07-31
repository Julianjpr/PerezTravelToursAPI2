using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // =====================================================
        // GENERAR TOKEN JWT
        // =====================================================
        public string GenerarToken(Usuario usuario)
        {
            // =====================================================
            // OBTENER CONFIGURACIÓN JWT
            // =====================================================

            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException(
                    "La clave JWT (Jwt:Key) no está configurada en appsettings.json."
                );
            }

            if (string.IsNullOrWhiteSpace(issuer))
            {
                throw new InvalidOperationException(
                    "El Issuer JWT (Jwt:Issuer) no está configurado en appsettings.json."
                );
            }

            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException(
                    "El Audience JWT (Jwt:Audience) no está configurado en appsettings.json."
                );
            }

            // =====================================================
            // DURACIÓN DEL TOKEN
            // =====================================================

            var durationText =
                _configuration["Jwt:DurationInMinutes"];

            if (!double.TryParse(
                durationText,
                out double duration))
            {
                duration = 60;
            }

            var expiration =
                DateTime.UtcNow.AddMinutes(duration);

            // =====================================================
            // CLAIMS MÍNIMOS
            // Solo ID y ROL para reducir el tamaño del JWT
            // =====================================================

            var claims = new List<Claim>
        {
            // ID DEL USUARIO
            new Claim(
                ClaimTypes.NameIdentifier,
                usuario.Id.ToString()
            ),

            // ROL DEL USUARIO
            new Claim(
                ClaimTypes.Role,
                usuario.RolId.ToString()
            )
        };

            // =====================================================
            // CREAR CLAVE DE SEGURIDAD
            // =====================================================

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key)
                );

            // =====================================================
            // CREDENCIALES DE FIRMA
            // =====================================================

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256
                );

            // =====================================================
            // CREAR TOKEN JWT
            // =====================================================

            var token =
                new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: expiration,
                    signingCredentials: credentials
                );

            // =====================================================
            // CONVERTIR TOKEN A STRING
            // =====================================================

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
