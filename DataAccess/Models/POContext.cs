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
        public virtual DbSet<SubstitutionSessions> SubstitutionSessions { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-UHPBI6SG\\SQLEXPRESS;Database=Allenatore;User Id=andre;Password=123Stella$;");
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
