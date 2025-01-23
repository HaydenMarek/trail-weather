using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trail_weather_data_access.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoData",
                columns: table => new
                {
                    GeoDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoData", x => x.GeoDataId);
                });

            migrationBuilder.CreateTable(
                name: "SportCenterType",
                columns: table => new
                {
                    SportCenterTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportCenterType", x => x.SportCenterTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SportCenter",
                columns: table => new
                {
                    SportCenterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeoDataId = table.Column<int>(type: "int", nullable: false),
                    SportCenterTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportCenter", x => x.SportCenterId);
                    table.ForeignKey(
                        name: "FK_SportCenter_GeoData_GeoDataId",
                        column: x => x.GeoDataId,
                        principalTable: "GeoData",
                        principalColumn: "GeoDataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SportCenter_SportCenterType_SportCenterTypeId",
                        column: x => x.SportCenterTypeId,
                        principalTable: "SportCenterType",
                        principalColumn: "SportCenterTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SportCenter_GeoDataId",
                table: "SportCenter",
                column: "GeoDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SportCenter_SportCenterTypeId",
                table: "SportCenter",
                column: "SportCenterTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportCenter");

            migrationBuilder.DropTable(
                name: "GeoData");

            migrationBuilder.DropTable(
                name: "SportCenterType");
        }
    }
}
