using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using trail_weather_data_access.Enums;
using trail_weather_data_access.Models;

namespace trail_weather_data_access
{
    public class TrailWeatherDbContext : DbContext
    {
        private readonly string _connectionString;
        private readonly DbProviders _dbProvider;        

        public TrailWeatherDbContext()
        {

        }
        public TrailWeatherDbContext(string connectionString, DbProviders dbProvider = DbProviders.MySql)
        {
            _connectionString = connectionString;
            _dbProvider = dbProvider;
        }

        public DbSet<SportCenter> SportCenter { get; set; }
        public DbSet<GeoData> GeoData { get; set; }
        public DbSet<SportCenterType> SportCenterType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_dbProvider == DbProviders.SqlServer)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
            else
            {
                optionsBuilder.UseMySQL(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SportCenter>(entity =>
            {
                entity.HasOne(s => s.GeoData).WithOne(s => s.SportCenter);
                entity.HasOne(s => s.SportCenterType).WithMany(s => s.SportCenter);
                entity.HasIndex(s => s.Name).IsUnique(); 
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
