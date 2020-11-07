using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurante.Models.DB
{
    public partial class RestaurantesContext : DbContext
    {
        public RestaurantesContext()
        {
        }

        public RestaurantesContext(DbContextOptions<RestaurantesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ItemDestacado> ItemDestacado { get; set; }
        public virtual DbSet<Restaurante> Restaurante { get; set; }
        public virtual DbSet<RestauranteVotaciones> RestauranteVotaciones { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Restaurantes;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemDestacado>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.ItemDestacado)
                    .HasForeignKey(d => d.RestauranteId)
                    .HasConstraintName("FK__ItemDesta__Resta__182C9B23");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.Property(e => e.HoraApertura)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoraCierre)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NombreRestaurante)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RestauranteVotaciones>(entity =>
            {
                entity.HasKey(e => e.VotoId)
                    .HasName("PK__Restaura__5D77CA926BA22FDE");

                entity.Property(e => e.FechaVoto)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.RestauranteVotaciones)
                    .HasForeignKey(d => d.RestauranteId)
                    .HasConstraintName("FK__Restauran__Resta__15502E78");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
