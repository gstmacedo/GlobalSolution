using GlobalSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Persistence
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<DbContext> options) : base(options) { }

        // DbSets para as tabelas
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<EnergyConsumption> EnergyConsumptions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações das tabelas
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Device>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UsuarioId);

            modelBuilder.Entity<EnergyConsumption>()
                .HasOne(ec => ec.User)
                .WithMany()
                .HasForeignKey(ec => ec.UsuarioId);

            modelBuilder.Entity<EnergyConsumption>()
                .HasOne(ec => ec.Report)
                .WithMany()
                .HasForeignKey(ec => ec.RelatorioId);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<UserSetting>()
                .HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UsuarioId);
        }
    }
}
