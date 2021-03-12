using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EchoBot.Models;

#nullable disable

namespace EchoBot.Data
{
    public partial class CovidContext : DbContext
    {
        public CovidContext()
        {
        }

        public CovidContext(DbContextOptions<CovidContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ComunaCl> ComunaCls { get; set; }
        public virtual DbSet<EstadoComuna> EstadoComunas { get; set; }
        public virtual DbSet<Fase> Fases { get; set; }
        public virtual DbSet<ProvinciaCl> ProvinciaCls { get; set; }
        public virtual DbSet<RegionCl> RegionCls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=demosso.database.windows.net;Initial Catalog=Covid;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;User Id=demoadmin;Password=DemoSSO00");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ComunaCl>(entity =>
            {
                entity.Property(e => e.StrDescripcion).IsUnicode(false);

                entity.HasOne(d => d.IdPrNavigation)
                    .WithMany(p => p.ComunaCls)
                    .HasForeignKey(d => d.IdPr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comuna_cl_provincia_cl");

                entity.HasOne(d => d.IdRegionNavigation)
                    .WithMany(p => p.ComunaCls)
                    .HasForeignKey(d => d.IdRegion)
                    .HasConstraintName("FK_comuna_cl_region_cl");
            });

            modelBuilder.Entity<EstadoComuna>(entity =>
            {
                entity.Property(e => e.FechaActualización).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdComunaNavigation)
                    .WithMany(p => p.EstadoComunas)
                    .HasForeignKey(d => d.IdComuna)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estado_Comuna_Estado_Comuna");

                entity.HasOne(d => d.IdFaseNavigation)
                    .WithMany(p => p.EstadoComunas)
                    .HasForeignKey(d => d.IdFase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estado_Comuna_Fases");
            });

            modelBuilder.Entity<Fase>(entity =>
            {
                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<ProvinciaCl>(entity =>
            {
                entity.HasKey(e => e.IdPr)
                    .HasName("PK__provinci__0148A34E931746B5");

                entity.Property(e => e.IdPr).ValueGeneratedNever();

                entity.Property(e => e.StrDescripcion).IsUnicode(false);
            });

            modelBuilder.Entity<RegionCl>(entity =>
            {
                entity.HasKey(e => e.IdRe)
                    .HasName("PK__region_c__01485339AEEA80D7");

                entity.Property(e => e.IdRe).ValueGeneratedNever();

                entity.Property(e => e.StrDescripcion).IsUnicode(false);

                entity.Property(e => e.StrRomano).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
