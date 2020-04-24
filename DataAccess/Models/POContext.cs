using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<Team> Teams { get; set; }

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
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Teams");

                entity.Property(e => e.Name).HasMaxLength(128);
                entity.Property(e => e.City).HasMaxLength(128);
                entity.Property(e => e.Mister).HasMaxLength(128);
                entity.Property(e => e.Category).HasMaxLength(128);
                entity.Property(e => e.Logo).HasMaxLength(128);
            });
        }
    }
}
