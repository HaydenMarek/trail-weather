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
            if (!optionsBuilder.IsConfigured)
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // SportCenter configuration
            modelBuilder.Entity<SportCenter>(entity =>
            {
                entity.HasKey(e => e.SportCenterId);
                
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasOne(e => e.GeoData)
                      .WithOne(e => e.SportCenter)
                      .HasForeignKey<SportCenter>(e => e.GeoDataId);

                entity.HasOne(e => e.SportCenterType)
                      .WithMany(e => e.SportCenter)
                      .HasForeignKey(e => e.SportCenterTypeId);

                entity.HasIndex(e => e.Name)
                      .IsUnique();
            });

            // GeoData configuration
            modelBuilder.Entity<GeoData>(entity =>
            {
                entity.HasKey(e => e.GeoDataId);
                
                entity.Property(e => e.Lat)
                      .IsRequired()
                      .HasColumnType("decimal(9,6)");

                entity.Property(e => e.Lon)
                      .IsRequired()
                      .HasColumnType("decimal(9,6)");

                entity.HasOne(e => e.SportCenter)
                      .WithOne(e => e.GeoData)
                      .HasForeignKey<SportCenter>(e => e.GeoDataId);
            });

            // SportCenterType configuration
            modelBuilder.Entity<SportCenterType>(entity =>
            {
                entity.HasKey(e => e.SportCenterTypeId);
                
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(e => e.SportCenter)
                      .WithOne(e => e.SportCenterType)
                      .HasForeignKey(e => e.SportCenterTypeId);
            });
        }
    }
}
