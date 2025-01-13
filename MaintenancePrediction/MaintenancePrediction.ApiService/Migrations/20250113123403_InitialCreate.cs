using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaintenancePrediction.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineMaintenanceChecks",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    RuntimeHours = table.Column<double>(type: "float", nullable: false),
                    RuntimeThreshold = table.Column<double>(type: "float", nullable: false),
                    CycleCount = table.Column<int>(type: "int", nullable: false),
                    CycleThreshold = table.Column<int>(type: "int", nullable: false),
                    RequiresMaintenance = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RuntimeThreshold = table.Column<double>(type: "float", nullable: false),
                    CycleTimeThreshold = table.Column<double>(type: "float", nullable: false),
                    CycleCountThreshold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "MachineEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    EventCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineEvents", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_MachineEvents_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineUsages",
                columns: table => new
                {
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    RuntimeHours = table.Column<float>(type: "real", nullable: false),
                    CycleCount = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MachineUsages_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Machines",
                columns: new[] { "MachineId", "CycleCountThreshold", "CycleTimeThreshold", "Location", "Name", "RuntimeThreshold" },
                values: new object[,]
                {
                    { 1, 50000, 2000.0, "Factory Floor 2", "Hydraulic Press A1", 500.0 },
                    { 2, 50000, 1500.0, "Workshop 1", "CNC Lathe B1", 400.0 }
                });

            migrationBuilder.InsertData(
                table: "MachineEvents",
                columns: new[] { "EventId", "Description", "EventCode", "MachineId" },
                values: new object[,]
                {
                    { 1, "Routine maintenance performed", "E100", 1 },
                    { 2, "Overheating detected", "E101", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvents_MachineId",
                table: "MachineEvents",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineUsages_MachineId",
                table: "MachineUsages",
                column: "MachineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineEvents");

            migrationBuilder.DropTable(
                name: "MachineMaintenanceChecks");

            migrationBuilder.DropTable(
                name: "MachineUsages");

            migrationBuilder.DropTable(
                name: "Machines");
        }
    }
}
