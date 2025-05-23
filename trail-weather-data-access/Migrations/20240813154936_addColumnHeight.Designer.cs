﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using trail_weather_data_access;

#nullable disable

namespace trail_weather_data_access.Migrations
{
    [DbContext(typeof(TrailWeatherDbContext))]
    [Migration("20240813154936_addColumnHeight")]
    partial class addColumnHeight
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("trail_weather_data_access.Models.GeoData", b =>
                {
                    b.Property<int>("GeoDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GeoDataId"));

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Lat")
                        .HasColumnType("float");

                    b.Property<double>("Lon")
                        .HasColumnType("float");

                    b.HasKey("GeoDataId");

                    b.ToTable("GeoData");
                });

            modelBuilder.Entity("trail_weather_data_access.Models.SportCenter", b =>
                {
                    b.Property<int>("SportCenterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SportCenterId"));

                    b.Property<int>("GeoDataId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SportCenterTypeId")
                        .HasColumnType("int");

                    b.HasKey("SportCenterId");

                    b.HasIndex("GeoDataId")
                        .IsUnique();

                    b.HasIndex("SportCenterTypeId");

                    b.ToTable("SportCenter");
                });

            modelBuilder.Entity("trail_weather_data_access.Models.SportCenterType", b =>
                {
                    b.Property<int>("SportCenterTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SportCenterTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SportCenterTypeId");

                    b.ToTable("SportCenterType");
                });

            modelBuilder.Entity("trail_weather_data_access.Models.SportCenter", b =>
                {
                    b.HasOne("trail_weather_data_access.Models.GeoData", "GeoData")
                        .WithOne("SportCenter")
                        .HasForeignKey("trail_weather_data_access.Models.SportCenter", "GeoDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trail_weather_data_access.Models.SportCenterType", "SportCenterType")
                        .WithMany("SportCenter")
                        .HasForeignKey("SportCenterTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GeoData");

                    b.Navigation("SportCenterType");
                });

            modelBuilder.Entity("trail_weather_data_access.Models.GeoData", b =>
                {
                    b.Navigation("SportCenter")
                        .IsRequired();
                });

            modelBuilder.Entity("trail_weather_data_access.Models.SportCenterType", b =>
                {
                    b.Navigation("SportCenter");
                });
#pragma warning restore 612, 618
        }
    }
}
