using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trail_weather_data_access.Migrations
{
    /// <inheritdoc />
    public partial class removeHeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "GeoData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "GeoData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
