using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PerezTravelToursAPI.Data;
using PerezTravelToursAPI.Middleware;
using PerezTravelToursAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// CONEXIÓN A BASE DE DATOS SQL SERVER
// ==========================================
builder.Services.AddDbContext<AgenciaToursContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ==========================================
// CONTROLADORES
// ==========================================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita ciclos de referencias (País -> Tours -> Reservas -> Tour...)
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        // Formato JSON legible
        options.JsonSerializerOptions.WriteIndented = true;
    });

// ==========================================
// SERVICIOS DE LA APLICACIÓN
// ==========================================
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<TourService>();

// ==========================================
// AUTENTICACIÓN JWT
// ==========================================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,

            ValidateAudience = true,

            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,

            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],

            ValidAudience =
                builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!
                    )
                ),

            ClockSkew = TimeSpan.Zero
        };
});

// ==========================================
// AUTORIZACIÓN
// ==========================================
builder.Services.AddAuthorization();

// ==========================================
// CORS
// ==========================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ==========================================
// SWAGGER
// ==========================================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pérez Travel Tours API",
        Version = "v1",
        Description = "API REST para la gestión de Pérez Travel Tours"
    });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese el token en el formato: Bearer {token}"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

// ==========================================
// CONSTRUIR LA APLICACIÓN
// ==========================================
var app = builder.Build();

// ==========================================
// MIDDLEWARE DE EXCEPCIONES
// ==========================================
app.UseMiddleware<ExceptionMiddleware>();

// ==========================================
// SWAGGER
// ==========================================
app.UseSwagger();
app.UseSwaggerUI();

// ==========================================
// HTTPS
// ==========================================
app.UseHttpsRedirection();

// ==========================================
// CORS
// ==========================================
app.UseCors("AllowAll");

// ==========================================
// AUTENTICACIÓN
// ==========================================
app.UseAuthentication();

// ==========================================
// AUTORIZACIÓN
// ==========================================
app.UseAuthorization();

// ==========================================
// MAPEAR CONTROLADORES
// ==========================================
app.MapControllers();

// ==========================================
// EJECUTAR
// ==========================================
app.Run();