using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTaskOmega.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringForMemoryEfficiency3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modifications<string>");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "EntityModification<string>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityModification<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityModification<string>_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityModification<string>_ServicesId",
                table: "EntityModification<string>",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityModification<string>");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Services",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Modifications<string>",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications<string>_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modifications<string>_ServicesId",
                table: "Modifications<string>",
                column: "ServicesId");
        }
    }
}
