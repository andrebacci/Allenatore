using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Models
{
    public partial class POContext : DbContext
    {
        public POContext()
        {
        }

        public POContext(DbContextOptions<POContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CardTypes> CardTypes { get; set; }
        public virtual DbSet<Feets> Feets { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<SubstitutionSessions> SubstitutionSessions { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Allenatore;User Id=andre;Password=123Stella$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardTypes>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Feets>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Players>(entity =>
            {
                entity.Property(e => e.Firstname).HasMaxLength(25);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.FeetNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Feet)
                    .HasConstraintName("FK_Players_Feets");

                entity.HasOne(d => d.IdTeamNavigation)
                    .WithMany(p => p.PlayersIdTeamNavigation)
                    .HasForeignKey(d => d.IdTeam)
                    .HasConstraintName("FK_Players_Teams");

                entity.HasOne(d => d.LastTeamNavigation)
                    .WithMany(p => p.PlayersLastTeamNavigation)
                    .HasForeignKey(d => d.LastTeam)
                    .HasConstraintName("FK_Players_Teams1");
            });

            modelBuilder.Entity<SubstitutionSessions>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(128);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Logo).HasMaxLength(128);

                entity.Property(e => e.Mister).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
