using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stocks.Data.Models
{
    public partial class StocksContext : DbContext
    {
        public StocksContext()
        {
        }

        public StocksContext(DbContextOptions<StocksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dionicar> Dionicar { get; set; }
        public virtual DbSet<DionicarDrustvo> DionicarDrustvo { get; set; }
        public virtual DbSet<Drustvo> Drustvo { get; set; }
        public virtual DbSet<PovijestTransakcija> PovijestTransakcija { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Stocks;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dionicar>(entity =>
            {
                entity.HasKey(e => e.Rbr);

                entity.Property(e => e.Rbr).HasColumnName("rbr");

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DionicarDrustvo>(entity =>
            {
                entity.HasKey(e => new { e.RbrDionicar, e.OznakaDrustvo, e.Datum });

                entity.Property(e => e.RbrDionicar).HasColumnName("rbrDionicar");

                entity.Property(e => e.OznakaDrustvo)
                    .HasColumnName("oznakaDrustvo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.BrojDionica).HasColumnName("brojDionica");

                entity.HasOne(d => d.OznakaDrustvoNavigation)
                    .WithMany(p => p.DionicarDrustvo)
                    .HasForeignKey(d => d.OznakaDrustvo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DionicarD__oznak__2D27B809");

                entity.HasOne(d => d.RbrDionicarNavigation)
                    .WithMany(p => p.DionicarDrustvo)
                    .HasForeignKey(d => d.RbrDionicar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DionicarD__rbrDi__2C3393D0");
            });

            modelBuilder.Entity<Drustvo>(entity =>
            {
                entity.HasKey(e => e.Oznaka);

                entity.Property(e => e.Oznaka)
                    .HasColumnName("oznaka")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.MojedioniceUrl)
                    .HasColumnName("mojedioniceUrl")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Naziv)
                    .HasColumnName("naziv")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PovijestTransakcija>(entity =>
            {
                entity.HasKey(e => new { e.Datum, e.OznakaDrustvo });

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("datetime");

                entity.Property(e => e.OznakaDrustvo)
                    .HasColumnName("oznakaDrustvo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.Kupnja)
                    .HasColumnName("kupnja")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Najniza)
                    .HasColumnName("najniza")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Najvisa)
                    .HasColumnName("najvisa")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Prodaja)
                    .HasColumnName("prodaja")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Promet).HasColumnName("promet");

                entity.Property(e => e.Prosijek)
                    .HasColumnName("prosijek")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Prva)
                    .HasColumnName("prva")
                    .HasColumnType("decimal(2, 0)");

                entity.Property(e => e.Zadnja)
                    .HasColumnName("zadnja")
                    .HasColumnType("decimal(2, 0)");

                entity.HasOne(d => d.OznakaDrustvoNavigation)
                    .WithMany(p => p.PovijestTransakcija)
                    .HasForeignKey(d => d.OznakaDrustvo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PovijestT__oznak__25869641");
            });
        }
    }
}
