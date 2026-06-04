using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SatelliteEmergencyHub.Domain.Entities;

namespace SatelliteEmergencyHub.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)  : base(options) { }

        public DbSet<Region> Regions => Set<Region>();
        public DbSet<Occurrence> Occurrences => Set<Occurrence>();
        public DbSet<Sensor> Sensors => Set<Sensor>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<EmergencyTeam> EmergencyTeams { get; set; }
        public DbSet<EmergencyTeamOccurrence> EmergencyTeamOccurrences { get; set; }


        public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                var root = Path.Combine(Directory.GetCurrentDirectory(), "..", "..");
                Env.Load(Path.Combine(root, ".env"));

                var connectionString =
                    $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                    $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                    $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";

                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(connectionString)
                    .Options;

                return new AppDbContext(options);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Region>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.Country).HasMaxLength(100).IsRequired();
                e.Property(x => x.State).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Occurrence>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Sensor>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Email).IsUnique();
                e.Property(x => x.Name).HasMaxLength(100).IsRequired();
                e.Property(x => x.Email).HasMaxLength(150).IsRequired();
                e.Property(x => x.Role).HasMaxLength(20).IsRequired();
            });

            modelBuilder.Entity<EmergencyTeamOccurrence>(entity =>
            {
                entity.HasKey(x => new { x.EmergencyTeamId, x.OccurrenceId });

                entity.HasOne(x => x.EmergencyTeam)
                      .WithMany(t => t.EmergencyTeamOccurrences)
                      .HasForeignKey(x => x.EmergencyTeamId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Occurrence)
                      .WithMany(o => o.EmergencyTeamOccurrences)
                      .HasForeignKey(x => x.OccurrenceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
                if(entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
