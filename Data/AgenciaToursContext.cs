using Microsoft.EntityFrameworkCore;
using PerezTravelToursAPI.Models;

namespace PerezTravelToursAPI.Data
{
    public class AgenciaToursContext : DbContext
    {
        public AgenciaToursContext(
            DbContextOptions<AgenciaToursContext> options)
            : base(options)
        {
        }

        // =====================================================
        // TABLAS
        // =====================================================

        public DbSet<Pais> Paises { get; set; }

        public DbSet<Destino> Destinos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<GuiaTuristico> GuiasTuristicos { get; set; }

        public DbSet<Transporte> Transportes { get; set; }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<MetodoPago> MetodosPago { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // =====================================================
            // USUARIO
            // Tabla: Usuarios
            // =====================================================

            modelBuilder.Entity<Usuario>(entity =>
            {
                // Nombre de la tabla
                entity.ToTable("Usuarios");

                // Clave primaria
                entity.HasKey(u => u.Id);

                // Id autoincremental IDENTITY
                entity.Property(u => u.Id)
                    .ValueGeneratedOnAdd();

                // Campos obligatorios
                entity.Property(u => u.Nombre)
                    .IsRequired();

                entity.Property(u => u.Apellido)
                    .IsRequired();

                entity.Property(u => u.Correo)
                    .IsRequired();

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.Activo)
                    .IsRequired();

                entity.Property(u => u.FechaRegistro)
                    .IsRequired();

                entity.Property(u => u.RolId)
                    .IsRequired();
            });


            // =====================================================
            // PAÍS - DESTINO
            // =====================================================

            modelBuilder.Entity<Destino>()
                .HasOne(d => d.Pais)
                .WithMany(p => p.Destinos)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // PAÍS - TOUR
            // =====================================================

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Pais)
                .WithMany(p => p.Tours)
                .HasForeignKey(t => t.PaisId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // DESTINO - TOUR
            // =====================================================

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Destino)
                .WithMany(d => d.Tours)
                .HasForeignKey(t => t.DestinoId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // CATEGORÍA - TOUR
            // =====================================================

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tours)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // GUÍA - TOUR
            // =====================================================

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Guia)
                .WithMany(g => g.Tours)
                .HasForeignKey(t => t.GuiaId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // TRANSPORTE - TOUR
            // =====================================================

            modelBuilder.Entity<Tour>()
                .HasOne(t => t.Transporte)
                .WithMany(t => t.Tours)
                .HasForeignKey(t => t.TransporteId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // CLIENTE - RESERVA
            // =====================================================

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // TOUR - RESERVA
            // =====================================================

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Tour)
                .WithMany(t => t.Reservas)
                .HasForeignKey(r => r.TourId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // MÉTODO DE PAGO - RESERVA
            // =====================================================

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.MetodoPago)
                .WithMany(m => m.Reservas)
                .HasForeignKey(r => r.MetodoPagoId)
                .OnDelete(DeleteBehavior.Restrict);


            // =====================================================
            // CONFIGURACIÓN DE DECIMALES
            // =====================================================

            modelBuilder.Entity<Tour>()
                .Property(t => t.Precio)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tour>()
                .Property(t => t.ITBIS)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Reserva>()
                .Property(r => r.Total)
                .HasColumnType("decimal(18,2)");
        }
    }
}