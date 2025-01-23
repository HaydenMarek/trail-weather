using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using trail_weather_data_access.Models;

namespace trail_weather_data_access
{
    public class TrailWeatherDbContext : DbContext
    {
        private readonly string _connectionString;
        public TrailWeatherDbContext()
        {
            var config = new ConfigurationBuilder().AddUserSecrets<TrailWeatherDbContext>().Build();

            var secretProvider = config.Providers.First();
            secretProvider.TryGet("ConnectionString", out var secretPass);

            if (secretPass is null)
                throw new ArgumentNullException("Connection string is empty", secretPass);

            _connectionString = secretPass;
        }

        public TrailWeatherDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<SportCenter> SportCenter { get; set; }
        public DbSet<GeoData> GeoData { get; set; }
        public DbSet<SportCenterType> SportCenterType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SportCenter>(entity =>
            {
                entity.HasOne(s => s.GeoData).WithOne(s => s.SportCenter);
                entity.HasOne(s => s.SportCenterType).WithMany(s => s.SportCenter);
            });
                
            modelBuilder.Entity<SportCenterType>()
                .HasMany(s => s.SportCenter)
                .WithOne(s => s.SportCenterType);

            modelBuilder.Entity<GeoData>()
                .HasOne(g => g.SportCenter)
                .WithOne(g => g.GeoData);
        }
    }
}
