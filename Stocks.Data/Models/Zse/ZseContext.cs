using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stocks.Data.Models.Zse
{
    public partial class ZseContext : DbContext
    {
        public ZseContext()
        {
        }

        public ZseContext(DbContextOptions<ZseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dionicar> Dionicar { get; set; }
        public virtual DbSet<DionicarDionickoDrustvo> DionicarDionickoDrustvo { get; set; }
        public virtual DbSet<DionickoDrustvo> DionickoDrustvo { get; set; }
        public virtual DbSet<DionickoDrustvoDjelatnost> DionickoDrustvoDjelatnost { get; set; }
        public virtual DbSet<DionickoDrustvoKategorijaLikvidnosti> DionickoDrustvoKategorijaLikvidnosti { get; set; }
        public virtual DbSet<Djelatnost> Djelatnost { get; set; }
        public virtual DbSet<DnevnoTrgovanje> DnevnoTrgovanje { get; set; }
        public virtual DbSet<Sektor> Sektor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Zse;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dionicar>(entity =>
            {
                entity.HasKey(e => e.IdDionicar);

                entity.Property(e => e.IdDionicar).HasColumnName("idDionicar");

                entity.Property(e => e.NazivDionicar)
                    .HasColumnName("nazivDionicar")
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DionicarDionickoDrustvo>(entity =>
            {
                entity.HasKey(e => new { e.IdDionicar, e.IdDd, e.Datum });

                entity.Property(e => e.IdDionicar).HasColumnName("idDionicar");

                entity.Property(e => e.IdDd).HasColumnName("idDD");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("date");

                entity.Property(e => e.PostoDionica)
                    .HasColumnName("postoDionica")
                    .HasColumnType("decimal(2, 0)");

                entity.HasOne(d => d.IdDdNavigation)
                    .WithMany(p => p.DionicarDionickoDrustvo)
                    .HasForeignKey(d => d.IdDd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DionicarDi__idDD__4F7CD00D");

                entity.HasOne(d => d.IdDionicarNavigation)
                    .WithMany(p => p.DionicarDionickoDrustvo)
                    .HasForeignKey(d => d.IdDionicar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DionicarD__idDio__4E88ABD4");
            });

            modelBuilder.Entity<DionickoDrustvo>(entity =>
            {
                entity.HasKey(e => e.IdDd);

                entity.Property(e => e.IdDd)
                    .HasColumnName("idDD")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImeDd)
                    .HasColumnName("imeDD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OznakaDd)
                    .HasColumnName("oznakaDD")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DionickoDrustvoDjelatnost>(entity =>
            {
                entity.HasKey(e => e.IdDd);

                entity.Property(e => e.IdDd)
                    .HasColumnName("idDD")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdDjelatnost).HasColumnName("idDjelatnost");

                entity.HasOne(d => d.IdDdNavigation)
                    .WithOne(p => p.DionickoDrustvoDjelatnost)
                    .HasForeignKey<DionickoDrustvoDjelatnost>(d => d.IdDd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DionickoDr__idDD__440B1D61");

                entity.HasOne(d => d.IdDjelatnostNavigation)
                    .WithMany(p => p.DionickoDrustvoDjelatnost)
                    .HasForeignKey(d => d.IdDjelatnost)
                    .HasConstraintName("FK__DionickoD__idDje__4316F928");
            });

            modelBuilder.Entity<DionickoDrustvoKategorijaLikvidnosti>(entity =>
            {
                entity.HasKey(e => new { e.Datum, e.IdDd });

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("date");

                entity.Property(e => e.IdDd).HasColumnName("idDD");

                entity.Property(e => e.Vrijednost).HasColumnName("vrijednost");

                entity.HasOne(d => d.IdDdNavigation)
                    .WithMany(p => p.DionickoDrustvoKategorijaLikvidnosti)
                    .HasForeignKey(d => d.IdDd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DionickoDr__idDD__46E78A0C");
            });

            modelBuilder.Entity<Djelatnost>(entity =>
            {
                entity.HasKey(e => e.IdDjelatnost);

                entity.Property(e => e.IdDjelatnost).HasColumnName("idDjelatnost");

                entity.Property(e => e.IdSektor).HasColumnName("idSektor");

                entity.Property(e => e.NazivDjelatnost)
                    .HasColumnName("nazivDjelatnost")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OznDjelatnost)
                    .HasColumnName("oznDjelatnost")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSektorNavigation)
                    .WithMany(p => p.Djelatnost)
                    .HasForeignKey(d => d.IdSektor)
                    .HasConstraintName("FK__Djelatnos__idSek__403A8C7D");
            });

            modelBuilder.Entity<DnevnoTrgovanje>(entity =>
            {
                entity.HasKey(e => new { e.Datum, e.IdDd });

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("date");

                entity.Property(e => e.IdDd).HasColumnName("idDD");

                entity.Property(e => e.BrojTransakcija).HasColumnName("brojTransakcija");

                entity.Property(e => e.Najniza)
                    .HasColumnName("najniza")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Najvisa)
                    .HasColumnName("najvisa")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Promet).HasColumnName("promet");

                entity.Property(e => e.Promijena)
                    .HasColumnName("promijena")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Prosijek)
                    .HasColumnName("prosijek")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Prva)
                    .HasColumnName("prva")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Zadnja)
                    .HasColumnName("zadnja")
                    .HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.IdDdNavigation)
                    .WithMany(p => p.DnevnoTrgovanje)
                    .HasForeignKey(d => d.IdDd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DnevnoTrgo__idDD__49C3F6B7");
            });

            modelBuilder.Entity<Sektor>(entity =>
            {
                entity.HasKey(e => e.IdSektor);

                entity.Property(e => e.IdSektor).HasColumnName("idSektor");

                entity.Property(e => e.NazivSektor)
                    .HasColumnName("nazivSektor")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OznSektor)
                    .HasColumnName("oznSektor")
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });
        }
    }
}
