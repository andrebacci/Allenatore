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
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Feets> Feets { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Gols> Gols { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<Presences> Presences { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Rounds> Rounds { get; set; }
        public virtual DbSet<SubstitutionSessions> SubstitutionSessions { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<Transfers> Transfers { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Allenatore;User Id=andre;Password=6988EAE6;");
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

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Round).HasMaxLength(2);
            });

            modelBuilder.Entity<Feets>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Games>(entity =>
            {
                entity.Property(e => e.ModuleAway).HasMaxLength(16);

                entity.Property(e => e.ModuleHome).HasMaxLength(16);

                entity.HasOne(d => d.IdTeamAwayNavigation)
                    .WithMany(p => p.GamesIdTeamAwayNavigation)
                    .HasForeignKey(d => d.IdTeamAway)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Teams1");

                entity.HasOne(d => d.IdTeamHomeNavigation)
                    .WithMany(p => p.GamesIdTeamHomeNavigation)
                    .HasForeignKey(d => d.IdTeamHome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_Teams");

                entity.HasOne(d => d.RoundNavigation)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.Round)
                    .HasConstraintName("FK_Games_Rounds");
            });

            modelBuilder.Entity<Gols>(entity =>
            {
                entity.HasOne(d => d.IdGameNavigation)
                    .WithMany(p => p.Gols)
                    .HasForeignKey(d => d.IdGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gols_Games");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Gols)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gols_Players");

                entity.HasOne(d => d.IdTeamNavigation)
                    .WithMany(p => p.Gols)
                    .HasForeignKey(d => d.IdTeam)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gols_Teams");
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

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK_Players_Roles");
            });

            modelBuilder.Entity<Presences>(entity =>
            {
                entity.HasOne(d => d.IdGameNavigation)
                    .WithMany(p => p.Presences)
                    .HasForeignKey(d => d.IdGame)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Presences_Games");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Presences)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Presences_Presences");

                entity.HasOne(d => d.IdTeamNavigation)
                    .WithMany(p => p.Presences)
                    .HasForeignKey(d => d.IdTeam)
                    .HasConstraintName("FK_Presences_Teams");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Rounds>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<SubstitutionSessions>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Teams>(entity =>
            {
                //entity.Property(e => e.Category).HasMaxLength(128);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Logo).HasMaxLength(128);

                entity.Property(e => e.Mister).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Transfers>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.IdPlayerNavigation)
                    .WithMany(p => p.Transfers)
                    .HasForeignKey(d => d.IdPlayer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfers_Players");

                entity.HasOne(d => d.IdTeamNewNavigation)
                    .WithMany(p => p.TransfersIdTeamNewNavigation)
                    .HasForeignKey(d => d.IdTeamNew)
                    .HasConstraintName("FK_Transfers_Teams");

                entity.HasOne(d => d.IdTeamOldNavigation)
                    .WithMany(p => p.TransfersIdTeamOldNavigation)
                    .HasForeignKey(d => d.IdTeamOld)
                    .HasConstraintName("FK_Transfers_Teams1");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserRoles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
