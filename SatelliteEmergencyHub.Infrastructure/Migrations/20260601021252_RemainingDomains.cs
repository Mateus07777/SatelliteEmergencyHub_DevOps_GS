using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SatelliteEmergencyHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemainingDomains : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occurrences_Regions_RegionId",
                table: "Occurrences");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Regions_RegionId",
                table: "Sensors");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Sensors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Sensors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Sensors",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sensors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sensors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Sensors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Occurrences",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Occurrences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "Occurrences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Occurrences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Occurrences",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Occurrences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    OccurrenceId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Occurrences_OccurrenceId",
                        column: x => x.OccurrenceId,
                        principalTable: "Occurrences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Specialization = table.Column<string>(type: "text", nullable: false),
                    ContactPhone = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyTeamOccurrences",
                columns: table => new
                {
                    EmergencyTeamId = table.Column<int>(type: "integer", nullable: false),
                    OccurrenceId = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyTeamOccurrences", x => new { x.EmergencyTeamId, x.OccurrenceId });
                    table.ForeignKey(
                        name: "FK_EmergencyTeamOccurrences_EmergencyTeams_EmergencyTeamId",
                        column: x => x.EmergencyTeamId,
                        principalTable: "EmergencyTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyTeamOccurrences_Occurrences_OccurrenceId",
                        column: x => x.OccurrenceId,
                        principalTable: "Occurrences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_OccurrenceId",
                table: "Alerts",
                column: "OccurrenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyTeamOccurrences_OccurrenceId",
                table: "EmergencyTeamOccurrences",
                column: "OccurrenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occurrences_Regions_RegionId",
                table: "Occurrences",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Regions_RegionId",
                table: "Sensors",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occurrences_Regions_RegionId",
                table: "Occurrences");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_Regions_RegionId",
                table: "Sensors");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "EmergencyTeamOccurrences");

            migrationBuilder.DropTable(
                name: "EmergencyTeams");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Occurrences");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Occurrences");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Occurrences");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Occurrences");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Occurrences");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Sensors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Occurrences",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Occurrences_Regions_RegionId",
                table: "Occurrences",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_Regions_RegionId",
                table: "Sensors",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
